using PainterApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PainterApplication.Methods
{
    public static class Method
    {
        public static double LineAngle(Vector3 start,Vector3 end)
        {
            double Angle = Math.Atan2(end.Y - start.Y, end.X - start.X)*180.0/Math.PI;
            if(Angle < 0.0)
            {
                Angle += 360.0;
            }
            return Angle;
        }
        public static Entities.Ellipse GetEllipse(Vector3 center,Vector3 firtPoint, Vector3 secondPoint)
        {
            double major = center.Distancefrom(firtPoint);
            double minor = center.Distancefrom(secondPoint);
            double lineAngle = LineAngle(center, firtPoint);
            Entities.Ellipse elp = new Entities.Ellipse(center, major, minor);
            elp.Rotation = lineAngle;
            return elp;
        }
        public static Vector3 LineLineIntersection( Entities.Line line1,Entities.Line line2, bool extended = false)
        {
            Vector3 result;
            Vector3 p1 =line1.StartPoint;
            Vector3 p2 = line1.EndPoint;
            Vector3 p3 = line2.StartPoint;
            Vector3 p4 = line2.EndPoint;

            double dx12 = p2.X - p1.X;
            double dy12 = p2.Y - p1.Y;
            double dx34 = p4.X - p3.X;
            double dy34 = p4.Y - p3.Y;

            double demoninator = (dy12 * dx34 - dx12 * dy34);
            double k1 = ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34) / demoninator;

            if (double.IsInfinity(k1))
                return new Vector3(double.NaN, double.NaN);

            result = new Vector3(p1.X + dx12 * k1, p1.Y + dy12 * k1);

            if (extended)
                return result;
            else
            {
                if(IsPointOnLine(line1, result)&& IsPointOnLine(line2, result))
                {
                    return result;
                }
                else
                {
                    return new Vector3(double.NaN, double.NaN);
                }
            }
        }

        private static bool IsPointOnLine(Line line1, Vector3 point)
        {
            return IsEqual(line1.Length, line1.StartPoint.Distancefrom(point) + line1.EndPoint.Distancefrom(point));
        }
        private static double Epsilon = 1e-12;
        private static bool IsEqual(double d1, double d2)
        {
            return IsEqual(d1, d2, Epsilon);
        }
        private static bool IsEqual(double d1, double d2, double epsilon)
        {
            return IsZero(d1- d2, epsilon);
        }

        private static bool IsZero(double d, double epsilon)
        {
            return d >= -epsilon && d <= epsilon;
        }
        public static Circle GetCircleWith3Point(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            double x1 = (p1.X - p2.X) / 2;
            double y1 = (p1.Y - p2.Y) / 2;
            double dx1 = p2.X - p1.X;
            double dy1 = p2.Y - p1.Y;

            double x2 = (p2.X - p3.X) / 2;
            double y2 = (p2.Y - p3.Y) / 2;
            double dx2 = p3.X - p2.X;
            double dy2 = p3.Y - p2.Y;

            Line line1 = new Line(new Vector3(x1, y1),new Vector3(x1-dy1, y1+dx1));
            Line line2 = new Line(new Vector3(x2, y2), new Vector3(x2-dy2, y2+dx2));

            Vector3 center = LineLineIntersection(line1,line2,true);

            double dx = center.X - p1.X;
            double dy = center.Y - p1.Y;

            double radius = Math.Sqrt(dx*dx+dy*dy);
            return new Circle(center, radius);
        }
        //public static Arc GetArcWith3Points(Vector3 p1,Vector3 p2,Vector3 p3)
        //{
        //    double start, end;
        //   Arc result = new Arc();
        //    Circle c = GetCircleWith3Point(p1, p2, p3);
        //    if(c.Radius > 0)
        //    {
        //        if(DeterminePointOfLine(new Line(p1, p3), p2) < 0)
        //        {
        //            start = LineAngle(c.Center, p3);
        //            end = LineAngle(c.Center, p1);
        //        }
        //        else
        //        {
        //            start = LineAngle(c.Center, p1);
        //            end = LineAngle(c.Center, p3);
        //        }
        //        if (end <= start)
        //            end -= start;
        //        else
        //            end += 360.0 - start;

        //        result = new Arc(c.Center, c.Radius, start, end);
        //    }
        //    return result;
           
            
        //}

        private static double DeterminePointOfLine(Line line, Vector3 v)
        {
            return (v.X - line.StartPoint.X) * (line.EndPoint.Y - line.StartPoint.Y) - (v.Y - line.StartPoint.Y) * (line.EndPoint.X - line.EndPoint.X);
        }
    }
}
