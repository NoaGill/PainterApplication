using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PainterApplication.Entities
{
    public class Line
    {
        private Vector3 startPoint;
        private Vector3 endPoint;
        private double thinkness;
        public Line()
            :this(Vector3.Zero,Vector3.Zero)
        {

        }
        public Line(Vector3 start, Vector3 end)
        {
            this.StartPoint = start;
            this.EndPoint = end;
            this.thinkness = 0.0;
        }
        public Vector3 StartPoint { get { return this.startPoint; } set { this.startPoint = value; } }
        public Vector3 EndPoint { get { return this.endPoint; } set { this.endPoint = value; } }

        public double Length
        {
            get
            {
                double dx = EndPoint.X - StartPoint.X;
                double dy = EndPoint.Y - StartPoint.Y;
                double dz = EndPoint.Z - StartPoint.Z;
                return Math.Sqrt(dx * dx + dy * dy + dz * dz);
            }
        }
    }
}
