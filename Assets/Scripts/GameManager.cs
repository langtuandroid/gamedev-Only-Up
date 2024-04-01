using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }

    public static bool isPlayerDie;
    public static bool isCheckPoint;

    private void Awake()
    {
        Instance = this;
        isPlayerDie = false;
        isCheckPoint = false;
    }
}
