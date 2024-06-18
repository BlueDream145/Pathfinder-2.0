using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

    public class Cell
    {
        #region Declarations

        public bool Pair { get; private set; }
        public bool ChangeMap { get; private set; }
        public bool Walkable { get; private set; }
        public bool Los { get; private set; }
        public bool InOpenList { get; set; }
        public bool InClosedList { get; set; }
        public bool Start { get; set; }
        public bool End { get; set; }
        public byte[] Position { get; private set; }
        public Point Location { get; private set; }
        public int Id { get; private set; }
        public Cell Parent { get; set; }
        public uint F { get; set; }
        public uint G { get; set; }
        public uint H { get; set; }
        public int Orientation { get; private set; }

        #endregion

        #region Public method

        public Cell(bool changeMap, bool walkable, bool los, int column, int line, int id, Point location)
        {
            ChangeMap = changeMap;
            Walkable = walkable;
            Los = los;
            Id = id;
            Pair = line % 2 == 0;
            Location = location;
            GetBorder(column, line);
        }   
        
        public void SetH(Cell endCell)
        {
            H = (uint)(10 * (Math.Abs(endCell.Location.X - Location.X) + Math.Abs(endCell.Location.X - Location.Y)));
        }

        public override string ToString()
        {
            return String.Format("Id : {0} Location : ({1} ; {2})", Id, Location.X, Location.Y);
        }

        #endregion

        #region Private method

        private void GetBorder(int column, int line)
        {
            Position = new byte[8];
            if (line == 0)
                Position[0] = 1;
            if (line == 1)
                Position[1] = 1;
            if (line == 39)
                Position[2] = 1;
            if (line == 38)
                Position[3] = 1;
            if (column == 0 && Pair)
                Position[4] = 1;
            if (column == 0 && !Pair)
                Position[5] = 1;
            if (column == 13 && !Pair)
                Position[6] = 1;
            if (column == 13 && Pair)
                Position[7] = 1;
        }

        #endregion
    }