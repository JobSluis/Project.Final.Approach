using System.Security.AccessControl;
using GXPEngine.Core;
using GXPEngine.Custom.Players;

namespace GXPEngine.Custom
{
    public class BigPlayer : Player
    {
        public BigPlayer(Vector2 position) : base(position, 1, "big_player_sheet.png", 4, 4, 16)
        {
            
        }

        void Update()
        {
            base.Update();
        }
    }
}