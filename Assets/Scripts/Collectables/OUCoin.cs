using DG.Tweening;
using UnityEngine;

public class OUCoin : MonoBehaviour
{
    public Vector3 Value;
    
    void Start() => RotateCoinAnimation();

    void RotateCoinAnimation()
    {
        transform.DORotate(Value, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

}
