using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devdog.SceneCleanerPro
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SettingsCategoryAttribute : Attribute
    {
        public string name { get; protected set; }


        public SettingsCategoryAttribute(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
