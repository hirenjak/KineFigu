using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KineFigu
{
    /// <summary> 本体部分 </summary>
    class MainScene
    {
        Square square;

        public MainScene()
        {
            square = new Square(new Vector2PLUS(100, 100), new Vector2PLUS(50, 50));
        }

        /// <summary> 初期化処理 </summary>
        public void Initialize()
        {

        }

        /// <summary> 読み込み処理 </summary>
        public void Load(ContentManager Content)
        {
            square.Load(Content);
        }

        /// <summary> 計算処理 </summary>
        public void Logic()
        {

        }

        /// <summary> 描画処理 </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            square.Draw(spriteBatch);
        }
    }
}
