namespace EStroids.EntitySystem.Templates
{
	using EStroids.EntitySystem.Components;

	public static class BulletTemplate
	{
		public static Entity Create(EntityManager manager, Position position)
		{
			var bullet = manager.CreateEntity();
			Vector3 velocity = new Vector3(0, 600, 0);
			Matrix4 rotation;
			Matrix4.Rotation(position.XRot, position.YRot, position.ZRot, out rotation);
			Matrix4.Multiply(ref rotation, ref velocity, out velocity);
			bullet.AddComponent(new Model(new[] {
				new Vector3(-1, 1, 0), new Vector3(1, 1, 0), new Vector3(1, -1, 0), new Vector3(-1, -1, 0)
			}, new[] {
				0, 1, 2, 3
			}, 1.5f));
			bullet.AddComponent(new Expiration() { MaxLifetime = 1 });
			bullet.AddComponent(new Position()
			{
				X = position.X,
				Y = position.Y,
				Z = position.Z,
				Velocity = velocity
			});
			bullet.AddComponent(new Bullet() { Damage = 0.5f });

			return bullet;
		}
	}
}