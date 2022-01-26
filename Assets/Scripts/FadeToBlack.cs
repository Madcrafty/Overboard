using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private RawImage fader;

    public void OnEnable()
    {
        Color fixedColor = fader.color;
        fixedColor.a = 1;
        fader.color = fixedColor;
        fader.CrossFadeAlpha(0f, 0f, true);
    }

    public void FadeToBlackAndABack()
    {
        gameObject.SetActive(true);
        fader.CrossFadeAlpha(1, 2, false);
        StartCoroutine("CrossBack");
    }

    IEnumerator CrossBack()
    {
        yield return new WaitForSecondsRealtime(2);
        fader.CrossFadeAlpha(0, 2, true);
    }
}
