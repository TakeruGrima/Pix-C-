using Microsoft.Xna.Framework;
using Pix.Gameplay.Sprites;
using Pix.Graphs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pix.Gameplay
{
    class Map
    {
        #region field region

        TileMap tilemap;
        public int Size;
        Vector2 playerPosition;
        List<Vector2> npcPosition = new List<Vector2>();

        #endregion

        #region Property

        public Vector2 PlayerPosition
        {
            get
            {
                return playerPosition;
            }
        }

        public List<Vector2> NpcPosition
        {
            get
            {
                return npcPosition;
            }
        }

        #endregion

        #region Constructor

        public Map(string path)//path of the map file
        {
            Size = 32;
            tilemap = new TileMap(path,Size);

            playerPosition = tilemap.PlayerPosition;
            npcPosition = tilemap.NpcPosition;
        }

        #endregion

        #region methods

        //getTile with x and y coordinate
        public char GetTileAt(float x,float y)
        {
            return tilemap.getTileAt((int)x, (int)y);
        }


        public bool IsSolid(char pID)
        {
            if (pID == '1') return true;
            if (pID == '0') return false;
            return false;
        }

        public int GetLine(int y)
        {
            return (int)Math.Floor((float)y / Size);
        }

        public int GetCol(int x)
        {
            return (int)Math.Floor((float)x / Size);
        }

        #endregion

        #region Draw

        public void Draw(GameTime gameTime)
        {
            tilemap.Draw(gameTime);
        }

        #endregion
    }
}
