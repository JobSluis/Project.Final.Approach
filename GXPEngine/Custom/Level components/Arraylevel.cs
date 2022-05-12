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
		private const int SPIKE = 8;
		private const int LASER = 9;
		private const int HEART = 10;
		private const int EXITDOOR = 11;

		private readonly int[,,] levels =
		{
			//level 1 (index = 0)
			{                                                           
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,8,8,0,0,0,8,0,0,0,8,0,0,0,0,0,0,0,0,6,},
				{0,0,0,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,},
				{3,0,0,0,1,0,0,2,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,11,3,},
				{3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,3,},
				{3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,},
			},
		};

		
		public ArrayLevel(int index)
		{
			Sprite background = new Sprite("backgroundd.png");
			background.SetXY(-32,0);
			AddChild(background);
			Sprite display = new Sprite("Profile_icon.png");
			display.SetScaleXY(0.5f,0.5f);
			display.SetXY(0,0);
			AddChild(display);
			MyGame myGame = (MyGame)game;
			const int tileSize = 64;
			for (int rows = 0; rows < levels.GetLength(1); rows++)
			{
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
							Block block = new Block(position, filename);

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
							myGame.blocks.Add(breakable);
							break;

						case BUTTON:
							Button button = new Button(position, null); //TODO add the right door to the right button

							AddChild(button);
							myGame.buttons.Add(button);
							break;

						case DOOR:
							Door door = new Door(new Vector2(position.x,position.y - 128), this);

							AddChild(door);
							Console.WriteLine(position);
							break;

						case SPIKE:
							Spike spike = new Spike(position);

							AddChild(spike);
							myGame.blocks.Add(spike);
							break;

						case LASER:
							Laser laser = new Laser(new Vector2(position.x - 90, position.y));

							AddChild(laser);
							myGame.lasers.Add(laser);
							break;
						
						case HEART:
							Heart heart = new Heart(position);
							
							AddChild(heart);
							myGame.hearts.Add(heart);
							break;
						
						case EXITDOOR:
							ExitDoor exit = new ExitDoor(position);
							
							AddChild(exit);
							myGame.exitdoors.Add(exit);
							break;
					}
				}
			}

			BigPlayer big = FindObjectOfType<BigPlayer>();
			SmallPlayer small = FindObjectOfType<SmallPlayer>();
			if (big != null || small != null)
			{
				Chain chain = new Chain(big, small);
				AddChild(chain);
			}

			Button button2 = FindObjectOfType<Button>();
			Door door2 = FindObjectOfType<Door>();
			if (button2 == null || door2 == null) return;
			button2.door = door2;
		}
	}
}
