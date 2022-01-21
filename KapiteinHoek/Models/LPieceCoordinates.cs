using System.Collections.Generic;

namespace KapiteinHoek.Models
{
    public class LPieceCoordinates
    {
        public LPieceCoordinates(int[] coordinate1, int[] coordinate2, int[] coordinate3, int[] coordinate4)
        {
            Coordinates = new List<int[]>{ coordinate1, coordinate2, coordinate3, coordinate4};
        }

        public List<int[]> Coordinates { get; }
    }
}