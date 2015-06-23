
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using SimpleJson;

namespace TreeTest
{
    public class TreeStoreLoader
    {
        public static TreeStore loadTreeStoreFromManifest(string manifestPath)
        {
            string manifestStr = File.ReadAllText(manifestPath);

          
            List<TreeManifestItem> manifestJSON = (List<TreeManifestItem>)JSONHelper.import(manifestStr, typeof(List<TreeManifestItem>));

            GlobalFlags globaFlags = new GlobalFlags();

            TreeStore treeStore = new TreeStore();

            foreach (var treeItem in manifestJSON)
            {
                ITree tempTree = loadTreeFromPath(treeItem.treeType, treeItem.treePath);
                treeStore.treeDictionary.Add(treeItem.treeIndex, tempTree);
            }

            return treeStore;
        }

        public static ITree loadTreeFromPath(TreeType treeType, string treePath)
        {
            string treeStr = File.ReadAllText(treePath);

            switch (treeType)
            {
                case TreeType.World:
                    return (WorldTree)JSONHelper.import(treeStr, typeof(WorldTree));


                case TreeType.Zone:
                    return (ZoneTree)JSONHelper.import(treeStr, typeof(ZoneTree));


                case TreeType.Dialog:
                    return (DialogTree)JSONHelper.import(treeStr, typeof(DialogTree));


                case TreeType.Quest:
                    return (QuestTree)JSONHelper.import(treeStr, typeof(QuestTree));


                default: return null;
            }



        }
    }
}
