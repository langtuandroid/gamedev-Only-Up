using Invector.vCharacterController;
using UnityEngine;
using UnityEngine.Events;

namespace Invector.Utils
{
    /// <summary>
    /// Simple Checkpoint Example, works by updating the vGameController SpawnPoint to this transform position/rotation.
    /// </summary>    
    [RequireComponent(typeof(BoxCollider))]
    public class vCheckpointExample : MonoBehaviour
    {
        vGameController gm;

        public UnityEvent onTriggerEnter;

        void Start()
        {
            gm = GetComponentInParent<vGameController>();
            GetComponent<BoxCollider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                vHUDController.instance.ShowText("Checkpoint reached!");
                gm.spawnPoint = gameObject.transform;
                onTriggerEnter.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}


