using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KineFigu
{
    /// <summary> 図形クラス </summary>
    class Figure
    {
        /// <summary> 画像アドレス </summary>
        private string texAddress { get; }
        /// <summary> 画像 </summary>
        private Texture2D texture { get; set; }

        /// <summary> 初期位置 </summary>
        protected Vector2PLUS initPosi { get; }
        /// <summary> 現在位置 </summary>
        protected Vector2PLUS position { get; set; }
        /// <summary> 大きさ </summary>
        protected Vector2PLUS size { get; set; }
        /// <summary> 描画色 </summary>
        protected Color color { get; set; }

        /// <summary> 中心位置 </summary>
        public Vector2PLUS centerPosi { get { return position + (size / 2); } }

        /// <summary> コンストラクタ </summary>
        /// <param name="texAddres">画像アドレス</param>
        /// <param name="initPosi">初期位置</param>
        /// <param name="size">大きさ</param>
        public Figure(string texAddres, Vector2PLUS initPosi, Vector2PLUS size)
        {
            this.texAddress = texAddres;
            this.initPosi = initPosi;
            this.size = size;
            this.color = Color.White;

            // 初期化処理
            Initialize();
        }

        /// <summary> 初期化処理 </summary>
        public void Initialize()
        {
            this.position = initPosi;
        }

        /// <summary> 読み込み処理 </summary>
        /// <param name="Content">ContentManager</param>
        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(texAddress);
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
    }
}
