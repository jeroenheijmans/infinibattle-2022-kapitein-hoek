using System;
using System.Collections.Generic;
using System.Text;

namespace StarterBot.Models
{
    public class LGameBoard
    {
        // Board should always be 4x4 and will be validated by the server
        public PieceType[][] Board { get; set; }

        // Retrieve the coordinates of a given piece type, e.g. the coordinates of your / your opponents L-piece or the neutral pieces
        public List<int[]> RetrievePieceTypeCoordinates(PieceType pieceType)
        {
            var currentPieceTypeCoordinates = new List<int[]>();
            for (int y = 0; y < Board.Length; y++)
            {
                for (int x = 0; x < Board[y].Length; x++)
                {
                    if (Board[y][x] == pieceType)
                    {
                        currentPieceTypeCoordinates.Add(new int[2] {x, y});
                    }
                }
            }

            return currentPieceTypeCoordinates;
        }

        // Remove a PieceType from the board. CAUTION: Only remove your own player
        public void RemoveCurrentPlayerLPieceFromBoard(PieceType player)
        {
            for (int y = 0; y < Board.Length; y++)
            {
                for (int x = 0; x < Board[y].Length; x++)
                {
                    if (Board[y][x] == player)
                    {
                        ClearSpace(new []{x, y});
                    }
                }
            }
        }

        public void ClearSpace(int[] coordinate)
        {
            Board[coordinate[1]][coordinate[0]] = PieceType.Empty;
        }

        public void PlaceLPieceMoveForPlayer(LPieceCoordinates lPieceMove, PieceType turnstatePlayer)
        {
            foreach (var coordinate in lPieceMove.Coordinates)
            {
                Board[coordinate[1]][coordinate[0]] = turnstatePlayer;
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            for (int y = 0; y < Board.Length; y++)
            {
                for (int x = 0; x < Board[y].Length; x++)
                {
                    result.Append((int)Board[y][x]);
                }
                result.AppendLine("");
            }

            return result.ToString();
        }

        public void PlaceNeutralPiece(int[] coordinate)
        {
            Board[coordinate[1]][coordinate[0]] = PieceType.NeutralPiece;
        }
    }
}