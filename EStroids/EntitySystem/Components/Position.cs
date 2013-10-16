namespace EStroids.EntitySystem.Components
{
	public class Position
	{
		public Vector3 Velocity { get; set; }

		public float XRot { get; set; }
		public float YRot { get; set; }
		public float ZRot { get; set; }

		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }

		public override string ToString()
		{
			return string.Format("<{0},{1},{2}>", X, Y, Z);
		}
	}
}