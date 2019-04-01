using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roarder.Collections.Dictionaries;
namespace Roarder.Helpers.A{
    abstract class AStrForMaps{
        private string AStr = "<?php\n return array(\n";
        protected AStrForMaps SetStrForMaps(ClassNameAndFile lists = null) => this.SetAStrForMaps(lists);
        private AStrForMaps SetAStrForMaps(ClassNameAndFile lists = null) {
            if (null == lists) {
                return this;
            }

            foreach (KeyValuePair<string, string> list in lists) {
                string key = "'" + list.Key + "'" ;
                string sp = " => ";
                string val = "'" + list.Value + "',\n";
                string line = key + sp + val;
                this.AStr = this.AStr + line;
            }

            this.AStr = this.AStr + ");";
            return this;
        }

        protected string GetStr => this.AStr;
    }
}
