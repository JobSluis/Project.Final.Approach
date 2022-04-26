using System;
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
        private const float JUMPFORCE = 10f;
        private const float ACCELERATION = 0.3f;
        private const float GRAVITY = 0.5f;
        private int player;

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

                        if (Input.GetKeyDown(Key.W))
                        {
                            velocity += new Vector2(0, -1f) * JUMPFORCE;
                        }
                        // if (Input.GetKey(Key.S))
                        // { 
                        //     velocity += new Vector2(0, 1f) * ACCELERATION;
                        // }

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
                            velocity += new Vector2(0, -1f) * JUMPFORCE;
                        }
                        // if (Input.GetKey(Key.DOWN))
                        // {
                        //     velocity += new Vector2(0, 1f) * ACCELERATION; 
                        // }

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
                velocity.x -= BRAKINGFORCE;
            }

            if (velocity.x < 0)
            {
                velocity.x += BRAKINGFORCE;
            }
            
            //these last 2 can be removed later, when we add jumping/gravity
            // if (velocity.y > 0) 
            // {
            //     velocity.y -= BRAKINGFORCE;
            // }

            // if (velocity.y <= 0)
            // {
            //     velocity.y += GRAVITY;
            // }

            velocity.y += GRAVITY;
            GameObject[] overlaps = GetCollisions (false,true);
            Console.WriteLine(overlaps);
            foreach (GameObject c in overlaps)
            {
                if (c is Sprite)
                {
                    velocity.y = 0;
                }
            }
        }

        private void UpdateScreenPosition()
        {
            x = position.x;
            y = position.y;
        }
    }
}