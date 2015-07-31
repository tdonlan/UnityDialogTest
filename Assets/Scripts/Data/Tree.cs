using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
  
    public class WorldTree : ITree
    {
        public string treeName { get; set; }
        public long treeIndex { get; set; }
        public long currentIndex { get; set; }
        public TreeType treeType { get; set; }

        public ITreeNode tempTreeNode;

        public Dictionary<long, WorldTreeNode> treeNodeDictionary { get; set; }

        public GlobalFlags globalFlags { get; set; }

        public WorldTree(GlobalFlags globalFlags, TreeType treeType)
        {
            currentIndex = 0;
            treeNodeDictionary = new Dictionary<long, WorldTreeNode>();
            this.globalFlags = globalFlags;
            this.treeType = treeType;
        }

            public ITreeNode getNode(long index)
        {
            if(treeNodeDictionary.ContainsKey(index))
            {
                return treeNodeDictionary[index];
            }
            return null;
        }

        public void SelectNode(long index)
        {
            this.currentIndex = index;

            treeNodeDictionary[currentIndex].SelectNode(this);

        }

        public bool checkNode(long index)
        {
            return treeNodeDictionary.ContainsKey(index);
        }

        public bool validateTreeLinks()
        {
            bool validLinks = true;
            foreach (TreeNode node in treeNodeDictionary.Values)
            {
                foreach (var branch in node.branchList)
                {
                    Console.Write(string.Format("Checking {0} for link {1} ...",branch.description, branch.linkIndex));
                    if (!checkNode(branch.linkIndex))
                    {
                        validLinks = false;
                         Console.Write(" MISSING.\n");
                    }
                    else{
                        Console.Write(" found.\n");
                    }
                }
            }
            return validLinks;
        }

  
    }

     public class ZoneTree : ITree
    {
        public string treeName { get; set; }
        public long treeIndex { get; set; }
        public long currentIndex { get; set; }
        public TreeType treeType { get; set; }

        public Dictionary<long, ZoneTreeNode> treeNodeDictionary { get; set; }

        public GlobalFlags globalFlags { get; set; }

        public ZoneTree(GlobalFlags globalFlags, TreeType treeType)
        {
            currentIndex = 0;
            treeNodeDictionary = new Dictionary<long, ZoneTreeNode>();
            this.globalFlags = globalFlags;
            this.treeType = treeType;
        }

            public ITreeNode getNode(long index)
        {
            if(treeNodeDictionary.ContainsKey(index))
            {
                return treeNodeDictionary[index];
            }
            return null;
        }

        public void SelectNode(long index)
        {
            this.currentIndex = index;

            treeNodeDictionary[currentIndex].SelectNode(this);

        }

        public bool checkNode(long index)
        {
            return treeNodeDictionary.ContainsKey(index);
        }

        public bool validateTreeLinks()
        {
            bool validLinks = true;
            foreach (TreeNode node in treeNodeDictionary.Values)
            {
                foreach (var branch in node.branchList)
                {
                    Console.Write(string.Format("Checking {0} for link {1} ...", branch.description, branch.linkIndex));
                    if (!checkNode(branch.linkIndex))
                    {
                        validLinks = false;
                        Console.Write(" MISSING.\n");
                    }
                    else
                    {
                        Console.Write(" found.\n");
                    }
                }
            }
            return validLinks;
        }

      

     }



     public class DialogTree : ITree
     {
          public string treeName { get; set; }
        public long treeIndex { get; set; }
        public long currentIndex { get; set; }
        public TreeType treeType { get; set; }

        public Dictionary<long, DialogTreeNode> treeNodeDictionary { get; set; }

        public GlobalFlags globalFlags {get;set;}

        public DialogTree(GlobalFlags globalFlags, TreeType treeType)
        {
            currentIndex = 0;
            treeNodeDictionary = new Dictionary<long, DialogTreeNode>();
            this.globalFlags = globalFlags;
            this.treeType = treeType;
        }

        public ITreeNode getNode(long index)
        {
            if(treeNodeDictionary.ContainsKey(index))
            {
                return treeNodeDictionary[index];
            }
            return null;
        }

        public void SelectNode(long index)
        {
            this.currentIndex = index;

            treeNodeDictionary[currentIndex].SelectNode(this);

        }

        public bool checkNode(long index)
        {
            return treeNodeDictionary.ContainsKey(index);
        }

        public bool validateTreeLinks()
        {
            bool validLinks = true;
            foreach (TreeNode node in treeNodeDictionary.Values)
            {
                foreach (var branch in node.branchList)
                {
                    Console.Write(string.Format("Checking {0} for link {1} ...", branch.description, branch.linkIndex));
                    if (!checkNode(branch.linkIndex))
                    {
                        validLinks = false;
                        Console.Write(" MISSING.\n");
                    }
                    else
                    {
                        Console.Write(" found.\n");
                    }
                }
            }
            return validLinks;
        }

     }

     public class QuestTree : ITree
     {
         public string treeName { get; set; }
         public long treeIndex { get; set; }
         public long currentIndex { get; set; }
         public TreeType treeType { get; set; }

         public Dictionary<long, QuestTreeNode> treeNodeDictionary { get; set; }

         public GlobalFlags globalFlags { get; set; }

         public QuestTree(GlobalFlags globalFlags, TreeType treeType)
         {
             currentIndex = 0;
             treeNodeDictionary = new Dictionary<long, QuestTreeNode>();
             this.globalFlags = globalFlags;
             this.treeType = treeType;
         }

         public ITreeNode getNode(long index)
         {
             if (treeNodeDictionary.ContainsKey(index))
             {
                 return treeNodeDictionary[index];
             }
             return null;
         }

         public void SelectNode(long index)
         {
             this.currentIndex = index;

             treeNodeDictionary[currentIndex].SelectNode(this);

         }

         public bool checkNode(long index)
         {
             return treeNodeDictionary.ContainsKey(index);
         }

         public bool validateTreeLinks()
         {
             bool validLinks = true;
             foreach (TreeNode node in treeNodeDictionary.Values)
             {
                 foreach (var branch in node.branchList)
                 {
                     Console.Write(string.Format("Checking {0} for link {1} ...", branch.description, branch.linkIndex));
                     if (!checkNode(branch.linkIndex))
                     {
                         validLinks = false;
                         Console.Write(" MISSING.\n");
                     }
                     else
                     {
                         Console.Write(" found.\n");
                     }
                 }
             }
             return validLinks;
         }
     }

     public class BattleTree : ITree
     {
         public string treeName { get; set; }
         public long treeIndex { get; set; }
         public long currentIndex { get; set; }
         public TreeType treeType { get; set; }

         public Dictionary<long, BattleTreeNode> treeNodeDictionary { get; set; }

         public GlobalFlags globalFlags { get; set; }

         public BattleTree(GlobalFlags globalFlags, TreeType treeType)
         {
             currentIndex = 0;
             treeNodeDictionary = new Dictionary<long, BattleTreeNode>();
             this.globalFlags = globalFlags;
             this.treeType = treeType;
         }

         public ITreeNode getNode(long index)
         {
             if (treeNodeDictionary.ContainsKey(index))
             {
                 return treeNodeDictionary[index];
             }
             return null;
         }

         public void SelectNode(long index)
         {
             this.currentIndex = index;

             treeNodeDictionary[currentIndex].SelectNode(this);

         }

         public bool checkNode(long index)
         {
             return treeNodeDictionary.ContainsKey(index);
         }

         public bool validateTreeLinks()
         {
             bool validLinks = true;
             foreach (TreeNode node in treeNodeDictionary.Values)
             {
                 foreach (var branch in node.branchList)
                 {
                     Console.Write(string.Format("Checking {0} for link {1} ...", branch.description, branch.linkIndex));
                     if (!checkNode(branch.linkIndex))
                     {
                         validLinks = false;
                         Console.Write(" MISSING.\n");
                     }
                     else
                     {
                         Console.Write(" found.\n");
                     }
                 }
             }
             return validLinks;
         }
     }

    public class TreeBranchCondition
    {
        public string flagName { get; set; }
        public string value { get; set; }
        public CompareType flagCompareType { get; set; }

        public TreeBranchCondition(string flagName, string value, CompareType compareType)
        {
            this.flagName = flagName;
            this.value = value;
            this.flagCompareType = compareType;
        }
    }

    public class TreeNodeFlagSet
    {
        public string flagName { get; set; }
        public string value { get; set; }
        public FlagType flagType { get; set; }
    }


    public class TreeBranch
    {
        public string description { get; set; }
        public long linkIndex { get; set; }
        public List<TreeBranchCondition> conditionList { get; set; }

        public TreeBranch()
        {
            this.conditionList = new List<TreeBranchCondition>();
        }
            
        public TreeBranch(string description, long linkIndex, List<TreeBranchCondition> conditionList)
        {
            this.description = description;
            this.linkIndex = linkIndex;
            this.conditionList = conditionList;
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", description, linkIndex);
        }
    }


