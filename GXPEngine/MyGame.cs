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
		private int health = 4;
		private const int INVINCIBILITYTIME = 1000; 
		private int lastHitTime; 
		private MyGame() : base(1600, 900, false)		// Create a window that's 800x600 and NOT fullscreen
		{
			SmallPlayer player = new();
			AddChild(player);
			BigPlayer player2 = new();
			AddChild(player2);
			Chain chain = new (player, player2);
			AddChild(chain);
			Sprite background = new Sprite("backgroundd.png");
			AddChild(background);
			Sprite display = new Sprite("Profile_icon.png");
			display.SetScaleXY(0.5f,0.5f);
			display.SetXY(0,0);
			AddChild(display);
			

			//TODO Level loader
			//StageManager.StageLoader.LoadStage(Stages.Test1);
			blocks = new List<Block>();
			lasers = new List<Laser>();
			keys = new List<KeyCollectable>();
			breakable = new List<BreakableBlock>();
			buttons = new List<Button>();

			
			for (int i = 0; i < 50;i++)
			{
				int rand = Utils.Random(1, 3);
				string filename = rand switch
				{
					1 => "tile_1.png",
					2 => "tile_2.png",
					3 => "tile_3.png",
					_ => "tile_1.png"
				};
				Block groundBlock = new (new Vector2(i*64,height - 50), filename);
				AddChild(groundBlock);
				blocks.Add(groundBlock);
			}
			
			Spike spike = new(new Vector2(8*64,height - 114));
			AddChild(spike);
			blocks.Add(spike);
			
			Spike spike2 = new(new Vector2(7*64,height - 114));
			AddChild(spike2);
			blocks.Add(spike2);

			Laser laser = new (new Vector2(10*64-64-32,height - 322));
			AddChild(laser);
			lasers.Add(laser);

			Block block = new (new Vector2(10 * 64, height - 306));
			AddChild(block);
			blocks.Add(block);

			KeyCollectable key = new (new Vector2(5 * 64,height - 114-114));
			AddChild(key);
			keys.Add(key);

			BreakableBlock breakBlock = new(new Vector2(6 * 64, height - 114 - 114));
			AddChild(breakBlock);
			blocks.Add(breakBlock);
			breakable.Add(breakBlock);
			
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
			//Console.WriteLine(health);
		}

		static void Main()							// Main() is the first method that's called when the program is run
		{
			new MyGame().Start();					// Create a "MyGame" and start it
		}
	}
}