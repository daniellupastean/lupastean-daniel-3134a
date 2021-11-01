using OpenTK.Input;
using System;
using System.Drawing;

namespace lupastean_daniel_3134a
{

    // Aceasta clasa a fost creata pentru rezolvarea cerintelor 8 si 9 si pentru structurarea mai buna a codului
    class ColorController
    {

        private RandomColorGenerator colorGenerator = new RandomColorGenerator();
        private KeyboardState lastKeyPressed;

        // Cerinta 8 - Metoda pentru schimbarea culorii de pe fiecare canal a suprafetei primului triunghi ( R,G,B,A + Sageata Sus sau Jos)
        // Am folosit argumente de tip ref pentru a modifica direct variabila(referinta) trimisa ca parametru
        public void SetColor(KeyboardState keyboard, ref double red, ref double blue, ref double green, ref double alpha)
        {
            if (keyboard[Key.Up] && keyboard[Key.R] && red < 1)
            {
                red += 0.05;
            }
            else if (keyboard[Key.Down] && keyboard[Key.R] && red > 0)
            {
                red -= 0.05;
            }
            else if (keyboard[Key.Up] && keyboard[Key.B] && blue < 1)
            {
                blue += 0.05;
            }
            else if (keyboard[Key.Down] && keyboard[Key.B] && blue > 0)
            {
                blue -= 0.05;
            }
            else if (keyboard[Key.Up] && keyboard[Key.G] && green < 1)
            {
                green += 0.05;
            }
            else if (keyboard[Key.Down] && keyboard[Key.G] && green > 0)
            {
                green -= 0.05;
            }
            else if (keyboard[Key.Up] && keyboard[Key.A] && alpha < 1)
            {
                alpha += 0.05;
            }
            else if (keyboard[Key.Down] && keyboard[Key.A] && alpha > 0)
            {
                alpha -= 0.05;
                if (alpha < 0.05)
                {
                    alpha = 0;
                }
            }

            
        }

        // Cerinta 9 - Metoda pentru schimbarea culorilor vertexurilor celui de-al doilea triunghi la apasarea tastelor numerice 1-6
        // 1 : schimba culoarea vertexului 1
        // 2 : schimba culoarea vertexului 2
        // 3 : schimba culoarea vertexului 3

        public void SetTriangleColors(KeyboardState keyboard, ref Color color1, ref Color color2, ref Color color3)
        {
            Color temp_color1 = color1;
            Color temp_color2 = color2;
            Color temp_color3 = color3;

            if (keyboard != lastKeyPressed)
            {
                if (keyboard[Key.Number1])
                {
                    color1 = colorGenerator.Generate();
                    Console.WriteLine("Vertex 1: " + color1);
                }
                if (keyboard[Key.Number2])
                {
                    color2 = colorGenerator.Generate();
                    Console.WriteLine("Vertex 2: " + color2);
                }
                if (keyboard[Key.Number3])
                {
                    color3 = colorGenerator.Generate();
                    Console.WriteLine("Vertex 3: " + color3);
                }

                lastKeyPressed = keyboard;
            }
        }
    }
}
