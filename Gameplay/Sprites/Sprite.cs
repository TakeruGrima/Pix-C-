using Microsoft.Xna.Framework;
using Pix.MyCollectionTypes;
using Pix.PrimitiveForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pix.Gameplay.Sprites
{
    class Sprite
    {
        #region Field Region

        List2D<char> graph;//represent an image in a 2D array 
        Vector2 size;

        #endregion

        #region Constructor

        //create a sprite with a tab in text
        //Constructor for animation
        public Sprite(string text,Vector2 position)
        {
            graph = new List2D<char>();

            using (StringReader sr = new StringReader(text))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    List<char> lineList = new List<char>();

                    for (int i = 0; i < line.Length; i++)
                    {
                        lineList.Add(line[i]);
                    }
                    Console.WriteLine(line);

                    graph.Add(lineList);
                }
            }

            size.Y = graph.Count * 2;
            size.X = graph[0].Count * 2;
        }

        //Constructor for single sprite
        public Sprite(string path)
        {
            graph = new List2D<char>();

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    List<char> lineList = new List<char>();

                    for (int i = 0; i < line.Length; i++)
                    {
                        lineList.Add(line[i]);
                    }
                    Console.WriteLine(line);

                    graph.Add(lineList);
                }
            }

            size.Y = graph.Count * 2;
            size.X = graph[0].Count * 2;
        }
        #endregion

        #region Draw

        public void Draw(Vector2 position,bool flip,float opacity)
        {
            for (int j = 0; j < graph.Count; j++)
            {
                if(flip == false)
                {
                    for (int i = 0; i < graph[j].Count; i++)
                    {
                        if (graph[j, i] == '1')
                        {
                            PrimitivGraphics.Instance.DrawPoint((int)position.X + i * 2,
                                (int)position.Y + j * 2, 2, Color.White,opacity);
                        }
                        else if (graph[j, i] == '2')
                        {
                            PrimitivGraphics.Instance.DrawPoint((int)position.X + i * 2,
                                (int)position.Y + j * 2, 2, Color.Black,1);
                        }
                    }
                }
                else
                {
                    for (int i = graph[j].Count-1; i >=0; i--)
                    {
                        if (graph[j, i] == '1')
                        {
                            PrimitivGraphics.Instance.DrawPoint((int)position.X + ((graph[j].Count-1)*2 -i * 2),
                                (int)position.Y + j * 2, 2, Color.White,opacity);
                        }
                        else if (graph[j, i] == '2')
                        {
                            PrimitivGraphics.Instance.DrawPoint((int)position.X + ((graph[j].Count-1) * 2 - i * 2),
                                (int)position.Y + j * 2, 2, Color.Black,1);
                        }
                    }
                }
            }
        }

        #endregion

        #region methods

        public Vector2 GetSize()
        {
            return size;
        }

        #endregion
    }
}
