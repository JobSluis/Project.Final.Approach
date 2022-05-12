using System.Drawing;
using System.Drawing.Drawing2D;
using GXPEngine.Core;

namespace GXPEngine.Custom
{
    public class Chain : GameObject
    {
        private readonly Player player1;
        private readonly Player player2;
        private const float MAX_CHAIN_LENGTH = 350;
        private const float ULTIMATE_MAX_CHAIN_LENGTH = 500;
        private const float CHAINSTRENGTH = 7f;
        private float chainLength;
        private uint lineColor;
        

        public Chain(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        }

        private void Update()
        {
            chainLength = (player1.position - player2.position).Length();
            ColorCheck();
            Gizmos.DrawLine(player1.position.x, player1.position.y,player2.position.x,player2.position.y,null,lineColor);
            LengthCheck();
        }

        private void LengthCheck()
        {
            Vector2 positionDelta = player1.position - player2.position;
            
            if (chainLength < MAX_CHAIN_LENGTH) return;
            player2.velocity += positionDelta.Normalized() * CHAINSTRENGTH;
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