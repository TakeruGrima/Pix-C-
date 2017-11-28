using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pix.Gameplay.Sprites;
using Pix.PrimitiveForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pix.Gameplay
{
    /// <summary>
    /// Level Map this class load, display and manage all things on a map ( Map, Player, NPC, and others)
    /// </summary>
    class Stage
    {
        #region Field

        Map Map;
        List<Character> Characters;
        Player player;
        Collision Collision;//class to manage collision between map and character and character to character
        string levelName;//Name of the stage

        int effaceX = 0;//x coordinate to clear the screen

        SpriteFont gameOverFont;
        Vector2 gameOverPos;

        public bool gameOver = false;

        #endregion

        #region Constructor

        public Stage(string path, string levelName)//path is the path of the file map
        {
            this.levelName = levelName;

            Map = new Map("Map/Map1.txt");

            Characters = new List<Character>();
            Collision = new Collision(Map, Characters);

            player = new Player(Map.PlayerPosition, "Sprites/player.txt", Collision);

            Characters.Add(player);
            foreach (Vector2 pos in Map.NpcPosition)
            {
                Characters.Add(new Npc(pos, "Sprites/ghost.txt", Collision));
            }
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            if (player.state != State.DEAD)
            {
                foreach (Character character in Characters)
                {
                    character.Update(gameTime);
                }
            }

            if (player.state == State.DEAD)
            {
                player.flip = true;

                foreach (Character character in Characters)
                {
                    character.UpdateDead(gameTime);
                }
            }
        }

        #endregion

        #region Content

        public void LoadContet(ContentManager content)
        {
            gameOverFont =content.Load<SpriteFont>("Fonts/GameOver");

            Vector2 dimension = gameOverFont.MeasureString("Game Over");

            gameOverPos.X = 800 / 2 - (int)dimension.X / 2;
            gameOverPos.Y = 600 / 2 - (int)dimension.Y / 2;
        }

        #endregion

        #region Draw

        public void Draw(GameTime gameTime)
        {
            if (effaceX <= 800)
            {
                Map.Draw(gameTime);
                if (player.state == State.DEAD)
                {
                    for (int i = 0; i < effaceX; i = i + 4)
                    {
                        PrimitivGraphics.Instance.DrawLine(i, 0, "Vertical", 4, 600, Color.Black);
                    }
                    if ((effaceX+4) % Map.Size == 0)
                    {
                        Map.ClearTileMap(effaceX / 32);
                    }
                    effaceX += 4;
                    PrimitivGraphics.Instance.SpriteBatch.DrawString(gameOverFont,
                    "Game Over", gameOverPos, Color.White);
                }
                foreach (Character character in Characters)
                {
                    if (character == player)
                    {
                        player.Draw(gameTime);
                    }
                    else
                        character.Draw(gameTime);
                }
            }
            else
            {
                gameOver = true;
            }
        }

        #endregion
    }
}