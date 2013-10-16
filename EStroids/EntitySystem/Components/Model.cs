namespace EStroids.EntitySystem.Components
{
	using System.Collections.Generic;

	public sealed class Model
	{
		public IEnumerable<Vector3> Vertices { get; private set; }
		public IEnumerable<int> Indices { get; private set; }

		public float Scale { get; private set; }

		public Model(IEnumerable<Vector3> vertices, IEnumerable<int> indices, float scale)
		{
			Vertices = new List<Vector3>(vertices);
			Indices = new List<int>(indices);
			Scale = scale;
		}
	}
}