using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//background
//deactivate the screen
//could set the fade out when choosing
namespace SceneManager
{
    public class TitleScreen : GameScreen
    {
        //KeyboardState keyState;
        SpriteFont font;
        MenuManager menu;

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            //Load a font
            if (font == null)
                font = content.Load<SpriteFont>("MenuFont");
            menu = new MenuManager();
            menu.LoadContent(content, "Title");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            menu.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //If enter is pressed call the screen manager and add a screen
            //keyState = Keyboard.GetState();
            inputManager.Update();
            menu.Update(gameTime, inputManager);
            //if (inputManager.KeyPressed(Keys.Z))
            //    ScreenManager.Instance.AddScreen(new SplashScreen(),inputManager);
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            //Displaying the classes name so we can see the transition between screens
            //spriteBatch.DrawString(font, "TitleScreen", new Vector2(100, 100), Color.Black);

            menu.Draw(spriteBatch);
        }
    }
}
