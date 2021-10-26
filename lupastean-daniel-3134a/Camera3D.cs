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

            // Mouse.SetPosition(450, 450);
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

        public void RotateUp()
        {
            eye = new Vector3(eye.X, eye.Y + 5, eye.Z);
            SetCamera();
        }

        public void RotateDown()
        {
            eye = new Vector3(eye.X, eye.Y - 5, eye.Z);
            SetCamera();
        }

        public void MoveRight()
        {
            eye = new Vector3(eye.X, eye.Y, eye.Z - MOVEMENT_UNIT);
            target = new Vector3(target.X, target.Y, target.Z - MOVEMENT_UNIT);
            SetCamera();
        }
        public void MoveLeft()
        {
            eye = new Vector3(eye.X, eye.Y, eye.Z + MOVEMENT_UNIT);
            target = new Vector3(target.X, target.Y, target.Z + MOVEMENT_UNIT);
            SetCamera();
        }

        public void MoveForward()
        {
            eye = new Vector3(eye.X - MOVEMENT_UNIT, eye.Y, eye.Z);
            target = new Vector3(target.X - MOVEMENT_UNIT, target.Y, target.Z);
            SetCamera();
        }

        public void MoveBackward()
        {
            eye = new Vector3(eye.X + MOVEMENT_UNIT, eye.Y, eye.Z);
            target = new Vector3(target.X + MOVEMENT_UNIT, target.Y, target.Z);
            SetCamera();
        }

        public void MoveUp()
        {
            eye = new Vector3(eye.X, eye.Y + MOVEMENT_UNIT, eye.Z);
            target = new Vector3(target.X, target.Y + MOVEMENT_UNIT, target.Z);
            SetCamera();
        }

        public void MoveDown()
        {
            eye = new Vector3(eye.X, eye.Y - MOVEMENT_UNIT, eye.Z);
            target = new Vector3(target.X, target.Y - MOVEMENT_UNIT, target.Z);
            SetCamera();
        }
    }
}
