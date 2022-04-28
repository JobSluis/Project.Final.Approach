namespace GXPEngine.Custom
{
    public class SmallPlayer : Player
    {
        public SmallPlayer(int x, int y, int player) : base(x, y, player)
        {
            
        } 
        
        void Update()
        {
            Controls();
            position += velocity;
            UpdateScreenPosition();
        }
    }
}