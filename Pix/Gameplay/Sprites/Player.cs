using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pix.Input;
using Microsoft.Xna.Framework.Input;

namespace Pix.Gameplay.Sprites
{
    class Player : Character
    {
        #region Field region

        Life life;
        float invinsible = 1;
        float countinvisible = 0;

        Camera camera;//we use a camera to follow the player

        #endregion

        #region Property Region

        public Camera Camera
        {
            get { return camera; }
        }

        #endregion

        #region Constructor

        public Player(Vector2 position, string filePath, Collision collision) :
            base(position, filePath, collision)
        {
            anims = new Dictionary<string, Animation>();

            Animation idle = new Animation();
            idle.Add(sprites[0]);

            anims.Add("idle", idle);

            Animation move = new Animation();
            move.Add(sprites[0]);
            move.Add(sprites[2]);
            move.Add(sprites[3]);
            move.Add(sprites[4]);
            move.Add(sprites[3]);
            move.Add(sprites[2]);
            move.Add(sprites[0]);

            anims.Add("move", move);

            Animation jump = new Animation();
            jump.Add(sprites[7]);

            anims.Add("jump", jump);

            currentAnimation = "idle";

            position.Y = position.Y - (size.Y - collision.Map.Size);

            life = new Life(3, new Vector2(0, 600 - 32));

            camera = new Camera(new Rectangle(0, 0, 800, 600),collision.Map);
        }

        //without collision
        public Player(Vector2 position, string filePath) :
            base(position, filePath)
        {
            anims = new Dictionary<string, Animation>();

            Animation idle = new Animation();
            idle.Add(sprites[0]);

            anims.Add("idle", idle);

            Animation move = new Animation();
            move.Add(sprites[0]);
            move.Add(sprites[2]);
            move.Add(sprites[3]);
            move.Add(sprites[4]);
            move.Add(sprites[3]);
            move.Add(sprites[2]);
            move.Add(sprites[0]);

            anims.Add("move", move);

            Animation jump = new Animation();
            jump.Add(sprites[7]);

            anims.Add("jump", jump);

            currentAnimation = "move";

            Console.WriteLine("TOI MON BEAU TOURNESOL!");

            life = new Life(0, new Vector2(0, 600 - 32));

            camera = new Camera(new Rectangle(0, 0, 800, 600));
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            int accel = 500;
            int friction = 280;
            int maxSpeed = 150;
            int jumpVelocity = -230;

            if (countinvisible > 0)
            {
                countinvisible -= (float)(gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (velocity.X > 0)
            {
                velocity.X = velocity.X - friction * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (velocity.X < 0)
                {
                    velocity.X = 0;
                }
            }
            else if (velocity.X < 0)
            {
                velocity.X = velocity.X + friction * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (velocity.X > 0)
                {
                    velocity.X = 0;
                }
            }
            currentAnimation = "idle";
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X += accel * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (velocity.X > maxSpeed)
                {
                    velocity.X = maxSpeed;
                }
                currentAnimation = "move";
                flip = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X -= accel * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (velocity.X < -maxSpeed)
                {
                    velocity.X = -maxSpeed;
                }
                currentAnimation = "move";
                flip = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && standing && bJumpReady)
            {
                velocity.Y += jumpVelocity;
                standing = false;
                bJumpReady = false;
                currentAnimation = "jump";
            }
            if (standing == false)
            {
                currentAnimation = "jump";
            }

            if(velocity!= Vector2.Zero)
            {
                camera.LockToSprite(this);
            }


            if (!Keyboard.GetState().IsKeyDown(Keys.Up) && !bJumpReady)
            {
                bJumpReady = true;
            }

            bool collide = false;

            if (collision.CollideCharacters(this))
            {
                collide = true;
            }

            if (countinvisible <= 0)
            {
                state = State.ALIVE;
            }

            if (collide)
            {
                if (state == State.ALIVE)
                {
                    if (velocity.X > 0)
                    {
                        velocity.X = -velocity.X;
                    }
                    else if (velocity.X < 0)
                    {
                        velocity.X = -velocity.X;
                    }

                    if (countinvisible <= 0)
                    {
                        life.value--;
                        countinvisible = invinsible;
                        state = State.HURT;
                    }

                    if (life.value == 0)
                    {
                        state = State.DEAD;
                    }
                }
            }

            base.Update(gameTime);

            anims[currentAnimation].Update(gameTime);
        }

        #endregion

        #region Draw

        public new void Draw(GameTime gameTime)
        {
            if (state != State.HURT)
            {
                anims[currentAnimation].Draw(camera,position, flip);
            }
            else if (state == State.HURT)
            {
                anims[currentAnimation].DrawSemiTransparent(camera,position, flip);
            }

            life.Draw(gameTime);
        }

        #endregion

        #region methods



        #endregion
    }
}