using Evolution.CommandHandlers;
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
	sealed record CreatureType
	{
		public static readonly CreatureType Default = new() { DrawingSettings = new() { Color = Color.Aqua }, CommandHandler = new DefaultCommandHandler() };


		public DrawingSettings DrawingSettings { get; init; }

		public ICommandHandler CommandHandler { get; init; }


		private string GetDebuggerDisplay()
		{
			return GetType().ToString();
		}
	}
}
