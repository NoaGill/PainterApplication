using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PainterApplication.Entities
{
    class Arc
    {
        private Vector3 center;
        private double radius;
        private double startAngle;
        private double endAngle;
        private double thinkness;

        public Arc():this(Vector3.Zero,1.0,0.0,180.0)
        {

        }
        public Arc(Vector3 center,double radiis, double start, double end)
        {
            this.center = center;
            this.radius = radiis;
            this.StartAngle = start;
            this.EndAngle = end;
            this.Thinkness = 0.0;

        }

        public Vector3 Center { get { return center; } set { center = value; } }
        public double Radius { get { return radius; } set { radius = value; } }
        public double StartAngle { get { return startAngle; } set { startAngle = value; } }
        public double EndAngle { get { return endAngle; } set { endAngle = value; } }
        public double Thinkness { get { return thinkness; } set { thinkness = value; } }

    }
}
