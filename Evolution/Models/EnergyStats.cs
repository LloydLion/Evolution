using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Models
{
	delegate void PrimaryEnergyChangedHandler(EnergyStats sender, int oldValue);


	[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
	sealed record EnergyStats
	{
		private int primaryEnergy;
		private CreatureMustDieHandler creatureMustDie;


		public int PrimaryEnergy { get => primaryEnergy; }

		public CreatureMustDieHandler CreatureMustDieHandler { init => creatureMustDie = value; }

		private int EventedPrimaryEnergy { get => primaryEnergy; set
			{ var old = primaryEnergy; primaryEnergy = value; PrimaryEnergyChanged?.Invoke(this, old); } }


		public event PrimaryEnergyChangedHandler PrimaryEnergyChanged;


		public void AddPrimaryEnergy(int energy)
		{
			EventedPrimaryEnergy += energy;
			if(EventedPrimaryEnergy < 0)
			{
				creatureMustDie?.Invoke("Energy end");
			}
		}

		public void RemovePrimaryEnergy(int energy) => AddPrimaryEnergy(-energy);

		private string GetDebuggerDisplay()
		{
			return GetType().ToString();
		}
	}
}
