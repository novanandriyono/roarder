using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roarder.A;
namespace Roarder
{
    class AppMain:AAppDomain
    {
        public AppMain(string[] args = null) => this.GetAppDomain(args);
    }
}
