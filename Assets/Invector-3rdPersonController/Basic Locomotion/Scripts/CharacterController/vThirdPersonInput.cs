using System;
using System.Collections;
using Gameplay;
using Invector.vCamera;
using UnityEngine;
using UnityEngine.Events;

namespace Invector.vCharacterController
{
    [vClassHeader("Input Manager", iconName = "inputIcon")]
    public class vThirdPersonInput : vMonoBehaviour, vIAnimatorMoveReceiver
    {
        public delegate void OnUpdateEvent();
        public event OnUpdateEvent onUpdate;
        public event OnUpdateEvent onLateUpdate;
        public event OnUpdateEvent onAnimatorMove;

        [vEditorToolbar("Inputs")]
        [vHelpBox("Check these options if you need to use the mouse cursor, ex: <b>2.5D, Topdown or Mobile</b>", vHelpBoxAttribute.MessageType.Info)]
        public bool unlockCursorOnStart;
        public bool showCursorOnStart;
        [vHelpBox("PC only - use it to toggle between run/walk", vHelpBoxAttribute.MessageType.Info)]
        public KeyCode toggleWalk = KeyCode.CapsLock;

        [Header("Movement Input")]
        public GenericInput horizontalInput = new GenericInput("Horizontal", "LeftAnalogHorizontal", "Horizontal");
        public GenericInput verticalInput = new GenericInput("Vertical", "LeftAnalogVertical", "Vertical");
        public GenericInput sprintInput = new GenericInput("LeftShift", "LeftStickClick", "LeftStickClick");
        public GenericInput jumpInput = new GenericInput("Space", "X", "X");

        protected bool _lockInput;
        [HideInInspector] public virtual bool lockInput { get { return _lockInput; } set { _lockInput = value; } }
        [vEditorToolbar("Inputs")]
        [Header("Camera Input")]
        public GenericInput rotateCameraXInput = new GenericInput("Mouse X", "RightAnalogHorizontal", "Mouse X");
        public GenericInput rotateCameraYInput = new GenericInput("Mouse Y", "RightAnalogVertical", "Mouse Y");

        public UnityEvent onEnableAnimatorMove = new();

        public vThirdPersonCamera tpCamera;
        public bool ignoreTpCamera;
        public string customCameraState;
        public string customlookAtPoint;
        public bool changeCameraState;
        public bool smoothCameraState;
        public OUThirdPersonController cc;
        public vHUDController hud;
        private bool updateIK;
        public bool lockMoveInput;

        private Camera _cameraMain;
        private bool withoutMainCamera;
        private bool lockUpdateMoveDirection { get; set; }                // lock the method UpdateMoveDirection

        private Camera cameraMain
        {
            get
            {
                if (!_cameraMain && !withoutMainCamera)
                {
                    if (!Camera.main)
                    {
                        Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                        withoutMainCamera = true;
                    }
                    else
                    {
                        _cameraMain = Camera.main;
                        cc.rotateTarget = _cameraMain.transform;
                    }
                }
                return _cameraMain;
            }
        }

        public Animator animator
        {
            get
            {
                if (cc == null)
                {
                    cc = GetComponent<OUThirdPersonController>();
                }

                if (cc.animator == null)
                {
                    return GetComponent<Animator>();
                }

                return cc.animator;
            }
        }

        private void Start()
        {
            cc = GetComponent<OUThirdPersonController>();

            if (cc != null)
            {
                cc.Init();
            }

            cc.onDead.AddListener(_gameObject => { cc.ResetInputAnimatorParameters(); SetLockAllInput(true); cc.StopCharacter(); });
            StartCoroutine(CharacterInit());

            ShowCursor(showCursorOnStart);
            LockCursor(unlockCursorOnStart);
            EnableOnAnimatorMove();
        }

        public virtual IEnumerator CharacterInit()
        {
            FindCamera();
            yield return new WaitForEndOfFrame();
            FindHUD();
        }

        private void FindHUD()
        {
            if (hud == null && vHUDController.instance != null)
            {
                hud = vHUDController.instance;
                hud.Init(cc);
            }
        }

