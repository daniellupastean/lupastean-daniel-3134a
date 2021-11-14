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
using System.IO;

namespace lupastean_daniel_3134a
{
    // Clasa pentru gestiunea intregii ferestre afisate pentru aplicatia creata
    // Mosteneste clasa GameWindow
    class Window3D : GameWindow
    {
        private KeyboardState previousKeyboard;
        private MouseState previousMouse;
        private readonly Randomizer rando;
        private readonly Axes ax;
        private readonly Grid grid;
        private readonly Camera3DIsometric cam;
        private bool displayMarker;
        private ulong updatesCounter;
        private ulong framesCounter;
        private MassiveObject objy;
        /// <summary>
        /// declarare lista de obiecte de tip Objectoid
        /// </summary>
        private List<SomeObject> objectsList;
        /// <summary>
        /// declarare lista de obiecte de tip Vector3 pentru vertexurile necesare pentru cub 
        /// </summary>
        private List<Vector3> vertices;
        private List<MassiveObject> massiveObjectsList;

        // DEFAULTS
        private readonly Color DEFAULT_BKG_COLOR = Color.FromArgb(49, 50, 51);

        /// <summary>
        /// Parametrised constructor. Invokes the anti-aliasing engine. All inits are placed here, for convenience.
        /// </summary>
        public Window3D() : base(1280, 768, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            // inits
            rando = new Randomizer();
            ax = new Axes();
            grid = new Grid();
            cam = new Camera3DIsometric();
            objy = new MassiveObject(Color.Yellow);

            vertices = readVerticesFromFile(@"./../../coordonate.txt");

            objectsList = new List<SomeObject>();

            massiveObjectsList = new List<MassiveObject>();

            DisplayHelp();
            displayMarker = false;
            updatesCounter = 0;
            framesCounter = 0;
        }

        /// <summary>
        /// OnLoad() method. Part of the control loop of the OpenTK API. Executed only once.
        /// </summary>
        /// <param name="e">event parameters that triggered the method;</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        /// <summary>
        /// OnResize() method. Part of the control loop of the OpenTK API. Executed at least once (after OnLoad()).
        /// </summary>
        /// <param name="e">event parameters that triggered the method;</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // set background
            GL.ClearColor(DEFAULT_BKG_COLOR);

            // set viewport
            GL.Viewport(0, 0, this.Width, this.Height);

