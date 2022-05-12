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
		public int health = 4;
		private const int INVINCIBILITYTIME = 1000; 
		private int lastHitTime;
		private MyGame() : base(1600, 900, false)		// Create a window that's 800x600 and NOT fullscreen
		{
			blocks = new List<Block>();
			lasers = new List<Laser>();
			keys = new List<KeyCollectable>();
			breakable = new List<BreakableBlock>();
			buttons = new List<Button>();
			hearts = new List<Heart>();
			exitdoors = new List<ExitDoor>();
			LoadLevel(0);

			Console.WriteLine("MyGame initialized");
		}
		
		public void LoseLife() 
		{ 
			if (Time.time <= lastHitTime) return; 
			health--; 
			lastHitTime = Time.time + INVINCIBILITYTIME; 
		} 
		

		void Update()
		{
			if (Input.GetKeyDown(Key.R))
			{
				LoadLevel(0);
			}
		}
		
		private void LoadLevel(int index)
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
		}

		static void Main()							// Main() is the first method that's called when the program is run
		{
			new MyGame().Start();					// Create a "MyGame" and start it
		}
	}
}