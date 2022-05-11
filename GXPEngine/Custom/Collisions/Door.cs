using GXPEngine.Core;

namespace GXPEngine.Custom.Collisions
{
    public class Door : GameObject
    {
        private Block block;
        private Block block2;
        private Block block3;
        public Door(Vector2 position)
        {
            block = new Block(position, "tile_1.png");
            position.y += 64;
            block2 = new Block(position, "tile_1.png");
            position.y += 64;
            block3 = new Block(position, "tile_1.png");
            Game.main.AddChild(block);
            Game.main.AddChild(block2);
            Game.main.AddChild(block3);
            MyGame myGame = (MyGame)game;
            myGame.blocks.Add(block);
            myGame.blocks.Add(block2);
            myGame.blocks.Add(block3);
        }

        void Update()
        {
            
        }
        
        protected override void OnDestroy()
        {
            MyGame myGame = (MyGame) game;
            block.Destroy();
            block2.Destroy();
            block3.Destroy();
            myGame.blocks.Remove(block);
            myGame.blocks.Remove(block2);
            myGame.blocks.Remove(block3);
        }
    }
}