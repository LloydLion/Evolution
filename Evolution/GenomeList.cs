using Evolution.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution
{
	class GenomeList : IReadOnlyList<Command>
	{
		private readonly List<Command> innerList;


		public GenomeList(List<Command> commands)
		{
			innerList = commands.ToList(); //Copy list
		}


		public Command this[int index] { get => innerList[index % Count]; set => innerList[index % Count] = value; }


		public int Count => innerList.Count;


		public bool Contains(Command item)
		{
			return innerList.Contains(item);
		}

		public void CopyTo(Command[] array, int arrayIndex)
		{
			innerList.CopyTo(array, arrayIndex);
		}

		public IEnumerator<Command> GetEnumerator()
		{
			return innerList.GetEnumerator();
		}

		public int IndexOf(Command item)
		{
			return innerList.IndexOf(item);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)innerList).GetEnumerator();
		}
	}
}
