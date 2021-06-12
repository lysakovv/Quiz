using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TaskText : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 4f;
    private Text text;

    
    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        Appear();
    }
    

    public void Appear()
    {
        text.DOFade(1, fadeDuration); 
    }

    public void Disappear()
    {
        text.DOFade(0f, 0f);
    }

    
}
