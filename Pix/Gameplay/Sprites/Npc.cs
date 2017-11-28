using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Pix.Gameplay.Sprites
{
    class Npc : Character
    {
        #region Field region

        public string direction;//the npc go to that direction

        #endregion

        #region Constructor

        public Npc(Vector2 position, string filePath, Collision collision) : base(position, filePath, collision)
        {
            anims = new Dictionary<string, Animation>();

            Animation idle = new Animation();
            idle.Add(sprites[0]);

            anims.Add("idle", idle);

            Animation move = new Animation();
            move.Add(sprites[0]);
            move.Add(sprites[1]);
            move.Add(sprites[2]);
            move.Add(sprites[1]);
            move.Add(sprites[0]);

            anims.Add("move", move);

            currentAnimation = "idle";

            direction = "right";

            trueHeight = 10 * 2;
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            //Tile behind the sprite
            char idOverlap = collision.Map.GetTileAt((int)position.X + collision.Map.Size / 2,
                (int)position.Y);

            if (idOverlap == '<')
            {
                direction = "left";
            }
            if (idOverlap == '>')
            {
                direction = "right";
            }

            //movement
            if (direction == "right")
            {
                velocity.X = 50;
                currentAnimation = "move";
                flip = false;
            }
            else if (direction == "left")
            {
                velocity.X = -50;
                currentAnimation = "move";
                flip = true;
            }

            if (collision.CollideCharacters(this))
            {
                if (velocity.X > 0)
                {
                    velocity.X = -velocity.X;
                    flip = false;
                    direction = "left";
                }
                else if (velocity.X < 0)
                {
                    velocity.X = -velocity.X;
                    flip = true;
                    direction = "right";
                }
            }

            base.Update(gameTime);
            anims[currentAnimation].Update(gameTime);
        }

        #endregion
    }
}