using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Models
{
	[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
	sealed record Field : IField
	{
		private readonly Cell[,] cells;
		private bool isPostWrite = false;
		private bool isCellEntityReseted = false;
		private List<PostWriteEntity> postWriteEntities;


		public Field(int weight, int height)
		{
			cells = new Cell[weight, height];

			for(int x = 0; x < weight; x++)
			{
				for(int y = 0; y < height; y++)
				{
					cells[x, y] = new Cell() { Field = this, XPostion = x, YPosition = y };
					cells[x, y].EntityChanged += OnEntityChanged;
				}
			}
		}


		public int Weight => cells.GetLength(0);

		public int Height => cells.GetLength(1);


		public void EnablePostWrite()
		{
			isPostWrite = true;
			postWriteEntities = new List<PostWriteEntity>();
		}

		public void DisablePostWrite()
		{
			isPostWrite = false;
			postWriteEntities = null;
		}

		public void FlushPostWrite()
		{
			isCellEntityReseted = true;

			foreach(var pwe in postWriteEntities)
			{
				cells[pwe.Cell.XPostion, pwe.Cell.YPosition].Entity = pwe.NewEntity;
			}

			isCellEntityReseted = false;
		}

		public Cell GetCellAt(int x, int y)
		{
			return cells[x, y];
		}

		private string GetDebuggerDisplay()
		{
			return GetType().ToString();
		}

		private void OnEntityChanged(Cell cell, ICellEntity oldEntity)
		{
			if(isPostWrite && !isCellEntityReseted)
			{
				isCellEntityReseted = true;

				postWriteEntities.Add(new PostWriteEntity() { Cell = cell, NewEntity = cell.Entity });
				cell.Entity = oldEntity;

				isCellEntityReseted = false;
			}
		}


		record PostWriteEntity
		{
			public Cell Cell { get; init; }

			public ICellEntity NewEntity { get; init; }
		}
	}
}
