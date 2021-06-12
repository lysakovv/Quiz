using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AddButtonListener : MonoBehaviour
{
    [SerializeField] private UIParticleSystem particles;
    private CellsPanel cellsPanel;
    private string name;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        cellsPanel = FindObjectOfType<CellsPanel>();
        button.onClick.AddListener(delegate { cellsPanel.CheckAnswer(name); });
        CellsPanel.CorrectAnswerGiven += CorrectAnswerAnimation;
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    private void CorrectAnswerAnimation(bool value, string answer)
    {
        if (answer == Name)
        {
            if (value) 
            {
                particles.Play();
                DOTween.Sequence().Append(transform.DOScale(new Vector3(1.2f, 1.2f, 0f), 0.3f))
                    .Append(transform.DOScale(new Vector3(0.8f, 0.8f, 0f), 0.3f))
                    .Append(transform.DOScale(new Vector3(1f, 1f, 0f), 0.3f));
            }
            else
            {
                transform.DOShakePosition(2.0f, strength: new Vector3(0, 6, 0),
                    vibrato: 5, randomness: 1, snapping: false, fadeOut: true);
            }
        }
    }
}