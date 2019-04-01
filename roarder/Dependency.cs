using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
using Roarder.Collections.Dictionaries;
using Roarder.A;
namespace Roarder
{
    class Dependency:ADependency
    {
        public static Dependency Main(JToken json = null) => new Dependency(json);
        public Dependency(JToken json = null) => this.SetDependency(json);
        public StrStrDicCol ClassnameNFileList => this.GetClassnameNFileList;
    }
}
