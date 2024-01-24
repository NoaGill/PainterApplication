using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PainterApplication.Entities
{
    public class Circle
    {
        private Vector3 center;
        private double thinkness;
        private double radius;

        public Vector3 Center { get { return center; } set { center = value; } }
        public double Thinkness { get { return thinkness; } set { thinkness = value; } }
        public double Radius { get { return radius; } set { radius = value; } }

        public Circle() : this(Vector3.Zero, 1.0)
        {

        }
        public Circle(Vector3 center, double radius)
        {
            this.Center = center;
            this.Radius = radius;
            this.thinkness = 0.0;
        }
        public double Diameter
        {
            get
            {
                return Radius * 2;
            }
            set
            {
                Radius = value / 2;
            }
        }
    }
}
