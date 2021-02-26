using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DragonSwapF
{
    class Tile
    {
        //Graphics variables 
        private Texture2D texture;
        public static ContentManager Cm;
        private ContentManager contentManager;
        public void setContentManager(ContentManager cm)
        {
            contentManager = cm;
        }
        public void setTexture(string imageName)
        {
            texture = contentManager.Load<Texture2D>(imageName);
        }


        //Tile variables  
      public Vector2 m_Pos { get; set; }
      public int m_Num { get; set; }
      public int m_Width { get; set; }
      public int m_Height { get; set; }
      public bool m_Movable { get; set; }
      public Rectangle rect;
      

      //Tile properties 
      public Tile(int Num, int width, int height, bool movable)
        {
            m_Num = Num;
            m_Width = width;
            m_Height = height;
            m_Movable = movable;

        }
        //Tile Directions
        private enum m_Direction
        {
            Up,
            Down,
            Left,
            Right
        }
        //Tile spritebatch funtion
        public void Draw(SpriteBatch spriteBatch)
        {
            rect = new Rectangle();
            rect.X = (int)(m_Pos.X - m_Width * 0.5f);
            rect.Y = (int)(m_Pos.Y - m_Height * 0.5f);
            rect.Width = m_Width;
            rect.Height = m_Height;

            spriteBatch.Draw(texture, rect, Color.White);
            
        }

    }
}
