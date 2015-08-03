﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets;


public class GameDataObject : MonoBehaviour
    {
        public TreeStore treeStore { get; set; }
        public string testText { get; set; }
        public Dictionary<long, Item> itemDictionary;

        public List<long> playerInventory = new List<long>();

        void Start()
        {
            loadTreeStore();
            this.testText = "Hello World";
            loadItemList();

            DontDestroyOnLoad(this);

        }

    private void loadItemList(){
        itemDictionary = new Dictionary<long, Item>(); 
        List<string> itemNameList = new List<string>() { "Rusty Sword", "Bent Key", "Golden Key", "Poison Arrows" };
        foreach (var s in itemNameList)
        {
            Item tempItem = ItemFactory.getItem(s);
            itemDictionary.Add(tempItem.index, tempItem);
        }
    }

        private void loadTreeStore()
        {
            TextAsset manifestTextAsset = Resources.Load<TextAsset>("SimpleWorld1/manifestSimple");
            this.treeStore = SimpleTreeParser.LoadTreeStoreFromSimpleManifest(manifestTextAsset.text);
        }




    }

