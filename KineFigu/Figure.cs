using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KineFigu
{
    class Figure
    {
        private string texAddress { get; }
        private Texture2D texture { get; set; }

        protected Vector2PLUS initPosi { get; }
        protected Vector2PLUS position { get; set; }
        protected Vector2PLUS size { get; set; }
        protected Color color { get; set; }
        public Vector2PLUS centerPosi { get { return position + (size / 2); } }

        public Figure(string texAddres, Vector2PLUS initPosi, Vector2PLUS size)
        {
            this.texAddress = texAddres;
            this.initPosi = initPosi;
            this.size = size;
            this.color = Color.White;

            Initialize();
        }

        public void Initialize()
        {
            this.position = initPosi;
        }

        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(texAddress);
        }
        

        public void Draw(SpriteBatch sBatch)
        {
            sBatch.Draw(
                texture, 
                position,
                color
                );
        }
    }
}
