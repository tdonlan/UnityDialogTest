using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


using SimpleJson;

namespace TreeTest
{
    public class TreeStoreExporter
    {
        public static void exportTreeStore(TreeStore ts, string path)
        {
            List<string> exportPathList = new List<string>();
             List<TreeManifestItem> treeManifestList = new List<TreeManifestItem>();

            foreach(var key in ts.treeDictionary.Keys)
            {
                var tree = ts.treeDictionary[key];
                string exportPath = getTreeExportPath(key, tree, path);

                string treeJSON = JSONHelper.export(tree);

                File.WriteAllText(exportPath, treeJSON, Encoding.Default);

                treeManifestList.Add(new TreeManifestItem() {treeIndex=key,treeName=tree.treeName,treePath=exportPath,treeType=tree.treeType });
            }

            string manifestPath = path + "/manifest.json";

            File.WriteAllText(manifestPath, JSONHelper.export(treeManifestList));
        }

        public static void exportTree(ITree t, string path)
        {
            string treeJSON = JSONHelper.export(t);
            File.WriteAllText(path, treeJSON);
        }

        public static string getTreeExportPath(long index, ITree t, string path)
        {
            return path + "/" + t.treeType + index + ".json";
        }
    }
}
