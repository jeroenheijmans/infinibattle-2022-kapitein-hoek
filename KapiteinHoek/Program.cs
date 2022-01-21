using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using KapiteinHoek.Models;

namespace KapiteinHoek
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot.Start(Strategy, $"bot-{DateTime.UtcNow.ToString("dd_MM_yy_HH_mm_ss_ffff", CultureInfo.InvariantCulture)}.log");

            // var turnstate = new TurnState(new GameState
            // {
            //     Board = new LGameBoard
            //     {
            //         Board = new []
            //         {
            //             new PieceType[4] {PieceType.Empty, PieceType.Player2LPiece, PieceType.Player2LPiece, PieceType.Player2LPiece},
            //             new PieceType[4] {PieceType.Player1LPiece, PieceType.Player2LPiece, PieceType.Empty, PieceType.Empty},
            //             new PieceType[4] {PieceType.Player1LPiece, PieceType.NeutralPiece, PieceType.Empty, PieceType.Empty},
            //             new PieceType[4] {PieceType.Player1LPiece, PieceType.Player1LPiece, PieceType.NeutralPiece, PieceType.Empty},
            //         },
            //     },
            //     ScorePlayer1 = 0,
            //     ScorePlayer2 = 0,
            // }, 0, PieceType.Player1LPiece);
            //
            // Console.WriteLine(JsonConvert.SerializeObject(turnstate));
            //
            // var placePiecesCommand = Strategy(turnstate);
            //
            // Console.WriteLine(turnstate.GameState.Board);
            // Console.WriteLine(JsonConvert.SerializeObject(placePiecesCommand));
        }

        private static PlacePiecesCommand Strategy(TurnState turnstate)
        {
            // Retrieve all the possible moves allowed by us
            var possibleMoves = GetPossibleLPieceMoves(turnstate.GameState.Board, turnstate.Player);

            // If no possible move was found, your opponent has locked the board and we (sadly) lose the game
            if (possibleMoves.Count == 0)
            {
                throw new Exception("No possible move available :(");
            }

            // Retrieve the current board
            var board = turnstate.GameState.Board;

            // Clear the players LPiece board;
            board.RemoveCurrentPlayerLPieceFromBoard(turnstate.Player);

            // Select a random move
            var random = new Random();
            var lPieceMove = possibleMoves[random.Next(possibleMoves.Count)];

            // Place the selected move on the playing board
            // NOTE: You will receive a point for every move you make that places one coordinate of your L-piece in a corner,
            //       in case of a draw after x-time / x moves, the score will be the decider
            board.PlaceLPieceMoveForPlayer(lPieceMove, turnstate.Player);

            // Retrieve the neutral piece coordinates
            // NOTE: You do not have to move these pieces if you dont want to / if its more beneficial to keep them in place
            var neutralPieceCoordinates =
                turnstate.GameState.Board.RetrievePieceTypeCoordinates(PieceType.NeutralPiece);

            // Move a neutral piece with 75% probability
            if (random.Next(100) < 75)
            {
                // remove one of the neutralPieces coordinates
                var removedNeutralPieceIndex = random.Next() % 2;
                board.ClearSpace(neutralPieceCoordinates[removedNeutralPieceIndex]);

                // Retrieve all the free spaces on the board
                // NOTE: Your L-Piece has to be placed on the empty coordinates, make sure that the coordinates differ from your previous position
                var emptySpaceCoordinates = board.RetrievePieceTypeCoordinates(PieceType.Empty);

                // Select a random emptySpace and set the removedNeutralPieceIndex to this coordinate
                var newNeutralPieceLocation = emptySpaceCoordinates[random.Next(emptySpaceCoordinates.Count)];
                neutralPieceCoordinates[removedNeutralPieceIndex] = newNeutralPieceLocation;

                // Place the selected move on the playing board
                // NOTE: Placing the new location of the neutral piece is not required, but can be useful to do for debugging purposes and printing the board
                board.PlaceNeutralPiece(newNeutralPieceLocation);
            }

            return new PlacePiecesCommand(lPieceMove.Coordinates, neutralPieceCoordinates);
        }

        private static List<LPieceCoordinates> GetPossibleLPieceMoves(LGameBoard board, PieceType player)
        {
            // Retrieve your L-piece coordinates
            // NOTE: You are not allowed to place your L-Piece in these same coordinates
            var currentPlayerLPieceCoordinates =
                board.RetrievePieceTypeCoordinates(player);

            // Retrieve the component L-piece coordinates
            // NOTE: You are not allowed to overlap L-Pieces, so the can not be placed in these same coordinates
            var currentEnemyLPieceCoordinates = board.RetrievePieceTypeCoordinates(
                player == PieceType.Player1LPiece ? PieceType.Player2LPiece : PieceType.Player1LPiece);


            // Retrieve the neutral piece coordinates
            // NOTE: You are not allowed to overlap L-Pieces on these coordinates.
            var neutralPieceCoordinates =
                board.RetrievePieceTypeCoordinates(PieceType.NeutralPiece);

            // Retrieve all possible positions of an L-piece
            var possibleLpieceMoves = PossibleLPieceCoordinates.GetAllPossibleLpieces();

            // Remove my previous position as a possible move
            RemovePossibleMoveForCurrentPosition(possibleLpieceMoves, currentPlayerLPieceCoordinates);

            // Remove all moves that overlap for atleast one coordinate with the other remaining L-piece
            RemovePossibleMoveContainingCoordinate(possibleLpieceMoves, currentEnemyLPieceCoordinates);

            // Remove all moves that overlap for atleast one coordinate with the neutral pieces
            RemovePossibleMoveContainingCoordinate(possibleLpieceMoves, neutralPieceCoordinates);

            return possibleLpieceMoves;
        }

        private static void RemovePossibleMoveForCurrentPosition(
            List<LPieceCoordinates> possibleLPieceMoves,
            List<int[]> removeCoordinates)
        {
            // Check all remaining possibleLPieceMoves
            foreach (var possibleLPieceMove in possibleLPieceMoves)
            {
                // Check if all the coordinates are the same

                // We assume that we will find the coordinate we are checking for
                var possibleCoordinateFound = true;
                foreach (var coordinate in removeCoordinates)
                {
                    // If any of the coordinates for this possiblemove do not match for the possible move, this is not the current move for this position
                    if (!possibleLPieceMove.Coordinates.Any(c => c[0] == coordinate[0] && c[1] == coordinate[1]))
                    {
                        possibleCoordinateFound = false;
                        break;
                    }
                }

                // If all the coordinates matched, we found the PossibleMove matching our current position, we should remove it
                if (possibleCoordinateFound)
                {
                    possibleLPieceMoves.Remove(possibleLPieceMove);
                    break;
                }
            }
        }

        private static void RemovePossibleMoveContainingCoordinate(
            List<LPieceCoordinates> possibleLpieces,
            List<int[]> removeCoordinates)
        {
            // Check each coordinate that should not be present in any possible move
            foreach (var coordinate in removeCoordinates)
            {
                // Retrieve all the possible moves WHERE one or more (ANY) of the coordinates match with a coordinate that is not allowed for a possible move
                var overlappingCoordinates = possibleLpieces.Where(possibleLPiece =>
                    possibleLPiece.Coordinates.Any(c => c[0] == coordinate[0] && c[1] == coordinate[1])).ToList();

                // Remove all the found possiblemoves that are not allowed because they overlap
                // Since the object reference remains the same, we iterate backwards
                for (var i = overlappingCoordinates.Count-1; i >= 0; i--)
                {
                    possibleLpieces.Remove(overlappingCoordinates[i]);
                }
            }
        }
    }
}