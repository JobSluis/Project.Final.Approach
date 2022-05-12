using GXPEngine.Core;
using GXPEngine.Custom.Players;

namespace GXPEngine.Custom
{
    public class SmallPlayer : Player
    {
        public SmallPlayer(Vector2 position) : base(position, 2, "small_player_idle.png", 1, 1, -1)
        {
            
        } 
        
        void Update()
        {
            base.Update();
        }
    }
}