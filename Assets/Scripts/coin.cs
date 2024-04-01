using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class coin : MonoBehaviour
{

    public Vector3 Value;
    void Start()
    {
        RotateCoin(Value);
    }


    void RotateCoin(Vector3 Value)
    {
        transform.DORotate(Value, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

}
