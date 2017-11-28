using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pix.PrimitiveForms
{
    public class PrimitivGraphics
    {
        #region field region

        public static PrimitivGraphics Instance;
        public Texture2D texture;
        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;

        #endregion

        #region Constructor

        public PrimitivGraphics()
        {

        }

        #endregion

        #region Content region

        public void LoadContent()
        {
            texture = new Texture2D(GraphicsDevice, 1, 1);
        }

        public void UnloadContent()
        {
            texture.Dispose();
        }

        #endregion

        #region Draw

        public void DrawPoint(int x, int y, int scale, Color color, float opacity)
        {
            texture.SetData(new[] { color });
            SpriteBatch.Draw(texture, new Rectangle(x, y, 1 * scale, 1 * scale), color * opacity);

            texture.SetData(new[] { Color.White });
        }

        public void DrawLine(int x, int y, string direction, int scale, int size, Color color)
        {
            texture.SetData(new[] { color });
            if (direction == "Horizontal")
            {
                SpriteBatch.Draw(texture, new Rectangle(x, y, size * scale, 1 * scale), color);
            }
            else if (direction == "Vertical")
            {
                SpriteBatch.Draw(texture, new Rectangle(x, y, 1 * scale, size * scale), color);
            }

            texture.SetData(new[] { Color.White });
        }

        #endregion
    }
}