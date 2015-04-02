using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{
	class NullLiteralToken : Token
	{

		internal NullLiteralToken(SourceExtent extent)
			: base(extent)
		{
			
		}

		public const string Value = "NULL";
	}
}
