﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


/**
    Aplicația utilizează biblioteca OpenTK v3.3
    Tipul de ferestră utilizat: GAMEWINDOW. Se demmonstrează modul imediat de randare (vezi comentariul!)...
**/

namespace lupastean_daniel_3134a
{
    class Program : GameWindow
    {
        public const int XYZ_SIZE = 75;
        private double red = 1, green = 1, blue = 1, alpha = 1;
        private Vector3[] co = new Vector3[3];
        private Camera3D camera;
        private ColorController colorController;
        private Color color1, color2, color3;

        public Program() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            // Cerinta 8 - Citirea din fisierul "coordonate.txt" a coordonatelor triunghiului
            //-------------------------------------------------------------------------------
            string text = System.IO.File.ReadAllText(@"./../../coordonate.txt");

            System.Console.WriteLine("Contents of WriteText.txt = {0}", text);
            string[] lines = text.Split('\n');

            for (int i=0; i<lines.Length; i++)
            {
                string[] textCoord = lines[i].Split(' ');
                co[i][0] = int.Parse(textCoord[0]);
                co[i][1] = int.Parse(textCoord[1]);
                co[i][2] = int.Parse(textCoord[2]);
            }
            //-------------------------------------------------------------------------------

            VSync = VSyncMode.On;

            Console.WriteLine("OpenGl versiunea: " + GL.GetString(StringName.Version));
            Title = "OpenGl versiunea: " + GL.GetString(StringName.Version) + " (mod imediat)";

            // Cerinta 8 - Instantierea obiectelor ce contin metode utile rotirii camerei, respectiv modificarii culorilor unui triunghi
            camera = new Camera3D();
            colorController = new ColorController();
            color1 = Color.Red;
            color2 = Color.Red;
            color3 = Color.Red;
        }
       
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Black); //culoare de fundal
            GL.Enable(EnableCap.DepthTest); //
            GL.DepthFunc(DepthFunction.Less); //adancime
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest); // anti-aliasing
        }

   
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height); //canvasul unui browser

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
        
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 lookat = Matrix4.LookAt(0, 0, 30, 0,0, 0, 0, 1, 0); //coordonatele camerei - primele 3
            // urm 3 - pozitia targetului
            // urm 3 - rotatia camerei
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            camera.SetCamera();
        }

  
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e); //bucla de control a opentk
            // este partea logica - update-urile de logica

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (mouse[OpenTK.Input.MouseButton.Left] && mouse.X > 100)
            {
                camera.RotateRight();
            }
            else if (mouse[OpenTK.Input.MouseButton.Left] && mouse.X < 100)
            {
                camera.RotateLeft();
            }


            if (keyboard[Key.Escape])
            {
                Exit();
            }


            colorController.SetColor(ref keyboard, ref red, ref blue, ref green, ref alpha);
            colorController.SetVertexColors(ref keyboard, ref color1, ref color2, ref color3);

            
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            DrawAxes();
            DrawObjects();
            SwapBuffers();
        }

        private void DrawAxes()
        {
            // Cerinta 1 - Desenarea axelor folosind un singur apel GL.Begin()
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

        private void DrawObjects()
        {

            GL.Begin(PrimitiveType.Triangles);
            GL.Color4(red, green, blue, alpha);
            GL.Vertex3(co[0][0], co[0][1], co[0][2]);
            GL.Vertex3(co[1][0], co[1][1], co[1][2]);
            GL.Vertex3(co[2][0], co[2][1], co[2][2]);
            GL.End();

            GL.Begin(PrimitiveType.TriangleStrip);
            GL.Color3(color1);
            GL.Vertex3(0, 0, 0);
            GL.Color3(color2);
            GL.Vertex3(0, -10, 0);
            GL.Color3(color3);
            GL.Vertex3(10, 0, 0);
            GL.End();
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (Program example = new Program())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}

