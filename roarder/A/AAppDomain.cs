using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Roarder.Collections.Dictionaries;
using Roarder.Helpers;
using Roarder.Configurations;
namespace Roarder.A
{
    abstract class AAppDomain
    {
        private readonly ConfigParams RPS = new ConfigParams();
        private readonly char DS = Path.DirectorySeparatorChar;
        private readonly string AAppPath = Directory.GetCurrentDirectory();
        private Dictionary<string, string> StrReplacer = new Dictionary<string, string>();

        private bool ALoaderDirExsist => Directory.Exists(this.AAppPath + DS + "roarder");

        //private string AutoloaderDoc = "<?php\nclass HEAD_CLASS{\nprivate $mapvar = [SWLOCARRAY];\npublic function __construct(){\nreturn HEAD_CLASS::init();\n}\nprivate function init(){\nreturn spl_autoload_register('HEAD_CLASS::requireFile');\n}\nprivate function requireFile(string $classvar){\n$XL = strlen($classvar);\n\nif(isset($mapvar[$XL])){\n$mapvar=require $mapvar[$XL];\n}\nif(!isset($mapvar[$classvar])){\nrequire_once 'ERROR_CLASS.php';\nthrow new ERROR_CLASS($classvar);\n}\nreturn require_once $mapvar[$classvar];\n}\n}";

        private string AutoloaderDoc = "<?php\nreturn new HEAD_CLASS;\nclass HEAD_CLASS{\npublic function __construct(){\nreturn HEAD_CLASS::init();\n}\nprivate function init(){\nreturn spl_autoload_register('HEAD_CLASS::requireFile');\n}\nprivate function requireFile(string $classvar){\n$XL = strlen($classvar);\nif($XL>0){\n$mapvar = [SWLOCARRAY];\nif(isset($mapvar[$XL])){\n$mapvar=require $mapvar[$XL];\n}\nif(isset($mapvar[$classvar])){\nreturn require_once $mapvar[$classvar];\n}\n}\n}\n}";

        //private string AutoloaderERR = "<?php\nclass ERROR_CLASS extends \\Exception{public function __construct($message, \\Exception $previous = null){parent::__construct($message, 0, $previous);}protected function setProp(){if (strpos($this->file, 'HEAD_CLASS') !== false){$this->file = 'loader';}}public function __toString(){return $this->setToString();}protected function setToString(){return \"class {$this->message} not found\";}}";

        private string LoaderStart = "<?php\nrequire_once __DIR__. '/roarder/HEAD_CLASS.php';";

        private StrStrDicCol AMaps = new StrStrDicCol();
        private DirectoryInfo LoaderDir;
        private DirectoryInfo AAppPathInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

        protected AAppDomain GetAppDomain(string[] args = null) => this.GetAAppDomain(args);
        private AAppDomain GetAAppDomain(string[] args = null) {
            Console.WriteLine("test");
            Console.ReadKey();

            this.StrReplacer.Add("HEAD_CLASS", this.GetRandomStr().Substring(0, 10));
            this.StrReplacer.Add("init", this.GetRandomStr().Substring(0, 4));
            this.StrReplacer.Add("requireFile", this.GetRandomStr().Substring(0, 5));
            this.StrReplacer.Add("classvar", this.GetRandomStr().Substring(0, 7));
            this.StrReplacer.Add("XL", this.GetRandomStr().Substring(0, 4));
            this.StrReplacer.Add("mapvar", this.GetRandomStr().Substring(0, 8));
            this.StrReplacer.Add("ERROR_CLASS", this.GetRandomStr().Substring(0, 7));
            this.StrReplacer.Add("LOADER_START", this.GetRandomStr().Substring(0, 7));
            string MapIf = "";
            foreach (KeyValuePair<string, int> listg in this.RPS.ListOfGroupKeys)
            {
                if (this.AMaps.ContainsKey(listg.Key) == false)
                {
                    string MapStr = this.CreateValueAMaps(listg.Value);
                    if (MapStr.Length > 0)
                    {
                        this.AMaps.Add(listg.Key, MapStr);
                        MapIf = MapIf + listg.Value + "=>'" + listg.Key + ".php',\n";
                    }
                }
            }

            this.AutoloaderDoc = this.AutoloaderDoc.Replace("SWLOCARRAY", MapIf);

            foreach (KeyValuePair<string, string> txts in StrReplacer)
            {
                this.AutoloaderDoc = this.AutoloaderDoc.Replace(txts.Key, txts.Value);
            }
            //this.AutoloaderERR = this.AutoloaderERR.Replace("ERROR_CLASS", StrReplacer["ERROR_CLASS"]).Replace("HEAD_CLASS", StrReplacer["HEAD_CLASS"]);
            this.LoaderStart = this.LoaderStart.Replace("HEAD_CLASS", StrReplacer["HEAD_CLASS"]).
                Replace("LOADER_START", StrReplacer["LOADER_START"]);
            //this.AMaps.Add(StrReplacer["ERROR_CLASS"], this.AutoloaderERR);
            
            this.AMaps.Add(StrReplacer["HEAD_CLASS"], this.AutoloaderDoc);
            Directory.CreateDirectory(this.AAppPath + DS + "roarder");
            
            this.CreateMapFiles();
            this.CreateLoaderStart();
            Console.ReadKey();
            return this;
        }
        private string GetRandomStr()
        {
            string str = this.RandomStr();
            if (this.StrReplacer.ContainsValue(str) == false)
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

        private void CreateLoaderStart()
        {
            Console.WriteLine("Creating roarder.php");
            string filename = this.AAppPath + this.DS + "roarder.php";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            using (FileStream fs = File.Create(filename, 1024))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(this.LoaderStart);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }

        private void CreateMapFiles()
        {
            Console.WriteLine("Create Map File");
            foreach (KeyValuePair<string, string> maps in this.AMaps)
            {
                string filename = this.AAppPath + this.DS + "roarder" + this.DS + maps.Key + ".php";
                using (FileStream fs = File.Create(filename, 1024))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(maps.Value.ToString());
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                if (maps.Key.Equals(this.StrReplacer["HEAD_CLASS"])) {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(filename);
                    Console.ResetColor();
                }
            }
        }

        private string CreateValueAMaps(int key)
        {
            if (this.RPS.ClassNameAndFileGroupsByLength.ContainsKey(key) == false)
            {
                return "";
            }
            return new StrForMaps(this.RPS.ClassNameAndFileGroupsByLength[key]).str;
        }

        

    }
}
