#nullable enable

using Evolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Helpers
{
	static class FieldHelper
	{
		public static bool IsEmpty(this IField field, int x, int y)
		{
			return field.GetCellAt(x, y).Entity != null;
		}

		public static bool IsEmpty(this Cell cell)
		{
			return cell.Entity == null;
		}

		public static bool IsValidCords(this IField field, int x, int y)
		{
			return isValid(x, field.Weight) && isValid(y, field.Height);

			static bool isValid(int check, int max)
			{
				return check >= 0 && check < max;
			}
		}

		public static bool IsValidCreatureRelativeCords(this IField field, Creature creature, int xOffest, int yOffest)
		{
			return field.IsValidCords(creature.Cell.XPostion + xOffest, creature.Cell.YPosition + yOffest);
		}

		public static Cell GetCreatureCellWithOffest(this IField field, Creature creature, int xOffest, int yOffest)
		{
			return field.GetCellAt(creature.Cell.XPostion + xOffest, creature.Cell.YPosition + yOffest);
		}

		public static Cell? TryGetCreatureCellWithOffest(this IField field, Creature creature, int xOffest, int yOffest)
		{
			if(!field.IsValidCords(creature.Cell.XPostion + xOffest, creature.Cell.YPosition + yOffest)) return null;
			return field.GetCellAt(creature.Cell.XPostion + xOffest, creature.Cell.YPosition + yOffest);
		}
	}
}
