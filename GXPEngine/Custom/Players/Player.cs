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
        private readonly float radius;
        private const float BRAKINGFORCE = 0.1f;
        private const float ACCELERATION = 1f;
        private static float GRAVITY = 10f;
        private readonly int playertype;
        private bool isPressingInteract;
        private bool hasKey;
        private bool youWin;

        //jump variables
        private const int JUMPINTERVAL = 1000;
        private const float JUMPSTRENGTH = 500f;
        private const float JUMPSTRENGTHSMALL = 200f;
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
            SetXY(position);
            playertype = player;
            SetOrigin(width/2,height/2);
            radius = height / 2;
        }
        
        protected void Update()
        {
	        Controls();
	        horizontalCollision = MoveUntilCollision(velocity.x, 0);
	        verticalCollision = MoveUntilCollision(0, velocity.y);
	        if (horizontalCollision != null)
	        {
		        velocity.x = 0;
		        Console.WriteLine("horizontal");
	        }
	        if (verticalCollision != null)
	        {
		        isGrounded = true;
		        velocity.y = 0;
		        Console.WriteLine("vertical");
	        }
	        //UpdateScreenPosition();
            // oldPosition = position;
            // Controls();
            // position += velocity;
            //firstCollision = FindEarliestCollision();
            
            // if (firstCollision != null)
            // {
	           //  //ResolveCollision(firstCollision);
	           //  switch (firstCollision.other)
	           //  {
		          //   case LineSegment l:
		          //   {
			         //    isGrounded = !l.isBottomLine;
			         //    break;
		          //   }
		          //   case Ball b:
		          //   {
			         //    isGrounded = !b.isBottomLine;
			         //    break;
		          //   }
	           //  }
            // } else if (firstCollision == null)
            // {
            //     isGrounded = false;
            // }

            if (youWin)
            {
	            MyGame myGame = (MyGame) game;
	            myGame.Reset();
	            youWin = false;
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
							width = 128;
                        }
						isPlayerOneTurnedAround = false;
						velocity += new Vector2(1f, 0) * ACCELERATION;
						SetCycle(4, 10, 5);
						Animate();
                    }

                    if (Input.GetKey(Key.A))
                    {
						if (width > 0)
						{
							width = -128;
						}
						isPlayerOneTurnedAround = true;
						velocity -= new Vector2(1f, 0) * ACCELERATION;
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
							width = 128;
						}
						isPlayerTwoTurnedAround = false;
						velocity += new Vector2(1f, 0) * ACCELERATION;
						SetCycle(3, 10, 5);
						Animate();
					}

					if (Input.GetKey(Key.DOWN) && Input.GetKey(Key.RIGHT))
					{
						if (isPlayerTwoTurnedAround)
						{
							width = 128;
						}
						isPlayerTwoTurnedAround = false;
						velocity += new Vector2(1f, 0) * ACCELERATION;
						SetCycle(15, 6, 6);
						Animate();
					}

					if (Input.GetKey(Key.LEFT) && !Input.GetKey(Key.DOWN))
                    {
						if (width > 0)
						{
							width = -128;
						}
						isPlayerTwoTurnedAround = true;
						velocity -= new Vector2(1f, 0) * ACCELERATION;
						SetCycle(3, 10, 5);
						Animate();
					}

					if (Input.GetKey(Key.DOWN) && Input.GetKey(Key.LEFT))
					{
						if (width > 0)
						{
							width = -128;
						}
						isPlayerTwoTurnedAround = true;
						velocity += new Vector2(-1f, 0) * ACCELERATION;
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
            
            
            // if (velocity.y < 0)
            // {
            //     velocity.y += GRAVITY;
            // }
            if (!isGrounded)
            {
	            velocity.y += GRAVITY;
            }
            // if (isGrounded)
            // {
            //     velocity.y -= GRAVITY;
            // }
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

        //private CollisionInfo FindEarliestCollision()
	    //{
        //MyGame myGame = (MyGame)game;
		// 	CollisionInfo lowestToi = null;
        //
		// foreach (Block t in myGame.blocks)  //caps collision
		// {
		// 	for (int i = 0; i < t.GetNumberOfCaps(); i++)
		// 	{
		// 		Ball mover = t.GetCaps(i);
		// 		// if (mover == this) continue;
		// 		//special check for performance reasons, seeing how far they are away from the ball before calculating
		// 		if(mover.position.x < position.x - MAX_DISTANCE || mover.position.y < position.y - MAX_DISTANCE ||
		// 		   mover.position.x > position.x + MAX_DISTANCE || mover.position.y > position.y + MAX_DISTANCE) continue;
		// 			
		// 		CollisionInfo ballcheck = Ballcheck(mover);
		// 		if (ballcheck == null) continue;
		// 		if (lowestToi == null || lowestToi.timeOfImpact > ballcheck.timeOfImpact)
		// 		{
		// 			lowestToi = ballcheck;
		// 		}
		// 		
		// 	}
  //
		// 	for (int i = 0; i < t.GetNumberOfLines(); i++) //line collisions
		// 	{
		// 		LineSegment l = t.GetLine(i);
		// 		Block g = (Block) l.owner;
		// 		//special check for performance reasons, seeing how far they are away from the ball before calculating
		// 		if (g.position.x < position.x - MAX_DISTANCE || g.position.x > position.x + MAX_DISTANCE ||		
		// 		    g.position.y < position.y - MAX_DISTANCE || g.position.y > position.y + MAX_DISTANCE) continue;
		// 		Vector2 diffVec = position - l.end;
		// 		Vector2 lineVec = l.end - l.start;
		// 		float ballDistance = lineVec.Normal().Dot(diffVec);
		// 		if (!(ballDistance < radius)) continue;
		// 		Vector2 oldDiffVec = oldPosition - l.end;
		// 		float a = lineVec.Normal().Dot(oldDiffVec) - radius;
		// 		float b = -lineVec.Normal().Dot(velocity);
		// 		if (!(a >= 0) || !(b > 0)) continue;
		// 		float toi = a / b;
		// 		Vector2 poi = oldPosition + velocity * toi;
		// 		Vector2 diffVec2 = l.end - poi;
		// 		Vector2 unitVec = lineVec.Normalized();
		// 		float distance = diffVec2.Dot(unitVec);
		// 		if (!(toi <= 1)) continue;
		// 		if (!(distance >= 0) || !(distance < lineVec.Length())) continue;
		// 		CollisionInfo lineCheck = new CollisionInfo(lineVec.Normal(), l, toi);
		// 		if (lowestToi == null || lowestToi.timeOfImpact > lineCheck.timeOfImpact)
		// 		{
		// 			lowestToi = lineCheck;
		// 		}
		// 	}
		// }
		// foreach (Laser t in myGame.lasers)  //caps collision
		// {
		// 	if (!t.isActive) continue;
		// 	for (int i = 0; i < t.GetNumberOfCaps(); i++)
		// 	{
		// 		Ball mover = t.GetCaps(i);
		// 		// if (mover == this) continue;
		// 		//special check for performance reasons, seeing how far they are away from the ball before calculating
		// 		if(mover.position.x < position.x - MAX_DISTANCE || mover.position.y < position.y - MAX_DISTANCE ||
		// 		   mover.position.x > position.x + MAX_DISTANCE || mover.position.y > position.y + MAX_DISTANCE) continue;
		// 			
		// 		CollisionInfo ballcheck = Ballcheck(mover);
		// 		if (ballcheck == null) continue;
		// 		if (lowestToi == null || lowestToi.timeOfImpact > ballcheck.timeOfImpact)
		// 		{
		// 			lowestToi = ballcheck;
		// 		}
		// 		
		// 	}
  //
		// 	for (int i = 0; i < t.GetNumberOfLines(); i++) //line collisions
		// 	{
		// 		LineSegment l = t.GetLine(i);
		// 		Laser g = (Laser) l.owner;
		// 		//special check for performance reasons, seeing how far they are away from the ball before calculating
		// 		if (g.x < position.x - MAX_DISTANCE || g.x > position.x + MAX_DISTANCE ||		
		// 		    g.y < position.y - MAX_DISTANCE || g.y > position.y + MAX_DISTANCE) continue;
		// 		Vector2 diffVec = position - l.end;
		// 		Vector2 lineVec = l.end - l.start;
		// 		float ballDistance = lineVec.Normal().Dot(diffVec);
		// 		if (!(ballDistance < radius)) continue;
		// 		Vector2 oldDiffVec = oldPosition - l.end;
		// 		float a = lineVec.Normal().Dot(oldDiffVec) - radius;
		// 		float b = -lineVec.Normal().Dot(velocity);
		// 		if (!(a >= 0) || !(b > 0)) continue;
		// 		float toi = a / b;
		// 		Vector2 poi = oldPosition + velocity * toi;
		// 		Vector2 diffVec2 = l.end - poi;
		// 		Vector2 unitVec = lineVec.Normalized();
		// 		float distance = diffVec2.Dot(unitVec);
		// 		if (!(toi <= 1)) continue;
		// 		if (!(distance >= 0) || !(distance < lineVec.Length())) continue;
		// 		CollisionInfo lineCheck = new CollisionInfo(lineVec.Normal(), l, toi);
		// 		if (lowestToi == null || lowestToi.timeOfImpact > lineCheck.timeOfImpact)
		// 		{
		// 			lowestToi = lineCheck;
		// 		}
		// 	}
		// }
		// return lowestToi;
		//
  //       }
  //       
  //       private CollisionInfo Ballcheck(Ball mover)
  //       {
	 //        Vector2 correctNormal = oldPosition - mover.position;
	 //        float a = Mathf.Pow(velocity.Length(),2);
	 //        if (a == 0) return null;
	 //        float b = (2 * correctNormal).Dot(velocity);
	 //        float c = Mathf.Pow(correctNormal.Length(),2) - Mathf.Pow(radius + mover.radius,2);
	 //        float d = Mathf.Pow(b, 2) - (4 * a * c);
	 //        float toi;
	 //        if (d < 0) return null;
	 //        if (c < 0)
	 //        {
		//         if (b < 0 || (a > -radius && a < 0))
		//         {
		// 	        toi = 0;
		//         }
		//         else return null;
	 //        }
	 //        else
	 //        {
		//         toi = (-b - Mathf.Sqrt(d))/ (2 * a);
	 //        }
  //
  //
	 //        return toi is < 0 or > 1 ? null : new CollisionInfo(correctNormal.Normalized(), mover, toi);
  //       }
  //       private void ResolveCollision(CollisionInfo col) {
		//         Vector2 poi = oldPosition + velocity * col.timeOfImpact;
		//         position = poi;
		//         velocity.Reflect(1,col.normal);
		//         velocity *= BOUNCINESS;
		//         MyGame myGame = (MyGame)game;
		//         if (col.other is not LineSegment l) return;
		//         switch (l.owner)
		//         {
		// 	        case Spike :
		// 		        myGame.LoseLife(l.owner);
		// 		        break; 
		// 	        case Laser :
		// 		        myGame.LoseLife(l.owner);
		// 		        break;
		// 	        case BreakableBlock b:
		// 		        if (isPressingInteract && playertype == 1)
		// 		        {
		// 			        b.Break();
		// 			        AudioPlayer.PlayAudio("Sounds/breaking_block.wav");
		// 		        }
		// 		        break;
		//         }
  //       }
  //
  //       private void UpdateScreenPosition()
  //       {
  //           x = position.x;
  //           y = position.y;
  //       }
    }
}