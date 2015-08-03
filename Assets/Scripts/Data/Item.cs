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

    public class ItemFactory
    {

        public static long itemCounter = 0;
        public static Item getItem(string name)
        {
            return new Item() { index = itemCounter++, name = name };

        }
    }
}
