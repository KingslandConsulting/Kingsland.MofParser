﻿using System.Collections.Generic;

namespace Kingsland.MofParser.HtmlReport.Wrappers
{

    public sealed class InstanceWrapperGroup
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

        public List<InstanceWrapper> Wrappers
        {
            get;
            set;
        }

    }

}
