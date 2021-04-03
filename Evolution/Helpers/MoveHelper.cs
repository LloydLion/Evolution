using Evolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Helpers
{
	static class MoveHelper
	{
		public static bool MoveCreature(this Creature creature, int xTranslate, int yTranslate)
		{
			var field = creature.Cell.Field;
			var ret = false;

			moveHorizontal(xTranslate);
			moveVertical(yTranslate);

			return ret;



			void moveHorizontal(int step)
			{
				if(field.IsValidCords(creature.Cell.XPostion + step, creature.Cell.YPosition) &&
					!field.IsEmpty(creature.Cell.XPostion + step, creature.Cell.YPosition))
				{
					creature.Cell.Entity = null;
					field.GetCreatureCellWithOffest(creature, step, 0).Entity = creature;
					ret = true;
				}
			}

			void moveVertical(int step)
			{
				if(field.IsValidCords(creature.Cell.XPostion, creature.Cell.YPosition + step) &&
					field.GetCellAt(creature.Cell.XPostion, creature.Cell.YPosition + step).Entity == null)
				{
					creature.Cell.Entity = null;
					field.GetCreatureCellWithOffest(creature, 0, step).Entity = creature;
					ret = true;
				}
			}
		}
	}
}
