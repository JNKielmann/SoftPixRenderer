using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareRenderer
{
    public class Matrix4x4
    {
        private float[] data;

        public Matrix4x4()
        {
            data = new float[16];
        }
        public Matrix4x4(float[] matrixData)
        {
            data = matrixData;
        }

        public float Get(int row, int column)
        {
            return data[row * 4 + column];
        }

        public void Set(int row, int column, float value)
        {
            data[row * 4 + column] = value;
        }

        public float this[int row, int column]
        {
            get
            {
                return Get(row, column);
            }
            set
            {
                Set(row, column, value);
            }
        }

        public static Matrix4x4 Identity()
        {
            return new Matrix4x4(new float[] {
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            });
        }

        public static Matrix4x4 operator *(Matrix4x4 left, Matrix4x4 right)
        {
            return Multiply(left, right);
        }

        public static Matrix4x4 Multiply(Matrix4x4 left, Matrix4x4 right)
        {
            Matrix4x4 result = new Matrix4x4();
            result.M11 = (left.M11 * right.M11) + (left.M12 * right.M21) + (left.M13 * right.M31) + (left.M14 * right.M41);
            result.M12 = (left.M11 * right.M12) + (left.M12 * right.M22) + (left.M13 * right.M32) + (left.M14 * right.M42);
            result.M13 = (left.M11 * right.M13) + (left.M12 * right.M23) + (left.M13 * right.M33) + (left.M14 * right.M43);
            result.M14 = (left.M11 * right.M14) + (left.M12 * right.M24) + (left.M13 * right.M34) + (left.M14 * right.M44);
            result.M21 = (left.M21 * right.M11) + (left.M22 * right.M21) + (left.M23 * right.M31) + (left.M24 * right.M41);
            result.M22 = (left.M21 * right.M12) + (left.M22 * right.M22) + (left.M23 * right.M32) + (left.M24 * right.M42);
            result.M23 = (left.M21 * right.M13) + (left.M22 * right.M23) + (left.M23 * right.M33) + (left.M24 * right.M43);
            result.M24 = (left.M21 * right.M14) + (left.M22 * right.M24) + (left.M23 * right.M34) + (left.M24 * right.M44);
            result.M31 = (left.M31 * right.M11) + (left.M32 * right.M21) + (left.M33 * right.M31) + (left.M34 * right.M41);
            result.M32 = (left.M31 * right.M12) + (left.M32 * right.M22) + (left.M33 * right.M32) + (left.M34 * right.M42);
            result.M33 = (left.M31 * right.M13) + (left.M32 * right.M23) + (left.M33 * right.M33) + (left.M34 * right.M43);
            result.M34 = (left.M31 * right.M14) + (left.M32 * right.M24) + (left.M33 * right.M34) + (left.M34 * right.M44);
            result.M41 = (left.M41 * right.M11) + (left.M42 * right.M21) + (left.M43 * right.M31) + (left.M44 * right.M41);
            result.M42 = (left.M41 * right.M12) + (left.M42 * right.M22) + (left.M43 * right.M32) + (left.M44 * right.M42);
            result.M43 = (left.M41 * right.M13) + (left.M42 * right.M23) + (left.M43 * right.M33) + (left.M44 * right.M43);
            result.M44 = (left.M41 * right.M14) + (left.M42 * right.M24) + (left.M43 * right.M34) + (left.M44 * right.M44);
            return result;
        }
        public static Vector3 operator *(Matrix4x4 left, Vector3 right)
        {
            return Multiply(left, right);
        }

        public static Vector3 Multiply(Matrix4x4 left, Vector3 right)
        {
            Vector3 result = new Vector3(
                (left.M11 * right.X) + (left.M12 * right.Y) + (left.M13 * right.Z) + (left.M14 * 1),
                (left.M21 * right.X) + (left.M22 * right.Y) + (left.M23 * right.Z) + (left.M24 * 1),
                (left.M31 * right.X) + (left.M32 * right.Y) + (left.M33 * right.Z) + (left.M34 * 1)
            );
            float w = (left.M41 * right.X) + (left.M42 * right.Y) + (left.M43 * right.Z) + (left.M44 * 1);
            if(Math.Abs(1-w) > 0.001f)
            {
                result /= w;
            }
            return result;
        }

        public static Matrix4x4 Translation(Vector3 delta)
        {
            return new Matrix4x4(new float[] {
                1, 0, 0, delta.X,
                0, 1, 0, delta.Y,
                0, 0, 1, delta.Z,
                0, 0, 0, 1
            });
        }

        public static Matrix4x4 RotationX(float angle)
        {
            float cosA = (float)Math.Cos(angle);
            float sinA = (float)Math.Sin(angle);
            return new Matrix4x4(new float[] {
                1,    0,     0, 0,
                0, cosA, -sinA, 0,
                0, sinA,  cosA, 0,
                0,    0,     0, 1
            });
        }

        public static Matrix4x4 RotationY(float angle)
        {
            float cosA = (float)Math.Cos(angle);
            float sinA = (float)Math.Sin(angle);
            return new Matrix4x4(new float[] {
                 cosA, 0, sinA, 0,
                    0, 1,    0, 0,
                -sinA, 0, cosA, 0,
                    0, 0,    0, 1
            });
        }

        public static Matrix4x4 RotationZ(float angle)
        {
            float cosA = (float)Math.Cos(angle);
            float sinA = (float)Math.Sin(angle);
            return new Matrix4x4(new float[] {
                cosA, -sinA, 0, 0,
                sinA,  cosA, 0, 0,
                   0,     0, 1, 0,
                   0,     0, 0, 1
            });
        }

        public static Matrix4x4 Rotation(float yaw, float pitch, float roll)
        {
            return RotationZ(roll) * RotationX(pitch) * RotationY(yaw);
        }



        public float M11
        {
            get { return data[0]; }
            set { data[0] = value; }
        }
        public float M12
        {
            get { return data[1]; }
            set { data[1] = value; }
        }
        public float M13
        {
            get { return data[2]; }
            set { data[2] = value; }
        }
        public float M14
        {
            get { return data[3]; }
            set { data[3] = value; }
        }

        public float M21
        {
            get { return data[4]; }
            set { data[4] = value; }
        }
        public float M22
        {
            get { return data[5]; }
            set { data[5] = value; }
        }
        public float M23
        {
            get { return data[6]; }
            set { data[6] = value; }
        }
        public float M24
        {
            get { return data[7]; }
            set { data[7] = value; }
        }

        public float M31
        {
            get { return data[8]; }
            set { data[8] = value; }
        }
        public float M32
        {
            get { return data[9]; }
            set { data[9] = value; }
        }
        public float M33
        {
            get { return data[10]; }
            set { data[10] = value; }
        }
        public float M34
        {
            get { return data[11]; }
            set { data[11] = value; }
        }

        public float M41
        {
            get { return data[12]; }
            set { data[12] = value; }
        }
        public float M42
        {
            get { return data[13]; }
            set { data[13] = value; }
        }
        public float M43
        {
            get { return data[14]; }
            set { data[14] = value; }
        }
        public float M44
        {
            get { return data[15]; }
            set { data[15] = value; }
        }


    }
}
