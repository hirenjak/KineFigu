using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KineFigu
{
    public class Source : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // メインシーン
        MainScene mainScene;

        public Source()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // インスタンス
            mainScene = new MainScene();
        }
        
        /// <summary> 初期化処理 </summary>
        protected override void Initialize()
        {
            mainScene.Initialize();

            base.Initialize();
        }
        
        /// <summary> 読み込み処理 </summary>
        protected override void LoadContent()
        {
            mainScene.Load();

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        
        /// <summary> 読み込み破棄 </summary>
        protected override void UnloadContent()
        {
            // 基本使わない
        }

        /// <summary> 計算処理 </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mainScene.Logic();

            base.Update(gameTime);
        }
        
        /// <summary> 描画処理 </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // 2次元宣言
            spriteBatch.Begin();
            
            // シーン内描画
            mainScene.Draw();

            // 2次元描画終了
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
