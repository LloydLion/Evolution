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
				commands[commandToChange] = new Command() { Type = random.Next(0, Command.CommandLimit + 1) };

				return new Genome() { Commands = commands };
			}
			else
			{
				return genome;
			}
		}
	}
}
