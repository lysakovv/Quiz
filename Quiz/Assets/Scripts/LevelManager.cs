using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Storage storage;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private CellsPanel cells;
    [SerializeField] private TaskText taskText;
    [SerializeField] private Background backGroundImage;

    public delegate void ChangePanelsState(Item[] array);

    public static event ChangePanelsState ItemsSent;

    private int BoxAmount
    {
        get => boxAmount;
        set
        {
            boxAmount = value;

            if (boxAmount > 9)
            {
                backGroundImage.enabled = true; 
                restartButton.SetActive(true);
                return;
            }
            

            ChooseLetter();
        }
    }

    public List<Storage.ItemsInventory> currentItemsList = new List<Storage.ItemsInventory>();
    public List<List<Item>> usedElements = new List<List<Item>>();
    private int boxAmount;
    private int maximumElements = 0;

    private void Start()
    {
        if (storage == null)
        {
            storage = FindObjectOfType<Storage>();
        }

        CellsPanel.CorrectAnswerGiven += ChangeLevelStage;
        
        FillItemsList();
        SetStaringCells();
    }


    private void ChooseLetter()
    {
        int listNumber = Random.Range(0, currentItemsList.Count);
        
        Item[] lettersToFindArray = new Item[boxAmount];
        int itemsAmount = currentItemsList[listNumber].Items.Count;
        
        for (int i = 0; i < boxAmount; i++)
        {
            int itemNumber;
            Item item;

            do
            {
                itemNumber = Random.Range(0, itemsAmount);
                item = currentItemsList[listNumber].Items[itemNumber];
            } while (usedElements[listNumber].Contains(item) && (i == 0) ||
                     lettersToFindArray.Contains(item));

            lettersToFindArray[i] = item;

            if (i == 0)
            {
                usedElements[listNumber].Add(item);
                if (usedElements[listNumber].Count == currentItemsList[listNumber].Items.Count)
                {
                    usedElements[listNumber].Clear();
                }
            }
        }

        ItemsSent?.Invoke(lettersToFindArray);
    }

    private void FillItemsList(int index = -1)
    {
        foreach (var item in storage.TypesList)
        {
            currentItemsList.Add(item);
        }

        for (int i = 0; i < currentItemsList.Count; i++)
        {
            usedElements.Add(new List<Item>());
        }
    }


    public void ChangeLevelStage(bool value, string answer)
    {
        if (value)
        {
            Invoke(nameof(ChangeBoxAmount), 1f);
        }
    }

    private void ChangeBoxAmount()
    {
        BoxAmount += 3;
    }

    public void StartGame()
    {
        restartButton.SetActive(false);
        backGroundImage.GetComponent<Background>().LoadScreen();
        taskText.Disappear();
        Invoke(nameof(SetStaringCells), 3f);
    }

    private void SetStaringCells()
    {
        backGroundImage.enabled = false;
        BoxAmount = 3;
        cells.Appear();
        taskText.Appear();
    }
}