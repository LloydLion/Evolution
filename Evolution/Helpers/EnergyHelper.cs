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
				return 4;
			}

			int caculateSun()
			{
				var level = (field.Height - creature.Cell.YPosition) / 10;
				var unRes = 20 - level;

				return unRes < 0 ? 0 : unRes;
			}
		}
	}
}
