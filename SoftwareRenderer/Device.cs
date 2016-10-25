using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SoftwareRenderer
{
    public class Device
    {
        private WriteableBitmap frontBuffer;
        private byte[] backBuffer;
        private int width, height;
        private const int bytesPerPixel = 4;


        public ImageSource FrontBuffer
        {
            get { return frontBuffer; }
        }

        public Device(int width, int height)
        {
            frontBuffer = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgr32, null);
            backBuffer = new byte[width * height * bytesPerPixel];
            this.width = width;
            this.height = height;
        }

        public void Present()
        {
            frontBuffer.WritePixels(new Int32Rect(0, 0, width, height), backBuffer, width * bytesPerPixel, 0);
        }

        public void Clear(Color color)
        {
            for(var i = 0; i < backBuffer.Length; i += 4)
            {
                backBuffer[i + 0] = color.Blue;
                backBuffer[i + 1] = color.Green;
                backBuffer[i + 2] = color.Red;
            }
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                backBuffer[(x + width * y) * bytesPerPixel + 0] = color.Blue;
                backBuffer[(x + width * y) * bytesPerPixel + 1] = color.Green;
                backBuffer[(x + width * y) * bytesPerPixel + 2] = color.Red;
            }
        }

        public void DrawDot(int x, int y, Color color)
        {
            for (var dy = -3; dy <= 3; ++dy)
            {
                for (var dx = -3; dx <= 3; ++dx)
                {
                    SetPixel(x + dx, y + dy, color);
                }
            }
        }

        public void Render(PerspectiveCamera camera, params Mesh[] meshes)
        {
            // To understand this part, please read the prerequisites resources
            var viewProjectionMatrix = camera.ProjectionMatrix * camera.ViewMatrix;

            foreach (Mesh mesh in meshes)
            {
                // Beware to apply rotation before translation 
                var worldMatrix = Matrix4x4.Rotation(mesh.Rotation.Y, mesh.Rotation.X, mesh.Rotation.Z) *
                                  Matrix4x4.Translation(mesh.Position);

                var transformMatrix = viewProjectionMatrix * worldMatrix;

                foreach (var vertex in mesh.Vertices)
                {
                    // First, we project the 3D coordinates into the 2D space
                    var point = transformMatrix * vertex;
                    // Then we can draw on screen
                    DrawDot((int)(point.X * (width / 2) + width / 2), (int)(point.Y * (height / 2) + height / 2), new Color(255, 0, 0));
                }
            }
        }




    }
}