        private void FindCamera()
        {
            var tpCameras = FindObjectsOfType<vThirdPersonCamera>();

            if (tpCameras.Length > 1)
            {
                tpCamera = Array.Find(tpCameras, tp => !tp.isInit);

                if (tpCamera == null)
                {
                    tpCamera = tpCameras[0];
                }

                if (tpCamera != null)
                {
                    for (int i = 0; i < tpCameras.Length; i++)
                    {
                        if (tpCamera != tpCameras[i])
                        {
                            Destroy(tpCameras[i].gameObject);
                        }
                    }
                }
            }
            else if (tpCameras.Length == 1)
            {
                tpCamera = tpCameras[0];
            }

            if (tpCamera && tpCamera.mainTarget != transform)
            {
                tpCamera.SetMainTarget(transform);
            }
        }

        private void LateUpdate()
        {
            if (cc == null)
            {
                return;
            }

            if (!updateIK)
            {
                return;
            }

            if (onLateUpdate != null)
            {
                onLateUpdate.Invoke();
            }

            CameraInput();                      // update camera input
            UpdateCameraStates();               // update camera states                        
            updateIK = false;
        }

        protected virtual void FixedUpdate()
        {
            Physics.SyncTransforms();
            cc.UpdateMotor();                                                   // handle the ThirdPersonMotor methods            
            cc.ControlLocomotionType();                                         // handle the controller locomotion type and movespeed   
            ControlRotation();
            cc.UpdateAnimator();                                                // handle the ThirdPersonAnimator methods
            updateIK = true;
        }

        protected virtual void Update()
        {
            if (cc == null || Time.timeScale == 0)
            {
                return;
            }

            if (onUpdate != null)
            {
                onUpdate.Invoke();
            }

            InputHandle();                      // update input methods                        
            UpdateHUD();                        // update hud graphics            
        }

        public virtual void OnAnimatorMoveEvent()
        {
            if (cc == null)
            {
                return;
            }

            cc.ControlAnimatorRootMotion();
            if (onAnimatorMove != null)
            {
                onAnimatorMove.Invoke();
            }
        }
        
        public virtual void SetLockBasicInput(bool value)
        {
            lockInput = value;
        }

        public virtual void SetLockAllInput(bool value)
        {
            SetLockBasicInput(value);
        }

        public virtual void ShowCursor(bool value)
        {
            Cursor.visible = value;
        }

