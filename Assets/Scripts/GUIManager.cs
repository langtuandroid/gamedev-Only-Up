using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance { set; get;}

    public GameObject RespawnPanal;
    public GameObject PausePanal;

    public TextMeshProUGUI EnergyText;

    public int EnergyInt;


    public Button RespawnBtn;
    
    private void Awake()
    {
        Instance = this;
        EnergyInt = PlayerPrefs.GetInt("Energy", 0);
        UpdateEnergy();
        RespawnPanal.SetActive(false);
        PausePanal.SetActive(false);
        AudioManager.Instance.BackgroundMusic.Play();
    }

    private void Update()
    {
        if(GameManager.isCheckPoint)
        {
            RespawnBtn.interactable = true;
        }
        else
        {
            RespawnBtn.interactable = false;
        }
    }

    public void CloseBtn()
    {
        AudioManager.Instance.OtherButton.Play();
        Time.timeScale = 1;
        GameManager.isPlayerDie = false;
        RespawnPanal.SetActive(false);
        PausePanal.SetActive(false);
    }

    public void Gameover()
    {
        StartCoroutine(OnGameover());
    }
    private IEnumerator OnGameover()
    {
        yield return new WaitForSeconds(2);
        RespawnPanal.SetActive(true);
    }

    public void UpdateEnergy()
    {
        EnergyText.text = EnergyInt.ToString();
        PlayerPrefs.SetInt("Energy", EnergyInt);
    }

    public void PauseBtn()
    {
        AudioManager.Instance.BackgroundMusic.Pause();
        AudioManager.Instance.OtherButton.Play();
        PausePanal.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeBtn()
    {
        AudioManager.Instance.BackgroundMusic.Play();
        AudioManager.Instance.OtherButton.Play();
        PausePanal.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartBtn()
    {
        AudioManager.Instance.BackgroundMusic.Stop();

        AudioManager.Instance.OtherButton.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HomeBtn()
    {
        AudioManager.Instance.OtherButton.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }


}
