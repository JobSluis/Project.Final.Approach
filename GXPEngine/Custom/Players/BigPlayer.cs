using System.Security.AccessControl;
using GXPEngine.Core;
using GXPEngine.Custom.Players;

namespace GXPEngine.Custom
{
    public class BigPlayer : Player
    {
        public BigPlayer(Vector2 position) : base(position, 1, "big_player_idle.png", 1, 1, -1)
        {
            
        }

        void Update()
        {
            base.Update();
        }
    }
}