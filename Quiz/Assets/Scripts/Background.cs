using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Background : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        image.enabled = true;
        image.DOFade(0.7f, 1f);
    }

    public void LoadScreen()
    {
        image.DOFade(1f, 0f);
        Invoke(nameof(Deactivate), 3f);
    }

    private void Deactivate()
    {
        image.enabled = false;
        image.DOFade(0f, 0f);
    }
}
