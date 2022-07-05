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
		private const int BUTTON1 = 12;
		private const int DOOR1 = 13;
		private const int BUTTON2 = 14;
		private const int DOOR2 = 15;
		private const int BUTTON3 = 16;
		private const int DOOR3 = 17;

		private readonly int[,,] levels =
		{
			//level 1 (index = 0)
			{                                                           
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,4,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,3,3,0,8,8,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,6,},
				{0,0,0,0,0,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,},
				{3,0,0,0,1,0,0,2,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,11,3,},
				{3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,3,},
				{3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,},
			},

			//level 2 (index = 1)
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,12,7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,},
				{0,0,0,0,0,0,9,5,5,5,5,5,5,0,0,0,0,0,0,3,3,3,3,3,3,},
				{0,0,0,0,0,0,0,5,5,5,5,5,5,5,0,0,0,0,3,0,0,3,0,0,0,},
				{0,0,0,0,0,0,0,5,5,5,5,5,5,5,0,0,0,5,0,0,0,3,0,0,0,},
				{0,0,0,0,0,0,0,5,5,5,5,5,5,5,0,0,5,0,0,0,0,3,0,0,0,},
				{0,0,0,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,},
				{3,0,0,0,0,0,0,0,0,16,0,0,0,3,0,0,0,0,0,0,0,13,0,14,0,},
				{0,3,0,0,0,0,0,0,0,3,3,3,3,0,9,0,0,0,0,3,3,3,3,3,3,},
				{0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{11,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,},
				{0,0,17,0,5,0,0,0,0,1,0,2,0,0,0,0,0,0,0,15,0,0,0,0,0,},
				{3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0,0,0,0,},
			},

            //level 3 (index = 2)
            {
				{0,0,0,0,0,0,0,0,0,0,9,0,0,9,0,0,9,0,0,0,0,0,3,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,},
				{8,16,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,6,0,0,0,3,0,0,},
				{3,3,3,0,0,0,0,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0,0,0,0,},
				{0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,},
				{0,0,0,0,0,3,3,14,0,0,0,0,0,0,0,0,0,0,0,0,0,0,17,0,12,},
				{0,0,0,0,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,},
				{0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,},
				{0,0,3,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,0,0,0,0,0,},
				{0,5,0,0,0,0,0,0,0,0,3,0,3,0,0,3,0,0,3,0,0,0,0,0,0,},
				{0,5,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
				{3,3,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,11,},
				{3,3,3,0,1,0,2,3,0,0,4,0,13,0,0,15,0,0,7,0,0,0,8,0,0,},
				{3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,},
			},
            
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

		
		public ArrayLevel(int index)
		{
			Sprite background = new Sprite("backgroundd.png",false,false);
			background.SetXY(-32,0);
			AddChild(background);
			MyGame myGame = (MyGame)game;
			EasyDraw border = new EasyDraw(1, myGame.height);
			border.SetXY(-1,-1);
			EasyDraw border2 = new EasyDraw(myGame.width, 1);
			border2.SetXY(-1,-1);
			// EasyDraw border3 = new EasyDraw(myGame.width, 1);
			// border3.SetXY(-1,myGame.height + 1);
			EasyDraw border4 = new EasyDraw(1, myGame.height);
			border4.SetXY(myGame.width + 1,-1);
			AddChild(border);
			AddChild(border2);
			// AddChild(border3);
			AddChild(border4);
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
							Button button = new Button(position, null);

							AddChild(button);
							myGame.buttons.Add(button);
							break;

						case DOOR:
							Door door = new Door(new Vector2(position.x,position.y - 128), this);

							AddChild(door);
							myGame.doors.Add(door);
							Console.WriteLine(position);
							break;

						case SPIKE:
							Spike spike = new Spike(position);

							AddChild(spike);
							myGame.blocks.Add(spike);
							break;

						case LASER:
							Laser laser = new Laser(new Vector2(position.x - 61, position.y));

							AddChild(laser);
							myGame.lasers.Add(laser);
							break;
						
						case HEART:
							Heart heart = new Heart(position);
							
							AddChild(heart);
							myGame.hearts.Add(heart);
							break;
						
						case EXITDOOR:
							ExitDoor exit = new ExitDoor(new Vector2(position.x - 32,position.y + 16));
							
							AddChild(exit);
							myGame.exitdoors.Add(exit);
							break;
						
						case BUTTON1:
							Button button1 = new Button(position, null,1);

							AddChild(button1);
							myGame.buttons.Add(button1);
							break;
						
						case BUTTON2:
							Button button2 = new Button(position, null,2);

							AddChild(button2);
							myGame.buttons.Add(button2);
							break;
						
						case BUTTON3:
							Button button3 = new Button(position, null,3);

							AddChild(button3);
							myGame.buttons.Add(button3);
							break;
						case DOOR1:
							Door door1 = new Door(new Vector2(position.x,position.y - 128), this,1);

							AddChild(door1);
							myGame.doors.Add(door1);
							Console.WriteLine(position);
							break;
						case DOOR2:
							Door door2 = new Door(new Vector2(position.x,position.y - 128), this,2);

							AddChild(door2);
							myGame.doors.Add(door2);
							Console.WriteLine(position);
							break;
						case DOOR3:
							Door door3 = new Door(new Vector2(position.x,position.y - 128), this,3);

							AddChild(door3);
							myGame.doors.Add(door3);
							Console.WriteLine(position);
							break;
						
					}
				}
			}

			BigPlayer big = FindObjectOfType<BigPlayer>();
			SmallPlayer small = FindObjectOfType<SmallPlayer>();
			if (big != null || small != null)
			{
				Chain chain = new Chain(big, small);
				Console.WriteLine("chain added");
				AddChild(chain);
			}

			// Button button2 = FindObjectOfType<Button>();
			// Door door2 = FindObjectOfType<Door>();
			// if (button2 == null || door2 == null) return;
			// button2.door = door2;
			Button b = null;
			Button b1 = null;
			Button b2= null;
			Button b3= null;
			Door d= null;
			Door d1= null;
			Door d2= null;
			Door d3= null;
			
			foreach (Button button in myGame.buttons)
			{
				switch (button.index)
				{
					case 0:
						b = button;
						break;
					case 1:
						b1 = button;
						break;
					case 2:
						b2 = button;
						break;
					case 3:
						b3 = button;
						break;
				}
			}
			foreach (Door button1 in myGame.doors)
			{
				switch (button1.index)
				{
					case 0:
						d = button1;
						break;
					case 1:
						d1 = button1;
						break;
					case 2:
						d2 = button1;
						break;
					case 3:
						d3 = button1;
						break;
				}
			}

			if (b != null)
			{
				b.door = d;
			}
			if (b1 != null)
			{
				b1.door = d1;
			}
			if (b2 != null)
			{
				b2.door = d2;
			}
			if (b3 != null)
			{
				b3.door = d3;
			}
		}
	}
}
