using System.Collections;
using Invector;
using Invector.vCharacterController;
using UnityEngine;

namespace Gameplay
{
    [vClassHeader("THIRD PERSON CONTROLLER", iconName = "controllerIcon")]
    public class OUThirdPersonController : vThirdPersonAnimator
    {
        public virtual void ControlAnimatorRootMotion()
        {
            if (!enabled) return;

            if (isRolling)
            {
                RollBehavior();
                return;
            }

            if (customAction || lockAnimMovement)
            {
                StopCharacterWithLerp();              
                transform.position = animator.rootPosition;
                transform.rotation = animator.rootRotation;
            }

            if (useRootMotion)
            {
                MoveCharacter(moveDirection);
            }
        }
        
        public virtual void ControlLocomotionType()
        {
            if (lockAnimMovement || lockMovement || customAction)
            {
                return;
            }

            if (!lockSetMoveSpeed)
            {
                if (locomotionType.Equals(LocomotionType.FreeWithStrafe) && !isStrafing || locomotionType.Equals(LocomotionType.OnlyFree))
                {
                    SetControllerMoveSpeed(freeSpeed);
                    SetAnimatorMoveSpeed(freeSpeed);
                }
                else if (locomotionType.Equals(LocomotionType.OnlyStrafe) || locomotionType.Equals(LocomotionType.FreeWithStrafe) && isStrafing)
                {
                    isStrafing = true;
                    SetControllerMoveSpeed(strafeSpeed);
                    SetAnimatorMoveSpeed(strafeSpeed);
                }
            }

            if (!useRootMotion)
            {
                MoveCharacter(moveDirection);
            }
        }
        
        public virtual void ControlRotationType()
        {
            if (lockAnimRotation || lockRotation || customAction || isRolling)
            {
                return;
            }

            bool validInput = input != Vector3.zero || (isStrafing ? strafeSpeed.rotateWithCamera : freeSpeed.rotateWithCamera);

            if (validInput)
            {
                if (lockAnimMovement)
                {
                    inputSmooth = Vector3.Lerp(inputSmooth, input, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);
                }
                Vector3 dir = (isStrafing && isGrounded && (!isSprinting || sprintOnlyFree == false) || (freeSpeed.rotateWithCamera && input == Vector3.zero)) && rotateTarget ? rotateTarget.forward : moveDirection;

                RotateToDirection(dir);
            }
        }
        
        public virtual void ControlKeepDirection()
        {
            if (!keepDirection)
            {
                oldInput = input;
            }
            else if ((input.magnitude < 0.01f || Vector3.Distance(oldInput, input) > 0.9f) && keepDirection)
            {
                keepDirection = false;
            }
        }
        
        public virtual void UpdateMoveDirection(Transform referenceTransform = null)
        {
            if (isRolling && !rollControl)
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);
                return;
            }

            if (referenceTransform && !rotateByWorld)
            {
                var right = referenceTransform.right;
                right.y = 0;
                var forward = Quaternion.AngleAxis(-90, Vector3.up) * right;
                moveDirection = (inputSmooth.x * right) + (inputSmooth.z * forward);
                var moveDirectionRaw= (input.x * right) + (input.z * forward);
                SetInputDirection(moveDirectionRaw);
            }
            else
            {
                moveDirection = new Vector3(inputSmooth.x, 0, inputSmooth.z);
                var moveDirectionRaw = new Vector3(input.x, 0, input.z);
                SetInputDirection(moveDirectionRaw);
            }
        }
        
        public virtual void Sprint(bool value)
        {
            var sprintConditions = (!isCrouching || (!inCrouchArea && CanExitCrouch())) && (currentStamina > 0 && hasMovementInput &&
                !(isStrafing && (horizontalSpeed >= 0.5 || horizontalSpeed <= -0.5 || verticalSpeed <= 0.1f) && !sprintOnlyFree));

            if (value && sprintConditions)
            {
                if (currentStamina > (finishStaminaOnSprint ? sprintStamina : 0) && hasMovementInput)
                {
                    finishStaminaOnSprint = false;
                    if (isGrounded && useContinuousSprint)
                    {
                        isCrouching = false;
                        isSprinting = !isSprinting;
                        if (isSprinting)
                        {
                            OnStartSprinting.Invoke();
                            alwaysWalkByDefault = false;
                        }
                        else
                        {
                            OnFinishSprinting.Invoke();
                        }
                    }
                    else if (!isSprinting)
                    {
                        OnStartSprinting.Invoke();

                        alwaysWalkByDefault = false;
                        isSprinting = true;
                    }
                }
                else if (!useContinuousSprint && isSprinting)
                {
                    if (currentStamina <= 0)
                    {
                        finishStaminaOnSprint = true;
                        OnFinishSprintingByStamina.Invoke();
                    }
                    isSprinting = false;
                    OnFinishSprinting.Invoke();
                }
            }
            else if (isSprinting && (!useContinuousSprint || !sprintConditions))
            {
                if (currentStamina <= 0)
                {
                    finishStaminaOnSprint = true;
                    OnFinishSprintingByStamina.Invoke();
                }

                isSprinting = false;
                OnFinishSprinting.Invoke();
            }
        }

        public virtual void Jump(bool consumeStamina = false)
        { 
            jumpCounter = jumpTimer;
            OnJump.Invoke();

            if (input.sqrMagnitude < 0.1f)
            {
                StartCoroutine(DelayToJump());
                animator.CrossFadeInFixedTime("Jump", 0.1f);
            }
            else
            {
                isJumping = true;
                animator.CrossFadeInFixedTime("JumpMove", .2f);
            }

            if (consumeStamina)
            {
                ReduceStamina(jumpStamina, false);
                currentStaminaRecoveryDelay = 1f;
            }
        }

        private IEnumerator DelayToJump()
        {
            inJumpStarted = true;
            yield return new WaitForSeconds(jumpStandingDelay);
            isJumping = true;
            inJumpStarted = false;
        }
        
        protected override void OnTriggerStay(Collider other)
        {
            try
            {
                CheckForAutoCrouch(other);
            }
            catch (UnityException e)
            {
                Debug.LogWarning(e.Message);
            }
            base.OnTriggerStay(other);
        }

        protected override void OnTriggerExit(Collider other)
        {
            AutoCrouchExit(other);
            base.OnTriggerExit(other);
        }
    }
}