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
    public class FadeAnimation : Animation
    {
        bool increase;
        float fadeSpeed;
        TimeSpan defaultTime, timer;
        float activateValue;
        bool stopUpdataing;
        float defaultAlpha;
        /* We have default alpah becasue the way we are fading.
         * This is because we have a black image and will change its transparency based on fading in or out.
         * When the alpha value is 255 the image is fully shown.
         * */

        public TimeSpan Timer
        {
            get { return timer; }
            set { defaultTime = value; timer = defaultTime; }
        }

        public float FadeSpeed { get; set; }

        //So if we edit it with the fade animation class then we set increase to false
        public override float Alpha
        {
            get { return alpha; }
            set
            {
                alpha = value;
                if (alpha == 1.0f)
                    increase = false;
                else if (alpha == 0.0f)
                    increase = true;
                    
            }
        }

        //Want to be able to change the activate value
        //public float ActivateValue
        //{
        //    get { return activateValue; }
        //    set { activateValue = value; }
        //}
        public float ActivateValue { get; set; }

        public bool Increase { get; set; }
        


        public override void LoadContent(ContentManager Content, 
            Texture2D image, string text, Vector2 position)
        {
            base.LoadContent(Content, image, text, position);
            increase = false;
            fadeSpeed = 1.0f;
            defaultTime = new TimeSpan(0, 0, 1);//Setting it to one second
            timer = defaultTime;
            activateValue = 0.0f;
            stopUpdataing = false;
            defaultAlpha = alpha;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                if (!stopUpdataing)
                {
                    //We do it like this so it will fade at the same speed on every computer
                    if (!increase)
                        alpha -= fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    else
                        alpha += fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (alpha <= 0.0f)
                    {
                        alpha = 0.0f;
                        increase = !increase;
                    }
                    else if (alpha >= 1.0f)
                    {
                        alpha = 1.0f;
                        increase = false;
                    }
                }

                //The reason we have this is that once we fade out we dont want to aade in immediatly we want to wait a few seconds
                if(alpha == activateValue)
                {
                    stopUpdataing = true;
                    timer -= gameTime.ElapsedGameTime;
                    if (timer.TotalSeconds <= 0)
                    {
                        //Here the increase onnly changes when we have the activate value so we get rid of that and change it above with the true false values
                        //increase = !increase;
                        timer = defaultTime;
                        stopUpdataing = false;
                    }
                }
            }
            else
            {
                alpha = defaultAlpha;
                stopUpdataing = false;
            }
        }
    }
}
