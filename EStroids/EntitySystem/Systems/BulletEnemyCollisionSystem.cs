namespace EStroids.EntitySystem.Systems
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using EStroids.EntitySystem.Components;

	public class BulletEnemyCollisionSystem : SystemBase
	{
		public override void Frame(double dt)
		{
			var enemies = Manager.GetEntitiesWithComponent<Enemy>().ToList();
			var bullets = Manager.GetEntitiesWithComponent<Bullet>().ToList();
			var collisions = new List<Tuple<Entity, Entity>>();

			foreach (var bullet in bullets)
			{
				foreach (var enemy in enemies)
				{
					if (Collision(bullet.GetComponent<Position>(), enemy.GetComponent<Position>()))
					{
						collisions.Add(Tuple.Create(bullet, enemy));
						break;
					}
				}
			}

			var deadbullets = collisions.Select(x => x.Item1).Distinct();
			var deadEnemies = collisions.Select(x => x.Item2).Distinct();

			foreach (var deadBullet in deadbullets)
			{
				Manager.DestroyEntity(deadBullet);
			}

			foreach (var deadEnemy in deadEnemies)
			{
				Manager.DestroyEntity(deadEnemy);
			}
		}

		private static bool Collision(Position bullet, Position enemy)
		{
			if (bullet == null || enemy == null) return false;

			var bulletBounding = new RectangleF(new PointF(bullet.X, bullet.Y), new SizeF(1.5f, 1.5f));
			var enemyBounding = new RectangleF(new PointF(enemy.X - 32, enemy.Y - 32), new SizeF(64, 64));

			return bulletBounding.IntersectsWith(enemyBounding);
		}

		public BulletEnemyCollisionSystem(EntityManager manager)
			: base(manager) { }
	}
}