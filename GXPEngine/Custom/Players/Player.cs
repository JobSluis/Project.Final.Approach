using System;
using System.Drawing.Drawing2D;
using System.Security.AccessControl;
using GXPEngine.Components;
using GXPEngine.Core;
using GXPEngine.Custom.Collisions;

namespace GXPEngine.Custom
{
    public class Player : Sprite
    {
        public Vector2 position;
        public Vector2 velocity;
        private readonly float radius;
        private const float BRAKINGFORCE = 0.1f;
        private const float JUMPFORCE = 2500f;
        private const float ACCELERATION = 0.2f;
        private const float GRAVITY = 0.4f;
        private readonly int playertype;

        //jump variables
        private const int JUMPINTERVAL = 1000;
        private const float JUMPSTRENGTH = 15f;
        private int lastJumpTime;
        private bool isGrounded = false;
        
        //collision variables
        private Vector2 oldPosition;
        private CollisionInfo firstCollision;
        private const float BOUNCINESS = 0.98f;
        private const int MAX_DISTANCE = 250;


        protected Player(int x, int y, int player) : base("circle.png")
        {
            position = new Vector2(x, y);
            SetXY(x, y);
            playertype = player;
            SetOrigin(width/2,height/2);
            radius = height / 2;
        }
        
        protected void Update()
        {
            oldPosition = position;
            Controls();
            position += velocity;
            firstCollision = FindEarliestCollision();
            Console.WriteLine(firstCollision);

            if (firstCollision != null)
            {
                ResolveCollision(firstCollision);
                isGrounded = true;
            }
            else
            {
	            isGrounded = false;
            }
            UpdateScreenPosition();
        }

        private void Controls()
        {
            switch (playertype)
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

                        if (Input.GetKey(Key.UP) && Time.time > lastJumpTime)
                        {
                            Jump();
                            lastJumpTime = Time.time + JUMPINTERVAL;
                        }

                        if (Input.GetKey(Key.RIGHT))
                        {
                            velocity += new Vector2(1f, 0) * ACCELERATION;
                        }

                        if (Input.GetKey(Key.LEFT))
                        {
                            velocity += new Vector2(-1f, 0) * ACCELERATION;
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

            if (!isGrounded)
            {
	            velocity.y += GRAVITY;
            }
        }

        private void Jump()
        {
            isGrounded = false;
            velocity.y -= JUMPSTRENGTH;
        }

        private CollisionInfo FindEarliestCollision()
        {
            MyGame myGame = (MyGame)game;
			CollisionInfo lowestToi = null;

		foreach (Block t in myGame.blocks)  //caps collision
		{
			for (int i = 0; i < t.GetNumberOfCaps(); i++)
			{
				Ball mover = t.GetCaps(i);
				// if (mover == this) continue;
				//special check for performance reasons, seeing how far they are away from the ball before calculating
				if(mover.position.x < position.x - MAX_DISTANCE || mover.position.y < position.y - MAX_DISTANCE ||
				   mover.position.x > position.x + MAX_DISTANCE || mover.position.y > position.y + MAX_DISTANCE) continue;
					
				CollisionInfo ballcheck = Ballcheck(mover);
				if (ballcheck == null) continue;
				if (lowestToi == null || lowestToi.timeOfImpact > ballcheck.timeOfImpact)
				{
					lowestToi = ballcheck;
				}
				
			}

			for (int i = 0; i < t.GetNumberOfLines(); i++) //line collisions
			{
				LineSegment l = t.GetLine(i);
				Block g = (Block) l.owner;
				//special check for performance reasons, seeing how far they are away from the ball before calculating
				if (g.position.x < position.x - MAX_DISTANCE || g.position.x > position.x + MAX_DISTANCE ||		
				    g.position.y < position.y - MAX_DISTANCE || g.position.y > position.y + MAX_DISTANCE) continue;
				Vector2 diffVec = position - l.end;
				Vector2 lineVec = l.end - l.start;
				float ballDistance = lineVec.Normal().Dot(diffVec);
				if (!(ballDistance < radius)) continue;
				Vector2 oldDiffVec = oldPosition - l.end;
				float a = lineVec.Normal().Dot(oldDiffVec) - radius;
				float b = -lineVec.Normal().Dot(velocity);
				if (!(a >= 0) || !(b > 0)) continue;
				float toi = a / b;
				Vector2 poi = oldPosition + velocity * toi;
				Vector2 diffVec2 = l.end - poi;
				Vector2 unitVec = lineVec.Normalized();
				float distance = diffVec2.Dot(unitVec);
				if (!(toi <= 1)) continue;
				if (!(distance >= 0) || !(distance < lineVec.Length())) continue;
				CollisionInfo lineCheck = new CollisionInfo(lineVec.Normal(), l, toi);
				if (lowestToi == null || lowestToi.timeOfImpact > lineCheck.timeOfImpact)
				{
					lowestToi = lineCheck;
				}
			}
		}
		return lowestToi;
		
        }
        
        private CollisionInfo Ballcheck(Ball mover)
        {
	        Vector2 correctNormal = oldPosition - mover.position;
	        float a = Mathf.Pow(velocity.Length(),2);
	        if (a == 0) return null;
	        float b = (2 * correctNormal).Dot(velocity);
	        float c = Mathf.Pow(correctNormal.Length(),2) - Mathf.Pow(radius + mover.radius,2);
	        float d = Mathf.Pow(b, 2) - (4 * a * c);
	        float toi;
	        if (d < 0) return null;
	        if (c < 0)
	        {
		        if (b < 0 || (a > -radius && a < 0))
		        {
			        toi = 0;
		        }
		        else return null;
	        }
	        else
	        {
		        toi = (-b - Mathf.Sqrt(d))/ (2 * a);
	        }


	        return toi is < 0 or > 1 ? null : new CollisionInfo(correctNormal.Normalized(), mover, toi);
        }
        private void ResolveCollision(CollisionInfo col) {
		        Vector2 poi = oldPosition + velocity * col.timeOfImpact;
		        position = poi;
		        velocity.Reflect(1,col.normal);
		        velocity *= BOUNCINESS;
		        
		        
        }


        private void UpdateScreenPosition()
        {
            x = position.x;
            y = position.y;
        }
    }
}