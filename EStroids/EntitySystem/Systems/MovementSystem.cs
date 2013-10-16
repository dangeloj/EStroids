namespace EStroids.EntitySystem.Systems
{
	using EStroids.EntitySystem.Components;

	public class MovementSystem : SystemBase
	{
		public override void Frame(double dt)
		{
			foreach (var entity in Manager.GetEntitiesWithComponent<Position>())
			{
				var position = entity.GetComponent<Position>();
				var velocity = position.Velocity;

				position.X += (float)(velocity.X * dt);
				position.Y += (float)(velocity.Y * dt);
				position.Z += (float)(velocity.Z * dt);
			}
		}

		public MovementSystem(EntityManager manager)
			: base(manager) { }
	}
}