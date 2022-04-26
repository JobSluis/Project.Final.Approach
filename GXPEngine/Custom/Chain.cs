namespace GXPEngine.Custom
{
    public class Chain : GameObject
    {
        private Player player1;
        private Player player2;
        private const float MAX_CHAIN_LENGTH = 500;

        public Chain(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        }

        private void Update()
        {
            Gizmos.DrawLine(player1.position.x, player1.position.y,player2.position.x,player2.position.y);
            LengthCheck();
        }

        private void LengthCheck()
        {
            float chainLength = (player1.position - player2.position).Length();
            if (chainLength < MAX_CHAIN_LENGTH) return;
            player1.velocity = player2.velocity;
        }
    }
}