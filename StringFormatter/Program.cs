using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new JObject();
            obj.Add("message", "asd:fdsfgs:Fault--[[Message:ImessageCreated");
            obj.Add("headers", "someOtherStuff");


            var formatter = new Formatter();
            formatter.AddRule("message", (x) => x.Replace("--", ""));

            var q = formatter.Format(obj);
        }
    }
}
