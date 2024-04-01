using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { set; get; }

    public Color ActiveColor;
    public Color NonActiveColor;
    public Image LowBtn;
    public Image HighBtn;

    int GraphicsIndex;

    public Text EnergyText;
    public int EnergyInt;

    public GameObject SettingPanal;

    public GameObject PLayerModel;

    public RenderPipelineAsset[] QualityLevel;

    private void Awake()
    {
        Instance = this;
        //SettingPanal.SetActive(false);

        GraphicsIndex = PlayerPrefs.GetInt("DefaultGraphics", 1);

        if(GraphicsIndex == 1)
        {
            HighBtn.color = ActiveColor;
            LowBtn.color = NonActiveColor;
            QualitySettings.SetQualityLevel(1);
        }
        else
        {
            LowBtn.color = ActiveColor;
            HighBtn.color = NonActiveColor;
            QualitySettings.SetQualityLevel(0);
        }
    }
    private void Start()
    {
        EnergyInt = PlayerPrefs.GetInt("Energy", 0);
        EnergyText.text = EnergyInt.ToString();
    }
    public void PlayButton()
    {
        StartCoroutine(OnPlayGame());
    }

    private IEnumerator OnPlayGame()
    {
        AudioManager.Instance.PlayButton.Play();
        DoTweening.Instance.DoFade(false);
        PLayerModel.SetActive(false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    public void SettingBtn()
    {
        AudioManager.Instance.OtherButton.Play();
        DoTweening.Instance.OpenSetting();
        //PLayerModel.SetActive(false);
        //SettingPanal.SetActive(true);
    }

    public void LowGraphics()
    {

        AudioManager.Instance.OtherButton.Play();
        LowBtn.color = ActiveColor;
        HighBtn.color = NonActiveColor;
        QualitySettings.SetQualityLevel(0);
        PlayerPrefs.SetInt("DefaultGraphics", 0);
        PLayerModel.SetActive(true);
    }

    public void HighGraphics()
    {
        AudioManager.Instance.OtherButton.Play();
        HighBtn.color = ActiveColor;
        LowBtn.color = NonActiveColor;
        QualitySettings.SetQualityLevel(1);
        PlayerPrefs.SetInt("DefaultGraphics", 1);
        PLayerModel.SetActive(true);
        
    }

    public void CloseSetting()
    {
        AudioManager.Instance.OtherButton.Play();

        DoTweening.Instance.CloseSettings();
        //PLayerModel.SetActive(true);
        //SettingPanal.SetActive(false);
    }

}
