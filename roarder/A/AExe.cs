namespace Roarder.A
{
    abstract class AExe
    {
        private AppDomain AppDomain = new AppDomain();
        protected AExe GetExe(string[] args) => this.GetAExe(args);
        private AExe GetAExe(string[] args) {
            return this;
        }
    }
}
