using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace lupastean_daniel_3134a
{
    class Program {

        [STAThread]
        static void Main(string[] args)
        {
            using (Window example = new Window())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}

