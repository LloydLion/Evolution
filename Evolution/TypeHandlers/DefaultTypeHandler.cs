using Evolution.Helpers;
using Evolution.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.TypeHandlers
{
	class DefaultTypeHandler : ReflectionCommandHandlerBase<DefaultTypeHandler.Commands>, ICreaturesEventsHandler, ICreatureDrawingSettingsFactory
	{
		public enum Commands
		{
			Photo = 0,
			Move = 1,
			Duplicate = 2,
			Eat = 3
		}


		[ReflectionCommandHandler]
		public void Move(Creature creature)
		{
			if(creature.Genome.Commands.Count - 1 < creature.Genome.CurrentPointer + 1) return;

			switch(creature.Genome.Commands[creature.Genome.CurrentPointer + 1].Type % 4)
			{
				case 0:
					creature.MoveCreature(1, 0);
				break;

				case 1:
					creature.MoveCreature(-1, 0);
				break;

				case 2:
					creature.MoveCreature(0, 1);
				break;

				case 3:
					creature.MoveCreature(0, -1);
				break;
			}
		}

		[ReflectionCommandHandler]
		public void Photo(Creature creature)
		{
			creature.Energy.AddPrimaryEnergy(creature.CalculatePhoto());
		}

		[ReflectionCommandHandler]
		public void Duplicate(Creature creature)
		{
			var field = creature.Cell.Field;
			var newGemone = creature.CreateNextGeneration();

			var newCreature = new Creature() { Genome = newGemone, Type = creature.Type };
			newCreature.Energy.AddPrimaryEnergy(Creature.DuplicateCost / 2);

			if(field.IsValidCreatureRelativeCords(creature, 0, 1) &&
				field.GetCreatureCellWithOffest(creature, 0, 1).IsEmpty())
				field.GetCreatureCellWithOffest(creature, 0, 1).Entity = newCreature;
			else if(field.IsValidCreatureRelativeCords(creature, 1, 0) &&
				field.GetCreatureCellWithOffest(creature, 1, 0).IsEmpty())
				field.GetCreatureCellWithOffest(creature, 1, 0).Entity = newCreature;
			else if(field.IsValidCreatureRelativeCords(creature, 0, -1) &&
				field.GetCreatureCellWithOffest(creature, 0, -1).IsEmpty())
				field.GetCreatureCellWithOffest(creature, 0, -1).Entity = newCreature;
			else if(field.IsValidCreatureRelativeCords(creature, -1, 0) &&
				field.GetCreatureCellWithOffest(creature, -1, 0).IsEmpty())
				field.GetCreatureCellWithOffest(creature, -1, 0).Entity = newCreature;
			else creature.Kill();

			creature.Energy.RemovePrimaryEnergy(Creature.DuplicateCost);
		}

		[ReflectionCommandHandler]
		public void Eat(Creature creature)
		{
			if (creature.Genome.Commands.Count - 1 < creature.Genome.CurrentPointer + 1) return;

			var field = creature.Cell.Field;
			ICellEntity entity = null;

			switch (creature.Genome.Commands[creature.Genome.CurrentPointer + 1].Type % 4)
			{
				case 0:
					entity = field.TryGetCreatureCellWithOffest(creature, 1, 0)?.Entity;
					break;

				case 1:
					entity = field.TryGetCreatureCellWithOffest(creature, -1, 0)?.Entity;
					break;

				case 2:
					entity = field.TryGetCreatureCellWithOffest(creature, 0, 1)?.Entity;
					break;

				case 3:
					entity = field.TryGetCreatureCellWithOffest(creature, 0, -1)?.Entity;
					break;
			}

			if(entity != null && entity is IEateble eateble)
			{
				eateble.Destroy();
				creature.Energy.AddPrimaryEnergy(eateble.EatPrimaryEnegry);
			}
		}

		//Events
		void ICreaturesEventsHandler.CreatureEnergyChanged(Creature creature, int oldValue)
		{
			if(creature.Energy.PrimaryEnergy >= Creature.AutoDuplicatePoint) Duplicate(creature);
		}

		void ICreaturesEventsHandler.GameUpdate(Creature creature)
		{
			creature.Energy.RemovePrimaryEnergy(Creature.StepCost);
		}

		//DrawingSettings
		DrawingSettings ICreatureDrawingSettingsFactory.CreateSettings(Creature creature, FieldViewMode viewMode)
		{
			switch (viewMode)
			{
				case FieldViewMode.Normal:
					var energy = creature.Energy.PrimaryEnergy;
					Color color;

					if (energy >= Creature.AutoDuplicatePoint - 10) color = Color.Green;
					else if (energy >= Creature.DuplicateCost) color = Color.Yellow;
					else color = Color.Red;

					return new DrawingSettings() { Color = color };
				default:
					return new DrawingSettings() { Color = Color.Aqua };
			}
		}
	}
}
