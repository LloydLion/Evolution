using Evolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.CommandHandlers
{
	class DefaultCommandHandler : ReflectionCommandHandlerBase<DefaultCommandHandler.Commands>
	{
		public enum Commands
		{
			MoveRight = 0
		}


		[ReflectionCommandHandler]
		private void MoveRight(Command command, IField field, Creature creature)
		{
			if(field.GetCellAt(creature.Cell.XPostion + 1, creature.Cell.YPosition).Entity == null)
			{
				creature.Cell.Entity = null;
				field.GetCellAt(creature.Cell.XPostion + 1, creature.Cell.YPosition).Entity = creature;
			}
		}
	}
}
