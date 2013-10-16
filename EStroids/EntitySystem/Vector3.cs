namespace EStroids.EntitySystem
{
	using System;

	public struct Vector3
	{
		public readonly float X, Y, Z;

		public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.X + rhs.X, rhs.Y + lhs.Y, rhs.Z + lhs.Z);
		}

		public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.X - rhs.X, rhs.Y - lhs.Y, rhs.Z - lhs.Z);
		}

		public static Vector3 operator -(Vector3 vector)
		{
			return new Vector3(-vector.X, -vector.Y, -vector.Z);
		}

		public static Vector3 operator *(Vector3 vector, float scalar)
		{
			return new Vector3(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
		}

		public static Vector3 operator /(Vector3 vector, float scalar)
		{
			return new Vector3(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);
		}

		public static float Dot(Vector3 lhs, Vector3 rhs)
		{
			return lhs.X * rhs.X +
				   lhs.Y * rhs.Y +
				   lhs.Z * rhs.Z;
		}

		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.Y * rhs.Z - lhs.Z * rhs.Y,
							   lhs.Z * rhs.X - lhs.X * rhs.Z,
							   lhs.X * rhs.Y - lhs.Y * rhs.X);
		}

		public Vector3 Cross(Vector3 rhs)
		{
			return Cross(this, rhs);
		}

		public float Dot(Vector3 rhs)
		{
			return Dot(this, rhs);
		}

		public override string ToString()
		{
			return string.Format("<{0},{1},{2}>", X, Y, Z);
		}

		public Vector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}
	}
}