using UnityEngine;

public class OUTargetFramerateController : MonoBehaviour
{
    void Start() => DontDestroyOnLoad(this);

    void Update()
    {
        if(Application.targetFrameRate < 60) Application.targetFrameRate = 120;
    }
}