        public virtual void LockCursor(bool value)
        {
            if (!value)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        protected virtual vAnimatorMoveSender animatorMoveSender { get; set; }

        private bool _useAnimatorMove { get; set; }

        private bool UseAnimatorMove
        {
            set
            {

                if (_useAnimatorMove != value)
                {
                    if (value)
                    {
                        animatorMoveSender = gameObject.AddComponent<vAnimatorMoveSender>();
                        onEnableAnimatorMove?.Invoke();
                    }
                    else
                    {
                        if (animatorMoveSender)
                        {
                            Destroy(animatorMoveSender);
                        }

                        onEnableAnimatorMove?.Invoke();
                    }
                }
                _useAnimatorMove = value;
            }
        }

        private void EnableOnAnimatorMove() => UseAnimatorMove = true;

        #region Basic Locomotion Inputs

        protected virtual void InputHandle()
        {
            if (lockInput || cc.ragdolled) return;

            MoveInput();
            SprintInput();
            JumpInput();
        }

        private void MoveInput()
        {
            if (!lockMoveInput)
            {
                var input = cc.input;
                input.x = horizontalInput.GetAxisRaw();
                input.z = verticalInput.GetAxisRaw();
                cc.input = input;
            }

            if (Input.GetKeyDown(toggleWalk))
            {
                cc.alwaysWalkByDefault = !cc.alwaysWalkByDefault;
            }

            cc.ControlKeepDirection();
        }

        private bool rotateToLockTargetConditions => tpCamera && tpCamera.lockTarget && cc.isStrafing && !cc.isRolling && !cc.isJumping && !cc.customAction;

        private void ControlRotation()
        {
            if (cameraMain && !lockUpdateMoveDirection)
            {
                if (!cc.keepDirection)
                {
                    cc.UpdateMoveDirection(cameraMain.transform);
                }
            }

            if (rotateToLockTargetConditions)
            {
                cc.RotateToPosition(tpCamera.lockTarget.position);
            }
            else
            {
                cc.ControlRotationType();
            }
        }

        public virtual void SprintInput()
        {
            if (sprintInput.useInput)
            {
                cc.Sprint(cc.useContinuousSprint ? sprintInput.GetButtonDown() : sprintInput.GetButton());
            }
        }

        public virtual bool JumpConditions()
        {
            return !cc.inJumpStarted && !cc.customAction && !cc.isCrouching && cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && cc.currentStamina >= cc.jumpStamina && !cc.isJumping && !cc.isRolling;
        }
        
        public virtual void JumpInput()
        {
            if (jumpInput.GetButtonDown() && JumpConditions())
            {
                cc.Jump(true);
            }
        }

        #endregion       

        #region Camera Methods

        public virtual void CameraInput()
        {
            if (!cameraMain || tpCamera == null) return;

            tpCamera.RotateCamera(rotateCameraXInput.GetAxis(), rotateCameraYInput.GetAxis());
        }

        public virtual void UpdateCameraStates()
        {
            if (ignoreTpCamera)
            {
                return;
            }

            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<vThirdPersonCamera>();
                if (tpCamera == null)
                {
                    return;
                }

                if (tpCamera)
                {
                    tpCamera.SetMainTarget(transform);
                    tpCamera.Init();
                }
            }

            if (changeCameraState)
            {
                tpCamera.ChangeState(customCameraState, customlookAtPoint, smoothCameraState);
            }
            else if (cc.isCrouching)
            {
                tpCamera.ChangeState("Crouch", true);
            }
            else if (cc.isStrafing)
            {
                tpCamera.ChangeState("Strafing", true);
            }
            else
            {
                tpCamera.ChangeState("Default", true);
            }
        }

        public virtual void ChangeCameraState(string cameraState, bool useLerp = true)
        {
            if (useLerp)
            {
                ChangeCameraStateWithLerp(cameraState);
            }
            else
            {
                ChangeCameraStateNoLerp(cameraState);
            }
        }

        public virtual void ChangeCameraStateWithLerp(string cameraState)
        {
            changeCameraState = true;
            customCameraState = cameraState;
            smoothCameraState = true;
        }

        public virtual void ChangeCameraStateNoLerp(string cameraState)
        {
            changeCameraState = true;
            customCameraState = cameraState;
            smoothCameraState = false;
        }

        public virtual void ResetCameraState()
        {
            changeCameraState = false;
            customCameraState = string.Empty;
        }

        #endregion

        public virtual void UpdateHUD()
        {
            if (hud == null)
            {
                if (vHUDController.instance != null)
                {
                    hud = vHUDController.instance;
                    hud.Init(cc);
                }
                else
                {
                    return;
                }
            }

            hud.UpdateHUD(cc);
        }

    }

    public interface vIAnimatorMoveReceiver
    {
        bool enabled { get; set; }
        void OnAnimatorMoveEvent();
    }

    public class vAnimatorMoveSender : MonoBehaviour
    {
        protected virtual void Awake()
        {
            ///Hide in Inpector
            hideFlags = HideFlags.HideInInspector;
            vIAnimatorMoveReceiver[] animatorMoves = GetComponents<vIAnimatorMoveReceiver>();
            for (int i = 0; i < animatorMoves.Length; i++)
            {
                var receiver = animatorMoves[i];
                animatorMoveEvent += () =>
                {
                    if (receiver.enabled)
                    {
                        receiver.OnAnimatorMoveEvent();
                    }
                };
            }
        }

        /// <summary>
        /// AnimatorMove event called using  default unity OnAnimatorMove
        /// </summary>
        public Action animatorMoveEvent;

        protected virtual void OnAnimatorMove()
        {
            animatorMoveEvent?.Invoke();
        }
    }
}