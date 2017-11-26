﻿using System;
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
        float invinsible = 2;
        float countinvisible=0;

        #endregion

        #region Constructor

        public Player(Vector2 position, string filePath,Collision collision) : 
            base(position, filePath,collision)
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

            position.Y = position.Y - (size. Y - collision.Map.Size);

            life = new Life(3, new Vector2(0,600 - 32));
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            int accel = 500;
            int friction = 280;
            int maxSpeed = 150;
            int jumpVelocity = -240;

            if(countinvisible>0)
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


            if (!Keyboard.GetState().IsKeyDown(Keys.Up) && !bJumpReady)
            {
                bJumpReady = true;
            }
            
            //Sprite falling
            if(!standing)
            {
                velocity.Y += 500 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }


            if (collision.CollideCharacters(this))
            {
                if(velocity.X>0)
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
                }

                if(life.value == 0)
                {
                    state = State.DEAD;
                }
            }

            base.Update(gameTime);

            anims[currentAnimation].Update(gameTime);
        }

        #endregion

        #region Draw

        public new void Draw(GameTime gameTime)
        {
            anims[currentAnimation].Draw(position, flip);

            life.Draw(gameTime);
        }

        #endregion

        #region methods



        #endregion
    }
}