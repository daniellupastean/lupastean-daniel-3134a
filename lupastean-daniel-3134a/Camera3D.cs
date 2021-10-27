using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace lupastean_daniel_3134a
{
    class Camera3D
    {
        private Vector3 eye = new Vector3(0, 10, 30);
        private Vector3 target = new Vector3(0, 0, 0);
        private Vector3 up = new Vector3(0, 1, 0);
        private const int MOVEMENT_UNIT = 1;

        public void SetCamera()
        {
            Matrix4 camera = Matrix4.LookAt(eye, target, up);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref camera);
        }

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
    }
}
