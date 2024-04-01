using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class TweeningGame : MonoBehaviour
{
    public Image FadeImage;
    // Start is called before the first frame update
    void Start()
    {
        DoFade(true);
    }

    public void DoFade(bool isFadein)
    {
        if (isFadein)
            FadeImage.DOFade(0, 1).SetEase(Ease.Linear);
        else
            FadeImage.DOFade(1, 1).SetEase(Ease.Linear);
    }
}
