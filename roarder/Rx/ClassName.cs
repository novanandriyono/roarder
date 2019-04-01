using System.Collections.Generic;
using System.IO;
using Roarder.Rx.A;
namespace Roarder.Rx
{
    class ClassName:AClassName
    {
        public static ClassName Main(FileInfo FileInfo = null) => new ClassName(FileInfo);
        public ClassName(FileInfo FileInfo = null) => this.SetClassName(FileInfo);
        public string NameSpace => this.GetNameSpace;
        public List<string> FullNames => this.GetFullNames;
        public string FileInfo => this.GetFileInfo;
        public bool HasNameSpace => this.GetHasNameSpace;

    }
}
