using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class JSONHelper
    {
        private  const bool USE_SIMPLE_JSON = true;

        public static string export(Object o)
        {
           
                return SimpleJson.SimpleJson.SerializeObject(o, new EnumSupportedStrategy());
           

        }

        public static object import(string json, Type T)
        {

            
                return SimpleJson.SimpleJson.DeserializeObject(json, T,new EnumSupportedStrategy());
           
            
        }


    }

