using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareRenderer
{
    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float Length
        {
            get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z); }
        }

        public static Vector3 Normalized(Vector3 vec)
        {
            return vec / vec.Length;
        }
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return Subtract(left, right);
        }
        public static Vector3 Subtract(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }
        public static Vector3 operator /(Vector3 left, float right)
        {
            return Divide(left, right);
        }
        public static Vector3 Divide(Vector3 left, float right)
        {
            return new Vector3(left.X / right, left.Y / right, left.Z / right);
        }
        public static Vector3 Cross(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left.Y * right.Z - left.Z * right.Y,
                left.Z * right.X - left.X * right.Z,
                left.X * right.Y - left.Y * right.X
            );
        }
        public static float Dot(Vector3 left, Vector3 right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }
    }
}
