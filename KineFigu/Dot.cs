using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineFigu
{
    class Dot : Figure
    {
        static Dot()
        {
        }

        public Dot(Vector2PLUS initPosi, Vector2PLUS size): base(initPosi, size)
        {
            texture = Content.Load<Texture2D>("Dot");
        }
    }
}
