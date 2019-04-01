using System;
using Roarder.Helpers.A;
using Roarder.Collections.Dictionaries;
namespace Roarder.Helpers
{
    class StrForMaps:AStrForMaps{
        public static StrForMaps Main(ClassNameAndFile g = null) => new StrForMaps(g);
        public StrForMaps(ClassNameAndFile g = null) => this.SetStrForMaps(g);
        public string str => this.GetStr;
    }
}
