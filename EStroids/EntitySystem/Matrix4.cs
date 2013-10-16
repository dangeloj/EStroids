namespace EStroids.EntitySystem
{
	using System;

	public struct Matrix4
	{
		public readonly float M11;
		public readonly float M12;
		public readonly float M13;
		public readonly float M14;

		public readonly float M21;
		public readonly float M22;
		public readonly float M23;
		public readonly float M24;

		public readonly float M31;
		public readonly float M32;
		public readonly float M33;
		public readonly float M34;

		public readonly float M41;
		public readonly float M42;
		public readonly float M43;
		public readonly float M44;

		public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
		{
			Matrix4 result;
			Multiply(ref lhs, ref rhs, out result);
			return result;
		}

		public static void Multiply(ref Matrix4 lhs, ref Matrix4 rhs, out Matrix4 result)
		{
			var items = new[]
			{
				lhs.M11 * rhs.M11 + lhs.M12 * rhs.M21 + lhs.M13 * rhs.M31 + lhs.M14 * rhs.M41,
				lhs.M11 * rhs.M12 + lhs.M12 * rhs.M22 + lhs.M13 * rhs.M32 + lhs.M14 * rhs.M42,
				lhs.M11 * rhs.M13 + lhs.M12 * rhs.M23 + lhs.M13 * rhs.M33 + lhs.M14 * rhs.M43,
				lhs.M11 * rhs.M14 + lhs.M12 * rhs.M24 + lhs.M13 * rhs.M34 + lhs.M14 * rhs.M44,

				lhs.M21 * rhs.M11 + lhs.M22 * rhs.M21 + lhs.M23 * rhs.M31 + lhs.M24 * rhs.M41,
				lhs.M21 * rhs.M12 + lhs.M22 * rhs.M22 + lhs.M23 * rhs.M32 + lhs.M24 * rhs.M42,
				lhs.M21 * rhs.M13 + lhs.M22 * rhs.M23 + lhs.M23 * rhs.M33 + lhs.M24 * rhs.M43,
				lhs.M21 * rhs.M14 + lhs.M22 * rhs.M24 + lhs.M23 * rhs.M34 + lhs.M24 * rhs.M44,
				
				lhs.M31 * rhs.M11 + lhs.M32 * rhs.M21 + lhs.M33 * rhs.M31 + lhs.M34 * rhs.M41,
				lhs.M31 * rhs.M12 + lhs.M32 * rhs.M22 + lhs.M33 * rhs.M32 + lhs.M34 * rhs.M42,
				lhs.M31 * rhs.M13 + lhs.M32 * rhs.M23 + lhs.M33 * rhs.M33 + lhs.M34 * rhs.M43,
				lhs.M31 * rhs.M14 + lhs.M32 * rhs.M24 + lhs.M33 * rhs.M34 + lhs.M34 * rhs.M44,
				
				lhs.M41 * rhs.M11 + lhs.M42 * rhs.M21 + lhs.M43 * rhs.M31 + lhs.M44 * rhs.M41,
				lhs.M41 * rhs.M12 + lhs.M42 * rhs.M22 + lhs.M43 * rhs.M32 + lhs.M44 * rhs.M42,
				lhs.M41 * rhs.M13 + lhs.M42 * rhs.M23 + lhs.M43 * rhs.M33 + lhs.M44 * rhs.M43,
				lhs.M41 * rhs.M14 + lhs.M42 * rhs.M24 + lhs.M43 * rhs.M34 + lhs.M44 * rhs.M44,
			};

			result = new Matrix4(items);
		}

		public static void Multiply(ref Matrix4 lhs, ref Vector3 rhs, out Vector3 result)
		{
			var x = lhs.M11 * rhs.X + lhs.M12 * rhs.Y + lhs.M13 * rhs.Z + lhs.M14;
			var y = lhs.M21 * rhs.X + lhs.M22 * rhs.Y + lhs.M23 * rhs.Z + lhs.M24;
			var z = lhs.M31 * rhs.X + lhs.M32 * rhs.Y + lhs.M33 * rhs.Z + lhs.M34;
			result = new Vector3(x, y, z);
		}

		public static void RotationX(float theta, out Matrix4 result)
		{
			var cos = (float)Math.Cos(theta);
			var sin = (float)Math.Sin(theta);

			result = new Matrix4(new[]
			{
				1, 0, 0, 0,
				0, cos, -sin, 0,
				0, sin, -cos, 0,
				0, 0, 0, 1
			});
		}

		public static void RotationY(float theta, out Matrix4 result)
		{
			var cos = (float)Math.Cos(theta);
			var sin = (float)Math.Sin(theta);

			result = new Matrix4(new[]
			{
				cos, 0, sin, 0,
				0, 1, 0, 0,
				-sin, 0, -cos, 0,
				0, 0, 0, 1
			});
		}

		public static void RotationZ(float theta, out Matrix4 result)
		{
			var cos = (float)Math.Cos(theta);
			var sin = (float)Math.Sin(theta);

			result = new Matrix4(new[]
			{
				cos, -sin, 0, 0,
				sin, cos, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1
			});
		}

		public static void Rotation(float thetaX, float thetaY, float thetaZ, out Matrix4 result)
		{
			Matrix4 xRotation;
			Matrix4 yRotation;
			Matrix4 zRotation;

			RotationX(thetaX, out xRotation);
			RotationY(thetaY, out yRotation);
			RotationZ(thetaZ, out zRotation);

			result = xRotation * yRotation * zRotation;
		}

		public static void Translation(float x, float y, float z, out Matrix4 result)
		{
			result = new Matrix4(new[]
			{
				1, 0, 0, x,
				0, 1, 0, y,
				0, 0, 1, z,
				0, 0, 0, 1
			});
		}

		public static void Translation(Vector3 position, out Matrix4 result)
		{
			Translation(position.X, position.Y, position.Z, out result);
		}

		public static void Scaling(float xScale, float yScale, float zScale, out Matrix4 result)
		{
			result = new Matrix4(new[]
			{
				xScale, 0, 0, 0,
				0, yScale, 0, 0,
				0, 0, zScale, 0,
				0, 0, 0, 1
			});
		}

		public static void Scaling(float scale, out Matrix4 result)
		{
			Scaling(scale, scale, scale, out result);
		}

		public static readonly Matrix4 Identity = new Matrix4(new float[]
		{
			1, 0, 0, 0,
			0, 1, 0, 0,
			0, 0, 1, 0,
			0, 0, 0, 1
		});

		public float this[int row, int column]
		{
			get { return this[row * 4 + column]; }
		}

		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return M11;
					case 1: return M12;
					case 2: return M13;
					case 3: return M14;

					case 4: return M21;
					case 5: return M22;
					case 6: return M23;
					case 7: return M24;

					case 8: return M31;
					case 9: return M32;
					case 10: return M33;
					case 11: return M34;

					case 12: return M41;
					case 13: return M42;
					case 14: return M43;
					case 15: return M44;
				}

				throw new ArgumentOutOfRangeException("index");
			}
		}

		public Matrix4(float m11, float m12, float m13, float m14,
					   float m21, float m22, float m23, float m24,
					   float m31, float m32, float m33, float m34,
					   float m41, float m42, float m43, float m44)
		{
			M11 = m11;
			M12 = m12;
			M13 = m13;
			M14 = m14;

			M21 = m21;
			M22 = m22;
			M23 = m23;
			M24 = m24;

			M31 = m31;
			M32 = m32;
			M33 = m33;
			M34 = m34;

			M41 = m41;
			M42 = m42;
			M43 = m43;
			M44 = m44;
		}

		public Matrix4(float[] values)
		{
			M11 = values[0];
			M12 = values[1];
			M13 = values[2];
			M14 = values[3];

			M21 = values[4];
			M22 = values[5];
			M23 = values[6];
			M24 = values[7];

			M31 = values[8];
			M32 = values[9];
			M33 = values[10];
			M34 = values[11];

			M41 = values[12];
			M42 = values[13];
			M43 = values[14];
			M44 = values[15];
		}
	}
}