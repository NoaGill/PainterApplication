﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PainterApplication
{
    public static class GraphicsExtension
    {
        private static float Height;

        public static void SetParameters(this System.Drawing.Graphics g, float height)
        {
            Height = height;
        }
        public static void SetTransform(this System.Drawing.Graphics g)
        {
            g.PageUnit = System.Drawing.GraphicsUnit.Millimeter;
            g.TranslateTransform(0, Height);
            g.ScaleTransform(1.0f, -1.0f);


        }
        public static void DrawPoint(this System.Drawing.Graphics g,  System.Drawing.Pen pen, Entities.Point point)
        {
            g.SetTransform();
            System.Drawing.PointF p = point.Posotoin.ToPointF;
            g.DrawEllipse(pen, p.X - 1, p.Y - 1,2,2);
            g.ResetTransform();
        }
        public static void DrawLine(this System.Drawing.Graphics g, System.Drawing.Pen pen, Entities.Line line)
        {
            g.SetTransform();
            g.DrawLine(pen, line.StartPoint.ToPointF, line.EndPoint.ToPointF);
            g.ResetTransform();
        }
        public static void DrawCirCle (this System.Drawing.Graphics g , System.Drawing.Pen pen, Entities.Circle circle)
        {
            float x = (float)(circle.Center.X - circle.Radius);
            float y = (float)(circle.Center.Y - circle.Radius);
            float d = (float)(circle.Diameter);

            g.SetTransform();
            g.DrawEllipse(pen, x, y, d, d);
            g.SetTransform();
        }
        public static void DrawEllipse(this System.Drawing.Graphics g, System.Drawing.Pen pen, Entities.Ellipse ellipse)
        {
            SetTransform(g);
            g.TranslateTransform(ellipse.Center.ToPointF.X, ellipse.Center.ToPointF.Y);
            g.RotateTransform((float) ellipse.Rotation);
            g.DrawEllipse(pen, -(float)ellipse.MajorAxis, -(float)ellipse.MinorAxis, (float)ellipse.MajorAxis*2, (float)ellipse.MinorAxis*2);
            g.ResetTransform();

        }
    }
}
