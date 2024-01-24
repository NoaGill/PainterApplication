using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PainterApplication
{
    public partial class GraphicsForm : Form
    {
        public GraphicsForm()
        {
            InitializeComponent();
        }

        private List<Entities.Line> lines = new List<Entities.Line>();
        private List<Entities.Point> points = new List<Entities.Point>();
        private List<Entities.Circle> cricles = new List<Entities.Circle>();
        private List<Entities.Ellipse> ellipses = new List<Entities.Ellipse>();
        private Vector3 CurrentPosition;
        private Vector3 FirstPosition;
        private Vector3 SecondPosition;
        private int DrawingIndex = -1;
        private bool Active_Drawing = false;
        private int ClickNum = 1;

        private void drawing_MouseMove(object sender, MouseEventArgs e)
        {
            CurrentPosition = PointsToCatersian(e.Location);
            lb1.Text = string.Format("{0},{1}", e.Location.X, e.Location.Y);
            lb2.Text = string.Format("{0,0:F3},{1,1:F3}", CurrentPosition.X, CurrentPosition.Y);
            drawing.Refresh();
        }
        //Get DPI 
        private float DPI { get
            {
                using (var g = CreateGraphics())
                    return g.DpiX;
            } 
        }
        //Convert system points to word points
        private Vector3 PointsToCatersian(Point point)
        {
            return new Vector3(PixelToMn(point.X), PixelToMn( drawing.Height - point.Y));
        }
        //Convert Pixels to Milimets
        private float PixelToMn(float pixel)
        {
            return pixel * 24.5f / DPI;
        }

        private void drawing_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (Active_Drawing)
                {
                    switch (DrawingIndex)
                    {
                        case 0: //point
                            points.Add(new Entities.Point(CurrentPosition));
                            break;
                        case 1: //line
                            switch (ClickNum)
                            {
                                case 1:
                                    FirstPosition = CurrentPosition;
                                    points.Add(new Entities.Point(CurrentPosition));
                                    ClickNum++;
                                    break;
                                case 2:
                                    lines.Add(new Entities.Line(FirstPosition, CurrentPosition));
                                    points.Add(new Entities.Point(CurrentPosition));
                                    FirstPosition = CurrentPosition;
                                    //ClickNum = 1;
                                    break;
                            }
                            break;
                        case 2://Cricle
                            switch (ClickNum)
                            {
                                case 1:
                                    FirstPosition = CurrentPosition;
                                    ClickNum++;
                                    break;
                                case 2:
                                    double r = FirstPosition.Distancefrom(CurrentPosition);
                                    cricles.Add(new Entities.Circle(FirstPosition, r));
                                    ClickNum = 1;
                                    break;
                            }
                            break;
                        case 3: //Ellipse
                            switch (ClickNum)
                            {
                                case 1:
                                    FirstPosition = CurrentPosition;
                                    ClickNum++;
                                    break;
                                case 2:
                                    SecondPosition = CurrentPosition;
                                    ClickNum++;
                                    break;
                                case 3:
                                    Entities.Ellipse ellipse = Methods.Method.GetEllipse(FirstPosition, SecondPosition, CurrentPosition);
                                    ellipses.Add(ellipse);
                                    ClickNum = 1;
                                    drawing.Cursor = Cursors.Default;
                                    break;
                            }
                            break;
                        case 4: // cricle with 3 points
                            switch (ClickNum)
                            {
                                case 1:
                                    FirstPosition = CurrentPosition;
                                    ClickNum++;
                                    break;
                                case 2:
                                    SecondPosition = CurrentPosition;
                                    ClickNum++;
                                    break;
                                case 3:
                                    Entities.Circle circle = Methods.Method.GetCircleWith3Point(FirstPosition, SecondPosition, CurrentPosition);
                                    cricles.Add(circle);
                                    CancelAll();
                                    break;
                            }
                            break;
                    }
                    drawing.Refresh();
                       
                            
                }
            }
        }

        private void drawing_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SetParameters(PixelToMn(drawing.Height));
            Pen pen = new Pen(Color.Blue, 0.1f);
            Pen expen = new Pen(Color.Green, 0.1f);
            expen.DashPattern = new float[] { 1.0f, 2.0f };
            //draw all points
            if (points.Count > 0)
            {
                foreach(Entities.Point p in points)
                {
                    e.Graphics.DrawPoint(new Pen(Color.Red, 0), p);
                }
            }
            //draw all lines
            if(lines.Count > 0)
            {
                foreach(Entities.Line line in lines)
                {
                    e.Graphics.DrawLine(pen, line);
                }
            }
            //draw all circle
            if(cricles.Count > 0)
            {
                foreach (Entities.Circle circle in cricles)
                {
                    e.Graphics.DrawCirCle(pen, circle);
                }
            }
            //draw all ellipse
            if(ellipses.Count > 0)
            {
                foreach (Entities.Ellipse ellipse in ellipses)
                {
                    e.Graphics.DrawEllipse(pen, ellipse);
                }
            }
            //Draw Line extended
            switch (DrawingIndex)
            {
                case 1:
                    if(ClickNum == 2)
                    {
                        Entities.Line line = new Entities.Line(FirstPosition,CurrentPosition);
                        e.Graphics.DrawLine(expen, line);
                    }
                    break;
                case 2:
                    if (ClickNum == 2)
                    {
                        Entities.Line line = new Entities.Line(FirstPosition, CurrentPosition);
                        e.Graphics.DrawLine(expen, line);
                        double r = FirstPosition.Distancefrom(CurrentPosition);
                        Entities.Circle circle = new Entities.Circle(FirstPosition,r);
                        e.Graphics.DrawCirCle(expen, circle);
                    }
                    break;
                case 3:
                    switch (ClickNum)
                    {
                        case 2:
                            Entities.Line line = new Entities.Line(FirstPosition,CurrentPosition);
                            e.Graphics.DrawLine(expen, line);
                            break;
                        case 3:
                            Entities.Line line1 = new Entities.Line(FirstPosition, CurrentPosition);
                            e.Graphics.DrawLine(expen, line1);
                            Entities.Ellipse elp = Methods.Method.GetEllipse(FirstPosition, SecondPosition, CurrentPosition);
                            e.Graphics.DrawEllipse(expen,elp);
                            break;
                    }
                    break;
                case 4:
                    switch (ClickNum)
                    {
                        case 2:
                            Entities.Line line = new Entities.Line(FirstPosition, CurrentPosition);
                            e.Graphics.DrawLine(expen, line);
                            break;
                        case 3:
                            Entities.Circle circle = Methods.Method.GetCircleWith3Point(FirstPosition,SecondPosition,CurrentPosition);
                            e.Graphics.DrawCirCle(expen,circle);
                            break;
                    }
                    break;
            }
            //test line line intersection
            if(lines.Count > 0)
            {
                foreach(Entities.Line l1 in lines)
                {
                    foreach (Entities.Line l2 in lines)
                    {
                        Vector3 v = Methods.Method.LineLineIntersection(l1, l2,true);
                        Entities.Point p = new Entities.Point(v);
                        e.Graphics.DrawPoint(new Pen(Color.Red, 0), p);
                    }
                }
            }
        }

        private void btnPoints_Click(object sender, EventArgs e)
        {
            DrawingIndex = 0;
            Active_Drawing = true;
            drawing.Cursor = Cursors.Cross;
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            DrawingIndex = 1;
            Active_Drawing = true;
            drawing.Cursor = Cursors.Cross;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            DrawingIndex = 2;
            Active_Drawing = true;
            drawing.Cursor = Cursors.Cross;
        }

        private void btnEllipse_Click(object sender, EventArgs e)
        {
            DrawingIndex = 3;
            Active_Drawing = true;
            drawing.Cursor = Cursors.Cross;
        }

        private void CancelAll()
        {
            DrawingIndex = -1;
            Active_Drawing = false;
            drawing.Cursor = Cursors.Default;
            ClickNum = 1;
        }
        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelAll();
        }

        private void btnCircle3p_Click(object sender, EventArgs e)
        {
            DrawingIndex = 4;
            Active_Drawing = true;
            drawing.Cursor = Cursors.Cross;
        }
    }
}
