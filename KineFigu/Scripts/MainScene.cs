using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System;

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

            direction = new float[10];
            for (int ID = 0; ID < 10; ID++)
            {
                rightHnadPoint[ID] = new Dot(new Vector2PLUS(), new Vector2PLUS(10, 10));
                direction[ID] = 0;
                directionName[ID] = DirectionName.Center;
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
        float[] direction;

        private enum DirectionName { Right, TopRight, Top, TopLeft, Left, BottomLeft, Bottom, BottomRight, Center}
        DirectionName[] directionName = new DirectionName[10];

        /// <summary> 計算処理 </summary>
        public void Logic(Vector2PLUS screenSize, Vector2PLUS leftHandPosi, Vector2PLUS[] rightHandPosi)
        {
            leftHnadPoint.Set_Position(PosiConverter(new Vector2PLUS(leftHandPosi.X, leftHandPosi.Y * -1), new Vector2PLUS(2.0f, 2.0f), screenSize));

            //var tempPosiSum = rightHandPosi[0] + rightHandPosi[1] + rightHandPosi[2];

            //rightHandPosi[0] = tempPosiSum / 3;

            Vector2PLUS tempVect = rightHandPosi[0] - rightHandPosi[1];
            float offset = 0.01f;
            if (tempVect.X < offset && tempVect.X > -offset && tempVect.Y < offset && tempVect.Y > -offset) { direction[0] = 0; directionName[0] = DirectionName.Center; }
            else
            {
                float tempValue = (float)Math.Atan((tempVect.Y / tempVect.X));

                direction[0] = MathHelper.ToDegrees(tempValue);

                if (tempVect.X < 0) { direction[0] += 180; }
                else if (tempVect.Y < 0) { direction[0] += 360; }

                if (direction[0] >= 337.5f && direction[0] < 22.5f) { directionName[0] = DirectionName.Right; }
                else if (direction[0] >= 22.5f && direction[0] < 67.5f) { directionName[0] = DirectionName.TopRight; }
                else if (direction[0] >= 67.5f && direction[0] < 112.5f) { directionName[0] = DirectionName.Top; }
                else if (direction[0] >= 112.5f && direction[0] < 157.5f) { directionName[0] = DirectionName.TopLeft; }
                else if (direction[0] >= 157.5f && direction[0] < 202.5f) { directionName[0] = DirectionName.Left; }
                else if (direction[0] >= 202.5f && direction[0] < 247.5f) { directionName[0] = DirectionName.BottomLeft; }
                else if (direction[0] >= 247.5f && direction[0] < 292.5f) { directionName[0] = DirectionName.Bottom; }
                else if (direction[0] >= 292.5f && direction[0] < 337.5f) { directionName[0] = DirectionName.BottomRight; }
            }


            for (int ID = 0; ID < 10; ID++)
            {
                if (ID != 9)
                {
                    direction[ID + 1] = direction[ID];
                    directionName[ID + 1] = directionName[ID];
                }

                rightHnadPoint[ID].Set_Position(PosiConverter(new Vector2PLUS(rightHandPosi[ID].X, rightHandPosi[ID].Y * -1), new Vector2PLUS(2.0f, 2.0f), screenSize));
            }

            //this.rightHandPosi = rightHandPosi;
            

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

            sBatch.DrawString(
                    sFont,
                    directionName[0].ToString(),
                    new Vector2(0, 0),
                    Color.White
                    );
            sBatch.DrawString(
                    sFont,
                    direction[0].ToString(),
                    new Vector2(0, 30),
                    Color.White
                    );

            //for (int ID = 0; ID < 10; ID++)
            //{
            //    sBatch.DrawString(
            //        sFont,
            //        directionName[ID] + " :   " + (int)direction[ID] + "    :   " + directionName[ID],
            //        new Vector2(0, ID * 30),
            //        Color.White
            //        );
            //}
        }
    }
}
