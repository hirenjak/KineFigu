using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KineFigu
{
    /// <summary> 矩形クラス </summary>
    class Square
    {
        Texture2D texture;

        Vector2PLUS positionLT;   // 位置(LeftTop)
        Vector2PLUS positionRB;
        Vector2PLUS size;       // 大きさ
        Color color;
        

        public Square(Vector2PLUS position, Vector2PLUS size)
        {
            this.positionLT = position;
            this.positionRB = positionLT + size;
            this.size = size;

            this.color = Color.White;
        }

        public void Initilize()
        {

        }

        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Square");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                positionLT + (size / 2),
                color
                );
        }
    }
}
