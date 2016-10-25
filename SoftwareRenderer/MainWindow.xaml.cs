using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftwareRenderer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int Width = 800;
        private const int Height = 600;

        private Device device;
        private Mesh mesh = new Mesh( 8);
        private PerspectiveCamera cam = new PerspectiveCamera();

        public MainWindow()
        {
            InitializeComponent();

            device = new Device(Width, Height);
            displayImage.Source = device.FrontBuffer;
            CompositionTarget.Rendering += Render;

            SetUpSecene();
        }

        public void SetUpSecene()
        {
            mesh.Vertices[0] = new Vector3(-1, 1, 1);
            mesh.Vertices[1] = new Vector3(1, 1, 1);
            mesh.Vertices[2] = new Vector3(-1, -1, 1);
            mesh.Vertices[3] = new Vector3(-1, -1, -1);
            mesh.Vertices[4] = new Vector3(-1, 1, -1);
            mesh.Vertices[5] = new Vector3(1, 1, -1);
            mesh.Vertices[6] = new Vector3(1, -1, 1);
            mesh.Vertices[7] = new Vector3(1, -1, -1);

            cam.Position = new Vector3(0, 0, 10.0f);
            cam.Target = new Vector3(0, 0, 0);
            cam.SetFOV(0.78f, Width / Height, 0.01f, 1.0f);

        }

        private void Render(object sender, object e)
        {
            device.Clear(new Color(255, 255, 255));
            mesh.Rotation = new Vector3(mesh.Rotation.X + 0.01f, mesh.Rotation.Y + 0.01f, mesh.Rotation.Z);
            device.Render(cam, mesh);
            device.Present();
        }


    }
}
