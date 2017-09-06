using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace StringFormatter
{
    public class Formatter
    {
        private Dictionary<string, List<Func<string, string>>> _rules = new Dictionary<string, List<Func<string, string>>>();

        public void AddRule(string propertyName, List<Func<string, string>> toRemove)
        {
            _rules[propertyName] = toRemove;
        }

        public void AddRule(string propertyName, Func<string, string> toRemove)
        {
            _rules[propertyName] = new List<Func<string, string>>() { toRemove };
        }

        public void RemoveRule(string propertyName)
        {
            _rules.Remove(propertyName);
        }

        public object Format(object obj)
        {
            var jObj = obj as JObject;
            var newObj = new JObject();

            foreach (var value in jObj.Values())
            {
                string temp = value.ToString();

                List<Func<string, string>> funcList;

                if (_rules.TryGetValue(value.Path, out funcList))
                {
                    foreach (var func in _rules[value.Path])
                    {
                        temp = func(temp);
                    }
                }

                newObj.Add(value.Path, temp);
            }

            return newObj;
        }
    }
}
