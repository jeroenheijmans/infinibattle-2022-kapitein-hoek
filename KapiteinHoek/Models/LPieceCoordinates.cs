using System.Collections.Generic;
using System.Linq;

namespace KapiteinHoek.Models
{
    public class LPieceCoordinates
    {
        public LPieceCoordinates(int[] coordinate1, int[] coordinate2, int[] coordinate3, int[] coordinate4)
        {
            Coordinates = new List<int[]>{ coordinate1, coordinate2, coordinate3, coordinate4};
            
            TouchesCornerField =
                Coordinates.Any(p => p[0] == 0 && p[1] == 0) ||
                Coordinates.Any(p => p[0] == 3 && p[1] == 0) ||
                Coordinates.Any(p => p[0] == 0 && p[1] == 3) ||
                Coordinates.Any(p => p[0] == 3 && p[1] == 3);

            HasLongSideAgainstOutside =
                Coordinates.Count(p => p[0] == 0) == 3 ||
                Coordinates.Count(p => p[0] == 3) == 3 ||
                Coordinates.Count(p => p[1] == 0) == 3 ||
                Coordinates.Count(p => p[1] == 3) == 3;
        }

        public List<int[]> Coordinates { get; }
        public bool TouchesCornerField { get; }
        public bool HasLongSideAgainstOutside { get; }
    }
}