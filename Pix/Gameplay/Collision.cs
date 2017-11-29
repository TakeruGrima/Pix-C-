using Microsoft.Xna.Framework;
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
        Rectangle collisionRect;//represents the object which collide with character
        bool topColide = false;//indicate that the collision is above the character

        #endregion

        #region Property

        public Rectangle CollisionRect
        {
            get
            {
                return collisionRect;
            }
        }

        public bool TopColide
        {
            get
            {
                return topColide;
            }
        }

        #endregion

        #region Constructor

        public Collision(Map map, List<Character> characters)
        {
            Map = map;
            Characters = characters;
        }

        #endregion

        #region CollideMap methods

        public bool CollideRightRect(Character c)
        {
            if (c.position.X + Map.Size >= Map.MapSizeInPixel.X)
            {
                collisionRect = new Rectangle((int)Map.MapSizeInPixel.X,
                    (int)c.position.Y, 4, Map.Size);

                return true;
            }

            //We use two position to check the collision to make sure that the player don't miss his jump
            //we can have our player between two tiles the up tile and down tile we test both 

            char id1 = Map.GetTileAt(c.position.X + Map.Size, c.position.Y + 3);//up tile
            char id2 = Map.GetTileAt(c.position.X + Map.Size, c.position.Y + Map.Size - 4);//down tile

            if (Map.IsSolid(id2))
            {
                if (Map.IsSolid(id1))
                {
                    //this condition is here to manage the collision between the character and a plateform at his top
                    //in our code a plateform is just a line

                    //we create a rectangle that correspond to the player
                    Rectangle characRect = CreateBounds(c);

                    //we create another rectangle for the plateform with a height of 3 to insure that the player don't
                    //pass through the plateform
                    Rectangle plateform = new Rectangle(Map.GetCol((int)c.position.X + Map.Size) * Map.Size,
                        Map.GetLine((int)c.position.Y + 3) * Map.Size, Map.Size, 5);

                    //we test if the two Rectangle don't intersects each other if they don't we return that
                    //the character don't collide
                    if (!characRect.Intersects(plateform))
                    {
                        return false;
                    }
                    else
                    {
                        //collision happend
                        //we set the collisionRect = characterrect + Map.Size but we increment the y coordinate 
                        collisionRect = characRect;
                        collisionRect.X += Map.Size;
                        collisionRect.Y = plateform.Bottom + 1;

                        topColide = true;
                        return true;
                    }
                }

                Console.WriteLine("CollisionID2");
                collisionRect = new Rectangle(Map.GetCol((int)(c.position.X + Map.Size)) * Map.Size,
                    Map.GetLine((int)c.position.Y + Map.Size - 4) * Map.Size, 4, Map.Size);
                return true;
            }
            if (Map.IsSolid(id1))
            {
                //this condition is here to manage the collision between the character and a plateform at his top
                //in our code a plateform is just a line

                //we create a rectangle that correspond to the player
                Rectangle characRect = CreateBounds(c);

                //we create another rectangle for the plateform with a height of 3 to insure that the player don't
                //pass through the plateform
                Rectangle plateform = new Rectangle(Map.GetCol((int)c.position.X + Map.Size) * Map.Size,
                    Map.GetLine((int)c.position.Y + 3) * Map.Size, Map.Size, 5);

                //we test if the two Rectangle don't intersects each other if they don't we return that
                //the character don't collide
                if (!characRect.Intersects(plateform))
                {
                    return false;
                }
                else
                {
                    //collision happend
                    //we set the collisionRect = characterrect + Map.Size but we increment the y coordinate 
                    collisionRect = characRect;
                    collisionRect.X += Map.Size;
                    collisionRect.Y = plateform.Bottom + 1;
                }

                Console.WriteLine("CollisionID1");
                return true;
            }
            return false;
        }

        public bool CollideLeftRect(Character c)
        {
            if (c.position.X <= 0)
            {
                collisionRect = new Rectangle(-4,
                    (int)c.position.Y, 4, Map.Size);

                return true;
            }
            //We use two position to check the collision to make sure that the player don't miss his jump
            //we can have our player between two tiles the up tile and down tile we test both 

            char id1 = Map.GetTileAt(c.position.X - 1, c.position.Y + 3);//up tile
            char id2 = Map.GetTileAt(c.position.X - 1, c.position.Y + Map.Size - 4);//down tile

            if (Map.IsSolid(id2))
            {
                if (Map.IsSolid(id1))
                {
                    //this condition is here to manage the collision between the character and a plateform at his top
                    //in our code a plateform is just a line

                    //we create a rectangle that correspond to the player
                    Rectangle characRect = CreateBounds(c);

                    //we create another rectangle for the plateform with a height of 3 to insure that the player don't
                    //pass through the plateform
                    Rectangle plateform = new Rectangle(Map.GetCol((int)c.position.X - 1) * Map.Size,
                        Map.GetLine((int)c.position.Y + 3) * Map.Size, Map.Size, 5);

                    //we test if the two Rectangle don't intersects each other if they don't we return that
                    //the character don't collide
                    if (!characRect.Intersects(plateform))
                    {
                        return false;
                    }
                    else
                    {
                        //collision happend
                        //we set the collisionRect = characterrect + Map.Size but we increment the y coordinate 
                        collisionRect = characRect;
                        collisionRect.X -= Map.Size;
                        collisionRect.Y = plateform.Bottom + 1;
                        collisionRect.Width = Map.Size;

                        topColide = true;
                        return true;
                    }
                }

                Console.WriteLine("CollisionID2");
                collisionRect = new Rectangle(Map.GetCol((int)c.position.X - 1) * Map.Size,
                    Map.GetLine((int)c.position.Y + Map.Size - 4) * Map.Size, Map.Size, Map.Size);
                return true;
            }
            if (Map.IsSolid(id1))
            {
                //this condition is here to manage the collision between the character and a plateform at his top
                //in our code a plateform is just a line

                //we create a rectangle that correspond to the player
                Rectangle characRect = CreateBounds(c);

                //we create another rectangle for the plateform with a height of 3 to insure that the player don't
                //pass through the plateform
                Rectangle plateform = new Rectangle(Map.GetCol((int)c.position.X - 1) * Map.Size,
                    Map.GetLine((int)c.position.Y + 3) * Map.Size, Map.Size, 5);

                //we test if the two Rectangle don't intersects each other if they don't we return that
                //the character don't collide
                if (!characRect.Intersects(plateform))
                {
                    return false;
                }
                else
                {
                    //collision happend
                    //we set the collisionRect = characterrect + Map.Size but we increment the y coordinate 
                    collisionRect = characRect;

                    Console.WriteLine(characRect);
                    collisionRect.X -= Map.Size;
                    collisionRect.Y = plateform.Bottom + 1;
                    collisionRect.Width = Map.Size;

                    Console.WriteLine(collisionRect);

                    topColide = true;
                    return true;
                }
            }
            return false;
        }

        public bool CollideAboveRect(Character c)
        {
            //We use two position to check the collision
            //we can have our player between two tiles the right tile and left tile we test both 

            char id1 = Map.GetTileAt(c.position.X + 1, c.position.Y - 1);//left tile
            char id2 = Map.GetTileAt(c.position.X + Map.Size - 2, c.position.Y - 1);//right tile

            if (Map.IsSolid(id1))
            {
                //this condition is here to manage the collision between the character and a plateform at his top
                //in our code a plateform is just a line

                //we create a rectangle that correspond to the player
                Rectangle characRect = CreateBounds(c);

                //we create another rectangle for the plateform with a height of 3 to insure that the player don't
                //pass through the plateform
                Rectangle plateform = new Rectangle(Map.GetCol((int)c.position.X + 1) * Map.Size,
                    Map.GetLine((int)c.position.Y - 1) * Map.Size, Map.Size, 5);

                //we test if the two Rectangle intersects each other if they do we return that
                //the character collide
                if (characRect.Intersects(plateform))
                {
                    return true;
                }
            }
            if (Map.IsSolid(id2))
            {
                //this condition is here to manage the collision between the character and a plateform at his top
                //in our code a plateform is just a line

                //we create a rectangle that correspond to the player
                Rectangle characRect = CreateBounds(c);

                //we create another rectangle for the plateform with a height of 5 to insure that the player don't
                //pass through the plateform
                Rectangle plateform = new Rectangle(Map.GetCol((int)c.position.X + Map.Size - 2) * Map.Size,
                    Map.GetLine((int)c.position.Y - 1) * Map.Size, Map.Size, 5);

                //we test if the two Rectangle intersects each other if they do we return that
                //the character collide
                if (characRect.Intersects(plateform))
                {
                    return true;
                }
            }
            return false;
        }

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
                if (c.position.Y - Map.Size / 4 - 1 > Map.Size * Map.GetLine((int)c.position.Y - 1))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool CollideLeft(Character c)
        {
            char id1 = Map.GetTileAt(c.position.X - 1, c.position.Y + 3);
            char id2 = Map.GetTileAt(c.position.X - 1, c.position.Y + Map.Size - 4);

            if (Map.IsSolid(id2))
            {
                return true;
            }
            if (Map.IsSolid(id1))
            {
                if (c.position.Y - Map.Size / 4 - 1 > Map.Size * Map.GetLine((int)c.position.Y - 1))
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
            char id2 = Map.GetTileAt(c.position.X + Map.Size - 2,
                c.position.Y + Map.Size + c.size.Y - Map.Size);

            if (Map.IsSolid(id1) || Map.IsSolid(id2))
                return true;
            return false;
        }

        public bool CollideAbove(Character c)
        {
            char id1 = Map.GetTileAt(c.position.X + 1, c.position.Y - 1);
            char id2 = Map.GetTileAt(c.position.X + Map.Size - 2, c.position.Y - 1);

            if (Map.IsSolid(id1))
            {
                if (c.position.Y - Map.Size / 4 - 1 <= Map.Size * (Map.GetLine((int)c.position.Y - 1)))
                {
                    return true;
                }
            }
            if (Map.IsSolid(id2))
            {
                if (c.position.Y - Map.Size / 4 - 1 <= Map.Size * (Map.GetLine((int)c.position.Y) - 1))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region CollideCharacters

        public bool CollideCharRight(Character c)
        {
            //we use rectangles to collide
            Rectangle characRect = CreateBounds(c);

            Rectangle rightRect = new Rectangle(characRect.Right, characRect.Y, 1, characRect.Height);

            foreach (Character curr in Characters)
            {
                Rectangle currRect = CreateBounds(curr);

                //we use the left boundary of the character to detect if the current character collide with
                //the left boundary
                Rectangle leftBound = new Rectangle(currRect.Left, currRect.Y, currRect.Width / 2, currRect.Height);

                if (currRect != characRect)
                {
                    if (rightRect.Intersects(leftBound))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CollideCharLeft(Character c)
        {
            //we use rectangles to collide
            Rectangle characRect = CreateBounds(c);

            Rectangle leftRect = new Rectangle(characRect.Left, characRect.Y, 1, characRect.Height);

            foreach (Character curr in Characters)
            {
                Rectangle currRect = CreateBounds(curr);

                //we use the right boundary of the character to detect if the current character collide with
                //the right boundary
                Rectangle rightBound = new Rectangle(currRect.Right - currRect.Width / 2,
                    currRect.Y, currRect.Width / 2, currRect.Height);

                if (currRect != characRect)
                {
                    if (leftRect.Intersects(rightBound))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CollideCharTop(Character c)
        {
            //we use rectangles to collide
            Rectangle characRect = CreateBounds(c);

            foreach (Character curr in Characters)
            {
                Rectangle currRect = CreateBounds(curr);

                //we use the bottom boundary of the character to detect if the current character collide with
                //the bottom boundary
                Rectangle bottomBound = new Rectangle(currRect.Left, currRect.Bottom, currRect.Width, 1);

                if (currRect != characRect)
                {
                    if (characRect.Intersects(bottomBound))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CollideCharBottom(Character c)
        {
            //we use rectangles to collide
            Rectangle characRect = CreateBounds(c);

            Rectangle bottomRect = new Rectangle(characRect.Left, characRect.Bottom - 16, characRect.Width, 16);

            foreach (Character curr in Characters)
            {
                Rectangle currRect = CreateBounds(curr);

                //we use the top boundary of the character to detect if the current character collide with
                //the top boundary
                Rectangle topBound = new Rectangle(currRect.Left, currRect.Top, currRect.Width, 1);

                if (currRect != characRect)
                {
                    if (bottomRect.Intersects(topBound))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CollideCharacters(Character c)//this character collide with others
        {
            //we use rectangles to collide
            Rectangle characRect = CreateBounds(c);

            if (c.state == State.ALIVE)
            {
                foreach (Character curr in Characters)
                {
                    Rectangle currRect = CreateBounds(curr);

                    if (currRect != characRect && curr.state == State.ALIVE)
                    {
                        if (characRect.Intersects(currRect))
                        {
                            return true;
                        }
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