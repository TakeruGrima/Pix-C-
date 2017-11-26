﻿using Microsoft.Xna.Framework;
using Pix.Gameplay.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pix.Gameplay
{
    //class to manage collision to sprites and map
    class Collision
    {
        #region Field Region

        public Map Map;//Stock the actual map
        List<Character> Characters;//list of characters

        #endregion

        #region Constructor

        public Collision(Map map ,List<Character> characters)
        {
            Map = map;
            Characters = characters;
        }

        #endregion

        #region CollideMap methods

        public bool CollideRight(Character c)
        {
            char id1 = Map.GetTileAt(c.position.X + Map.Size, c.position.Y + 3);
            char id2 = Map.GetTileAt(c.position.X + Map.Size, c.position.Y + Map.Size - 4);

            if (Map.IsSolid(id2))
            {
                return true;
            }
            if (Map.IsSolid(id1))
             {
                 if(c.position.Y - Map.Size/4 -1 > Map.Size* Map.GetLine((int)c.position.Y -1))
                 {
                     return false;
                 }
                 return true;
             }
             return false;
        }

        public bool CollideLeft(Character c)
        {
            char id1 = Map.GetTileAt(c.position.X -1, c.position.Y + 3);
            char id2 = Map.GetTileAt(c.position.X -1, c.position.Y + Map.Size - 4);

            if (Map.IsSolid(id2))
            {
                return true;
            }
            if (Map.IsSolid(id1))
            {
                if (c.position.Y - Map.Size / 4-1 > Map.Size * Map.GetLine((int)c.position.Y-1))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool CollideBellow(Character c)
        {
            char id1 = Map.GetTileAt(c.position.X + 1, c.position.Y + Map.Size + c.size.Y - Map.Size);
            char id2 = Map.GetTileAt(c.position.X + Map.Size -2, 
                c.position.Y + Map.Size + c.size.Y - Map.Size);

            if (Map.IsSolid(id1) || Map.IsSolid(id2))
                return true;
            return false;
        }

        public bool CollideAbove(Character c)
        {
            char id1 = Map.GetTileAt(c.position.X + 1, c.position.Y-1);
            char id2 = Map.GetTileAt(c.position.X + Map.Size - 2, c.position.Y-1);

            if(Map.IsSolid(id1))
            {
                if(c.position.Y - Map.Size/4 -1<= Map.Size * (Map.GetLine((int)c.position.Y-1)))
                {
                    return true;
                }
            }
            if (Map.IsSolid(id2))
            {
                if (c.position.Y -Map.Size/4 -1 <= Map.Size * (Map.GetLine((int)c.position.Y)-1))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region CollideCharacters

        public bool CollideCharacters(Character c)//this character collide with others
        {
            //we use rectangles to collide
            Rectangle characRect = CreateBounds(c);

            foreach (Character curr in Characters)
            {
                Rectangle currRect = CreateBounds(curr);

                if(currRect!= characRect)
                {
                    if(characRect.Intersects(currRect))
                    {
                        Console.WriteLine("COLLIDE");
                        return true;
                    }
                }
            }
            return false;
        }

        public Rectangle CreateBounds(Character c)
        {
            return new Rectangle((int)c.position.X,
                (int)c.position.Y + (int)(c.size.Y - c.trueHeight),
                (int)c.size.X, (int)c.trueHeight);
        }
        
        #endregion
    }
}