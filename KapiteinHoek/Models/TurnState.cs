namespace KapiteinHoek.Models
{
    public class TurnState
    {
        public TurnState(GameState gameState, int turn, int player)
        {
            GameState = gameState;
            Turn = turn;
            Player = (PieceType)player+1;
        }

        public GameState GameState { get; }
        public int Turn { get; }
        public PieceType Player { get; }
    }
}