using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineFigu
{
    /// <summary> 矩形クラス </summary>
    class Square
    {
        Vector2PLUS positionLT;   // 位置(LeftTop)
        Vector2PLUS positionRB;
        Vector2PLUS size;       // 大きさ

        public Square(Vector2PLUS position, Vector2PLUS size)
        {
            this.positionLT = position;
            this.positionRB = positionLT + size;
            this.size = size;
        }
    }
}
