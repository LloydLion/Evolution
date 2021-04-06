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
			Eat = 3,
			WhatMyEnergy
		}


		protected override void DefaultHandler(Command command, Creature creature)
		{
			creature.Genome.CurrentPointer += command.Type;
		}

		[ReflectionCommandHandler]
		public void Move(Creature creature)
		{
			var field = creature.Cell.Field;
			(int targetX, int targetY) =
				SwitchDirectionWithStandardAnotation(creature.Genome.Commands[creature.Genome.CurrentPointer + 1].Type);

			int commandOffest;

			if (creature.MoveCreature(targetX, targetY) == true) commandOffest = 1;
			else
			{
				var cell = field.TryGetCreatureCellWithOffest(creature, targetX, targetY);

				if (cell == null) commandOffest = 2;
				else
					if (cell.Entity is Creature creature2)
						if (creature.IsFimily(creature2)) commandOffest = 3;
						else commandOffest = 4;
					else commandOffest = 5;
			}

			creature.Genome.CurrentPointer += creature.Genome.Commands[creature.Genome.CurrentPointer + commandOffest + 1].Type;
		}

		[ReflectionCommandHandler]
		public void WhatMyEnergy(Creature creature)
		{
			var pe = creature.Energy.PrimaryEnergy;
			var te = 15 * creature.Genome.GetCommandWithOffest(1).Type;

			if(pe >= te) creature.Genome.CurrentPointer += 2;
			else creature.Genome.CurrentPointer += 3;
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

			var dir = SwitchDirectionWithStandardAnotation(creature.Genome.Commands[creature.Genome.CurrentPointer + 1].Type);
			entity = field.TryGetCreatureCellWithOffest(creature, dir.xOffest, dir.yOffest)?.Entity;

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

		//Static
		(int xOffest, int yOffest) SwitchDirectionWithStandardAnotation(int switchBase)
		{
			switch(switchBase % 4)
			{
				case 0: return (+1, 0);
				case 1: return (-1, 0);
				case 2: return (0, +1);
				case 3: return (0, -1);

				default: throw new Exception("It is imposible!");
			}
		}
	}
}
