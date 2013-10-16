namespace EStroids.EntitySystem
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public sealed class EntityManager
	{
		private readonly HashSet<Guid> _entities = new HashSet<Guid>();
		private readonly Dictionary<Guid, List<object>> _entityComponents = new Dictionary<Guid, List<object>>();

		public Entity CreateEntity()
		{
			return CreateEntity(Guid.NewGuid(), this);
		}

		public Entity CreateEntity(Guid id)
		{
			return CreateEntity(id, this);
		}

		private static Entity CreateEntity(Guid id, EntityManager manager)
		{
			if (manager._entityComponents.ContainsKey(id) == false)
			{
				manager._entityComponents[id] = new List<object>();
			}

			manager._entities.Add(id);
			return new Entity(id, manager);
		}

		public T AddComponent<T>(Entity entity, T component)
		{
			_entityComponents[entity].Add(component);
			return component;
		}

		public void RemoveComponent<T>(Entity entity, T component)
		{
			_entityComponents[entity].Remove(component);
		}

		public IEnumerable<Entity> GetEntitiesWithComponent<T>()
		{
			return _entityComponents.Where(x => x.Value.OfType<T>().Any())
									.Select(x => CreateEntity(x.Key, this));
		}

		public T GetComponent<T>(Entity entity)
		{
			return _entityComponents[entity].OfType<T>().FirstOrDefault();
		}

		public bool DestroyEntity(Entity entity)
		{
			return _entityComponents.Remove(entity) && _entities.Remove(entity);
		}
	}
}