using Roarder.Collections.Dictionaries;
using Roarder.Collections.Lists;
using Roarder.Configurations.A;
namespace Roarder.Configurations
{
    class ConfigParams:AConfigParams
    {
        public static ConfigParams Main() => new ConfigParams();
        public ConfigParams() => this.SetConfigParams();
        public ClassNameAndFile ClassNameAndFile => this.GetClassNameAndFile;
        public ClassNameAndFileGroupsByLength ClassNameAndFileGroupsByLength => this.GetClassNameAndFileGroupsByLength;
        public ListByStr NameSpaces => this.GetNameSpaces;
        public StrIntDicCol ListOfGroupKeys => this.GetListOfGroupKeys;
    }
}
