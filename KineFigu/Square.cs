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
    class Square : Figure
    {
        public Square(Vector2PLUS initPosi, Vector2PLUS size) : base("Square", initPosi, size)
        {
        }
        
    }
}
