using OpenTK.Graphics.OpenGL;
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
    // Clasa pentru gestionarea unui cub in spatiul 3D
    class Cube
    {
        // Vectorul de Vector3 in care vor fi citite coordonatele cubului din fisier
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

            // Instantierea unor obiecte pentru controller-ul de culori si pentru generatorul de culori random
            colorController = new ColorController();
            colorGenerator = new RandomColorGenerator();

        }


        // Metoda pentru setarea culorii cubului si a unui triunghi din componenta acestuia
        public void SetColor()
        {

            // Definire obiecte pentru starea tastaturii si mouse-ului
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            // Setare culoare cub in functie de tastele apasate
            colorController.SetColor(keyboard, ref red, ref blue, ref green, ref alpha);
            // Setare culoare triunghi din componenta cubului in fucntie de tastele apasate
            colorController.SetTriangleColors(keyboard, ref tcolor1, ref tcolor2, ref tcolor3);
        }


        // Metoda pentru desenarea propriu-zisa a cubului pe ecran 
        public void Draw()
        {
            GL.Begin(PrimitiveType.Triangles);
            for (int i = 0; i < 36; i = i + 3)
            {

                // Cerinta 1: Setarea culorii unei suprafete a cubului
                if (i > 28)
                    GL.Color4(red, green, blue, alpha);
                else
                    GL.Color3(Color.Blue);


                // Cerinta 2 : blocuri pentru setarea unei culori generata random pentru fiecare vertex al unui triunghi din componenta cubului
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
