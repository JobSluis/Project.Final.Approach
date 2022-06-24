using System.Drawing;
using System.Drawing.Drawing2D;
using GXPEngine.Core;
using GXPEngine.Custom.Players;

namespace GXPEngine.Custom
{
    public class Chain : GameObject
    {
        private readonly Player player1;
        private readonly Player player2;
        private const float MAX_CHAIN_LENGTH = 400;
        private const float ULTIMATE_MAX_CHAIN_LENGTH = 500;
        private const float CHAINSTRENGTH = 1f;
        private float chainLength;
        private uint lineColor;
        

        public Chain(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        }

        private void Update()
        {
            chainLength = (new Vector2(player1.x,player1.y) - new Vector2(player2.x,player2.y)).Length();
            ColorCheck();
            Gizmos.DrawLine(player1.x, player1.y,player2.x,player2.y,null,lineColor);
            LengthCheck();
        }

        private void LengthCheck()
        {
            Vector2 positionDelta = new Vector2(player1.x,player1.y) - new Vector2(player2.x,player2.y);
            
            if (chainLength < MAX_CHAIN_LENGTH) return;
            player2.velocity += positionDelta.Normalized() * CHAINSTRENGTH * Time.deltaTime;
            // if (chainLength > ULTIMATE_MAX_CHAIN_LENGTH)
            // {
            //     player1.velocity += positionDelta.Normalized() * CHAINSTRENGTH;
            // }
            
        }

        private void ColorCheck()
        {
            lineColor = chainLength > MAX_CHAIN_LENGTH ? 0xffff0000 : 0xffffffff;
        }
    }
}