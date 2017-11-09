using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Utilities;

namespace GameComponentExample
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MovingCircle : DrawableGameComponent
    {
        
        Texture2D movingCircle;
        Vector2 movingCirclePosition;
        static Vector2 Target;
        float speed;

        public MovingCircle(Game game)
            : base(game)
        {
            game.Components.Add(this);
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            speed = Utility.NextRandomFloat(0.03);
            movingCircle = Game.Content.Load<Texture2D>("MovingCircle");
            movingCirclePosition = new Vector2(Utility.NextRandom(0, 600), Utility.NextRandom(0, 400));
            Target = new Vector2(Utility.NextRandom(0,600), Utility.NextRandom(0, 400));
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            MouseState m = Mouse.GetState();
            if(m.LeftButton == ButtonState.Pressed)
            {
                Rectangle r = new
                    Rectangle((int)movingCirclePosition.X,
                    (int)movingCirclePosition.Y, 
                    movingCircle.Width, 
                    movingCircle.Height);

                if(r.Contains(m.Position))
                {
                    Visible = !Visible;
                }
            }
            // TODO: Add your update code here
            movingCirclePosition = Vector2.Lerp(movingCirclePosition, Target, speed);
            if (Vector2.Distance(movingCirclePosition, Target) < speed)
            {
                movingCirclePosition = Target;
                Target = new Vector2(Utility.NextRandom(0, 600), Utility.NextRandom(0, 400));
                speed = Utility.NextRandomFloat(0.03);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Begin();
            spriteBatch.Draw(movingCircle, movingCirclePosition, Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
