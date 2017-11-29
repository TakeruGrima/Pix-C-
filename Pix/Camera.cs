using Microsoft.Xna.Framework;
using Pix.Gameplay;
using Pix.Gameplay.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pix
{
    class Camera
    {
        #region Field Region

        Vector2 position;
        float speed;
        float zoom;
        Rectangle viewportRectangle;
        Map map;

        #endregion

        #region Property Region

        public Vector2 Position
        {
            get { return position; }
            private set { position = value; }
        }

        public float Speed
        {
            get { return speed; }
            set
            {
                speed = (float)MathHelper.Clamp(speed, 1f, 16f);
            }
        }

        public float Zoom
        {
            get { return zoom; }
        }

        #endregion

        #region Constructor Region

        public Camera(Rectangle viewportRect, Map map)
        {
            speed = 4f;
            viewportRectangle = viewportRect;
            this.map = map;
        }

        public Camera(Rectangle viewportRect)
        {
            speed = 4f;
            viewportRectangle = viewportRect;
        }

        public Camera(Rectangle viewportRect, Vector2 position)
        {
            speed = 4f;
            viewportRectangle = viewportRect;
            Position = position;
        }

        #endregion

        #region Method Region
        public void Update(GameTime gameTime)
        {
           
        }

        private void LockCamera()
        {
            position.X = MathHelper.Clamp(position.X,
            0,
            map.MapSizeInPixel.X - viewportRectangle.Width);
            position.Y = MathHelper.Clamp(position.Y,
            0,
            map.MapSizeInPixel.Y - viewportRectangle.Height);
        }

        public void LockToSprite(Character character)
        {
            position.X = character.position.X + character.size.X / 2
            - (viewportRectangle.Width / 2);
            position.Y = character.position.Y + character.size.Y / 2
            - (viewportRectangle.Height / 2);
            LockCamera();
        }
        #endregion
    }
}
