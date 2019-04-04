using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Roarder.Collections.Dictionaries;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Roarder.Helpers.A
{
    class AEnvHelper
    {
        private readonly string keys = Path.GetRandomFileName().Replace(".","");
        private List<string> hr = new List<string>();
        private string[] envs = Environment.GetEnvironmentVariable("PATH").Split(';');
        private readonly Char DS = Path.DirectorySeparatorChar;
        protected AEnvHelper SetEnvHelper() => this.SetAEnvHelper();
        private AEnvHelper SetAEnvHelper() {
            foreach (string item in envs) {
                string str = item + DS + "Roarder.exe";
                if (item.Length == 0) {
                    continue;
                }
                if (File.Exists(str) == true) {
                    this.hr.Add(str);
                }
                str = item + "Roarder.exe";
                if (File.Exists(str) == true)
                {
                    this.hr.Add(str);
                }
            }
            if (this.hr.Count() == 1) {
//                this.ChkAPP(this.hr[0]);
                return this ;
            } else if(this.hr.Count() == 0){
                Console.WriteLine("can found env roarder");
                Console.WriteLine("see:https://github.com/novanandriyono/roarder");
                Console.ReadKey();
                Environment.Exit(0);
            }else if(this.hr.Count() > 1){
                Console.WriteLine("Duplicated roarder env");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else {
                Console.WriteLine("Error while chk env");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Console.ReadKey();
            return this;
        }

    }
}
