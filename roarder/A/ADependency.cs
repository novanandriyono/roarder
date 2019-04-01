using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Roarder.Rx;
using Roarder.Collections.Dictionaries;
namespace Roarder.A
{
    class ADependency
    {
        private SearchOption AOption = SearchOption.TopDirectoryOnly;
        private string AExcept;
        private string ADir;
        private DirectoryInfo ADirInfo;
        private StrStrDicCol AClassnameNFileList = new StrStrDicCol();
        protected StrStrDicCol GetClassnameNFileList => this.AClassnameNFileList;
        protected ADependency SetDependency(JToken json = null)
            => this.SetADependency(json);
        private ADependency SetADependency(JToken json = null)
        {
            if (null == json) { return this; }
            if (json.HasValues != true) { return this; }
            if (json["dir"] != null) { this.ADir = json["dir"].ToString(); }

            if (Directory.Exists(this.ADir) != true)
            {
                Console.WriteLine("<SKIP> Directory not exsist {0}", this.ADir);
                return this;
            }

            if (json["option"] != null)
            {
                if (json["option"].ToString() == "all")
                {
                    this.AOption = SearchOption.AllDirectories;
                }
            }

            this.ADirInfo = new DirectoryInfo(this.ADir);
            if (json["except"] != null)
            {
                this.AExcept = json["except"].ToString();
            }
            foreach (FileInfo fileinfo in this.GetAllAFiles())
            {
                if (fileinfo.Extension.Equals(".php") == true
                    && this.AExceptChk(fileinfo.FullName, this.ADir) == false)
                {
                    this.SetAClassnameNFileList(fileinfo);
                }
            }
            return this;
        }

        private void SetAClassnameNFileList(FileInfo fi)
        {
            ClassName cl = new ClassName(fi);
            foreach (string fl in cl.FullNames)
            {
                if (this.AClassnameNFileList.ContainsKey(fl) == false)
                {
                    this.AClassnameNFileList.Add(fl, cl.FileInfo);
                }
            }
        }

        private FileInfo[] GetAllAFiles()
        {
            return this.ADirInfo.GetFiles("*.php", this.AOption);
        }

        private bool AHasExcept()
        {
            return this.AExcept.ToString().Length > 0;
        }

        private bool AExceptChk(string file, string root)
        {
            if (this.AHasExcept() == false)
            {
                return false;
            }
            string pattern = "^";
            pattern = pattern + root;
            pattern = pattern + this.AExcept;
            pattern = pattern.Replace("\\", "\\\\");
            return new Regex(pattern).Match(file).Success;
        }
    }
}
