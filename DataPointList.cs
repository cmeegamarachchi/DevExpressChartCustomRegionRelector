using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpressChartCustomRegionSelector
{
    public class DataPointList : List<DataPoint>
    {
        public int MaxX
        {
            get { return this.Max(e => e.X); }
        }

        public int MinX
        {
            get { return this.Min(e => e.X); }
        }

        public int MaxY
        {
            get { return this.Max(e => e.Y); }
        }

        public int MinY
        {
            get { return this.Min(e => e.Y); }
        }

        public void Populate(int dataPointCount = 100)
        {
            Clear();

            var rnd = new Random();

            for (var i = 0; i < 100; i++)
            {
                Add(new DataPoint {X = rnd.Next(0, 100), Y = rnd.Next(0, 100)});
            }
        }
    }
}