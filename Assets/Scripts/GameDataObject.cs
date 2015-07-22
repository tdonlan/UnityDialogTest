using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class GameDataObject : MonoBehaviour
    {
        public TreeStore treeStore { get; set; }
        public string testText { get; set; }


        void Start()
        {
            loadTreeStore();
            this.testText = "Hello World";

            DontDestroyOnLoad(this);

        }

        private void loadTreeStore()
        {
            TextAsset manifestTextAsset = Resources.Load<TextAsset>("SimpleWorld1/manifestSimple");
            this.treeStore = SimpleTreeParser.LoadTreeStoreFromSimpleManifest(manifestTextAsset.text);
        }

    }

