using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    [Serializable]
    public struct ItemsInventory
    {
        
        [SerializeField] private string listName;
        [SerializeField] private List<Item> items;

        public ItemsInventory(int a)
        {
            items = new List<Item>();
            listName = "";
        }

        public List<Item> Items
        {
            private set { items = value; }
            get { return items; }
        }
    }
    
    [SerializeField] private List<ItemsInventory> typesList = new List<ItemsInventory>();
    
    public List<ItemsInventory> TypesList
    {
        private set { typesList = value; }
        get { return typesList; }
    }
}