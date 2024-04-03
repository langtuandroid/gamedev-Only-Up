using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Invector
{    
    [vClassHeader("Destroy GameObject", openClose = false)]
    public class vDestroyGameObject : vMonoBehaviour
    {
        public float delay;
        public UnityEvent onDestroy;
        IEnumerator Start()
        {
            yield return new WaitForSeconds(delay);
            onDestroy.Invoke();
            Destroy(gameObject);
        }
    }
}