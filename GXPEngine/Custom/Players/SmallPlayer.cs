using GXPEngine.Core;
using GXPEngine.Custom.Players;

namespace GXPEngine.Custom
{
    public class SmallPlayer : Player
    {
        public SmallPlayer(Vector2 position) : base(position, 2, "small_player_sheet.png", 5, 5, 23)
        {
            
        } 
        
        void Update()
        {
            base.Update();
        }
    }
}