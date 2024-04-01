using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DoTweening : MonoBehaviour
{
    public static DoTweening Instance { set; get; }
    [Header("Buttons")]
    public Transform PlayButton;
    public Transform ShopButton;
    public Transform QuitButton;
    public Transform EnergyPanal;

    public Transform Player;
    public Vector3 PlayerSettingPos;
    public Vector3 PlayerNormalPost;


    public RectTransform Settings;




    [Header("Textures")]
    public Image FadeImage;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        PlayButton.DOLocalMoveX(680f, 0.6f).SetEase(Ease.Linear);
        ShopButton.DOLocalMoveX(660f, 0.8f).SetEase(Ease.Linear);
        QuitButton.DOLocalMoveX(660f, 1f).SetEase(Ease.Linear);
        EnergyPanal.DOLocalMoveX(660f, 1f).SetEase(Ease.Linear);


        DoFade(true);

      
    }

    public void DoFade(bool isFadein)
    {
        if (isFadein)
            FadeImage.DOFade(0, 1).SetEase(Ease.Linear);
        else
            FadeImage.DOFade(1, 1).SetEase(Ease.Linear);
    }


    public void OpenSetting()
    {
        Settings.DOAnchorPosX(0, 0.3f).SetEase(Ease.Linear);
        Player.DOMove(PlayerSettingPos,0.3f).SetEase(Ease.Linear);

    }
    public void CloseSettings()
    {
        Settings.DOAnchorPosX(-890f, 0.3f).SetEase(Ease.Linear);
        Player.DOMove(PlayerNormalPost, 0.3f).SetEase(Ease.Linear);

    }
}
