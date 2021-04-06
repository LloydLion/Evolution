using Evolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Helpers
{
	static class GenomeHelper
	{
		private static readonly Random random;


		static GenomeHelper()
		{
			random = new Random();
		}


		public static Genome CreateNextGeneration(this Creature creature)
		{
			var genome = creature.Genome;
			var changeGenome = random.Next(0, 2) == 0;

			if(changeGenome == true)
			{
				var commands = new List<Command>(genome.Commands);
				var commandToChange = random.Next(0, commands.Count + 1 - 1);
				commands[commandToChange] = new Command() { Type = random.Next(0, Command.CommandLimit) };

				return new Genome() { Commands = new GenomeList(commands) };
			}
			else
			{
				return genome;
			}
		}

		public static bool IsFimily(this Creature creature, Creature secondCreature)
		{
			var gens1 = creature.Genome.Commands.ToArray();
			var gens2 = secondCreature.Genome.Commands.ToArray();

			var diff = 0;
			for(int i = 0; i < gens1.Length; i++)
				if(gens1[i].Type != gens2[i].Type) diff++;

			return diff <= 1;
		}

		public static Command GetCommandWithOffest(this Genome genome, int offest)
		{
			return genome.Commands[genome.CurrentPointer + offest];
		}
	}
}
