﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PainterApplication
{
    public class Vector3
    {
        private double x;
        private double y;
        private double z;

        public Vector3(double x, double y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0.0;
        }
        public Vector3(double x, double y, double z):this(x,y)
        {
            
            this.Z = z;
        }
        //public Vector3()
        //{
        //    this.X = 0.0;
        //    this.Y = 0.0;
        //    this.Z = 0.0;
        //}
        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }
        public double Z { get { return z; } set { z = value; } }

        public System.Drawing.PointF ToPointF
        {
            get { return new System.Drawing.PointF((float)X, (float)Y); }
        }
        public static Vector3 Zero
        {
            get { return new Vector3(0.0, 0.0, 0.0); }
        }
        public double Distancefrom( Vector3 v)
        {
            double dx = v.X - X;
            double dy = v.Y - Y;
            double dz = v.Z - Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);

        }
    }
}
