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
    /// Clasa ce contine punctul de intrare in aplicatie
    class Program {

        [STAThread]
        static void Main(string[] args)
        {
            // Instatierea unui obiect de tipul Window
            using (Window3D example = new Window3D())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}

