using System.Collections.Generic;
using GXPEngine.Components;
using GXPEngine.Core;

namespace GXPEngine.Custom.Collisions
{
    public class BreakableBlock : Block
    {
        public BreakableBlock(Vector2 position) : base(position,"tile.png")
        {
            
        }

        void Update()
        {
            
        }

        public void Break()
        {
            Destroy();
        }
        
        protected override void OnDestroy()
        {
            MyGame myGame = (MyGame) game;
            myGame.blocks.Remove(this);
            myGame.breakable.Remove(this);
        }
    }
}