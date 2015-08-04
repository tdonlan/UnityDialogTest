using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class Item
    {
        public long index;
        public string name;

    }

    //Used for testing.
    //Items should be loaded from external data source
    public class ItemFactory
    {
        public static long itemCounter = 1;
        public static Item getItem(string name)
        {
            return new Item() { index = itemCounter++, name = name };
        }

        public static Dictionary<long, Item> getItemDictionary()
        {
            Dictionary<long, Item> itemDictionary = new Dictionary<long, Item>();
            List<string> itemNameList = new List<string>() { "Gold", "Gold Tooth", "Old Key", "Red Gem" };
            foreach (var s in itemNameList)
            {
                Item tempItem = ItemFactory.getItem(s);
                itemDictionary.Add(tempItem.index, tempItem);
            }

            return itemDictionary;
        }
    }
}
