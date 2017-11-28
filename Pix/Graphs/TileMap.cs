using Microsoft.Xna.Framework;
using Pix.MyCollectionTypes;
using Pix.PrimitiveForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pix.Graphs
{
    class TileMap
    {
        #region field region

        List2D<char> tileMap;
        int size;//size of the tile

        Vector2 playerPosition;//position of the player at the start
        List<Vector2> npcPosition = new List<Vector2>();

        #endregion

        #region Property

        public int Size
        {
            get
            {
                return size;
            }
        }

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

        public TileMap(string path,int size)
        {
            this.size = size;
            tileMap = new List2D<char>();

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    List<char> lineList = new List<char>();

                    for (int i = 0; i < line.Length; i++)
                    {
                        lineList.Add(line[i]);

                        if(line[i] =='P')//indicate that the player start here
                        {
                            playerPosition = new Vector2(i * Size, tileMap.Count * Size);
                        }
                        if(line[i] == '@')
                        {
                            npcPosition.Add(new Vector2(i * Size, tileMap.Count * Size));
                        }
                    }

                    tileMap.Add(lineList);
                }
            }
        }

        #endregion

        #region methods

        //getTile with x and y coordinate
        public char getTileAt(int x,int y)
        {
            int l = (int)Math.Floor((float)y / Size);
            int c = (int)Math.Floor((float)x / Size);

            if (l < 0)
                return '0';
            if(l>=0 && c>=0 && l< tileMap.Count*size && c<tileMap[0].Count*size)
                return tileMap[l, c];
            return '1';
        }

        //Set a column of the tileMap to 0
        public void ClearTileMap(int i)
        {
            foreach (List<char> list in tileMap)
            {
                int id = tileMap.IndexOf(list);
                tileMap[id, i] = '0';
            }
        }

        #endregion

        #region Draw

        public void Draw(GameTime gameTime)
        {
            for (int j = 0; j < tileMap.Count; j++)
            {
                for (int i = 0; i < tileMap[j].Count; i++)
                {
                    if(tileMap[j,i] == '1')
                    {
                        PrimitivGraphics.Instance.DrawLine(i * Size, j * Size, "Horizontal", 2, 16, Color.White);
                    }
                }
            }
        }

        #endregion
    }
}
