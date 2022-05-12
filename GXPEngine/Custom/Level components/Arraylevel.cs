using System;
using GXPEngine.Core;
using GXPEngine.Custom.Collisions;

namespace GXPEngine.Custom.Level_components
{
    
    public class ArrayLevel : GameObject
	{
		private const int PLAYER1 = 1;
		private const int PLAYER2 = 2;
		private const int BLOCK = 3;
		private const int KEY = 4;
		private const int BREAKABLE = 5;
		private const int BUTTON = 6;
		private const int DOOR = 7;

		private readonly int[,,] levels =
		{
			//level 1 (index = 0)
			{                                                           
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
			},
		};

		//initializing all the objects in the level
		public ArrayLevel(int index)
		{
			MyGame myGame = (MyGame)game;
			const int tileSize = 64;
			for (int rows = 0; rows < levels.GetLength(1); rows++)
			for (int cols = 0; cols < levels.GetLength(2); cols++)
			{
				int tileType = levels[index, rows, cols];
				Vector2 position = new Vector2(tileSize * cols, tileSize * rows);
				switch (tileType)
				{
					case PLAYER1:
						BigPlayer player2 = new(position);
						AddChild(player2);
						break;
					
					case PLAYER2:
						SmallPlayer player = new(position);
						AddChild(player);
						break;
					
					case BLOCK:
						int rand = Utils.Random(1, 3);
						string filename = rand switch
						{
							1 => "tile_1.png",
							2 => "tile_2.png",
							3 => "tile_3.png",
							_ => "tile_1.png"
						};
						Block block = new Block(position,filename);
						
						AddChild(block);
						myGame.blocks.Add(block);
						break;
					
					case KEY:
						KeyCollectable key = new KeyCollectable(position);
						
						AddChild(key);
						myGame.keys.Add(key);
						break;
					
					case BREAKABLE:
						BreakableBlock breakable = new BreakableBlock(position);
						
						AddChild(breakable);
						myGame.breakable.Add(breakable);
						break;
					
					case BUTTON:
						Button button = new Button(position, null); //TODO add the right door to the right button
						
						AddChild(button);
						myGame.buttons.Add(button);
						break;
					case DOOR:
						Door door = new Door(position);
						
						AddChild(door);
						break;
				}
			}
		}
	}
}
