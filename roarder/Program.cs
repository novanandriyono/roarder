using Roarder.A;
namespace Roarder
{
    class Program:AProgram
    {
        public static void Main(string[] args) => new Program(args);
        public Program(string[] args) => this.SetProgram(args);
    }
}
