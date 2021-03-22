using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Models
{
	[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
	sealed record Genome
	{
		public IReadOnlyList<Command> Commands { get; init; }

		public int CurrentPointer { get; set; }


		private string GetDebuggerDisplay()
		{
			return GetType().ToString();
		}
	}
}
