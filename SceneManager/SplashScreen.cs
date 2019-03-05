using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SceneManager
{
    public class SplashScreen : GameScreen
    {
        //KeyboardState keyState;
        SpriteFont font;

        //List of fade animations and images only want to initialize when we need them
        List<FadeAnimation> fade;
        List<Texture2D> images;

        FileManager fileManager;

        int imageNumber;

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            if (font == null)//Load a font
                font = this.content.Load<SpriteFont>("TestFont");

            //Inisializing
            imageNumber = 0;
            fileManager = new FileManager();
            fade = new List<FadeAnimation>();
            images = new List<Texture2D>();

            //Calling the file manager load
            //This is going to load all the files and and store them in attributes and contents
            fileManager.LoadContent("Load/Splash.cme", attributes, contents);
            
            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        //Dont have to have a default name for everything
                        case "Image"://If one of the attributes is equal to image then we know we want to load in an image
                            images.Add(content.Load<Texture2D>(contents[i][j]));//If the corresponding attribute was an image then we know we are loading in an image
                            fade.Add(new FadeAnimation());//adding a new fade animation for every image we load
                            break;
                    }
                }
            }

            //We want to loop to our fade.count 
            for (int i = 0; i < fade.Count; i++)
            {
                //The formulae to get the scale for the image you are using is 
                //ImageWidth / 2 * scale - (imageWidth / 2)
                //Do the same with the imageHeight

                //Changed the bottom two lines
                //fade[i].LoadContent(content, images[i], "",new  Vector2(80, 60));
                fade[i].LoadContent(content, images[i], "", new Vector2(ScreenManager.Instance.Dimensions.X / 2 - images[i].Width / 2, ScreenManager.Instance.Dimensions.Y / 2 - images[i].Height / 2));
                fade[i].Scale = 1.25f;//Here the image will be stretched to fit the screen
                fade[i].IsActive = true;
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            fileManager = null;
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();

            //If Z is pressed call the screen manager and add a screen
            //keyState = Keyboard.GetState();//Got rod of this for the input manager
            //if (keyState.IsKeyDown(Keys.Z))
            //    ScreenManager.Instance.AddScreen(new TitleScreen());

            //for (int i = 0; i < fade.Count; i++)
            //{
            //    if (imageNumber == i)
            //        fade[i].IsActive = true;
            //    else
            //        fade[i].IsActive = false;

            //    fade[i].Update(gameTime);

            //}

            fade[imageNumber].Update(gameTime);

            //Below is one way to do this fading in and out both work
            //The scond way is better if you want it to fade out with with different colors
            if (fade[imageNumber].Alpha == 0.0f)
                imageNumber++;
            //Imediatly jumps to the title screen//Got rid of the below for the sake of checking the input manager and changed it below
            //if (imageNumber >= fade.Count - 1 || keyState.IsKeyDown(Keys.Z))
            //{
            //    ScreenManager.Instance.AddScreen(new TitleScreen());
            //}; 

            //Using this instead of the above for the input manager 
            if (imageNumber >= fade.Count - 1 || inputManager.KeyPressed(Keys.Z))
            {
                //the below if else was added in
                if (fade[imageNumber].Alpha != 1.0f)
                    ScreenManager.Instance.AddScreen(new TitleScreen(), inputManager, fade[imageNumber].Alpha);
                else
                    ScreenManager.Instance.AddScreen(new TitleScreen(), inputManager);

                //ScreenManager.Instance.AddScreen(new TitleScreen(), inputManager);
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Displaying the classes name so we can see the transition between screens
            //spriteBatch.DrawString(font, "SplashScreen",
            //new Vector2(100, 100), Color.Black);

            fade[imageNumber].Draw(spriteBatch);
        }
    }
}
