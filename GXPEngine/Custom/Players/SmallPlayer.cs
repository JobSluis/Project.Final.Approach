namespace GXPEngine.Custom
{
    public class SmallPlayer : Player
    {
        public SmallPlayer() : base(400, 750, 2, "small_player_idle.png", 1, 1, -1)
        {
        } 
        
        void Update()
        {
            base.Update();
            Gizmos.DrawArrow(position.x, position.y, position.x, position.y + height);
        }
    }
}