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

        public Vector2 MapSizeInPixel
        {
            get
            {
                return tilemap.MapSizeInPixel;
            }
        }

        #endregion

        #region Constructor

        public Map(string path)//path of the map file
        {
            Size = 32;
            tilemap = new TileMap(path, Size);

            playerPosition = tilemap.PlayerPosition;
            npcPosition = tilemap.NpcPosition;
        }

        #endregion

        #region methods

        //getTile with x and y coordinate
        public char GetTileAt(float x, float y)
        {
            return tilemap.getTileAt((int)x, (int)y);
        }


        public bool IsSolid(char pID)
        {
            if (pID == '1') return true;
            if (pID == '0') return false;
            return false;
        }

        public int GetLine(float y)
        {
            return (int)Math.Floor(y / Size);
        }

        public int GetCol(float x)
        {
            return (int)Math.Floor(x / Size);
        }

        //Set a column of the tileMap to 0
        public void ClearTileMap(int i)
        {
            tilemap.ClearTileMap(i);
        }

        #endregion

        #region Draw

        public void Draw(GameTime gameTime,Camera camera)//camera determine what part of the map to draw
        {
            tilemap.Draw(gameTime,camera);
        }

        #endregion
    }
}