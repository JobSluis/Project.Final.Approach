using System;
using GXPEngine.Components;
using GXPEngine.Core;
using GXPEngine.Custom.Collisions;

namespace GXPEngine.Custom.Players
{   //TODO Crouching, animating and potentially fixing the movement if possible
    public class Player : AnimationSprite
    {
        public Vector2 position;
        public Vector2 velocity;
        private const float BRAKINGFORCE = 0.15f;
        private const float ACCELERATION = 0.02f;
        private static float GRAVITY = 0.05f;
        private readonly int playertype;
        private bool isPressingInteract;
        private bool hasKey;
        private bool youWin;

        //jump variables
        private const int JUMPINTERVAL = 1000;
        private const float JUMPSTRENGTH = 20f;
        private const float JUMPSTRENGTHSMALL = 25f;
        private int lastJumpTime;
        private bool isGrounded;
		private bool isPlayerOneTurnedAround = false;
		private bool isPlayerTwoTurnedAround = false;

		//collision variables
		private Vector2 oldPosition;
        private Collision horizontalCollision;
        private Collision verticalCollision;
        private static float BOUNCINESS = 0f;
        private const int MAX_DISTANCE = 250;


        public Player(Vector2 position, int player, string texturePath, int cols, int rows, int frames) : base(texturePath, cols, rows)
        {
	        collider.isTrigger = true;
	        SetXY(position);
            playertype = player;
            SetOrigin(width/2,height/2);
        }
        
        protected void Update()
        {
	        Controls();
	        CollisionChecks();
	        CheckWinCon();
        }

        private void CheckWinCon()
        {
	        if (youWin)
	        {
		        MyGame myGame = (MyGame) game;
		        myGame.Reset();
		        youWin = false;
	        }
        }

        private void CollisionChecks()
        {
	        horizontalCollision = MoveUntilCollision(velocity.x, 0);
	        verticalCollision = MoveUntilCollision(0, velocity.y);
	        if (horizontalCollision != null)
	        {
		        MyGame myGame = (MyGame)game;
		        velocity.x = 0;
		        switch (horizontalCollision.other)
		        {
			        case Spike:
				        myGame.LoseLife(horizontalCollision.other);
				        Console.WriteLine("lose life bitch");
				        break;
			        case Laser:
				        myGame.LoseLife(horizontalCollision.other);
				        break;
		        }

		        if (playertype == 1)
		        {
			        if (horizontalCollision.other is BreakableBlock b && isPressingInteract)
			        {
				        b.Break();
			        }
		        }
	        }
	        if (verticalCollision != null)
	        {
		        isGrounded = true;
		        velocity.y = 0;
		        MyGame myGame = (MyGame) game;
		        switch (verticalCollision.other)
		        {
			        //Console.WriteLine("vertical");
			        case Spike:
			        case Laser:
				        myGame.LoseLife(verticalCollision.other);
				        break;
		        }
	        }
	        else
	        {
		        velocity.y += GRAVITY * Time.deltaTime;
	        }
        }

