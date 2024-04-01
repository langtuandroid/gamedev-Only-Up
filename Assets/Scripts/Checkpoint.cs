using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Checkpoint : MonoBehaviour
{
    public Transform CheckPointObjcet;
    public Transform FBXCheckpointObject;
    private void Start()
    {
        RotateObject();
    }
    void RotateObject()
    {
        FBXCheckpointObject.DORotate(new Vector3(0, 90, 0), 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
}
