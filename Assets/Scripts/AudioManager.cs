using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public static AudioManager Instance { set; get; }

    public AudioSource PlayButton;
    public AudioSource OtherButton;
    public AudioSource BackgroundMusic;
    public AudioSource EnergySound;


    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
