using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roarder.Configurations;
using Roarder.Rx;
using Roarder.Collections.Dictionaries;
using Roarder.Collections.Lists;

namespace Roarder.Configurations.A
{
    abstract class AConfigParams
    {
        private readonly Config ACFG = new Config();
        private ListByStr ANameSpaces = new ListByStr();
        private ClassNameAndFile AClassNameAndFile = new ClassNameAndFile();
        private ClassNameAndFileGroupsByLength AClassNameAndFileGroupsByLength = new ClassNameAndFileGroupsByLength();
        private StrIntDicCol AListOfGroupKeys = new StrIntDicCol();
        private StrStrDicCol ADirOfGroupKeys = new StrStrDicCol();
        protected AConfigParams SetConfigParams() => this.SetAConfigParams();
        private AConfigParams SetAConfigParams()
        {
            return this.SetAClassMap();
        }
        private AConfigParams SetAClassMap()
        {
            foreach (Dependency dependency in this.ACFG.Dependencies)
            {
                foreach (KeyValuePair<string, string> classname in dependency.ClassnameNFileList)
                {
                    if (this.AClassNameAndFile.ContainsKey(classname.Key) == false)
                    {
                        this.AClassNameAndFile.Add(classname.Key, classname.Value);
                        Console.WriteLine("{0} =>\n{1}", new string[]{
                            classname.Key, classname.Value
                        });
                    }
                    else
                    {
                        Console.Write("<SKIP> Duplicated classname:{0} ;File:{1}\nOld one will be use\n", new string[]{
                            classname.Key, classname.Value
                        });
                    }
                }
            }
            return this.SetAClassNameAndFileGroupsByLength();
        }

        private AConfigParams SetAClassNameAndFileGroupsByLength()
        {
            Console.WriteLine("Create classname group by length classname");
            foreach (KeyValuePair<string, string> cnf in this.AClassNameAndFile)
            {
                string key = cnf.Key;
                string val = cnf.Value;
                if (this.AClassNameAndFileGroupsByLength.ContainsKey(key.Length) == false)
                {
                    this.AClassNameAndFileGroupsByLength.Add(key.Length, new ClassNameAndFile());

                }
                if (this.AClassNameAndFileGroupsByLength[key.Length].ContainsKey(key) == false)
                {
                    this.AClassNameAndFileGroupsByLength[key.Length].Add(key, val);
                }
            }
            return this.SetAClassNameAndFileGroups();
        }
        private AConfigParams SetAClassNameAndFileGroups()
        {
            Console.WriteLine("Create random filename");
            foreach (KeyValuePair<int, ClassNameAndFile> classnamenfile in this.AClassNameAndFileGroupsByLength)
            {
                int key = classnamenfile.Key;
                if (this.AListOfGroupKeys.ContainsValue(key) == false)
                {
                    this.AListOfGroupKeys.Add(this.GetRandomStr(), key);
                }
            }
            return this;
        }

        private string CreateNameForAListOfGroupKeys()
        {
            string name = this.GetRandomStr();
            if (this.AListOfGroupKeys.ContainsKey(name) == false)
            {
                return this.CreateNameForAListOfGroupKeys();
            }
            Console.WriteLine(name);
            return name;
        }

        private string GetRandomStr()
        {
            string str = this.RandomStr();
            if (this.AListOfGroupKeys.ContainsKey(str) == false)
            {
                if ((str[0] >= 'A' && str[0] <= 'Z') || (str[0] >= 'a' && str[0] <= 'z'))
                {
                    return str;
                }
            }
            return this.GetRandomStr();
        }

        private string RandomStr()
        {
            return Path.GetRandomFileName().Replace(".", "");
        }

        protected ClassNameAndFile GetClassNameAndFile => this.AClassNameAndFile;
        protected ClassNameAndFileGroupsByLength GetClassNameAndFileGroupsByLength => this.AClassNameAndFileGroupsByLength;
        protected ListByStr GetNameSpaces => this.ANameSpaces;
        protected StrIntDicCol GetListOfGroupKeys => this.AListOfGroupKeys;
        protected Config GetConfig => this.ACFG;
    }
}