            // set perspective
            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 1024);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            // set the eye
            cam.SetCamera();
        }

        /// <summary>
        /// OnUpdateFrame() method. Part of the control loop of the OpenTK API. Executed periodically, with a frequency determined when launching
        /// the graphics window (<see cref="GameWindow.Run(double, double)"/>). In this case should be 30.00 (if unmodified).
        ///
        /// All logic should reside here!
        /// </summary>
        /// <param name="e">event parameters that triggered the method;</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            updatesCounter++;

            if (displayMarker)
            {
                TimeStampIt("update", updatesCounter.ToString());
            }

            // LOGIC CODE
            KeyboardState currentKeyboard = Keyboard.GetState();
            MouseState currentMouse = Mouse.GetState();

            if (currentKeyboard[Key.Escape])
            {
                Exit();
            }

            if (currentKeyboard[Key.H] && !previousKeyboard[Key.H])
            {
                DisplayHelp();
            }

            if (currentKeyboard[Key.R] && !previousKeyboard[Key.R])
            {
                GL.ClearColor(DEFAULT_BKG_COLOR);
                ax.Show();
                grid.Show();
            }

            if (currentKeyboard[Key.K] && !previousKeyboard[Key.K])
            {
                ax.ToggleVisibility();
            }

            if (currentKeyboard[Key.B] && !previousKeyboard[Key.B])
            {
                GL.ClearColor(rando.RandomColor());
            }

            if (currentKeyboard[Key.V] && !previousKeyboard[Key.V])
            {
                grid.ToggleVisibility();
            }

            if (currentKeyboard[Key.O] && !previousKeyboard[Key.O])
            {
                objy.ToggleVisibility();
            }

            // camera control (isometric mode)
            if (currentKeyboard[Key.W])
            {
                cam.MoveForward();
            }
            if (currentKeyboard[Key.S])
            {
                cam.MoveBackward();
            }
            if (currentKeyboard[Key.A])
            {
                cam.MoveLeft();
            }
            if (currentKeyboard[Key.D])
            {
                cam.MoveRight();
            }
            if (currentKeyboard[Key.Q])
            {
                cam.MoveUp();
            }
            if (currentKeyboard[Key.E])
            {
                cam.MoveDown();
            }
            if (currentKeyboard[Key.KeypadMinus])
            {
                cam.ZoomOut();
            }

            if (currentKeyboard[Key.KeypadPlus])
            {
                cam.ZoomIn();
            }

            if (currentKeyboard[Key.M])
            {
                cam.Far();
            }

            if (currentKeyboard[Key.N])
            {
                cam.Near();
            }
            if (currentMouse[MouseButton.Left] && !previousMouse[MouseButton.Left])
            {
                objectsList.Add(new SomeObject(true, vertices));
            }


            foreach (SomeObject obj in objectsList)
            {
                obj.UpdatePosition(true);
            }

            foreach (MassiveObject obj in massiveObjectsList)
            {
                obj.UpdatePosition(true);
            }

            // object spam cleanup
            if (currentMouse[MouseButton.Right] && !previousMouse[MouseButton.Right])
            {
                objectsList.Clear();
                //listaMassiveObj.Clear();
            }

            // helper functions
            if (currentKeyboard[Key.L] && !previousKeyboard[Key.L])
            {
                displayMarker = !displayMarker;
            }

            previousKeyboard = currentKeyboard;
            previousMouse = currentMouse;
            // END logic code
        }

        /// <summary>
        /// OnRenderFrame() method. Part of the control loop of the OpenTK API. Executed periodically, with a frequency determined when launching
        /// the graphics window (<see cref="GameWindow.Run(double, double)"/>). In this case should be 0.00 (if unmodified) - the rendering is triggered
        /// only when the scene is modified.
        ///
        /// All render calls should reside here!
        /// </summary>
        /// <param name="e">event parameters that triggered the method;</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            framesCounter++;

            if (displayMarker)
            {
                TimeStampIt("render", framesCounter.ToString());
            }

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            // RENDER CODE
            grid.Draw();
            ax.Draw();
            objy.Draw();


            foreach (SomeObject obj in objectsList)
            {
                obj.Draw();
            }

            foreach (MassiveObject obj in massiveObjectsList)
            {
                obj.Draw();
            }

            // END render code

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
            Console.WriteLine(" (M,N) - setare camera la locatii predefinite (aproape si departe)");
            Console.WriteLine(" (+, -) - tastatura numerica - manipulare zoom camera");
        }

        private void TimeStampIt(String source, String counter)
        {
            String dt = DateTime.Now.ToString("hh:mm:ss.ffff");
            Console.WriteLine("     TSTAMP from <" + source + "> on iteration <" + counter + ">: " + dt);
        }

        /// <summary>
        /// laborator 5 - punctul 3 - functie ce va citi dintr-un fisier vertexurile necesare pentru obiectul de tip cub 
        /// are loc testarea de eroare la deschiderea resurselor, aruncandu-se o exceptie in cazul in care datele din fisier sunt eronate 
        /// </summary>
        /// <param name="numeFisier"></param>
        /// <returns></returns>
        public List<Vector3> readVerticesFromFile(string numeFisier)
        {
            List<Vector3> vertexuriDinFisier = new List<Vector3>();

            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    var numere = linie.Split(',');
                    int i = 0;
                    float[] coordonate = new float[3];
                    foreach (var nr in numere)
                    {
                        coordonate[i++] = float.Parse(nr);

                        if (coordonate[i - 1] < 0 || coordonate[i - 1] > 250)
                        {
                            throw new ArithmeticException("Invalid vertex !");
                        }
                    }
                    vertexuriDinFisier.Add(new Vector3(coordonate[0], coordonate[1], coordonate[2]));
                }
            }

            return vertexuriDinFisier;
        }

    }
}
