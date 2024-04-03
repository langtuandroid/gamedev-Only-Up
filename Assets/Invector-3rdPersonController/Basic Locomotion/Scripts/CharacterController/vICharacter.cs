using System;
using UnityEngine;
using UnityEngine.Events;

namespace Invector.vCharacterController
{
    [Serializable]
    public class OnActiveRagdoll : UnityEvent<vDamage> { }
    public interface vICharacter : vIHealthController
    {
        OnActiveRagdoll onActiveRagdoll { get; }
        Animator animator { get; }
        bool isCrouching { get; }
        bool ragdolled { get; set; }
        void EnableRagdoll();
        void ResetRagdoll();
    }
}