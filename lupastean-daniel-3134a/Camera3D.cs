using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace lupastean_daniel_3134a
{   
    // Aceasta clasa a fost implementata pentru a ajuta la rezolvarea cerintei 8 si pentru structurarea mai buna a codului
    class Camera3D
    {
        private Vector3 eye = new Vector3(0, 10, 30);
        private Vector3 target = new Vector3(0, 0, 0);
        private Vector3 up = new Vector3(0, 1, 0);
        private const int MOVEMENT_UNIT = 1;


        // Metoda pentru initializarea
        public void SetCamera()
        {
            Matrix4 camera = Matrix4.LookAt(eye, target, up);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref camera);
        }


        // Metoda pentru verificarea pozitiei camerei si mutarii eye-ului camerei cu o unitate in partea dreapta
        public void RotateRight()
        {
            if (eye.X < 40 && eye.Z>=40)
            {
                eye = new Vector3(eye.X + MOVEMENT_UNIT, eye.Y, eye.Z);
            }
            else if(eye.X>=40 && eye.Z>-40)
            {
                eye = new Vector3(eye.X, eye.Y, eye.Z - MOVEMENT_UNIT);
            }
            else if(eye.X>-40 && eye.Z<=-40)
            {
                eye = new Vector3(eye.X - MOVEMENT_UNIT, eye.Y, eye.Z);
            }
            else
            {
                eye = new Vector3(eye.X, eye.Y, eye.Z + MOVEMENT_UNIT);
            }
            SetCamera();
        }


        // Metoda pentru verificarea pozitiei camerei si mutarii eye-ului camerei cu o unitate in partea stanga
        public void RotateLeft()
        {
            if (eye.X > -40 && eye.Z >= 40)
            {
                eye = new Vector3(eye.X - MOVEMENT_UNIT, eye.Y, eye.Z);
            }
            else if (eye.X <= -40 && eye.Z > -40)
            {
                eye = new Vector3(eye.X, eye.Y, eye.Z - MOVEMENT_UNIT);
            }
            else if (eye.X < 40 && eye.Z <= -40)
            {
                eye = new Vector3(eye.X + MOVEMENT_UNIT, eye.Y, eye.Z);
            }
            else
            {
                eye = new Vector3(eye.X, eye.Y, eye.Z + MOVEMENT_UNIT);
            }
            SetCamera();
        }


        // Metoda pentru verificarea pozitiei pe ecran si a starii mouseului
        // Practic se verifica daca este apasat click dreapta mouse-ul se afla in partea stanga sau dreapta a ecranului
        // Iar in functie de asta se apeleaza metoda RotateRight() sau RotateLeft()
        public void ControlCamera(MouseState mouse)
        {
            if (mouse[OpenTK.Input.MouseButton.Left] && mouse.X > 100)
            {
                this.RotateRight();
            }
            else if (mouse[OpenTK.Input.MouseButton.Left] && mouse.X < 100)
            {
                this.RotateLeft();
            }
        }
    }
}
