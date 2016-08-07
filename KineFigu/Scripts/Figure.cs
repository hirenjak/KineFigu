using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KineFigu
{
    /// <summary> 図形クラス </summary>
    class Figure
    {
        protected static ContentManager Content { get; private set; }
        public static void Set_ContentManager(ContentManager _Content) { Content = _Content; }

        /// <summary> 画像アドレス </summary>
        private string texAddress { get; }
        /// <summary> 画像 </summary>
        protected Texture2D texture { get; set; }

        /// <summary> 初期位置 </summary>
        protected Vector2PLUS initPosi { get; }
        /// <summary> 現在位置 </summary>
        public Vector2PLUS position;
        /// <summary> 大きさ </summary>
        protected Vector2PLUS size { get; set; }
        /// <summary> 描画色 </summary>
        protected Color color { get; set; }

        public bool gravityFlag;
        public bool collisionFlag;
        private Vector2PLUS vector;

        /// <summary> 中心位置 </summary>
        public Vector2PLUS centerPosi { get { return position + (size / 2); } }

        /// <summary> コンストラクタ </summary>
        /// <param name="texAddres">画像アドレス</param>
        /// <param name="initPosi">初期位置</param>
        /// <param name="size">大きさ</param>
        public Figure(Vector2PLUS initPosi, Vector2PLUS size)
        {
            this.initPosi = initPosi;
            this.size = size;
            this.color = Color.White;
            this.gravityFlag = false;
            this.collisionFlag = false;
            this.vector = new Vector2PLUS();

            Initialize();
        }

        public Figure(Vector2PLUS initPosi, Vector2PLUS size, bool gravityFlag, bool collisionFlag) : this(initPosi, size)
        {
            this.gravityFlag = gravityFlag;
            this.collisionFlag = collisionFlag;
        }

        /// <summary> 初期化処理 </summary>
        public void Initialize()
        {
            this.position = initPosi;
        }

        public void Logic(Vector2PLUS screenSize, Vector2PLUS leftHnadPosi, Vector2PLUS rightHnadPosi)
        {
            if (gravityFlag)
            {
                vector.Y += 0.098f;
            }

            if(vector.X != 0)
            {
                float tempVal = 0.05f;
                if(vector.X < -tempVal ){ vector.X+= tempVal; }
                else if(vector.X > tempVal) { vector.X -= tempVal; }
                else { vector.X = 0; }
            }

            if (position.Y + vector.Y + size.Y > screenSize.Y -100) { position.Y = screenSize.Y - size.Y -100; vector.Y = 0; }

            if (collisionFlag)
            {
                if (CollisionCheck.BoxToBox(position, position + size, leftHnadPosi, leftHnadPosi + new Vector2PLUS(30, 30)))
                {
                    Vector2PLUS temp = (position + (size / 2)) - (leftHnadPosi + new Vector2PLUS(15, 15));

                    vector += new Vector2PLUS(1 / temp.X, 1 / temp.Y) * 10;
                }

                if (CollisionCheck.BoxToBox(position, position + size, rightHnadPosi, rightHnadPosi + new Vector2PLUS(30, 30)))
                {
                    Vector2PLUS temp = (position + (size / 2)) - (rightHnadPosi + new Vector2PLUS(15, 15));

                    vector += new Vector2PLUS(1 / temp.X, 1 / temp.Y) * 10;
                }
            }

            position += vector;
        }

        /// <summary> 描画処理 </summary>
        /// <param name="sBatch">SpriteBatch</param>
        public void Draw(SpriteBatch sBatch)
        {
            sBatch.Draw(
                texture, 
                position,
                new Rectangle(0, 0, texture.Width, texture.Height),
                color,
                0.0f,
                new Vector2(0, 0),
                size / 128,
                SpriteEffects.None,
                0.5f
                );
        }

        public void Set_Position(Vector2PLUS position)
        {
            this.position = position;
        }
    }
}
