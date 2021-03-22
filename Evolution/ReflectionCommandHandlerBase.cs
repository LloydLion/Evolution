using Evolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Evolution
{
	abstract class ReflectionCommandHandlerBase<TEnum> : ICommandHandler where TEnum : struct, Enum
	{
		public void ExecuteCommand(Command command, IField field, Creature creature)
		{
			if(PureHandler(command, field, creature) == true) return;

			var values = Enum.GetValues(typeof(TEnum)).OfType<TEnum>().ToArray();

			var targeValues = values.Where(s => (int)(object)s == command.Type);

			if(targeValues.Any() == false) return;
			else if(targeValues.Count() >= 2)
				throw new InvalidOperationException($"{(int)(object)targeValues.First()} command presents in enum bigger then 1 times");
			else
			{
				var method = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
					.Single(s => s.Name == targeValues.First().ToString() && s.GetCustomAttributes(typeof(ReflectionCommandHandlerAttribute), false).Any());

				method.Invoke(this, new object[] { command, field, creature });
			}
		}


		protected virtual bool PureHandler(Command command, IField fieid, Creature creature)
		{
			return false;
		}
	}
}
