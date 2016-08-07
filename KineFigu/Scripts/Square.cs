﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KineFigu
{
    /// <summary> 矩形クラス </summary>
    class Square : Figure
    {
        public Square(Vector2PLUS initPosi, Vector2PLUS size) : base(initPosi, size)
        {
            texture = Content.Load<Texture2D>("Square");
        }
    }
}