        private void Controls()
        {
	        MyGame myGame = (MyGame)game;
	        switch (playertype)
            {
                case 1 :

                    if (Input.GetKeyDown(Key.W) && Time.time > lastJumpTime)
                    {
                        Jump();
                        lastJumpTime = Time.time + JUMPINTERVAL;
						SetCycle(13, 4, 5);
						Animate();
					}
                    
                    if (Input.GetKey(Key.D))
                    {
						if (isPlayerOneTurnedAround)
						{
							_mirrorX = false;
						}
						isPlayerOneTurnedAround = false;
						velocity += new Vector2(1f, 0) * ACCELERATION * Time.deltaTime;
						SetCycle(4, 10, 5);
						Animate();
                    }

                    if (Input.GetKey(Key.A))
                    {
						if (width > 0)
						{
							_mirrorX = true;
						}
						isPlayerOneTurnedAround = true;
						velocity -= new Vector2(1f, 0) * ACCELERATION * Time.deltaTime;
						SetCycle(4, 10, 5);
						Animate();
					}

                    if (Input.GetKey(Key.E))
                    {
	                    isPressingInteract = true;
	                    foreach (KeyCollectable k in myGame.keys)
	                    {
		                    if (HitTest(k))
		                    {
			                    k.Pickup();
			                    AudioPlayer.PlayAudio("Sounds/pick_up_key.wav");
			                    hasKey = true;
		                    }
	                    }
	                    
	                    foreach (ExitDoor k in myGame.exitdoors)
	                    {
		                    if (HitTest(k) && hasKey)
		                    {
			                    youWin = true;

		                    }
	                    }
	                    
	                    foreach (Heart k in myGame.hearts)
	                    {
		                    if (HitTest(k))
		                    {
			                    k.PickUp();
		                    }
	                    }

	                    foreach (Button b in myGame.buttons)
	                    {
		                    if (HitTest(b))
		                    {
			                    b.Press();
		                    }
	                    }
                    }
                    else
                    {
	                    isPressingInteract = false;
                    }

					if (!Input.AnyKey() && isGrounded)
					{
						SetFrame(0);
					}

					break;
                    
                case 2 :

					if (Input.GetKey(Key.UP) && Time.time > lastJumpTime)
                    {
                        Jump();
                        lastJumpTime = Time.time + JUMPINTERVAL;
						SetCycle(22, 2, 5);
						Animate();
					}

					if (Input.GetKey(Key.RIGHT) && !Input.GetKey(Key.DOWN))
                    {
						if (isPlayerTwoTurnedAround)
						{
							_mirrorX = false;
						}
						isPlayerTwoTurnedAround = false;
						velocity += new Vector2(1f, 0) * ACCELERATION * Time.deltaTime;
						SetCycle(3, 10, 5);
						Animate();
					}

					if (Input.GetKey(Key.DOWN) && Input.GetKey(Key.RIGHT))
					{
						if (isPlayerTwoTurnedAround)
						{
							_mirrorX = true;
						}
						isPlayerTwoTurnedAround = false;
						velocity += new Vector2(1f, 0) * ACCELERATION * Time.deltaTime;
						SetCycle(15, 6, 6);
						Animate();
					}

					if (Input.GetKey(Key.LEFT) && !Input.GetKey(Key.DOWN))
                    {
						if (width > 0)
						{
							_mirrorX = true;
						}
						isPlayerTwoTurnedAround = true;
						velocity -= new Vector2(1f, 0) * ACCELERATION * Time.deltaTime;
						SetCycle(3, 10, 5);
						Animate();
					}

					if (Input.GetKey(Key.DOWN) && Input.GetKey(Key.LEFT))
					{
						if (width > 0)
						{
							_mirrorX = true;
						}
						isPlayerTwoTurnedAround = true;
						velocity += new Vector2(-1f, 0) * ACCELERATION * Time.deltaTime;
						SetCycle(15, 6, 6);
						Animate();
					}

					if (Input.GetKey(Key.RIGHT_CTRL))
                    {
	                    foreach (KeyCollectable k in myGame.keys)
	                    {
		                    if (HitTest(k))
		                    {
			                    k.Pickup();
			                    hasKey = true;
		                    }
	                    }
	                    
	                    foreach (ExitDoor k in myGame.exitdoors)
	                    {
		                    if (HitTest(k) && hasKey)
		                    {
			                    youWin = true;
			                    
		                    }
	                    }

	                    
	                    
	                    foreach (Button b in myGame.buttons)
	                    {
		                    if (HitTest(b))
		                    {
			                    b.Press();
		                    }
	                    } 
	                    
	                    foreach (Heart k in myGame.hearts)
	                    {
		                    if (HitTest(k))
		                    {
			                    k.PickUp();
		                    }
	                    }
                    }

					if (!Input.AnyKey() && isGrounded)
					{
						SetFrame(0);
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
            
            if (!isGrounded)
            {
	            velocity.y += GRAVITY;
            }
        }

        private void Jump()
        {
            isGrounded = false;
            if (playertype != 1)
            {
	            velocity.y -= JUMPSTRENGTHSMALL;
            }
            else
            {
	            velocity.y -= JUMPSTRENGTH;
            }
        }
    }
}