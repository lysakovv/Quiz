using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "SceneObjects/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string name;
    public string Name
    {
        get => name;
        private set => name = value;
    }

    [SerializeField] private Sprite icon;

    public Sprite Icon
    {
        get => icon;
        private set => icon = value;
    }
}
