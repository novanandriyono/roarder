using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Roarder.Rx.A
{
    abstract class AClassName
    {
        private readonly char nssparator = Path.DirectorySeparatorChar;
        private readonly string pnamespace = "(namespace)(\\s+)([A-Za-z0-9\\\\]+?)(\\s*);";
        private readonly string classpattern = "(^|\\s|\\s+)(abstract class|class|interface|trait)[\\s|\\s+]+([\\w\\d_]+)[\\s]*(extends|implements|{)?{?";
        private readonly string[] cleanreg =
            new string[7] {
                @"(\s+)\/\*([^\/]*)\*\/(\s+)",
                @"\/\/.*\s+",
                @"\"".*\""|\'.*\'|\(.*?\)",
                @"\{.*?\}",
                @"\}\{",
                @"\{.*?\}",
                @"\s+"
            };
        private string ANameSpace = "";
        private string AFileInfo;
        private List<string> AFullNames = new List<string>();
        private Dictionary<string, string> AFullNamesNFiles = new Dictionary<string, string>();
        private bool AHasNamespace => this.ANameSpace.Length > 0;
        protected bool GetHasNameSpace => this.AHasNamespace;
        protected string GetNameSpace => this.ANameSpace;
        protected List<string> GetFullNames => this.AFullNames;
        protected string GetFileInfo => this.AFileInfo;
        protected AClassName SetClassName(FileInfo AFI = null) => this.SetAClassName(AFI);
        private AClassName SetAClassName(FileInfo AFI = null) => null != AFI ? this.CheckInputType(AFI) : this;

        private AClassName CheckInputType(FileInfo AFI)
        {
            if (AFI.Extension != ".php")
            {
                Console.WriteLine("File Must php");
                return this;
            }
            this.AFileInfo = AFI.FullName;
            return this.ClearStr();
        }

        private AClassName ClearStr()
        {
            string txt = File.ReadAllText(this.AFileInfo);
            foreach (string rexitem in this.cleanreg){
                Regex regex = new Regex(rexitem);
                txt = regex.Replace(txt, " ");
            }
            return this.SetNameSpace(txt);
        }

        private AClassName SetNameSpace(string str)
        {
            Match m = Regex.Match(str, this.pnamespace);
            if (m.Groups[3].Success == true)
            {
                this.ANameSpace = m.Groups[3].Value;
            }
            return this.SetANames(str);
        }

        private AClassName SetANames(string str)
        {
            Regex regex = new Regex(this.classpattern);
            foreach (Match match in regex.Matches(str))
            {
                this.SetAddANames(match.Groups[3].Value);
            }
            return this;
        }

        private void SetAddANames(string classname)
        {
            classname = this.CreateFullName(classname);
            if (this.AFullNames.Contains(classname) == false)
            {
                this.AFullNames.Add(classname);
            }
        }

        private string CreateFullName(string classname)
        {
            return this.AHasNamespace? this.CombineNameWithNamSpace(classname) : classname;
        }

        private string CombineNameWithNamSpace(string classname) {
            return this.ANameSpace + nssparator + classname;
        }
    }
}
