using Evolution.TypeHandlers;
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
		public static readonly CreatureType Default = new() {
			DrawingSettingsFactory = new DefaultTypeHandler(),
			CommandHandler = new DefaultTypeHandler(), EventsHandler = new DefaultTypeHandler() /*as EventsHandler*/ };


		public ICreatureDrawingSettingsFactory DrawingSettingsFactory { get; init; }

		public ICreatureCommandHandler CommandHandler { get; init; }

		public ICreaturesEventsHandler EventsHandler { get; init; }


		private string GetDebuggerDisplay()
		{
			return GetType().ToString();
		}
	}
}
