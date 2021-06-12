using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class CellsPanel : MonoBehaviour
{
    [SerializeField] private Text taskText;
    [SerializeField] private string findText = "Find";

    public delegate void AnswerSent(bool value, string _answer = "");
    public static event AnswerSent CorrectAnswerGiven; 
    
    private string correctAnswer;

    private void Start()
    {
        LevelManager.ItemsSent += UpdatePanels;
    }

    private void UpdatePanels(Item[] items)
    {
        correctAnswer = items[0].Name;
        taskText.text = findText + " " + correctAnswer;
        Random rand = new Random();

        for (int i = items.Length - 1; i >= 1; i--) // расстановка ячеек в рандомном порядке
        {
            int j = rand.Next(i + 1);

            var tmp = items[j];
            items[j] = items[i];
            items[i] = tmp;
        }

        int cellsToHideAmount = transform.childCount - items.Length;

        for (int i = 0; i < cellsToHideAmount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < items.Length; i++)
        {
            GameObject cellsToOpen = transform.GetChild(transform.childCount - i - 1).gameObject;
            cellsToOpen.SetActive(true);
            cellsToOpen.GetComponentInChildren<Button>().image.sprite = items[i].Icon;
            cellsToOpen.GetComponentInChildren<AddButtonListener>().Name = items[i].Name;
        }
    }

    public void CheckAnswer(string answer)
    {
        if (answer == correctAnswer)
        {
            CorrectAnswerGiven?.Invoke(true, answer);
        }
        else
        {
            CorrectAnswerGiven?.Invoke(false, answer);
        }
    }

    private void OnEnable()
    {
        Appear();
    }

    public void Appear()
    {
        DOTween.Sequence().Append(transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.7f))
            .Append(transform.DOScale(new Vector3(0.8f, 0.8f, 0f), 0.7f))
            .Append(transform.DOScale(new Vector3(1f, 1f, 0f), 0.7f));
    }
}