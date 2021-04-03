using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Models
{
	interface IEateble : ICellEntity
	{
		int EatPrimaryEnegry { get; }


		void Destroy();
	}
}
