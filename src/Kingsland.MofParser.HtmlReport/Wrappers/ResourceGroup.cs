using Kingsland.MofParser.HtmlReport.Resources;
using System.Collections.Generic;

namespace Kingsland.MofParser.HtmlReport.Wrappers
{

    public sealed class ResourceGroup
    {

        public string Filename
        {
            get;
            set;
        }

        public string ComputerName
        {
            get;
            set;
        }

        public List<DscResource> Wrappers
        {
            get;
            set;
        }

    }

}
