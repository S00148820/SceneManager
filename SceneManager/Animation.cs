using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SceneManager
{
    //Base class for all the animaiton classes
    public class Animation
    {
        protected Texture2D image;
        protected string text;
        protected SpriteFont font;
        protected Color color;
        protected Rectangle sourceRect;//In case we are doing player animation
        protected float rotation, scale, axis;
        protected Vector2 origin, position;
        protected ContentManager content;
        protected float alpha;

        protected bool isActive;//Important for update
        //public virtual float Alpha { get; set; }

        public virtual float Alpha
        {
            get { return alpha; }
            set { alpha =  value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public float Scale
        {
            set { scale = value; }
        }

        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        public virtual void LoadContent(ContentManager Content, Texture2D image, string text, Vector2 position)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            this.image = image;
            this.text = text;
            this.position = position;

            if (text != String.Empty)
            {
                font = this.content.Load<SpriteFont>("TestFont");
                color = new Color(114, 77, 255);
            }

            //If image is not equal to null set the default source rectangle equal to the images width and height
            if (image != null)
                sourceRect = new Rectangle(0, 0, image.Width, image.Height);

            rotation = 0.0f;
            axis = 0.0f;
            scale = alpha = 1.0f;
            IsActive = false;
            //alpha = 1;//Default alpha value is 1 meaning it shows the whole image 
        }

        public virtual void UnloadContent()
        {
            content.Unload();//Just unload the font
            image = null;
            //font = null;
            text = String.Empty;
            position = Vector2.Zero;
            sourceRect = Rectangle.Empty;
        }

        public virtual void Update(GameTime gameTime)
        {
            //Each animation 
        }

        
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //The reason we say source rect instead of the images width is 
            //that we want to set it on the center of the image so if we 
            //have a source rectangle or are cropping on a picture
            //we want the center of the whole image we cropped out
            //we want the origin to be at the center of the image 

            if (image != null)
            {
                origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);//Setting the origin
                //alos need to set the color * alpha to getthe alpha effect we want
                //position + origin its going to treat the center of the object as zero
                spriteBatch.Draw(image, position + origin, sourceRect,
                    Color.White * alpha, rotation, origin, scale,
                    SpriteEffects.None, 0.0f);
            }

            if (text != String.Empty)
            {
                //change the origin because its text not an image 
                //font.measurestring centers it on the string
                //origin is then in the center
                origin = new Vector2(font.MeasureString(text).X / 2, font.MeasureString(text).Y / 2);

                spriteBatch.DrawString(font, text, position + origin,
                    color * alpha, rotation, origin, scale, SpriteEffects.None, 0.0f);
            }
            
        }
    }
}
