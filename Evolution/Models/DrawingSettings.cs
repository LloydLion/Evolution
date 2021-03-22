using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Models
{
	[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
	sealed record DrawingSettings
	{
		public Bitmap Image { get; init; }

		public Color Color { get; init; }


		private string GetDebuggerDisplay()
		{
			return GetType().ToString();
		}
	}
}
