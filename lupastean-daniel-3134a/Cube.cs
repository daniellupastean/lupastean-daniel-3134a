﻿using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace lupastean_daniel_3134a
{
    class Cube
    {
        // Vectorul de Vector3 in care vor fi citite coordonatele din fisier
        private List<Vector3> vertices;

        // Variabile pentru canalele de culoare
        private double red = 1, green = 1, blue = 1, alpha = 1;
        private Color tcolor1 = Color.Yellow, tcolor2 = Color.Yellow, tcolor3 = Color.Yellow;
        private RandomColorGenerator colorGenerator;

        // Declarare variabila controller pentru mofidicarea culorilor triunghiurilor
        private ColorController colorController;

        public Cube(string caleFisier)
        {
            vertices = new List<Vector3>();

            // Citirea din fisierul "coordonate.txt" a coordonatelor cubului
            //-------------------------------------------------------------------------------
            string text = System.IO.File.ReadAllText(@caleFisier);

            System.Console.WriteLine("Contents of WriteText.txt = {0}", text);
            string[] lines = text.Split('\n');

            for (int i = 0; i < 36; i++)
            {
                string[] co = lines[i].Split(' ');
                vertices.Add(new Vector3(int.Parse(co[0]), int.Parse(co[1]), int.Parse(co[2])));
            }
            //-------------------------------------------------------------------------------

            colorController = new ColorController();
            colorGenerator = new RandomColorGenerator();

        }

        public void SetColor()
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();


            colorController.SetColor(keyboard, ref red, ref blue, ref green, ref alpha);
            colorController.SetTriangleColors(keyboard, ref tcolor1, ref tcolor2, ref tcolor3);
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Triangles);
            for (int i = 0; i < 36; i = i + 3)
            {
                if (i > 28)
                    GL.Color4(red, green, blue, alpha);
                else
                    GL.Color3(Color.Blue);

                if (i == 18) GL.Color3(tcolor1);
                GL.Vertex3(vertices[i]);
                if (i == 18) GL.Color3(tcolor2);
                GL.Vertex3(vertices[i + 1]);
                if (i == 18) GL.Color3(tcolor3);
                GL.Vertex3(vertices[i + 2]);
            }
            GL.End();
        }
    }
}
