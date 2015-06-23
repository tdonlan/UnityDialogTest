using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using SimpleJson;

namespace TreeTest
{
    public class TreeSerializationStrategy : PocoJsonSerializerStrategy 
    {
        public override object DeserializeObject(object value, Type type)
        {
            return base.DeserializeObject(value, type);
        }
    }
}
