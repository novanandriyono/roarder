namespace Roarder.A
{
    abstract class AExe
    {
        private AppMain AppDomain = new AppMain();
        protected AExe GetExe(string[] args) => this.GetAExe(args);
        private AExe GetAExe(string[] args) {
            return this;
        }
    }
}
