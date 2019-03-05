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
    //Abstract class holding a bunch of virtual methods to be over written
    public class GameScreen
    {
        //Content manager for this screen for loading and unloading
        protected ContentManager content;

        //Most likely going to be loading something in every game screen so 
        //I created a list of attributes and contents
        protected List<List<string>> attributes, contents;

        //Since most of our screens are going to take input then should make an input manager in gamescreen
        protected InputManager inputManager;
        
        //The problem with the input manager class is that every screen we go to we are using a new input manager
        //because we are loading a brand new input manager so from the previous screen to the new screen it doesnt know what we held down in the last scene
        //so below we add in an input manager in the paramater 
        public virtual void LoadContent(ContentManager Content, InputManager inputManager)
        {
            //Making the protected content above a new contentmanager
            content = new ContentManager(Content.ServiceProvider, "Content");
            attributes = new List<List<string>>();
            contents = new List<List<string>>();

            this.inputManager = inputManager;
        }

        public virtual void UnloadContent()
        {
            content.Unload();
            inputManager = null;
            attributes.Clear();
            contents.Clear();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
