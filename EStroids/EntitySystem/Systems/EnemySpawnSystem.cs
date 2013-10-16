namespace EStroids.EntitySystem.Systems
{
	using System;
	using System.Linq;
	using EStroids.EntitySystem.Components;
	using EStroids.EntitySystem.Templates;

	public class EnemySpawnSystem : SystemBase
	{
		private const double TimeBetweenWaves = 1.5;
		private static readonly Random _rand = new Random(Environment.TickCount);
		private double _timeSinceWaveEnded = 0;
		private int _currentWave = 0;

		public override void Frame(double dt)
		{
			var enemyCount = Manager.GetEntitiesWithComponent<Enemy>().Count();

			if (enemyCount == 0)
			{
				_timeSinceWaveEnded += dt;
			}

			if (_timeSinceWaveEnded > TimeBetweenWaves)
			{
				_timeSinceWaveEnded = 0;
				CreateNextWave(Manager, ++_currentWave);
			}
		}

		public static void CreateNextWave(EntityManager manager, int currentWave)
		{
			var enemies = _rand.Next(currentWave, 5 + currentWave);
			for (int i = 0; i < enemies; ++i)
				EnemyTemplate.Create(manager);
		}

		public EnemySpawnSystem(EntityManager manager)
			: base(manager) { }
	}
}