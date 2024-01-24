using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PainterApplication.Entities
{
    public class Point
    {
        private Vector3 position;
        private double thinkness;

        public Point()
        {
            this.Posotoin = Vector3.Zero;
            this.Thinkness = 0.0;
        }
        public Point(Vector3 position)
        {
            this.Posotoin = position;
            this.Thinkness = 0.0;
        }
        public Vector3 Posotoin
        {
            get { return this.position; }
            set { this.position = value; }
        }
        public double Thinkness
        {
            get { return this.thinkness; }
            set { this.thinkness = value; }
        }
    }
}
