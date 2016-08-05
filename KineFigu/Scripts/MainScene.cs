using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
        }

        /// <summary> 図形クラスを纏める </summary>
        private void FiguresSet()
        {
            figures.Clear();
            figures.AddRange(squares);
            figures.AddRange(circles);
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

        /// <summary> 計算処理 </summary>
        public void Logic(Vector2PLUS leftHandPosi, Vector2PLUS rightHandPosi)
        {
            leftHnadPoint.

            FiguresSet();
        }

        /// <summary> 描画処理 </summary>
        public void Draw(SpriteBatch sBatch)
        {
            foreach (var value in figures) { value.Draw(sBatch); }
        }
    }
}
