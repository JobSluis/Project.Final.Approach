using System;
using System.Drawing.Drawing2D;
using GXPEngine.Core;

namespace GXPEngine.Custom
{
    public class Player : Sprite
    {
        public Vector2 position;
        public Vector2 velocity;
        private const float BRAKINGFORCE = 0.1f;
        private const float JUMPFORCE = 10f;
        private const float ACCELERATION = 0.4f;
        private const float GRAVITY = 0.4f;
        private int player;
        
        //jump variables
        private const int JUMPINTERVAL = 1000;
        private const float JUMPSTRENGTH = 5f;
        private int lastJumpTime;
        private bool isGrounded = false;

        public Player(int x, int y, int player) : base("circle.png")
        {
            position = new Vector2(x, y);
            SetXY(x, y);
            this.player = player;
            SetOrigin(width/2,height/2);
        }

        void Update()
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

                        if (Input.GetKeyDown(Key.W) && Time.time > lastJumpTime)
                        {
                            Jump();
                            lastJumpTime = Time.time + JUMPINTERVAL;
                        }

                        if (Input.GetKey(Key.D))
                        {
                            velocity += new Vector2(1f, 0) * ACCELERATION;
                        }

                        if (Input.GetKey(Key.A))
                        {
                            velocity += new Vector2(-1f, 0) * ACCELERATION;
                        }

                        break;
                    
                    case 2 :

                        if (Input.GetKey(Key.UP))
                        {
                            Jump();
                        }

                        if (Input.GetKey(Key.RIGHT))
                        {
                            velocity += new Vector2(1f, 0);
                        }

                        if (Input.GetKey(Key.LEFT))
                        {
                            velocity += new Vector2(-1f, 0);
                        }
                        
                        break;
            }
            
            if (velocity.x > 0)
            {
                if (velocity.x < BRAKINGFORCE)
                {
                    velocity.x = 0;
                }
                velocity.x -= BRAKINGFORCE;
            }

            if (velocity.x < 0)
            {
                if (velocity.x > BRAKINGFORCE)
                {
                    velocity.x = 0;
                }
                velocity.x += BRAKINGFORCE;
            }
            

            if (velocity.y < 0)
            {
                velocity.y += GRAVITY;
            }
            
        }

        private void Jump()
        {
            isGrounded = false;
            velocity.y -= JUMPSTRENGTH;
        }

        private void UpdateScreenPosition()
        {
            x = position.x;
            y = position.y;
        }
    }
}