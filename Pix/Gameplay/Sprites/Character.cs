using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pix.Gameplay.Sprites
{
    public enum State { ALIVE, DEAD, HURT };
    class Character
    {
        #region Field region

        public State state = State.ALIVE;

        public List<Sprite> sprites;//sprites for the character
        public Dictionary<string, Animation> anims;//animation is identified by a string
        public string currentAnimation;

        public Vector2 size;
        public int trueHeight;//the true height of the npc
        public Vector2 position;

        public Vector2 velocity;

        protected bool standing = true;
        protected bool bJumpReady = true;
        public bool flip = false;//flip is to turn the sprite horizontaly

        protected Collision collision;

        #endregion

        #region Constructor

        public Character(Vector2 position, string filePath, Collision collision)
        {
            sprites = new List<Sprite>();
            string text = "";
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string line;
                    if ((line = sr.ReadLine()) == "$")
                    {
                        sprites.Add(new Sprite(text, position));
                        text = "";
                    }
                    else
                    {
                        text += line + "\n";
                    }
                }
            }
            this.position = position;

            size = sprites[0].GetSize();

            velocity.X = 0;
            velocity.Y = 0;

            this.collision = collision;

            trueHeight = (int)size.Y;

            Console.WriteLine(trueHeight);
        }

        //Without collision
        public Character(Vector2 position, string filePath)
        {
            sprites = new List<Sprite>();
            string text = "";
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string line;
                    if ((line = sr.ReadLine()) == "$")
                    {
                        sprites.Add(new Sprite(text, position));
                        text = "";
                    }
                    else
                    {
                        text += line + "\n";
                    }
                }
            }
            this.position = position;

            size = sprites[0].GetSize();

            velocity.X = 0;
            velocity.Y = 0;

            trueHeight = (int)size.Y;

            Console.WriteLine(trueHeight);
        }

        #endregion

        #region UpdateCollision

        public virtual void Update(GameTime gameTime)
        {
            //Sprite falling
            if (!standing)
            {
                velocity.Y += 500 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            //Move
            position.X += velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y += velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Collision detection
            bool collide = false;

            //Above
            if (velocity.Y < 0)
            {
                collide = collision.CollideAboveRect(this);//collision.CollideAbove(this);
                if (collide)
                {
                    velocity.Y = 0;

                    int line = (int)Math.Floor((position.Y + collision.Map.Size / 2) / collision.Map.Size);
                    position.Y = line * collision.Map.Size + 2;
                }
            }

            //Bellow
            if (standing || velocity.Y > 0)
            {
                collide = collision.CollideBellow(this);
                if (collide)
                {
                    standing = true;
                    velocity.Y = 0;

                    int line = (int)Math.Floor((position.Y + collision.Map.Size / 2) / collision.Map.Size);
                    position.Y = line * collision.Map.Size - (size.Y - collision.Map.Size) + 2;
                }
                else
                {
                    standing = false;
                }
            }

            collide = false;
            //on the right
            if (velocity.X > 0)
            {
                collide = collision.CollideRightRect(this);//collision.CollideRight(this);
            }
            //on the left
            else if (velocity.X < 0)
            {
                collide = collision.CollideLeftRect(this);//collision.CollideLeft(this);
            }
            if (collide)
            {
                velocity.X = 0;

                int col = (int)Math.Floor((position.X + collision.Map.Size / 2) / collision.Map.Size);
                position.X = (col) * collision.Map.Size;
            }

            collide = false;
        }

        //update fonction call when the player is dead
        public void UpdateDead(GameTime gameTime)
        {
            //When the player is dead everybody fall!!!!
            //Sprite falling
            velocity.X = 0;
            Console.WriteLine(collision.Map.GetTileAt(0 * 32, 17 * 32));
            if (!standing)
            {
                velocity.Y = 250 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                position += velocity;
            }

            //Bellow
            if (standing )
            {
                bool collide = collision.CollideBellow(this);
                if(!collide)
                {
                    Console.WriteLine("PLUS DEBOUT");
                    standing = false;
                }
            }
        }

        #endregion

        #region Draw

        public void Draw(GameTime gameTime)
        {
            anims[currentAnimation].Draw(position, flip);
        }

        #endregion
    }
}