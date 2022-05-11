using System.Security.AccessControl;

namespace GXPEngine.Custom
{
    public class BigPlayer : Player
    {
        public BigPlayer() : base(400, 100, 1, "big_player_idle.png", 1, 1, -1)
        {
        }

        void Update()
        {
            base.Update();
        }
    }
}