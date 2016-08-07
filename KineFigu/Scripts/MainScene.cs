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

            direction = 0;
            
            for (int ID = 0; ID < 100; ID++)
            {
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

        float direction;

        private enum DirectionName { Right, TopRight, Top, TopLeft, Left, BottomLeft, Bottom, BottomRight, Center}


        DirectionName nowDirectionName = DirectionName.Center;
        DirectionName[] directionNames = new DirectionName[100];

        Vector2PLUS startPosi;
        Vector2PLUS endPosi;

        /// <summary> 計算処理 </summary>
        public void Logic(KeyboardState keyState, Vector2PLUS screenSize, Vector2PLUS leftHandPosi, Vector2PLUS[] rightHandPosi)
        {
            // 角度の算出
            DirectionConputing(rightHandPosi);
            
            // 手の位置を描画に反映
            HandPositionAppply(screenSize, leftHandPosi, rightHandPosi);

            // 方向の履歴を保存
            DirectionSave();

            // 矩形が絵がれているかを判定
            if (keyState.IsKeyDown(Keys.Enter))
            {
                SquareCheck();
            }
            else
            {
                DirectionNameInitialize();
            }

            // フラグが立ったら図形を作る
            if (flagCreate) { CreateFigures(); }
            


            // 図形クラスをまとめる処理
            FiguresSet();

            foreach(var value in figures) { value.Logic(); }
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
                    direction.ToString(),
                    new Vector2(0, num+=30),
                    Color.White
                    );
            sBatch.DrawString(
                    sFont,
                    flagCreate.ToString(),
                    new Vector2(0, num += 30),
                    Color.White
                    );
        }


        /// <summary> キネクト座標系を画面の座標系に変換する </summary>
        /// <param name="target">対象とするキネクト座標</param>
        /// <param name="targetMaxSize">キネクト座標のマックスの値</param>
        /// <param name="screenSize">ウィンドウのサイズ</param>
        /// <returns></returns>
        private Vector2PLUS PosiConverter(Vector2PLUS target, Vector2PLUS targetMaxSize, Vector2PLUS screenSize)
        {
            return new Vector2PLUS(target.X / targetMaxSize.X * screenSize.X, target.Y / targetMaxSize.Y * screenSize.Y) + (screenSize / 2);
        }

        /// <summary> 図形を作成する </summary>
        private void CreateFigures()
        {
            squares.Add(new Square(rightHnadPoint[0].position, new Vector2PLUS(100, 100), true));
            flagCreate = false;
            DirectionNameInitialize();
        }

        /// <summary> 矩形が描かれているか判定する </summary>
        private void SquareCheck()
        {
            for (int ID = 0; ID < directionNames.Length; ID++)
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
        }

        /// <summary> 1つ前の過去データと比較して変化があれば保存される </summary>
        private void DirectionSave()
        {
            if (nowDirectionName != directionNames[0])
            {
                for (int ID = directionNames.Length - 1; ID > 0; ID--)
                {
                    directionNames[ID] = directionNames[ID - 1];
                }

                directionNames[0] = nowDirectionName;
            }
        }

        /// <summary> 逆三角関数を使って角度を算出する </summary>
        private void DirectionConputing(Vector2PLUS[] rightHandPosi)
        {
            Vector2PLUS tempVect = rightHandPosi[0] - rightHandPosi[1];
            float offset = 0.01f;
            if (tempVect.X < offset && tempVect.X > -offset && tempVect.Y < offset && tempVect.Y > -offset) { direction = 0; directionNames[0] = DirectionName.Center; }
            else
            {
                float tempValue = (float)Math.Atan((tempVect.Y / tempVect.X));

                direction = MathHelper.ToDegrees(tempValue);

                if (tempVect.X < 0) { direction += 180; }
                else if (tempVect.Y < 0) { direction += 360; }

                if (direction >= 337.5f || direction < 22.5f) { nowDirectionName = DirectionName.Right; }
                else if (direction >= 22.5f && direction < 67.5f) { nowDirectionName = DirectionName.TopRight; }
                else if (direction >= 67.5f && direction < 112.5f) { nowDirectionName = DirectionName.Top; }
                else if (direction >= 112.5f && direction < 157.5f) { nowDirectionName = DirectionName.TopLeft; }
                else if (direction >= 157.5f && direction < 202.5f) { nowDirectionName = DirectionName.Left; }
                else if (direction >= 202.5f && direction < 247.5f) { nowDirectionName = DirectionName.BottomLeft; }
                else if (direction >= 247.5f && direction < 292.5f) { nowDirectionName = DirectionName.Bottom; }
                else if (direction >= 292.5f && direction < 337.5f) { nowDirectionName = DirectionName.BottomRight; }
            }
        }

        /// <summary> 手の位置を描画位置に反映させる </summary>
        /// <param name="screenSize"></param>
        /// <param name="leftHandPosi"></param>
        /// <param name="rightHandPosi"></param>
        private void HandPositionAppply(Vector2PLUS screenSize, Vector2PLUS leftHandPosi, Vector2PLUS[] rightHandPosi)
        {
            leftHnadPoint.Set_Position(PosiConverter(new Vector2PLUS(leftHandPosi.X, leftHandPosi.Y * -1), new Vector2PLUS(2.0f, 2.0f), screenSize));

            for (int ID = 0; ID < 10; ID++)
            {
                rightHnadPoint[ID].Set_Position(PosiConverter(new Vector2PLUS(rightHandPosi[ID].X, rightHandPosi[ID].Y * -1), new Vector2PLUS(2.0f, 2.0f), screenSize));
            }
        }

        private void DirectionNameInitialize()
        {
            for (int ID = 0; ID < directionNames.Length - 1; ID++)
            {
                directionNames[ID] = DirectionName.Center;
            }
        }
    }
}
