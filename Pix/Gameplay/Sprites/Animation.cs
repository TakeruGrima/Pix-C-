using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pix.Gameplay.Sprites
{
    class Animation
    {
        #region Field Region

        List<Sprite> sprites;//sprites in the animation
        int currentFrame = 0;
        public int FrameCounter;
        public int SwitchFrame;

        #endregion

        #region Constructor

        public Animation()
        {
            sprites = new List<Sprite>();
            SwitchFrame = 100;
        }

        public Animation(List<Sprite> sprites)
        {
            this.sprites = sprites;
            SwitchFrame = 100;
        }

        #endregion

        #region update

        public void Update(GameTime gameTime)
        {
            FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (FrameCounter >= SwitchFrame)
            {
                FrameCounter = 0;

                if (sprites.Count > 1)
                {
                    currentFrame++;

                    if (currentFrame >= sprites.Count - 1)
                        currentFrame = 0;
                }
            }
        }

        #endregion

        #region methods

        //methods to add sprite to the animation
        public void Add(Sprite sprite)
        {
            sprites.Add(sprite);
        }

        #endregion

        #region Draw

        public void Draw(Camera camera, Vector2 position, bool flip)
        {
            position -= camera.Position;
            sprites[currentFrame].Draw(position, flip, 1);
        }

        public void DrawSemiTransparent(Camera camera, Vector2 position, bool flip)
        {
            position -= camera.Position;
            sprites[currentFrame].Draw(position, flip, 0.5f);
        }

        #endregion
    }
}