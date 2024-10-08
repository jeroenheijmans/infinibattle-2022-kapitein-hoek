﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using KapiteinHoek.Models;

namespace KapiteinHoek
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger($"bot-{DateTime.UtcNow.ToString("dd_MM_yy_HH_mm_ss_ffff", CultureInfo.InvariantCulture)}.log");
            Bot.Start(Strategy, logger);
        }

        private static PlacePiecesCommand Strategy(TurnState turnstate)
        {
            var possibleMoves = GetPossibleLPieceMoves(turnstate.GameState.Board, turnstate.Player);

            if (possibleMoves.Count == 0)
            {
                throw new Exception("Kapitein Hoek heeft een krokodil ontmoet. Eentje die beter is in L-Game dan de Kapitein zelf! We kunnen geen zetten meer doen, dus verloren!");
            }

            var board = turnstate.GameState.Board;
            board.RemoveCurrentPlayerLPieceFromBoard(turnstate.Player);

            // Select the first move we find, preferring those (A) in a corner and secondary (B) without the 3-way side to the walls
            var lPieceMove = possibleMoves
                .OrderByDescending(m => m.TouchesCornerField)
                .ThenBy(m => m.HasLongSideAgainstOutside)
                .First();
            board.PlaceLPieceMoveForPlayer(lPieceMove, turnstate.Player);

            // Move neutral piece to a corner if we can to prevent others from going there
            var neutralPieceCoordinates = turnstate.GameState.Board.RetrievePieceTypeCoordinates(PieceType.NeutralPiece);
            var neutralPieceNotInCorner = neutralPieceCoordinates.FirstOrDefault(p => !p.IsCorner());
            if (neutralPieceNotInCorner != null)
            {
                var removedNeutralPieceIndex = neutralPieceCoordinates.IndexOf(neutralPieceNotInCorner);
                board.ClearSpace(neutralPieceCoordinates[removedNeutralPieceIndex]);

                var emptySpaceCoordinates = board.RetrievePieceTypeCoordinates(PieceType.Empty);
                var newNeutralPieceLocation = emptySpaceCoordinates.OrderByDescending(c => c.IsCorner()).First();
                neutralPieceCoordinates[removedNeutralPieceIndex] = newNeutralPieceLocation;

                board.PlaceNeutralPiece(newNeutralPieceLocation);
            }

            return new PlacePiecesCommand(lPieceMove.Coordinates, neutralPieceCoordinates);
        }

        private static List<LPieceCoordinates> GetPossibleLPieceMoves(LGameBoard board, PieceType player)
        {
            var currentPlayerLPieceCoordinates = board.RetrievePieceTypeCoordinates(player);
            var enemyPieceType = player == PieceType.Player1LPiece ? PieceType.Player2LPiece : PieceType.Player1LPiece;
            var currentEnemyLPieceCoordinates = board.RetrievePieceTypeCoordinates(enemyPieceType);
            var neutralPieceCoordinates = board.RetrievePieceTypeCoordinates(PieceType.NeutralPiece);
            var possibleLpieceMoves = PossibleLPieceCoordinates.GetAllPossibleLpieces();

            RemovePossibleMoveForCurrentPosition(possibleLpieceMoves, currentPlayerLPieceCoordinates);
            RemovePossibleMoveContainingCoordinate(possibleLpieceMoves, currentEnemyLPieceCoordinates);
            RemovePossibleMoveContainingCoordinate(possibleLpieceMoves, neutralPieceCoordinates);

            return possibleLpieceMoves;
        }

        private static void RemovePossibleMoveForCurrentPosition(
            List<LPieceCoordinates> possibleLPieceMoves,
            List<int[]> removeCoordinates)
        {
            foreach (var possibleLPieceMove in possibleLPieceMoves)
            {
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
            foreach (var coordinate in removeCoordinates)
            {
                // Retrieve all the possible moves WHERE one or more (ANY) of the coordinates match with a coordinate that is not allowed for a possible move
                var overlappingCoordinates = possibleLpieces
                    .Where(possibleLPiece => possibleLPiece.Coordinates.Any(c => c[0] == coordinate[0] && c[1] == coordinate[1]))
                    .ToList();

                for (var i = overlappingCoordinates.Count-1; i >= 0; i--)
                {
                    possibleLpieces.Remove(overlappingCoordinates[i]);
                }
            }
        }
    }
}