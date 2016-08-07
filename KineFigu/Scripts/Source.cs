using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.IO;


namespace KineFigu
{
    public class Source : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Kinect kinect;
        LeapMotion leapMotion;

        // メインシーン
        MainScene mainScene;

        Vector2PLUS screenSize;

        KeyboardState keyState;

        public Source()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Figure.Set_ContentManager(Content);

            // インスタンス
            mainScene = new MainScene();

            kinect = new Kinect();
            leapMotion = new LeapMotion();

            screenSize = new Vector2PLUS(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        ~Source()
        {
            kinect.Stop();
            leapMotion.Stop();
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

            mainScene.Load(Content);

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
            keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Escape)) {  Exit(); }

            mainScene.Logic(keyState, leapMotion.Get_Flag(), screenSize, kinect.Get_LeftHandPosition(), kinect.Get_RightHandPosition());

            

            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) { }

            base.Update(gameTime);
        }
        
        /// <summary> 描画処理 </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // 2次元宣言
            spriteBatch.Begin();

            // シーン内描画
            mainScene.Draw(spriteBatch);

            // 2次元描画終了
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
