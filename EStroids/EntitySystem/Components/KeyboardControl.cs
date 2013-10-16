namespace EStroids.EntitySystem.Components
{
	public class KeyboardControl
	{
		public float RadsPerSecX { get; set; }
		public float RadsPerSecY { get; set; }
		public float RadsPerSecZ { get; set; }

		public Vector3 Direction { get; set; }
	}
}