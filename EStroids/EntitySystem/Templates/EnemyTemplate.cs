namespace EStroids.EntitySystem.Templates
{
	using System;
	using System.Linq;
	using EStroids.EntitySystem.Components;

	public class EnemyTemplate
	{
		private static readonly Model EnemyModel;
		private static readonly Random Rand = new Random(Environment.TickCount);

		public static Entity Create(EntityManager manager)
		{
			var enemy = manager.CreateEntity();
			var xMult = Rand.Next() % 2 == 0 ? -1 : 1;
			var yMult = Rand.Next() % 2 == 0 ? -1 : 1;
			var velocity = new Vector3(Rand.Next(10, 75) * xMult, Rand.Next(10, 75) * yMult, 0);

			enemy.AddComponent(new Enemy());
			enemy.AddComponent(EnemyModel);
			enemy.AddComponent(new Position() { X = Rand.Next(-300, 300), Y = Rand.Next(-300, 300), ZRot = (float)(Math.PI * Rand.NextDouble() * Rand.Next(0, 4)), Velocity = velocity });

			return enemy;
		}

		static EnemyTemplate()
		{
			var vertices = new[] {
				new Vector3(0.03125f, 0.9f, 0), new Vector3(0.34375f, 1, 0), new Vector3(0.84375f, 0.65265f, 0),
				new Vector3(1, 0, 0), new Vector3(0.78125f, -0.71875f, 0), new Vector3(0.25f, -1, 0),
				new Vector3(-0.3125f, -0.84375f, 0), new Vector3(-1, -0.65625f, 0), new Vector3(-0.84375f, 0.5f, 0),
				new Vector3(-0.9375f, 0.625f, 0), new Vector3(-0.75f, 0.8125f, 0), new Vector3(-0.5625f, 1, 0)
			};

			EnemyModel = new Model(vertices, Enumerable.Range(0, vertices.Length), 32);
		}
	}
}