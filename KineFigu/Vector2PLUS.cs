using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace KineFigu
{
    /// <summary> ユーザー定義のベクトル構造体 </summary>
    struct Vector2PLUS
    {
        public float X;
        public float Y;

        public Vector2PLUS(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static implicit operator Vector2(Vector2PLUS target) { return new Vector2(target.X, target.Y); }

        public static Vector2PLUS operator +(Vector2PLUS target, Vector2PLUS other) { return new Vector2PLUS(target.X + other.X, target.Y + other.Y); }
        public static Vector2PLUS operator -(Vector2PLUS target, Vector2PLUS other) { return new Vector2PLUS(target.X - other.X, target.Y - other.Y); }

        public static Vector2PLUS operator *(Vector2PLUS target, float other) { return new Vector2PLUS(target.X * other, target.Y * other); }
        public static Vector2PLUS operator /(Vector2PLUS target, float other) { if (other == 0) { return new Vector2PLUS(0, 0); } return new Vector2PLUS(target.X / other, target.Y / other); }
    }
}
