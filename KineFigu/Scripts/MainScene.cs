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
            rightHnadPoint = new Dot[10];

            direction = new float[100];
            
            for (int ID = 0; ID < 100; ID++)
            {
                direction[ID] = 0;
                directionNames[ID] = DirectionName.Center;
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
            squares.Add(new Square(new Vector2PLUS(100, 100), new Vector2PLUS(50, 50)));
            circles.Add(new Circle(new Vector2PLUS(200, 100), new Vector2PLUS(50, 50)));
            leftHnadPoint = new Dot(new Vector2PLUS(), new Vector2PLUS(30, 30));
            for (int ID = 0; ID < 10; ID++)
            {
                rightHnadPoint[ID] = new Dot(new Vector2PLUS(), new Vector2PLUS(10, 10));

            }
        }

        /// <summary> 読み込み処理 </summary>
        public void Load(ContentManager Content)
        {
            FiguresSet();

            sFont = Content.Load<SpriteFont>("Arial");


        }


        bool flagEnter = false;
        bool flagCreate = false;

        bool flagTop = false, flagBottom = false, flagLeft = false, flagRight = false;

        float[] direction;

        private enum DirectionName { Right, TopRight, Top, TopLeft, Left, BottomLeft, Bottom, BottomRight, Center}


        DirectionName nowDirectionName = DirectionName.Center;
        DirectionName[] directionNames = new DirectionName[100];

        Vector2PLUS startPosi;
        Vector2PLUS endPosi;

        /// <summary> 計算処理 </summary>
        public void Logic(Vector2PLUS screenSize, Vector2PLUS leftHandPosi, Vector2PLUS[] rightHandPosi)
        {
            leftHnadPoint.Set_Position(PosiConverter(new Vector2PLUS(leftHandPosi.X, leftHandPosi.Y * -1), new Vector2PLUS(2.0f, 2.0f), screenSize));

            //var tempPosiSum = rightHandPosi[0] + rightHandPosi[1] + rightHandPosi[2];

            //rightHandPosi[0] = tempPosiSum / 3;

            Vector2PLUS tempVect = rightHandPosi[0] - rightHandPosi[1];
            float offset = 0.01f;
            if (tempVect.X < offset && tempVect.X > -offset && tempVect.Y < offset && tempVect.Y > -offset) { direction[0] = 0; directionNames[0] = DirectionName.Center; }
            else
            {
                float tempValue = (float)Math.Atan((tempVect.Y / tempVect.X));

                direction[0] = MathHelper.ToDegrees(tempValue);

                if (tempVect.X < 0) { direction[0] += 180; }
                else if (tempVect.Y < 0) { direction[0] += 360; }

                if (direction[0] >= 337.5f || direction[0] < 22.5f) { nowDirectionName = DirectionName.Right; }
                else if (direction[0] >= 22.5f && direction[0] < 67.5f) { nowDirectionName = DirectionName.TopRight; }
                else if (direction[0] >= 67.5f && direction[0] < 112.5f) { nowDirectionName = DirectionName.Top; }
                else if (direction[0] >= 112.5f && direction[0] < 157.5f) { nowDirectionName = DirectionName.TopLeft; }
                else if (direction[0] >= 157.5f && direction[0] < 202.5f) { nowDirectionName = DirectionName.Left; }
                else if (direction[0] >= 202.5f && direction[0] < 247.5f) { nowDirectionName = DirectionName.BottomLeft; }
                else if (direction[0] >= 247.5f && direction[0] < 292.5f) { nowDirectionName = DirectionName.Bottom; }
                else if (direction[0] >= 292.5f && direction[0] < 337.5f) { nowDirectionName = DirectionName.BottomRight; }
            }

            for (int ID = 0; ID < directionNames.Length; ID++)
            {
                if (ID != directionNames.Length - 1)
                {
                    direction[ID + 1] = direction[ID];
                }
            }

            for (int ID = 0; ID < 10; ID++)
            {
                rightHnadPoint[ID].Set_Position(PosiConverter(new Vector2PLUS(rightHandPosi[ID].X, rightHandPosi[ID].Y * -1), new Vector2PLUS(2.0f, 2.0f), screenSize));
            }

            // 方向の履歴を保存
            if (nowDirectionName != directionNames[0])
            {
                for(int ID = directionNames.Length - 1; ID > 0; ID--)
                {
                    directionNames[ID] = directionNames[ID - 1];
                }

                directionNames[0] = nowDirectionName;
            }

            for(int ID = 0; ID < directionNames.Length; ID++)
            {
                if (directionNames[ID] == DirectionName.Bottom)
                {
                    for (int ID2 = ID; ID2 < directionNames.Length; ID2++)
                    {
                        if (directionNames[ID2] == DirectionName.Right)
                        {
                            for (int ID3 = ID2; ID3 < directionNames.Length; ID3++)
                            {
                                if (directionNames[ID3] == DirectionName.Top)
                                {
                                    for (int ID4 = ID3; ID4 < directionNames.Length; ID4++)
                                    {
                                        if (directionNames[ID4] == DirectionName.Left)
                                        {
                                            flagCreate = true;
                                            break;
                                        }
                                    }
                                }
                                if (flagCreate) { break; }
                            }
                        }
                        if (flagCreate) { break; }
                    }
                }
                if (flagCreate) { break; }
            }

            if (flagCreate)
            {
                squares.Add(new Square(rightHnadPoint[0].position, new Vector2PLUS(100, 100)));
                flagCreate = false;
                for (int ID = 0; ID < directionNames.Length - 1; ID++)
                {
                    directionNames[ID] = DirectionName.Center;
                }
            }
            
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
            int num = 0;
            sBatch.DrawString(
                    sFont,
                    directionNames[0].ToString(),
                    new Vector2(0, 0),
                    Color.White
                    );
            sBatch.DrawString(
                    sFont,
                    direction[0].ToString(),
                    new Vector2(0, num+=30),
                    Color.White
                    );
            sBatch.DrawString(
                    sFont,
                    flagCreate.ToString(),
                    new Vector2(0, num += 30),
                    Color.White
                    );
            sBatch.DrawString(
                    sFont,
                    flagCreate.ToString(),
                    new Vector2(0, num += 30),
                    Color.White
                    );
            sBatch.DrawString(
                    sFont,
                    flagCreate.ToString(),
                    new Vector2(0, num += 30),
                    Color.White
                    );
            sBatch.DrawString(
                    sFont,
                    flagCreate.ToString(),
                    new Vector2(0, num += 30),
                    Color.White
                    );
            sBatch.DrawString(
                    sFont,
                    flagCreate.ToString(),
                    new Vector2(0, num += 30),
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
