using Evolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Models
{
	interface IField
	{
		int Weight { get; }

		int Height { get; }


		Cell GetCellAt(int x, int y);
	}
}
