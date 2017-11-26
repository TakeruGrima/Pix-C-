using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pix.Gameplay.Sprites
{
    class Life
    {
        #region Field Region

        public int value;
        public Vector2 position;
        private Sprite sprite;

        #endregion

        #region Constructor

        public Life(int life,Vector2 position)
        {
            value = life;
            this.position = position;

            sprite = new Sprite("Sprites/life.txt");
        }

        #endregion

        #region Draw

        public void Draw(GameTime gameTime)
        {
            int x = (int)position.X;

            for (int i = 0; i < value; i++)
            {
                sprite.Draw(new Vector2(x+i, position.Y), false);
                x += 32/2 + 4;
            }
        }

        #endregion
    }
}
