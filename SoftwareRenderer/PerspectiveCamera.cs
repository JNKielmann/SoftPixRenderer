using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareRenderer
{
    public class PerspectiveCamera
    {
        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }

        public float Left { get; set; }
        public float Right { get; set; }
        public float Top { get; set; }
        public float Bottom { get; set; }
        public float Near { get; set; }
        public float Far { get; set; }

        public void SetFOV(float fov, float aspect, float near, float far)
        {
            Top = (float)Math.Tan(fov / 2) * near;
            Bottom = -Top;
            Right = Top * aspect;
            Left = -Top * aspect;
            Near = near;
            Far = Far;
        }

        public Matrix4x4 ProjectionMatrix
        {
            get
            {
                return new Matrix4x4(new float[] {
                    (2 * Near) / ( Right - Left), 0, (Right + Left) / (Right - Left), 0,
                    0, (2 * Near) / (Top - Bottom), (Top + Bottom) / (Top - Bottom), 0,
                    0, 0, -(Far + Near) / (Far - Near), -(2 * Far * Near) / (Far - Near),
                    0      , 0      , -1      , 0
                });
            }
        }

        public Matrix4x4 ViewMatrix
        {
            get
            {
                Vector3 zAxis = Vector3.Normalized(Position - Target);
                Vector3 xAxis = Vector3.Normalized(Vector3.Cross(new Vector3(0, 1, 0), zAxis));
                Vector3 yAxis = Vector3.Cross(zAxis, xAxis);

                return new Matrix4x4(new float[] {
                    xAxis.X, xAxis.Y, xAxis.Z, -Vector3.Dot(xAxis, Position),
                    yAxis.X, yAxis.Y, yAxis.Z, -Vector3.Dot(yAxis, Position),
                    zAxis.X, zAxis.Y, zAxis.Z, -Vector3.Dot(zAxis, Position),
                    0      , 0      , 0      , 1
                });
            }
        }
    }
}
