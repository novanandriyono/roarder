using System.IO;
using System.Collections.Generic;
using Roarder.Configurations.A;
using Roarder.Collections.Lists;
namespace Roarder.Configurations
{
    class Config:AConfig
    {
        public static Config Main() => new Config();
        public Config() => this.SetConfig();
        public string AppPath => this.GetAppPath;
        public SearchOption Option => this.GetOption;
        public string Except => this.GetExcept;
        public Dependencies Dependencies => this.GetDependencies;
    }
}
