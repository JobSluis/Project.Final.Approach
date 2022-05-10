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
		public int health = 4;
		private MyGame() : base(1600, 900, false)		// Create a window that's 800x600 and NOT fullscreen
		{
			SmallPlayer player = new (100, 100,1);
			AddChild(player);
			BigPlayer player2 = new (400, 100,2);
			AddChild(player2);
			Chain chain = new (player, player2);
			AddChild(chain);

			
			StageManager.StageLoader.LoadStage(Stages.Test1);
			blocks = new List<Block>();
			
			for (int i = 0; i < 50;i++)
			{
				Block groundBlock = new (new Vector2(i*64,height - 50));
				AddChild(groundBlock);
				blocks.Add(groundBlock);
			}
			
			Spike spike = new (new Vector2(8*64,height - 120));
			AddChild(spike);
			blocks.Add(spike);
			
			Console.WriteLine("MyGame initialized");
		}

		void Update()
		{
			Console.WriteLine(health);
		}
		
		// public int GetNumberOfBlocks()
		// {
		// 	return blocks.Count;
		// }
		//
		// public Block GetBlocks(int index) {
		// 	if (index >= 0 && index < blocks.Count) {
		// 		return blocks [index];
		// 	}
		// 	return null;
		// }

		static void Main()							// Main() is the first method that's called when the program is run
		{
			new MyGame().Start();					// Create a "MyGame" and start it
		}
	}
}