using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pix.Gameplay.Sprites;
using Pix.PrimitiveForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pix
{
    class TitleScreen
    {
        #region Field region

        Vector2 positionLine;
        int displayX = 0;

        private SpriteFont font;
        SpriteFont pressStartFont;

        Vector2 dimensionStart;
        Vector2 positionStart;
        Vector2 dimensionText;
        Vector2 positionText;

        //variable for fadeEffect
        float FadeSpeed;
        float alpha = 0;
        float alphaPress = 0;//alpha for the pressStart
        bool Increase;

        Player player;//the player is use for the animated scene

        public bool play = false;//is true when the player want to play and press Enter
        
        #endregion

        #region Constructor

        public TitleScreen()
        {
            FadeSpeed = 0.1f;
        }

        #endregion

        #region Content

        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/Title");

            dimensionText = font.MeasureString("Pix");

            positionText = new Vector2(800 / 2 - dimensionText.X / 2, 600 / 4 - dimensionText.Y / 4);

            pressStartFont = content.Load<SpriteFont>("Fonts/PressStart");

            dimensionStart = pressStartFont.MeasureString("Press Start");
            positionStart = new Vector2(800 / 2 - (int)dimensionStart.X / 2, 
                600 - 600 / 4 - dimensionStart.Y / 4);

            positionLine = new Vector2(0,positionStart.Y - 32);

            player = new Player(new Vector2(positionLine.X,positionLine.Y-46), "Sprites/player.txt");
            Console.WriteLine(player.ToString());
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            if( alpha <1f)
            {
                alpha += FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (alpha > 1f)
                {
                    alpha = 1.0f;
                }
            }
            if(alpha>0.5f)
            {
                FadeSpeed = 1;
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    play = true;
                }
                if (!Increase)
                    alphaPress -= FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    alphaPress += FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (alphaPress < 0.0f)
                {
                    Increase = true;
                    alphaPress = 0.0f;
                }
                else if (alphaPress > 1.0f)
                {
                    Increase = false;
                    alphaPress = 1.0f;
                }
                if (player.position.X < 800/2 - player.size.X/2)
                {
                    player.velocity.X = 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    player.position += player.velocity;
                }

                player.anims["move"].Update(gameTime);
            }
        }

        #endregion

        #region Draw

        public void Draw(GameTime gameTime)
        {
            PrimitivGraphics.Instance.SpriteBatch.DrawString(font, "Pix", positionText,Color.White * alpha);

            if(alpha > 0.5f )
            {
                PrimitivGraphics.Instance.SpriteBatch.DrawString(pressStartFont, "Press Start", 
                    positionStart, Color.White * alphaPress);
                PrimitivGraphics.Instance.DrawLine(0, (int)positionLine.Y, "Horizontal", 2, displayX, Color.White);
                if (displayX <= 800)
                {
                    displayX += 2;
                }
                player.Draw(gameTime);
            }
        }

        #endregion
    }
}
