using Evolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Helpers
{
	static class EnergyHelper
	{
		public static int CalculatePhoto(this Creature creature)
		{
			var field = creature.Cell.Field;
			var depth = caculateDepth();
			var height = 5 - depth < 0 ? 0 : 5 - depth;
			var sun = caculateSun();

			return height * sun;



			int caculateDepth()
			{
				var depthLevel = 0;

				for (int y = creature.Cell.YPosition /*+1 for next creature, but need -1 for index*/; y < field.Height; y++)
				{
					var entity = field.GetCellAt(creature.Cell.XPostion, y).Entity;
					if(entity != null) depthLevel += 1;
					if(entity == null) depthLevel -= 2;
				}

				depthLevel = depthLevel < 0 ? 0 : depthLevel;

				return depthLevel;
			}

			int caculateSun()
			{
				var levelSize = field.Height * 0.25f; // 20% for level
				var creatureELevel = (int)(creature.Cell.YPosition / levelSize);
				if (creatureELevel > 0) creatureELevel -= 1;

				//creatureELevel *= 2;

				return creatureELevel;
			}
		}
	}
}
