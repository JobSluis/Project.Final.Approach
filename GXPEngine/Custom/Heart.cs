using GXPEngine.Core;

namespace GXPEngine.Custom
{
    public class Heart : Sprite
    {
        public Heart(Vector2 position) : base("Heart.png")
        {
            SetXY(position);
        }

        void Update()
        {
            
        }

        public void PickUp()
        {
            MyGame myGame = (MyGame) game;
            myGame.health++;
            Destroy();
        }
    }
}