using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Models
{
	[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
	sealed record Creature : ICellEntity
	{
		private Cell cell;


		public Genome Genome { get; init; }

		public Cell Cell => cell;

		public CreatureType Type { get; init; }

		public DrawingSettings DrawingSettings => Type.DrawingSettings;


		void ICellEntity.InitializeNewCell(Cell cell)
		{
			this.cell = cell;
		}

		private string GetDebuggerDisplay()
		{
			return GetType().ToString();
		}
	}
}
