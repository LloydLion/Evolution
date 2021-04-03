using Evolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution
{
	interface ICreaturesEventsHandler
	{
		void CreatureDie(Creature creature)
		{

		}

		void CreatureEnergyChanged(Creature creature, int oldValue)
		{

		}

		void GameUpdate(Creature creature)
		{

		}
	}
}
