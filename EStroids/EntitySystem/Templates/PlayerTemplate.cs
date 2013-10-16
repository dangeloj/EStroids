namespace EStroids.EntitySystem.Templates
{
	using EStroids.EntitySystem.Components;

	public static class PlayerTemplate
	{
		public static Model Model;

		public static Entity Create(EntityManager manager)
		{
			var ship = manager.CreateEntity();
			ship.AddComponent(Model);
			ship.AddComponent(new Position() { X = 0, Y = 0, Z = 0 });
			ship.AddComponent(new Weapon() { FireRate = .25 });
			ship.AddComponent(new KeyboardControl());

			return ship;
		}

		static PlayerTemplate()
		{
			var vertices = new[] { new Vector3(0, 1, 0), new Vector3(0.75f, -1, 0), new Vector3(0, -0.5f, 0), new Vector3(-0.75f, -1, 0) };
			var indices = new[] { 0, 1, 2, 3, 0 };
			Model = new Model(vertices, indices, 13);
		}
	}
}