using System;
using System.Reflection;
namespace Roarder.A
{
    abstract class AProgram
    {
        protected AProgram SetProgram(string[] args) => this.SetAProgram(args);
        private AProgram SetAProgram(string[] args) {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, arg) => { if (arg.Name.StartsWith("Newtonsoft")) return Assembly.Load(Properties.Resources.Newtonsoft_Json); return null; };
            return typeof(Exe).Equals(new Exe(args)) ?this:null;
        }
    }
}
