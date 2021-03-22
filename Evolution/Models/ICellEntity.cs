namespace Evolution.Models
{
	interface ICellEntity
	{
		Cell Cell { get; }

		DrawingSettings DrawingSettings { get; }


		void InitializeNewCell(Cell cell);
	}
}