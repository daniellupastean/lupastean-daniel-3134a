using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lupastean_daniel_3134a
{

    // Clasa pentru gestiunea axelor din spatiul 3D
    class Axes
    {
        public const int XYZ_SIZE = 75;

        // Metoda pentru desenarea propriu-zisa a axelor pe ecran
        public void Draw()
        {
            GL.Begin(PrimitiveType.Lines);
            // Desenează axa Ox
            GL.Color3(Color.White);
            GL.Vertex3(0, 0, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(XYZ_SIZE, 0, 0);
            // Desenează axa Oy.
            GL.Color3(Color.White);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, XYZ_SIZE, 0);
            // Desenează axa Oz.
            GL.Color3(Color.White);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, XYZ_SIZE);
            GL.End();
        }
    }
}
