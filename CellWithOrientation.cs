using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class CellWithOrientation
    {
        #region Declarations

        public int Id { get; private set; }
        public int Orientation { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public short CompressedValue { get; private set; }

        #endregion

        #region Public method

        public CellWithOrientation(int id, int x, int y)
        {
            X = x;
            Y = y;
            Id = id;
        }

        public void SetOrientation(CellWithOrientation nextCell)
        {
            if (nextCell.X == X)
            {
                if (nextCell.Y == Y + 1)
                    Orientation = 7;
                else if (nextCell.Y == Y - 1)
                    Orientation = 3;
            }
            else if (nextCell.Y == Y)
            {
                if (nextCell.X == X + 1)
                    Orientation = 1;
                else if (nextCell.X == X - 1)
                    Orientation = 5;
            }
            else
            {
                if (nextCell.X == X + 1 && nextCell.Y == Y + 1)
                    Orientation = 0;
                else if (nextCell.X == X + 1 && nextCell.Y == Y - 1)
                    Orientation = 2;
                else if (nextCell.X == X - 1 && nextCell.Y == Y - 1)
                    Orientation = 4;
                else if (nextCell.X == X - 1 && nextCell.Y == Y + 1)
                    Orientation = 6;
            }
        }

        public void SetOrientation(int orientation)
        {
            Orientation = orientation;
        }

        public void GetCompressedValue()
        {
            CompressedValue = (short)(((int)Orientation & 7) << 12 | Id & 4095);
        }

        #endregion
    }
