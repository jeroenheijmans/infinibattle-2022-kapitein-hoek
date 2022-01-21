using System.Collections.Generic;

namespace KapiteinHoek.Models
{
    public class PlacePiecesCommand
    {
        public readonly IList<int[]> PlayerLPieceCoordinates; // the 4 new coordinates [x, y] of your L-piece
        public readonly IList<int[]> NeutralPieceCoordinates; // 2 coordinates [x, y] of the neutral pieces (only 1 piece can optionally be moved)


        public PlacePiecesCommand(IList<int[]> playerLPieceCoordinates, IList<int[]> neutralPieceCoordinates)
        {
            PlayerLPieceCoordinates = playerLPieceCoordinates;
            NeutralPieceCoordinates = neutralPieceCoordinates;
        }
    }
}