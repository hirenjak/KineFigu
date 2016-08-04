using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KineFigu
{
    /// <summary> 本体部分 </summary>
    class MainScene
    {
        List<Square> square;

        List<Figure> figures;

        public MainScene()
        {
            figures = new List<Figure>();

            square = new List<Square>();


            square.Add(new Square(new Vector2PLUS(100, 100), new Vector2PLUS(50, 50)));
        }

        private void FiguresSet()
        {
            figures.Clear();
            figures.AddRange(square);
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
        public void Logic()
        {
            FiguresSet();
        }

        /// <summary> 描画処理 </summary>
        public void Draw(SpriteBatch sBatch)
        {
            foreach (var value in figures) { value.Draw(sBatch); }
        }
    }
}
