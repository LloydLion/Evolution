using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Models
{
	delegate void CreatureMustDieHandler(string reason);


	[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
	sealed record Creature : ICellEntity, IEateble
	{
		public const int DuplicateCost = 200;
		public const int AutoDuplicatePoint = 1000;
		public const int StepCost = 3;


		private bool isDie;
		private Cell cell;


		public Creature()
		{
			Energy = new EnergyStats() { CreatureMustDieHandler = CreatureMustDieHandler };
			Energy.PrimaryEnergyChanged += Energy_PrimaryEnergyChanged;
		}


		public Genome Genome { get; init; }

		public Cell Cell => cell;

		public CreatureType Type { get; init; }

		public DrawingSettings DrawingSettings => Type.DrawingSettingsFactory.CreateSettings(this, FieldViewMode.Normal);

		public EnergyStats Energy { get; }

		public bool IsDie => isDie;

		int IEateble.EatPrimaryEnegry => Energy.PrimaryEnergy / 2;


		void ICellEntity.InitializeNewCell(Cell cell)
		{
			this.cell = cell;
		}

		private void CreatureMustDieHandler(string reason) => Kill();

		void IEateble.Destroy() => Kill();

		public void Update()
		{
			Type.EventsHandler.GameUpdate(this);	
		}

		public void Kill()
		{
			Cell.Entity = null;
			isDie = true;
		}

		private void Energy_PrimaryEnergyChanged(EnergyStats sender, int oldValue)
		{
			Type.EventsHandler.CreatureEnergyChanged(this, oldValue);
		}

		private string GetDebuggerDisplay()
		{
			return GetType().ToString();
		}
	}
}
