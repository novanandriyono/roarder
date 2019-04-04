using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Roarder.Collections.Lists;
using Roarder.Helpers;
namespace Roarder.Configurations.A
{
    abstract class AConfig
    {
        private EnvHelper AEnvHelper = new EnvHelper();

        private readonly string ADS = Path.DirectorySeparatorChar.ToString();

        private readonly string AConfigFileName = "roarder.json";

        private readonly string AAppPath = Directory.GetCurrentDirectory();

        private DirectoryInfo AAppPathInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

        private SearchOption AOption = SearchOption.TopDirectoryOnly;

        private string AExcept;
        
        private Dependencies ADependencies = new Dependencies();

        private string AFullConfigPath(){
            return this.AAppPath + this.ADS + this.AConfigFileName;
        }

        protected AConfig SetConfig() => this.SetAConfig();

        private AConfig SetAConfig() => this.ReadAConfig();

        private bool HasAConfig(){
            return File.Exists(AAppPath + ADS + AConfigFileName);
        }

        private bool HasAutoLoaderDir =>
            Directory.Exists(this.AAppPath + this.ADS + "roarder");
        private bool HasAutoLoaderStart =>
            File.Exists(this.AAppPath + this.ADS + "roarder.php");

        private AConfig ReadAConfig(){
            if (this.HasAConfig() != true) {
                return this.CreateAConfig();
            }

            if (this.HasAutoLoaderDir == true) {
                Console.WriteLine("Found roarder directory");
                Console.WriteLine("Deleting roader directory");
                Directory.Delete(this.AAppPath + this.ADS + "roarder", true);
            }

            if (this.HasAutoLoaderStart == true)
            {
                Console.WriteLine("Found roarder directory");
                Console.WriteLine("Deleting roader directory");
                File.Delete(this.AAppPath + this.ADS + "roarder.php");
            }

            JObject jsons = this.GetAConfigFromJson();
            if(jsons.HasValues != true){
                Console.Write("Unvalid config");
                Console.ReadKey();
                return this;
            }
            
            if (jsons["option"] == null) {
                jsons["option"] = "only";
            }

            if (jsons["option"].Equals("all") == true){
                this.AOption = SearchOption.AllDirectories;
            }

            if (jsons["except"] != null){this.AExcept = jsons["except"].ToString();}
            dynamic array = new JObject();

            if (jsons["dependencies"] != null){
                if (jsons["dependencies"].GetType() == typeof(JArray)){
                    array.dir = this.AAppPath;
                    array.except = this.AExcept;
                    array.option = this.AOption == SearchOption.AllDirectories ? "all" : "only";
                    if (jsons["dependencies"].Count() == 0)
                    {
                        array.dir = this.AAppPath;
                        array.except = this.AExcept;

                        Dependency dependency = new Dependency(array);
                        this.ADependencies.Add(dependency);
                    }
                    else {
                        jsons["dependencies"][0].AddBeforeSelf(array);
                        this.ADependencies = new Dependencies();
                        foreach (JToken item in jsons["dependencies"])
                        {
                            Dependency dependency = new Dependency(item);
                            this.ADependencies.Add(dependency);
                        }
                    }
                }
                return this;
            }
            else {
                array.dir = this.AAppPath;
                array.except = this.AExcept;
                array.option = this.AOption == SearchOption.AllDirectories ? "all" : "only";
                Dependency dependency = new Dependency(array);
                this.ADependencies.Add(dependency);
            }

            if (this.ADependencies.Count() < 1) {
                Console.WriteLine("Cant found classnames");
                Console.ReadKey();
                Environment.Exit(0);
            }

            return this;
        }
     

        private bool AFilesChk(FileInfo file)
        {
            if (this.AExcept == null)
            {
                return false;
            }
            string pattern = "^";
            pattern = pattern + this.AAppPath;
            pattern = pattern + this.AExcept;
            pattern = pattern.Replace("\\", "\\\\");
            return !new Regex(pattern).Match(file.FullName).Success;
        }

        private JObject GetAConfigFromJson() {
            try
            {
                return JObject.Parse(this.GetAConfigContents);
            }
            catch (JsonReaderException e)
            {
                return new JObject();
            }
        }

        private string GetAConfigContents =>
            this.HasAConfig()?File.ReadAllText(AAppPath+ADS+AConfigFileName):"";
        

        private AConfig CreateAConfig() {
            Console.WriteLine("file roarder.json not found");
            Console.WriteLine("Creating roarder.json");
            string filename = this.AAppPath + this.ADS + "roarder.json";
            string configstr = "{\"except\": \"(\\\\[.].*$|.*\\\\vendor\\\\.*)\",\"option\": \"only\",\"dependencies\":[]}";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            using (FileStream fs = File.Create(filename, 1024))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(configstr);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }

            return this;
        }

        protected string GetAppPath => this.AAppPath;
        protected SearchOption GetOption => this.AOption;
        protected string GetExcept => this.AExcept;
        protected Dependencies GetDependencies => this.ADependencies;
    }
}
