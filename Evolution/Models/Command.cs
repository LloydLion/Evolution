using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Models
{
	[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
	record Command
	{
		public const int CommandLimit = 256;


		public int Type { get; init; }


		private string GetDebuggerDisplay()
		{
			return GetType().ToString();
		}
	}
}
