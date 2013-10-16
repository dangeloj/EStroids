namespace EStroids.EntitySystem.Systems
{
	using System.Collections.Generic;
	using EStroids.EntitySystem.Components;

	public class LifetimeSystem : SystemBase
	{
		public override void Frame(double dt)
		{
			var toDestroy = new List<Entity>();
			foreach (var entity in Manager.GetEntitiesWithComponent<Expiration>())
			{
				var expiration = entity.GetComponent<Expiration>();
				expiration.TimeAlive += dt;
				if (expiration.TimeAlive < expiration.MaxLifetime) continue;

				toDestroy.Add(entity);
			}

			toDestroy.ForEach(x => Manager.DestroyEntity(x));
		}

		public LifetimeSystem(EntityManager manager)
			: base(manager) { }
	}
}