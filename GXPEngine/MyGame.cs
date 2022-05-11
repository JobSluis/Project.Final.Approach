using System;
using System.Collections.Generic;
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

			
			//StageManager.StageLoader.LoadStage(Stages.Test1);
			blocks = new List<Block>();
			lasers = new List<Laser>();
			
			for (int i = 0; i < 50;i++)
			{
				Block groundBlock = new (new Vector2(i*64,height - 50));
				AddChild(groundBlock);
				blocks.Add(groundBlock);
			}
			
			Spike spike = new (new Vector2(8*64,height - 114));
			AddChild(spike);
			blocks.Add(spike);
			
			Spike spike2 = new (new Vector2(7*64,height - 114));
			AddChild(spike2);
			blocks.Add(spike2);

			Laser laser = new Laser(new Vector2(10*64-64-32,height - 114 - 64 - 64-16 - 64));
			AddChild(laser);
			lasers.Add(laser);

			Block block = new Block(new Vector2(10 * 64, height - 114 - 128 - 64));
			AddChild(block);
			blocks.Add(block);
			
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
			Console.WriteLine(health);
		}

		static void Main()							// Main() is the first method that's called when the program is run
		{
			new MyGame().Start();					// Create a "MyGame" and start it
		}
	}
}