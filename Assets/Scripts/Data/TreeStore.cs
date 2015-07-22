using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

    public class TreeManifestItem
    {
        public string treePath { get; set; }
        public TreeType treeType { get; set; }
        public string treeName { get; set; }
        public long treeIndex { get; set; }
    }

    public class TreeStore
    {
        public Dictionary<long, ITree> treeDictionary { get; set; }
        public GlobalFlags globalFlags {get;set;}
        public long currentTreeIndex { get; set; }

        public TreeStore()
        {
            this.globalFlags = new GlobalFlags();
            this.treeDictionary = new Dictionary<long, ITree>();
        }

        public ITree getCurrentTree()
        {
            if (treeDictionary.ContainsKey(currentTreeIndex))
            {
                return treeDictionary[currentTreeIndex];
            }
            return null;
        }

        public void SelectTree(long index)
        {
            if (treeDictionary.ContainsKey(index))
            {
                this.currentTreeIndex = index;
            }
        }
    }
