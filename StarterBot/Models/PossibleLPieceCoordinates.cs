using System.Collections.Generic;

namespace StarterBot.Models
{
    public static class PossibleLPieceCoordinates
    {
        // There are 48 ways you can play an L-piece on a 4x4 board
        // Coordinates are represented as {x, y}
        public static List<LPieceCoordinates> GetAllPossibleLpieces() => new List<LPieceCoordinates>
        {
            // xxoo
            // oxoo
            // oxoo
            // oooo
            new LPieceCoordinates(new [] {0, 0}, new [] {1, 0}, new [] {1, 1}, new [] {1, 2}),
            // oxxo
            // ooxo
            // ooxo
            // oooo
            new LPieceCoordinates(new [] {1, 0}, new [] {2, 0}, new [] {2, 1}, new [] {2, 2}),
            // ooxx
            // ooox
            // ooox
            // oooo
            new LPieceCoordinates(new [] {2, 0}, new [] {3, 0}, new [] {3, 1}, new [] {3, 2}),
            // oooo
            // xxoo
            // oxoo
            // oxoo
            new LPieceCoordinates(new [] {0, 1}, new [] {1, 1}, new [] {1, 2}, new [] {1, 3}),
            // oooo
            // oxxo
            // ooxo
            // ooxo
            new LPieceCoordinates(new [] {1, 1}, new [] {2, 1}, new [] {2, 2}, new [] {2, 3}),
            // oooo
            // ooxx
            // ooox
            // ooox
            new LPieceCoordinates(new [] {2, 1}, new [] {3, 1}, new [] {3, 2}, new [] {3, 3}),
            // xxoo
            // xooo
            // xooo
            // oooo
            new LPieceCoordinates(new [] {0, 0}, new [] {1, 0}, new [] {0, 1}, new [] {0, 2}),
            // oxxo
            // oxoo
            // oxoo
            // oooo
            new LPieceCoordinates(new [] {1, 0}, new [] {2, 0}, new [] {1, 1}, new [] {1, 2}),
            // ooxx
            // ooxo
            // ooxo
            // oooo
            new LPieceCoordinates(new [] {2, 0}, new [] {3, 0}, new [] {2, 1}, new [] {2, 2}),
            // oooo
            // xxoo
            // xooo
            // xooo
            new LPieceCoordinates(new [] {0, 1}, new [] {1, 1}, new [] {0, 2}, new [] {0, 3}),
            // oooo
            // oxxo
            // oxoo
            // oxoo
            new LPieceCoordinates(new [] {1, 1}, new [] {2, 1}, new [] {1, 2}, new [] {1, 3}),
            // oooo
            // ooxx
            // ooxo
            // ooxo
            new LPieceCoordinates(new [] {2, 1}, new [] {3, 1}, new [] {2, 2}, new [] {2, 3}),
            // oxoo
            // oxoo
            // xxoo
            // oooo
            new LPieceCoordinates(new [] {1, 0}, new [] {1, 1}, new [] {0, 2}, new [] {1, 2}),
            // ooxo
            // ooxo
            // oxxo
            // oooo
            new LPieceCoordinates(new [] {2, 0}, new [] {2, 1}, new [] {1, 2}, new [] {2, 2}),
            // ooox
            // ooox
            // ooxx
            // oooo
            new LPieceCoordinates(new [] {3, 0}, new [] {3, 1}, new [] {2, 2}, new [] {3, 2}),
            // oooo
            // oxoo
            // oxoo
            // xxoo
            new LPieceCoordinates(new [] {1, 1}, new [] {1, 2}, new [] {0, 3}, new [] {1, 3}),
            // oooo
            // ooxo
            // ooxo
            // oxxo
            new LPieceCoordinates(new [] {2, 1}, new [] {2, 2}, new [] {1, 3}, new [] {2, 3}),
            // oooo
            // ooox
            // ooox
            // ooxx
            new LPieceCoordinates(new [] {3, 1}, new [] {3, 2}, new [] {2, 3}, new [] {3, 3}),
            // xooo
            // xooo
            // xxoo
            // oooo
            new LPieceCoordinates(new [] {0, 0}, new [] {0, 1}, new [] {0, 2}, new [] {1, 2}),
            // oxoo
            // oxoo
            // oxxo
            // oooo
            new LPieceCoordinates(new [] {1, 0}, new [] {1, 1}, new [] {1, 2}, new [] {2, 2}),
            // ooxo
            // ooxo
            // ooxx
            // oooo
            new LPieceCoordinates(new [] {2, 0}, new [] {2, 1}, new [] {2, 2}, new [] {3, 2}),
            // oooo
            // xooo
            // xooo
            // xxoo
            new LPieceCoordinates(new [] {0, 1}, new [] {0, 2}, new [] {0, 3}, new [] {1, 3}),
            // oooo
            // oxoo
            // oxoo
            // oxxo
            new LPieceCoordinates(new [] {1, 1}, new [] {1, 2}, new [] {1, 3}, new [] {2, 3}),
            // oooo
            // ooxo
            // ooxo
            // ooxx
            new LPieceCoordinates(new [] {2, 1}, new [] {2, 2}, new [] {2, 3}, new [] {3, 3}),
            // xxxo
            // ooxo
            // oooo
            // oooo
            new LPieceCoordinates(new [] {0, 0}, new [] {1, 0}, new [] {2, 0}, new [] {2, 1}),
            // oooo
            // xxxo
            // ooxo
            // oooo
            new LPieceCoordinates(new [] {0, 1}, new [] {1, 1}, new [] {2, 1}, new [] {2, 2}),
            // oooo
            // oooo
            // xxxo
            // ooxo
            new LPieceCoordinates(new [] {0, 2}, new [] {1, 2}, new [] {2, 2}, new [] {2, 3}),
            // oxxx
            // ooox
            // oooo
            // oooo
            new LPieceCoordinates(new [] {1, 0}, new [] {2, 0}, new [] {3, 0}, new [] {3, 1}),
            // oooo
            // oxxx
            // ooox
            // oooo
            new LPieceCoordinates(new [] {1, 1}, new [] {2, 1}, new [] {3, 1}, new [] {3, 2}),
            // oooo
            // oooo
            // oxxx
            // ooox
            new LPieceCoordinates(new [] {1, 2}, new [] {2, 2}, new [] {3, 2}, new [] {3, 3}),
            // ooxo
            // xxxo
            // oooo
            // oooo
            new LPieceCoordinates(new [] {2, 0}, new [] {0, 1}, new [] {1, 1}, new [] {2, 1}),
            // oooo
            // ooxo
            // xxxo
            // oooo
            new LPieceCoordinates(new [] {2, 1}, new [] {0, 2}, new [] {1, 2}, new [] {2, 2}),
            // oooo
            // oooo
            // ooxo
            // xxxo
            new LPieceCoordinates(new [] {2, 2}, new [] {0, 3}, new [] {1, 3}, new [] {2, 3}),
            // ooox
            // oxxx
            // oooo
            // oooo
            new LPieceCoordinates(new [] {3, 0}, new [] {1, 1}, new [] {2, 1}, new [] {3, 1}),
            // oooo
            // ooox
            // oxxx
            // oooo
            new LPieceCoordinates(new [] {3, 1}, new [] {1, 2}, new [] {2, 2}, new [] {3, 2}),
            // oooo
            // oooo
            // ooox
            // oxxx
            new LPieceCoordinates(new [] {3, 2}, new [] {1, 3}, new [] {2, 3}, new [] {3, 3}),
            // xxxo
            // xooo
            // oooo
            // oooo
            new LPieceCoordinates(new [] {0, 0}, new [] {1, 0}, new [] {2, 0}, new [] {0, 1}),
            // oooo
            // xxxo
            // xooo
            // oooo
            new LPieceCoordinates(new [] {0, 1}, new [] {1, 1}, new [] {2, 1}, new [] {0, 2}),
            // oooo
            // oooo
            // xxxo
            // xooo
            new LPieceCoordinates(new [] {0, 2}, new [] {1, 2}, new [] {2, 2}, new [] {0, 3}),
            // oxxx
            // oxoo
            // oooo
            // oooo
            new LPieceCoordinates(new [] {1, 0}, new [] {2, 0}, new [] {3, 0}, new [] {1, 1}),
            // oooo
            // oxxx
            // oxoo
            // oooo
            new LPieceCoordinates(new [] {1, 1}, new [] {2, 1}, new [] {3, 1}, new [] {1, 2}),
            // oooo
            // oooo
            // oxxx
            // oxoo
            new LPieceCoordinates(new [] {1, 2}, new [] {2, 2}, new [] {3, 2}, new [] {1, 3}),
            // xooo
            // xxxo
            // oooo
            // oooo
            new LPieceCoordinates(new [] {0, 0}, new [] {0, 1}, new [] {1, 1}, new [] {2, 1}),
            // oooo
            // xooo
            // xxxo
            // oooo
            new LPieceCoordinates(new [] {0, 1}, new [] {0, 2}, new [] {1, 2}, new [] {2, 2}),
            // oooo
            // oooo
            // xooo
            // xxxo
            new LPieceCoordinates(new [] {0, 2}, new [] {0, 3}, new [] {1, 3}, new [] {2, 3}),
            // oxoo
            // oxxx
            // oooo
            // oooo
            new LPieceCoordinates(new [] {1, 0}, new [] {1, 1}, new [] {2, 1}, new [] {3, 1}),
            // oooo
            // oxoo
            // oxxx
            // oooo
            new LPieceCoordinates(new [] {1, 1}, new [] {1, 2}, new [] {2, 2}, new [] {3, 2}),
            // oooo
            // oooo
            // oxoo
            // oxxx
            new LPieceCoordinates(new [] {1, 2}, new [] {1, 3}, new [] {2, 3}, new [] {3, 3}),
        };
    }
}