using System;
using GXPEngine.Custom;
// System contains a lot of default C# libraries 
// GXPEngine contains the engine

// System.Drawing contains drawing tools such as Color definitions

namespace GXPEngine
{
	public class MyGame : Game
	{
		public MyGame() : base(1600, 900, false)		// Create a window that's 800x600 and NOT fullscreen
		{
			Player player = new Player(100, 100,1);
			AddChild(player);
			Player player2 = new Player(400, 100,2);
			AddChild(player2);
			Chain chain = new Chain(player, player2);
			AddChild(chain);
			for (int i = 0; i < 50;i++)
			{
				Sprite groundBlock = new Sprite("square.png");
				groundBlock.SetXY(i*64,height/2);
				AddChild(groundBlock);
				Console.WriteLine(groundBlock.y);
			}
			Console.WriteLine("MyGame initialized");
		}
	
		void Update()
		{
		
		}

		static void Main()							// Main() is the first method that's called when the program is run
		{
			new MyGame().Start();					// Create a "MyGame" and start it
		}
	}
}