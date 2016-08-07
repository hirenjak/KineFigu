using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineFigu
{
    class Circle : Figure
    {
        public Circle(Vector2PLUS initPosi, Vector2PLUS size) : base(initPosi, size)
        {
            texture = Content.Load<Texture2D>("Circle");
        }
        
    }
}
