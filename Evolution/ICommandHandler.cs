using Evolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution
{
	interface ICommandHandler
	{
		void ExecuteCommand(Command command, IField field, Creature creature);
	}
}
