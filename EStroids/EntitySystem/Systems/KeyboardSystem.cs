namespace EStroids.EntitySystem.Systems
{
	using System;
	using System.Collections.Generic;
	using System.Windows.Forms;
	using EStroids.EntitySystem.Components;
	using EStroids.EntitySystem.Templates;

	public class KeyboardSystem : SystemBase
	{
		private const double Delay = 1 / 4;

		private readonly Form _form;
		private readonly Keyboard _keyboard;

		public override void Frame(double dt)
		{
			var shooting = new List<Entity>();

			foreach (var entity in Manager.GetEntitiesWithComponent<KeyboardControl>())
			{
				var moveable = entity.GetComponent<Position>();
				var keyboard = entity.GetComponent<KeyboardControl>();

				if (moveable == null || keyboard == null) continue;

				Rotate(dt, moveable);
				if (IsShooting(dt, entity)) shooting.Add(entity);
			}

			shooting.ForEach(BuildBullet);
		}

		private void Rotate(double dt, Position moveable)
		{
			var left = _keyboard.IsKeyDown(Keys.Left);
			var right = _keyboard.IsKeyDown(Keys.Right);

			var zRotMult = _keyboard.IsKeyDown(Keys.Left) ? -1 :
						   _keyboard.IsKeyDown(Keys.Right) ? 1 :
						   0;

			moveable.ZRot += (float)(zRotMult * Math.PI * dt);
		}

		private bool IsShooting(double dt, Entity entity)
		{
			var shot = entity.GetComponent<Weapon>();
			if (shot == null) return false;

			shot.TimeSinceLastFire += dt;
			if (shot.TimeSinceLastFire < shot.FireRate || _keyboard.IsKeyDown(Keys.Space) == false)
				return false;

			shot.TimeSinceLastFire = 0;

			return true;
		}

		private void BuildBullet(Entity entity)
		{
			var position = entity.GetComponent<Position>();
			if (position == null) return;

			BulletTemplate.Create(Manager, position);
		}

		public KeyboardSystem(EntityManager manager, Form form, Keyboard keyboard)
			: base(manager)
		{
			_form = form;
			_keyboard = keyboard;
		}
	}
}