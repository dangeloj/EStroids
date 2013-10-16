namespace EStroids.EntitySystem
{
	public abstract class SystemBase
	{
		private readonly EntityManager _manager;
		protected EntityManager Manager { get { return _manager; } }

		public virtual void Initialize() { }
		public virtual void Shutdown() { }
		public abstract void Frame(double dt);

		protected SystemBase(EntityManager manager)
		{
			_manager = manager;
		}
	}
}
