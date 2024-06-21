using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CasseBrique.GameObjects
{
    internal class Bonus : SpriteGameObject

    {
        protected MouseState oldMouseState;
        protected SpriteFont font;
        protected bool checkClick = false;
        protected string text;
        protected Texture2D highlightTexture;
        protected Texture2D basicTexture;
        protected String target;
        protected GameController gc = ServicesLocator.Get<GameController>();

        protected Texture2D greyedTexture;
        
        private int costNb;
        private string costType;


        public Bonus(string pTarget, string pText, string pCostType, int pCostNb, Vector2 pPosition, Scene pRoot) : base(pRoot)
        {
            font = ServicesLocator.Get<IAssetsService>().Get<SpriteFont>("BasicText");
            text = $"{pText}({pCostNb} {pCostType})";
            target = pTarget;
            costNb = pCostNb;
            costType = pCostType;
            basicTexture =  ServicesLocator.Get<IAssetsService>().Get<Texture2D>("GreenButton");
            greyedTexture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("GreyButton");
            texture = basicTexture;

            position.X = pPosition.X;
            position.Y = pPosition.Y;
            size.X = texture.Width;
            size.Y = texture.Height;
            offset = size * 0.5f;
        }

        private bool isPayable()
        {
            if (gc.GetResourceQty(costType) >= costNb) return true;
            else return false;
        }

        private void PayCost()
        {
            gc.LooseResource(costType, costNb);
        }

        public override void Update(float dt)
        {
            if (isPayable()) texture = basicTexture;
            else
            {
                texture = greyedTexture;
                return;
            }

            MouseState NewMouseState = Mouse.GetState();
            if (ServicesLocator.Get<MouseService>().CheckMouseClicks(oldMouseState, NewMouseState) == true)
            {
                checkClick = ServicesLocator.Get<MouseService>().CheckObjectClick(NewMouseState, position, offset);
                if (checkClick)
                {
                    PayCost();
                    if (this.target == "DamageUp") 
                    {
                        gc.BallDamage = gc.BallDamage + 1;
                    }

                    if (this.target == "PadSizeUp")
                    {
                        gc.PaddleSize = gc.PaddleSize + 30;
                    }

                    if (this.target == "PadSpeedUp")
                    {
                        gc.PaddleSpeed = gc.PaddleSpeed+ 50;
                    }

                    if (this.target == "MaxLifes")
                    {
                        gc.MaxLifes += 1;
                    }

                }

            }
            oldMouseState = NewMouseState;
        }


        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            Vector2 offset = TextOffset(font, text, position);
            
            sb.DrawString(font, text, offset, Color.Black);
            
        }


        protected Vector2 TextOffset(SpriteFont font, string text, Vector2 origin)
        {
            Vector2 textSize = font.MeasureString(text);
            Vector2 offset = new Vector2(origin.X - textSize.X / 2, origin.Y - textSize.Y / 2);

            return offset;
        }
    }
}
