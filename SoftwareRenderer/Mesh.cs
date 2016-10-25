using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareRenderer
{
    public class Mesh
    {
        public Vector3[] Vertices { get; private set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public Mesh(int verticesCount)
        {
            Vertices = new Vector3[verticesCount];
            Position = new Vector3(0, 0, 0);
            Rotation = new Vector3(0, 0, 0);
        }

        public static Mesh CreateCube()
        {
            var mesh = new Mesh(8);
            mesh.Vertices[0] = new Vector3(-1, 1, 1);
            mesh.Vertices[1] = new Vector3(1, 1, 1);
            mesh.Vertices[2] = new Vector3(-1, -1, 1);
            mesh.Vertices[3] = new Vector3(-1, -1, -1);
            mesh.Vertices[4] = new Vector3(-1, 1, -1);
            mesh.Vertices[5] = new Vector3(1, 1, -1);
            mesh.Vertices[6] = new Vector3(1, -1, 1);
            mesh.Vertices[7] = new Vector3(1, -1, -1);
            return mesh;
        }
    }
}
