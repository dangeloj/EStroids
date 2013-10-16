namespace EStroids.EntitySystem.Systems
{
	using EStroids.EntitySystem.Components;

	public class FieldOfPlaySystem : SystemBase
	{
		public override void Frame(double dt)
		{
			foreach (var entity in Manager.GetEntitiesWithComponent<Position>())
			{
				var position = entity.GetComponent<Position>();
				var velocity = position.Velocity;

				if (position.X < -400 && velocity.X <= 0 || position.X > 400 && velocity.X >= 0)
				{
					position.X *= -1;
				}

				if (position.Y < -400 && velocity.Y <= 0 || position.Y > 400 && velocity.Y >= 0)
				{
					position.Y *= -1;
				}
			}
		}

		public FieldOfPlaySystem(EntityManager manager)
			: base(manager) { }
	}
}