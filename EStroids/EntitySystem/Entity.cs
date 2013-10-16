namespace EStroids.EntitySystem
{
	using System;

	public struct Entity : IEquatable<Entity>, IComparable<Entity>
	{
		private readonly Guid _id;
		private readonly EntityManager _manager;

		public T AddComponent<T>(T component)
		{
			return _manager.AddComponent<T>(this, component);
		}

		public void RemoveComponent<T>(T component)
		{
			_manager.RemoveComponent<T>(this, component);
		}

		public T GetComponent<T>()
		{
			return _manager.GetComponent<T>(this);
		}

		public int CompareTo(Entity other)
		{
			return _id.CompareTo(other._id);
		}

		public bool Equals(Entity other)
		{
			if (ReferenceEquals(null, other)) return false;
			return ReferenceEquals(this, other) || _id.Equals(other._id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return ReferenceEquals(this, obj) ||
				obj is Entity ? Equals((Entity)obj) : false;
		}

		public override int GetHashCode()
		{
			return _id.GetHashCode();
		}

		public override string ToString()
		{
			return "Entity/" + _id;
		}

		public static implicit operator Guid(Entity rhs)
		{
			return rhs._id;
		}

		public Entity(Guid id, EntityManager manager)
		{
			_id = id;
			_manager = manager;
		}
	}
}