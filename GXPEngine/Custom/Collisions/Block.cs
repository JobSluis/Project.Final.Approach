using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GXPEngine.Components;
using GXPEngine.Core;
using GXPEngine.Custom.Collisions;

namespace GXPEngine.Custom
{
    public class Block : Sprite
    {
        public Vector2 position;
        private float radius;

        public Block(Vector2 position, string filename = "square.png") : base(filename)
        {
            
            this.position = position;
            radius = height / 2;
            SetXY(position);
        }

        void Update()
        {
            
        }

        protected override void OnDestroy()
        {
            MyGame myGame = (MyGame) game;
            myGame.blocks.Remove(this);
        }
    }
}