using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Input;

using System.IO;

namespace KineFigu
{
    /// <summary> 本体部分 </summary>
    class MainScene
    {
        // 矩形
        List<Square> squares;

        // 円形
        List<Circle> circles;

        Dot leftHnadPoint;
        Dot rightHnadPoint;

        // 纏める用
        List<Figure> figures;

        public MainScene()
        {
            figures = new List<Figure>();

            squares = new List<Square>();
            circles = new List<Circle>();

            squares.Add(new Square(new Vector2PLUS(100, 100), new Vector2PLUS(50, 50)));
            circles.Add(new Circle(new Vector2PLUS(200, 100), new Vector2PLUS(50, 50)));
            leftHnadPoint = new Dot(new Vector2PLUS(), new Vector2PLUS(30, 30));
            rightHnadPoint = new Dot(new Vector2PLUS(), new Vector2PLUS(30, 30));

        }

        /// <summary> 図形クラスを纏める </summary>
        private void FiguresSet()
        {
            figures.Clear();
            figures.AddRange(squares);
            figures.AddRange(circles);
            figures.Add(leftHnadPoint);
            figures.Add(rightHnadPoint);
        }

        /// <summary> 初期化処理 </summary>
        public void Initialize()
        {

        }

        /// <summary> 読み込み処理 </summary>
        public void Load(ContentManager Content)
        {
            FiguresSet();
            foreach(var value in figures) { value.Load(Content); }
        }

        int count = 0;


        /// <summary> 計算処理 </summary>
        public void Logic(Vector2PLUS leftHandPosi, Vector2PLUS rightHandPosi)
        {
            leftHnadPoint.Set_Position(new Vector2PLUS(leftHandPosi.X, leftHandPosi.Y * -1) * 500 + new Vector2PLUS(400, 200));
            rightHnadPoint.Set_Position(new Vector2PLUS(rightHandPosi.X, rightHandPosi.Y * -1) * 500 + new Vector2PLUS(400, 200));
            

            FiguresSet();
        }

        /// <summary> 描画処理 </summary>
        public void Draw(SpriteBatch sBatch)
        {

            foreach (var value in figures) { value.Draw(sBatch); }
        }
    }
}
