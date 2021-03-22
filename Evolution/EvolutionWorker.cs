using Evolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution
{
	sealed class EvolutionWorker
	{
		private Field field;


		public IField Field => field;


		public EvolutionWorker(int weight, int height)
		{
			field = new Field(weight, height);
		}


		public void Setup()
		{
			var commands = new byte[64].Select(s => new Command() { Type = 0 }).ToList();

			field.GetCellAt(0, 0).Entity = new Creature() { Genome = new Genome() { Commands = commands }, Type = CreatureType.Default };
			field.GetCellAt(1, 0).Entity = new Creature() { Genome = new Genome() { Commands = commands }, Type = CreatureType.Default };
			field.GetCellAt(2, 0).Entity = new Creature() { Genome = new Genome() { Commands = commands }, Type = CreatureType.Default };
			field.GetCellAt(3, 0).Entity = new Creature() { Genome = new Genome() { Commands = commands }, Type = CreatureType.Default };
			field.GetCellAt(4, 0).Entity = new Creature() { Genome = new Genome() { Commands = commands }, Type = CreatureType.Default };
		}

		public void Update()
		{
			field.EnablePostWrite();

			for(int x = 0; x < field.Weight; x++)
			{
				for(int y = 0; y < field.Height; y++)
				{
					var entity = field.GetCellAt(x, y).Entity;
					if(entity is not Creature creature) continue;

					UpdateCreature(creature);
				}
			}

			field.FlushPostWrite();
			field.DisablePostWrite();
		}

		private void UpdateCreature(Creature creature)
		{
			var pointer = creature.Genome.CurrentPointer;

			var command = creature.Genome.Commands[pointer];
			creature.Type.CommandHandler.ExecuteCommand(command, field, creature);

			if(pointer + 1 == creature.Genome.Commands.Count) creature.Genome.CurrentPointer = 0;
			else creature.Genome.CurrentPointer++;
		}
	}
}
