using Evolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Evolution
{
	abstract class ReflectionCommandHandlerBase<TEnum> : ICreatureCommandHandler where TEnum : struct, Enum
	{
		public void ExecuteCommand(Command command, Creature creature)
		{
			if(PureHandler(command, creature) == true) return;

			var values = Enum.GetValues(typeof(TEnum)).OfType<TEnum>().ToArray();

			var targeValues = values.Where(s => (int)(object)s == command.Type);

			if(targeValues.Any() == false)
			{
				DefaultHandler(command, creature);
			}
			else if(targeValues.Count() >= 2)
				throw new InvalidOperationException($"{(int)(object)targeValues.First()} command presents in enum bigger then 1 times");
			else
			{
				var method = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public)
					.Single(s => s.Name == targeValues.First().ToString() && s.GetCustomAttributes(typeof(ReflectionCommandHandlerAttribute), false).Any());

				method.Invoke(this, new object[] { creature });
			}
		}


		protected virtual bool PureHandler(Command command, Creature creature)
		{
			return false;
		}

		protected virtual void DefaultHandler(Command command, Creature creature)
		{

		}
	}
}
