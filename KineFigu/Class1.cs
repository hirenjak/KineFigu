using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineFigu
{
    /// <summary>
    /// 衝突チェック(Ohyama)
    /// </summary>
    static class CollisionCheck
    {
        /// <summary>
        /// 矩形同士の衝突判定(Ohyama)
        /// </summary>
        /// <param name="ATL">BoxAの左上の座標</param>
        /// <param name="ABR">BoxAの右下の座標</param>
        /// <param name="BTL">BoxBの左上の座標</param>
        /// <param name="BBR">BoxBの右下の座標</param>
        /// <returns></returns>
        public static bool BoxToBox(Vector2PLUS ATL, Vector2PLUS ABR, Vector2PLUS BTL, Vector2PLUS BBR)
        {
            //衝突していなければ true / 衝突していれば false を返す
            if (ABR.X < BTL.X || ABR.Y < BTL.Y || ATL.X > BBR.X || ATL.Y > BBR.Y)
                return false;
            else
                return true;
            
        }

        /// <summary>
        ///  円同士の衝突判定(Ohyama)
        /// </summary>
        /// <param name="positionA">円Aの中心座標</param>
        /// <param name="positionB">円Bの中心座標</param>
        /// <param name="radiusA">円Aの半径</param>
        /// <param name="radiusB">円Bの半径</param>
        /// <returns></returns>
        public static bool CircleToCircle(Vector2PLUS positionA, Vector2PLUS positionB, int radiusA, int radiusB) 
        {
            float tempDistanceX = (positionA.X - positionB.X) * (positionA.X - positionB.X);
            float tempDistanceY = (positionA.Y - positionB.Y) * (positionA.Y - positionB.Y);
            float tmepDistance = tempDistanceX + tempDistanceY;
            float tempRadiusSum = (radiusA + radiusB) * (radiusA + radiusB);

            if (tempDistanceY < tempRadiusSum)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 円と矩形の衝突判定(Ohyama)
        /// </summary>
        /// <param name="TL">矩形の左上の座標</param>
        /// <param name="BR">矩形の右下の座標</param>
        /// <param name="CirclePosition">円の中心座標</param>
        /// <param name="radius">円の半径</param>
        /// <returns></returns>
        public static bool BoxToCircle(Vector2PLUS TL, Vector2PLUS BR, Vector2PLUS CirclePosition, int radius)
        {
            if (CirclePosition.Y + radius < TL.Y || CirclePosition.X + radius < TL.X ||
                CirclePosition.Y - radius > BR.Y || CirclePosition.X - radius > BR.X)
                return false;
            else
                return true;
        }
    }
    
}
