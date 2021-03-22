using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Models
{
	delegate void EntityChangedHandler(Cell cell, ICellEntity oldValue);


	[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
	sealed record Cell
	{
		private ICellEntity entity;


		public ICellEntity Entity { get => entity; 
			set { var oe = entity; value?.InitializeNewCell(this); entity = value; EntityChanged?.Invoke(this, oe); } }

		public int XPostion { get; init; }

		public int YPosition { get; init; }

		public IField Field { get; init; }


		private string GetDebuggerDisplay()
		{
			return GetType().ToString();
		}


		public event EntityChangedHandler EntityChanged;
	}
}
