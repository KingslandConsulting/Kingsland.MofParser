using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsland.MofParser.Lexing
{

    /// <summary>
    /// Tracks the sequential position in a stream, together with the textual line and column number.
    /// </summary>
    internal class StreamPosition
    {

        public int Position
        {
            get;
            protected set;
        }

        public int LineNumber
        {
            get;
            protected set;
        }

        public int Column
        {
            get;
            protected set;
        }

    }

}
