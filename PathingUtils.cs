using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public static class PathingUtils
    {
        #region Declarations

        private const int MapHeight = 20;
        private const int MapWidth = 14;
        private const int MapCellsCount = 560;
        private static Point[] cellsPositions = new Point[MapCellsCount];

        #endregion

        #region PublicMethod

        static PathingUtils()
        {
            int _loc_1, _loc_2, _loc_3, _loc_4, _loc_5;
            _loc_1 = _loc_2 = _loc_3 = _loc_4 = _loc_5 = 0;

            while (_loc_5 < MapHeight)
            {
                _loc_4 = 0;
                while (_loc_4 < MapWidth)
                {
                    cellsPositions[_loc_3] = new Point(_loc_1 + _loc_4, _loc_2 + _loc_4);
                    _loc_3++;
                    _loc_4++;
                }
                _loc_1++;
                _loc_4 = 0;
                while (_loc_4 < MapWidth)
                {
                    cellsPositions[_loc_3] = new Point(_loc_1 + _loc_4, _loc_2 + _loc_4);
                    _loc_3++;
                    _loc_4++;
                }
                _loc_2 = _loc_2 - 1;
                _loc_5++;
            }
        }

        public static short CoordToCellId(int cellX, int cellY)
        {
            return (short)((cellX - cellY) * MapWidth + cellY + (cellX - cellY) / 2);
        }

        public static short CoordToCellId(Point cellPosition)
        {
            return CoordToCellId(cellPosition.X, cellPosition.Y);
        }

        public static Point CellIdToCoord(short cellId)
        {
            if (cellId < 0 || cellId >= cellsPositions.Count())
                throw new ArgumentOutOfRangeException("Invalid cell id");
            return cellsPositions[cellId];
        }

        public static short[] GetCompressedPath(List<CellWithOrientation> path)
        {
            List<short> compressedPath = new List<short>();
            if (path.Count < 2)
            {

                foreach (CellWithOrientation node in path)
                {
                    node.GetCompressedValue();
                    compressedPath.Add(node.CompressedValue);
                }
            }
            else
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    path[i].SetOrientation(path[i + 1]);
                }

                path[path.Count - 1].SetOrientation(path[path.Count - 2].Orientation);
                foreach (CellWithOrientation cell in path)
                {
                    cell.GetCompressedValue();
                }
                compressedPath.Add(path[0].CompressedValue);
                for (int i = 1; i < path.Count - 1; i++)
                {
                    if (path[i].Orientation != path[i - 1].Orientation)
                        compressedPath.Add(path[i].CompressedValue);
                }
                compressedPath.Add(path[path.Count - 1].CompressedValue);
            }
            return compressedPath.ToArray();
        }

        public static uint DistanceToPoint(Point point1, Point point2)
        {
            return (uint)Math.Sqrt((point1.X - point2.X) * (point1.X - point2.X) + (point1.Y - point2.Y) * (point1.Y - point2.Y));
        }

        #endregion
    }