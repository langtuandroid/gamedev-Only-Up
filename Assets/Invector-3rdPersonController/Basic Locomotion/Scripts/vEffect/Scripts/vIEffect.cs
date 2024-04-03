using UnityEngine;

namespace Invector
{
    public interface vIEffect
    {
        string EffectName { get; }
        float EffectDuration { get; }
        Vector3 EffectPosition { get; }
        Transform Sender { get; }
    }
}