using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace SceneManager
{
    public class InputManager
    {
        KeyboardState prevKeyState, keyState;

        //Props to get the values in case we need to 
        public KeyboardState PrevKeyState
        {
            get { return prevKeyState; }
            set { prevKeyState = value; }
        }
        public KeyboardState KeyState
        {
            get { return keyState; }
            set { keyState = value; }
        }

        //Funcitions
        public void Update()//Have to call this before we use any of the below functions
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();
        }

        public bool KeyPressed(Keys key)//This just checks for single key presses
        {
            if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                return true;
            return false; 
        }

        //Below checks for mutiple key presses
        public bool KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        //Below two moethods are chekcing for a key release
        public bool KeyReleased(Keys key)
        {
            if (keyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                return true;
            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        //Checking for mutiple key presses
        public bool KeyDown(Keys key)
        {
            if (keyState.IsKeyDown(key))
                return true;
            return false;
    
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyDown(key))//Didnt fix this in the forst time around
                    return true;
            }
            return false;
        }

    }
}
