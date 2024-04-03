using Gameplay;
using Invector.vCharacterController;
using UnityEngine;
using UnityEngine.Events;

namespace Invector.Utils
{
    public class vSetParentOfController : MonoBehaviour
    {
        [vHelpBox("Set this GameObject as parent of the Controller")]

        private OUThirdPersonController cc;

        public UnityEvent onStart;

        private void Start()
        {
            cc = GetComponentInParent<OUThirdPersonController>();
            transform.parent = cc.transform;

            onStart.Invoke();
        }
    }
}