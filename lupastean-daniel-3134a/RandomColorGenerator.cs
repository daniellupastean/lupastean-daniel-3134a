using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lupastean_daniel_3134a
{
    class RandomColorGenerator
    {
        private Random random;

        public RandomColorGenerator()
        {
            random = new Random();
        }

        public Color Generate()
        {
            int red = random.Next(0, 255);
            int green = random.Next(0, 255);
            int blue = random.Next(0, 255);

            Color color  = Color.FromArgb(red, green, blue);

            return color;
        }

    }
}
