using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace lupastean_daniel_3134a
{
    // Clasa pentru gestiunea intregii ferestre afisate pentru aplicatia creata
    // Mosteneste clasa GameWindow
    class Window : GameWindow
    {
        // Definirea unor variabile ce vor fi folosite pentru crearea scenei 3D
        private Camera camera;
        private Cube cube;
        private Axes axes;


        // Constructorul clasei continand date despre dimensiunea ferestrei si anti-aliasing
        public Window() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;


            // Afisarea versiunii de OpenTK
            Console.WriteLine("OpenGl versiunea: " + GL.GetString(StringName.Version));
            Title = "OpenGl versiunea: " + GL.GetString(StringName.Version) + " (mod imediat)";

            // Instantierea obiectelor ce reprezinta camera si axele de coordonate
            camera = new Camera();
            axes = new Axes();

            // Instantierea unui cub folosind coordonatele din fisierul trimis ca parametru
            cube = new Cube("./../../coordonate.txt");
        }

        // Metoda ce se apeleaza la incarcarea ferestrei
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Setarea culorii de fundal a scenei 3D
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        // Metoda ce ruleaza o data la instantierea ferestrei si apoi de fiecare data cand este redimensionata fereastra
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            camera.SetCamera();
        }

        // Metoda ce se apeleaza de fiecare data cand este actualizata logica aplicatiei(are o frecventa constanta, nedepinzand de statia pe care ruleaza aplicatia)
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);


            // Instantiere obiecte pentru preluarea starii tastaturii si mouse-ului
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();


            // Setarea culorilor si a pozitiei camerei la detectarea anumitor inputuri de la mouse sau tastatura
            camera.ControlCamera(mouse);
            cube.SetColor();


            // Verificare daca este apasata tasta Escape si parasirea aplicatiei
            if (keyboard[Key.Escape])
            {
                Exit();
            }

        }


        // Metoda ce se apeleaza de fiecare data cand este generat un cadru pentru afisarea propriu-zisa pe ecran
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            // Desenarea axelor
            axes.Draw();

            // Desenarea cubului
            cube.Draw();


            // Sfârșit desenare

            SwapBuffers();

        }

        /// <summary>
        /// Internal method, used to dump the menu on the console window (text mode!)...
        /// </summary>
        private void DisplayHelp()
        {
            Console.WriteLine("\n      MENIU");
            Console.WriteLine(" (H) - meniul");
            Console.WriteLine(" (ESC) - parasire aplicatie");
            Console.WriteLine(" (K) - schimbare vizibilitate sistem de axe");
            Console.WriteLine(" (R) - resteaza scena la valori implicite");
            Console.WriteLine(" (B) - schimbare culoare de fundal");
            Console.WriteLine(" (V) - schimbare vizibilitate linii");
            Console.WriteLine(" (W,A,S,D) - deplasare camera (izometric)");
        }

        private void TimeStampIt(String source, String counter)
        {
            String dt = DateTime.Now.ToString("hh:mm:ss.ffff");
            Console.WriteLine("     TSTAMP from <" + source + "> on iteration <" + counter + ">: " + dt);
        }

    }
}
