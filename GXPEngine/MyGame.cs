using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GXPEngine.Components;
using GXPEngine.Core;
using GXPEngine.Custom;
using GXPEngine.Custom.Collisions;
using GXPEngine.Custom.Level_components;
using TiledMapParser;

// System contains a lot of default C# libraries 
// GXPEngine contains the engine

// System.Drawing contains drawing tools such as Color definitions

namespace GXPEngine
{
	public class MyGame : Game
	{
		public readonly List<Block> blocks;
		public readonly List<Laser> lasers;
		public readonly List<KeyCollectable> keys;
		public readonly List<BreakableBlock> breakable;
		public readonly List<Button> buttons;
		public readonly List<Heart> hearts;
		public readonly List<ExitDoor> exitdoors;
		private EasyDraw healthDisplay;
		public int health = 4;
		private const int INVINCIBILITYTIME = 1000; 
		private int lastHitTime;
		private bool isPressed;
		private MyGame() : base(1600, 900, false)		// Create a window that's 800x600 and NOT fullscreen
		{
			blocks = new List<Block>();
			lasers = new List<Laser>();
			keys = new List<KeyCollectable>();
			breakable = new List<BreakableBlock>();
			buttons = new List<Button>();
			hearts = new List<Heart>();
			exitdoors = new List<ExitDoor>();
			Sprite startScreen = new Sprite("start_screen.png");
			AddChild(startScreen);

			Console.WriteLine("MyGame initialized");
		}
		
		public void LoseLife(GameObject causer) 
		{ 
			if (Time.time <= lastHitTime) return; 
			health--;
			if (causer is Spike)
			{
				AudioPlayer.PlayAudio("Sounds/spike.wav");
			}
			if (health <= 0)
			{
				Death();
			}
			lastHitTime = Time.time + INVINCIBILITYTIME; 
		}

		private void Death()
		{
			AudioPlayer.PlayAudio("Sounds/Death.wav");
			health = 3;
			LoadLevel(0);
		}

		void Update()
		{
			if (Input.GetKeyDown(Key.R))
			{
				LoadLevel(0);
			}

			if (!isPressed)
			{
				if (Input.GetMouseButtonDown(0))
				{
					if (Input.mouseX is > 608 and < 975 && Input.mouseY is > 665  and < 795)
					{
						LoadLevel(0);
						isPressed = true;
					}
				}
			}

			if (isPressed)
			{
				healthDisplay.Clear(64, 71, 82);
				healthDisplay.TextSize(32);
				healthDisplay.Text(health.ToString());
				
				Sprite healthIcon = new Sprite("Heart.png");
				healthIcon.SetXY(new Vector2(160, 90));
				AddChild(healthIcon);
			}

			if (Input.GetMouseButtonDown(0))
			{
				Console.WriteLine(new Vector2(Input.mouseX,Input.mouseY));
			}
		}

		public void LoadLevel(int index)
		{
			foreach (GameObject g in GetChildren())
			{
				g.LateDestroy();
			}
			foreach (Block b in blocks)
			{
				b.LateDestroy();
			}

			blocks.Clear();

			foreach (KeyCollectable k in keys)
			{
				k.LateDestroy();
			}

			keys.Clear();
			
			foreach (BreakableBlock k in breakable)
			{
				k.LateDestroy();
			}

			breakable.Clear();
			
			foreach (Laser k in lasers)
			{
				k.LateDestroy();
			}

			lasers.Clear();
			
			foreach (Button k in buttons)
			{
				k.LateDestroy();
			}

			keys.Clear();

			ArrayLevel level = new (index);
			AddChild(level);
			Sprite display = new Sprite("Profile_icon.png");
			display.SetScaleXY(0.5f,0.5f);
			display.SetXY(0,0);
			AddChild(display);
			healthDisplay = new EasyDraw(75,50);
			healthDisplay.SetXY(225,100);
			AddChild(healthDisplay);
		}

		public void Reset()
		{
			LoadLevel(0);
		}

		static void Main()							// Main() is the first method that's called when the program is run
		{
			new MyGame().Start();					// Create a "MyGame" and start it
		}
	}
}