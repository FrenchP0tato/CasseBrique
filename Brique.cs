
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace CasseBrique
{
    public class Brique
    {
      
        private Texture2D texture;
        public Vector2 position;
        private Color color = Color.White; // change to add in types of bricks
        public Vector2 size;
        private int life;
        // public List<Brique> _Briques = new List<Brique>();

        public Vector2 Size
        {
            get { return size; }
            private set { size = value; }
        }


        public Vector2 Position
        {
            get { return position; }
            private set { position = value; } //accesseur
        }

        public Brique(Vector2 position, Color color) // a recevoir de la part de la Tilemap et du level maker
        {
            this.texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("GreyBrick"); // a recevoir du constructeur des héritiers
            this.size = new Vector2(64, 32);
            this.position = position - size * 0.5f; 
            this.color= color;
            this.life = 2;
            
        }

        public void Draw(SpriteBatch sprb) // peut le mettre ici ou mieux dans les heritiers? 
        {
            sprb.Draw(texture, position, color);
        }

        public void TakeDamage(int damage)
        {
            life-=damage;
            if (life <= 0)
            { Disapear(); }
        }

        public void DropResource(int nb, Resource resource)
        {

        }

        public void Disapear()
        {
          //  _Briques.Remove(this);
        }

    }
}
