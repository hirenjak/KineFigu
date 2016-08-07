using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
        Dot[] rightHnadPoint;

        // 纏める用
        List<Figure> figures;

        SpriteFont sFont;

        public MainScene()
        {
            figures = new List<Figure>();

            squares = new List<Square>();
            circles = new List<Circle>();

            squares.Add(new Square(new Vector2PLUS(100, 100), new Vector2PLUS(50, 50)));
            circles.Add(new Circle(new Vector2PLUS(200, 100), new Vector2PLUS(50, 50)));
            leftHnadPoint = new Dot(new Vector2PLUS(), new Vector2PLUS(30, 30));
            rightHnadPoint = new Dot[10];
            for (int ID = 0; ID < 10; ID++)
            {
                rightHnadPoint[ID] = new Dot(new Vector2PLUS(), new Vector2PLUS(10, 10));
            }
        }

        /// <summary> 図形クラスを纏める </summary>
        private void FiguresSet()
        {
            figures.Clear();
            figures.AddRange(squares);
            figures.AddRange(circles);
            figures.Add(leftHnadPoint);
            figures.AddRange(rightHnadPoint);
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

            sFont = Content.Load<SpriteFont>("Arial");
        }
        

        Vector2PLUS[] rightHandPosi;
        /// <summary> 計算処理 </summary>
        public void Logic(Vector2PLUS screenSize, Vector2PLUS leftHandPosi, Vector2PLUS[] rightHandPosi)
        {
            leftHnadPoint.Set_Position(PosiConverter(new Vector2PLUS(leftHandPosi.X, leftHandPosi.Y * -1), new Vector2PLUS(2.0f, 2.0f),screenSize));//new Vector2PLUS(leftHandPosi.X, leftHandPosi.Y * -1) * 500 + new Vector2PLUS(400, 200));

            var tempPosiSum =  rightHandPosi[0] + rightHandPosi[1] + rightHandPosi[2];

            rightHandPosi[0] = tempPosiSum / 3;

            for(int ID = 0; ID < 10; ID++)
            {
                rightHnadPoint[ID].Set_Position(new Vector2PLUS(rightHandPosi[ID].X, rightHandPosi[ID].Y * -1) * 500 + new Vector2PLUS(400, 200));
            }

            this.rightHandPosi = rightHandPosi;



            // 図形クラスをまとめる処理
            FiguresSet();
        }

        private Vector2PLUS PosiConverter(Vector2PLUS target, Vector2PLUS targetMaxSize, Vector2PLUS screenSize)
        {
            return new Vector2PLUS(target.X / targetMaxSize.X * screenSize.X, target.Y / targetMaxSize.Y * screenSize.Y) + (screenSize / 2);
        }

        /// <summary> 描画処理 </summary>
        public void Draw(SpriteBatch sBatch)
        {
            foreach (var value in figures) { value.Draw(sBatch); }

            for (int ID = 0; ID < 10; ID++)
            {
                sBatch.DrawString(
                    sFont,
                    rightHandPosi[ID].X + " : " + rightHandPosi[ID].Y,
                    new Vector2(0, ID * 30),
                    Color.White
                    );
            }
        }
    }
}
