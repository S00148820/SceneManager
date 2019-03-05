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
    //ScreenManager will handle all the screens and modules we have
    public class ScreenManager
    {
        #region Variables

        //creating custom content manager for the class
        ContentManager content;

        //Current screen being displayed
        GameScreen currentScreen;

        //Screen thats going to be overlapping the current screen
        GameScreen newScreen;
        
        /*Screen Manager Instance
         * Making the screen manager class a singleton
         * This makes one instance of the class and makes all the puclic 
         * functions global
         * */
        private static ScreenManager instance;

        /*Stack of screens
         * This lets us know what screens are open and in what order
         * It lets us move the screen we want to the top of the stack and 
         * pushes the previous screen back behind the new screen which lets me 
         * move between screens.
         * */
        Stack<GameScreen> screenStack = new Stack<GameScreen>();

        //The screens width and height
        Vector2 dimensions;

        //Whats this bool for?
        bool transition;

        //Instance of fadeanimation
        FadeAnimation fade =  new FadeAnimation();

        Texture2D fadeTexture, nullImage;

        InputManager inputManager;

        #endregion Variables

        #region Properties
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }

        //changed this
        public ContentManager Content
        {
            get { return content; }
        }

        public Vector2 Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

        public Texture2D NullImage
        {
            get { return nullImage; }
        }
        #endregion Properties

        #region Main Methods

        //This will load and unload whichever screens we need
        //Once we add a new screen we need to add it to the top of our screen stack
        public void AddScreen(GameScreen screen, InputManager inputManager)
        {
            transition = true;//lets us know we are transitioning
            newScreen = screen;
            fade.IsActive = true;
            //No animation can happen unless isactive is equal to true.
            //If alpha is = 1.0 it is opauqe and not transparent meaning  
            //we are fully showing the object. if it 0.0 the object is fully            
            //transparent we want hte black image to at first be fully 
            //transparent then we slowly show tehe black image
            fade.Alpha = 0.0f;
            fade.ActivateValue = 1.0f;//the animation will keep increaseing until it hits the activate value

            this.inputManager = inputManager;
        }

        public void AddScreen(GameScreen screen, InputManager inputManager, float alpha)
        {
            transition = true;
            newScreen = screen;
            fade.IsActive = true;
            fade.ActivateValue = 1.0f;

            if (alpha != 1.0f)
                fade.Alpha = 1.0f - alpha;
            else
                fade.Alpha = alpha;
            fade.Increase = true;
            this.inputManager = inputManager;

        }

        public void Initialize()
        {
            currentScreen = new SplashScreen();
            fade = new FadeAnimation();

            inputManager = new InputManager();
        }

        public void LoadContent(ContentManager Content)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent(content, inputManager);

            nullImage = content.Load<Texture2D>("NullImage");
            fadeTexture = content.Load<Texture2D>("FadeImage");
            fade.LoadContent(content, fadeTexture, "", Vector2.Zero);
            fade.Scale = Dimensions.X;//It doesnt matter whether it is larger than the screen. So if the width is higher that the height set it to the width
        }

        public  void Update(GameTime gameTime)
        {
            //We do this because we dont want to update the screens while we are transitioning
            if (!transition)
                //update the current screen
                currentScreen.Update(gameTime);

            else
                Transistion(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //draw the current screen
            currentScreen.Draw(spriteBatch);
            if (transition)
                fade.Draw(spriteBatch);//The reason to draw when we are fading is so we dont clear the screeen when we are fading
        }

        #endregion Main Methods

        #region Private Methods
        
        private void Transistion(GameTime gameTime)
        { 
            fade.Update(gameTime);
            //this will increase until alpha is equal to one
            //so basically if timer is = 1 and alpha is = 1 activate this
            //it then does all the screen transitioning
            if(fade.Alpha == 1.0f && fade.Timer.TotalSeconds == 1.0f)
            {
                screenStack.Push(newScreen);//push to the top of the stack
                currentScreen.UnloadContent();//This will unload the content of the first screen
                currentScreen = newScreen;
                currentScreen.LoadContent(content, inputManager);//load the new screens content
            }
            //once all the above happens it will start decreasing and head towards zero again
            //then the transition is over and we set these to false
            else if (fade.Alpha == 0.0f)
            {
                transition = false;
                fade.IsActive = false;
            }
        }
        #endregion Private Methods
    }
}
