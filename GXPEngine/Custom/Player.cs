using System.Drawing.Drawing2D;
using GXPEngine.Core;

namespace GXPEngine.Custom
{
    public class Player : Sprite
    {
        public Vector2 position;
        public Vector2 velocity;
        private Vector2 direction;
        private const float BRAKINGFORCE = 0.1f;
        private const float JUMPFORCE = 1f;
        private int player;

        public Player(int x, int y, int player) : base("circle.png")
        {
            position = new Vector2(x, y);
            SetXY(x, y);
            this.player = player;
            SetOrigin(width/2,height/2);
        }

        private void Update()
        {
            Controls();
            position += velocity;
            UpdateScreenPosition();
        }

        private void Controls()
        {
            switch (player)
            {
                    case 1 :

                    if (Input.GetKey(Key.W))
                    {
                        velocity.y += JUMPFORCE;
                    }

                    if (Input.GetKey(Key.D))
                    {
                        velocity += new Vector2(1f, 0);
                    }

                    if (Input.GetKey(Key.A))
                    {
                        velocity += new Vector2(-1f, 0);
                    }

                    if (velocity.x > 0)
                    {
                        velocity.x -= BRAKINGFORCE;
                    }

                    if (velocity.x < 0)
                    {
                        velocity.x += BRAKINGFORCE;
                    }

                    break;
                    case 2 :

                    if (Input.GetKey(Key.UP))
                    {

                    }

                    if (Input.GetKey(Key.RIGHT))
                    {
                        velocity += new Vector2(1f, 0);
                    }

                    if (Input.GetKey(Key.LEFT))
                    {
                        velocity += new Vector2(-1f, 0);
                    }

                    if (velocity.x > 0)
                    {
                        velocity.x -= BRAKINGFORCE;
                    }

                    if (velocity.x < 0)
                    {
                        velocity.x += BRAKINGFORCE;
                    }
                    
                    break;
                    
            }
            
        }

        private void UpdateScreenPosition()
        {
            x = position.x;
            y = position.y;
        }
    }
}