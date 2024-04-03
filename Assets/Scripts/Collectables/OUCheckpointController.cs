using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class OUCheckpointController : MonoBehaviour
{
    [SerializeField] private Transform _checkpointTransform;
    [SerializeField] private Transform _transformToAnimate;
    
    private void Start() => RotateObject();
    
    void RotateObject()
    {
        _transformToAnimate.DORotate(new Vector3(0, 90, 0), 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

    public Vector3 GetCheckpointPos() => _checkpointTransform.position;
}
