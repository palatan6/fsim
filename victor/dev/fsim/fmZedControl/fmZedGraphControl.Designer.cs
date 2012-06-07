using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using ZedGraph;

namespace fmZedGraph
{
    public class HighlighPointsEventArgs : EventArgs
    {
        private double x;
        private bool isHighlight;
        public double X
        {
            get { return x; }
        }
        public bool IsHighlight
        {
            get { return isHighlight; }
            //set { isHighlight = value; }
        }
        public HighlighPointsEventArgs(double x, bool isHighlight)
        {
            this.x = x;
            this.isHighlight = isHighlight;
        }
    }
    partial class fmZedGraphControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            this.MouseDownEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(fmZedGraphControl_MouseDownEvent);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(fmZedGraphControl_MouseClick);
            this.MouseUpEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(fmZedGraphControl_MouseUpEvent);
            this.MouseCaptureChanged += new System.EventHandler(fmZedGraphControl_MouseCaptureChanged);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(fmZedGraphControl_KeyUp);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(fmZedGraphControl_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(fmZedGraphControl_KeyDown);
        }

        #endregion

        #region my overloaded events
        
        private void fmZedGraphControl_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (IsEnableSelection)
            {
                //selectedCurves.Clear();
                //selectedPoints.Clear();

                for (int j = 0; j < originalCurvesCount; j++)
                {
                    CurveItem curve = GraphPane.CurveList[j];
                    for (int i = 0; i < curve.Points.Count; i++)
                    {
                        fmCurvePoint cp = new fmCurvePoint(curve, i);
                        if (IsPointInRectArea(GraphPane, curve, i) && !selectedPoints.Contains(cp))
                        {
                            selectedPoints.Add(cp);
                        }
                    }
                }
                foreach (fmCurvePoint cp in selectedPoints)
                {

                    LineItem myCurve = AddSelectedPoint(GraphPane, cp.Curve, cp.PointInx);
                    selectedCurves.Add(myCurve);
                }
                Invalidate();
            }
        }

        private static LineItem AddSelectedPoint(GraphPane gp, CurveItem curve, int iPt)
        {
            PointPairList list = new PointPairList();
            PointPair p = new PointPair(curve.Points[iPt]);
            p.Z = curve.Points[iPt].Z + 3;
            list.Add(curve.Points[iPt]);
            LineItem myCurve = gp.AddCurve("",list, curve.Color, ((LineItem) curve).Symbol.Type);
            myCurve.Line.IsAntiAlias = true;
            myCurve.Label.IsVisible = false;
            myCurve.Symbol.Fill = new Fill(curve.Color);
            myCurve.IsY2Axis = curve.IsY2Axis;
            myCurve.YAxisIndex = curve.YAxisIndex;
            myCurve.Symbol.Size = ((LineItem) curve).Symbol.Size+5;
            return myCurve;
        }

        private bool IsPointInRectArea(GraphPane gp, CurveItem curve, int iPt)
        {
            Point p = TransformToFormPoint(curve, gp, iPt);
            return ((p.X - rect.X) >= 0 && (rect.Width + rect.X - p.X) >= 0 && (p.Y - rect.Y) >= 0 &&
                    (rect.Height + rect.Y - p.Y) >= 0);
        }

        private static bool IsPointInPixel(GraphPane gp, CurveItem curve, int iPt, Point pix)
        {
            Point p = TransformToFormPoint(curve, gp, iPt);
            
            return Math.Abs(p.X- pix.X)<=((LineItem) curve).Symbol.Size/2 && Math.Abs(p.Y - pix.Y)<=((LineItem) curve).Symbol.Size/2 ;
        }
        
        private static Point TransformToFormPoint(CurveItem curve, GraphPane gp, int iPt)
        {
            //todo : make sure that it is for form
            int x = (int)( curve.GetXAxis(gp).Scale.Transform( curve.Points[iPt].X ) + 0.5 );
            int y = (int)( curve.GetYAxis(gp).Scale.Transform( curve.Points[iPt].Y ) + 0.5 );
            return new Point(x,y);
        }

        private void fmZedGraphControl_MouseClick(object sender, MouseEventArgs e)
        { 
            if (IsEnableSelection)
            {
                //CurveItem curve; 
                //int iPt; 
                //bool cl = ZedControl.GraphPane.FindNearestPoint(new PointF(e.X, e.Y), out curve, out iPt);
                if (IsCancelSelections)
                {
                    CancelSelections();
                }


                for (int j = 0; j < originalCurvesCount; j++)
                {
                    CurveItem curve = GraphPane.CurveList[j];
                    for (int i = 0; i < curve.Points.Count; i++)
                    {
                        fmCurvePoint cp = new fmCurvePoint(curve, i);
                        if (IsPointInPixel(GraphPane, curve, i, e.Location) && !selectedPoints.Contains(cp))
                        {
                            selectedPoints.Add(cp);
                        }
                    }
                }
                foreach (fmCurvePoint cp in selectedPoints)
                {

                    LineItem myCurve = AddSelectedPoint(GraphPane, cp.Curve, cp.PointInx);
                    selectedCurves.Add(myCurve);
                }

                Invalidate();
            }
        }

        private List<CurveItem> FindNearestPoints(double x)
        {
            List<CurveItem> result = new List<CurveItem>();
            foreach (CurveItem curve in GraphPane.CurveList)
            {
                if (highLightedPoints != null
                    && highLightedPoints.Contains(curve))
                {
                    continue;
                }

                if (curve.Points.Count == 0)
                    continue;

                PointPair bestPoint = curve.Points[0];
                const double eps = 1e-9;
                for (int i = 1; i < curve.Points.Count; i++)
                {
                    PointPair currentPoint = curve.Points[i];
                    if (Math.Abs(currentPoint.X - x) < Math.Abs(bestPoint.X - x) - eps)
                    {
                        bestPoint = currentPoint;
                    }
                }
                LineItem newCurvePoint = new LineItem("", new double[] { bestPoint.X },
                                                    new double[] { bestPoint.Y },
                                                    curve.Color, SymbolType.Circle);
                newCurvePoint.Symbol.Fill = new Fill(curve.Color);
                newCurvePoint.IsY2Axis = curve.IsY2Axis;
                result.Add(newCurvePoint);
            }
            return result;
        }

        private void AddHighLightedPoints(List<CurveItem> points)
        {
            highLightedPoints = points;
            foreach (LineItem point in highLightedPoints)
                GraphPane.CurveList.Add(point);
        }

        private void RemoveHighLightedPoints()
        {
            if (highLightedPoints != null)
            {
                for (int i = GraphPane.CurveList.Count - 1; i >= 0; --i)
                    if (highLightedPoints.Contains(GraphPane.CurveList[i]))
                        GraphPane.CurveList.RemoveAt(i);
                highLightedPoints.Clear();
            }
        }

        private void fmZedGraphControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isClick)
            {
                Rectangle r = CalcScreenRect(pnt, cPnt);
                ControlPaint.DrawReversibleFrame( r, BackColor, FrameStyle.Dashed );
                cPnt = e.Location;
                r = CalcScreenRect(pnt, cPnt);
                ControlPaint.DrawReversibleFrame( r, BackColor, FrameStyle.Dashed );
            }

            double currentX = GraphPane.XAxis.Scale.ReverseTransform(e.Location.X);
            if (GraphPane.XAxis.Scale.Min <= currentX
                && currentX <= GraphPane.XAxis.Scale.Max)
            {
                HighlightPoints(currentX);
            }
            else
            {
                RemoveHighLightedPoints();
                if (HighLightedPointsChanged != null)
                {
                    HighLightedPointsChanged(this, new HighlighPointsEventArgs(currentX, false));
                }
                Refresh();
            }
        }

        public void HighlightPoints(double currentX)
        {
            RemoveHighLightedPoints();
            if (IsHighlightPoints == true)
            {
                highLightedPoints = FindNearestPoints(currentX); ;
                HighlightPoints();
            }

            if (HighLightedPointsChanged != null)
            {
                HighLightedPointsChanged(this, new HighlighPointsEventArgs(currentX, true));
            }
        }

        private void HighlightPoints()
        {
            AddHighLightedPoints(highLightedPoints);
            Refresh();
        }

        private bool IsSamePointsList(List<CurveItem> a, List<CurveItem> b)
        {
            if (a == null || b == null)
                return false;

            if (a.Count != b.Count)
                return false;

            for (int i = 0; i < a.Count; ++i)
                if (a[i][0].X != b[i][0].X
                        || a[i][0].Y != b[i][0].Y)
                {
                    return false;
                }

            return true;
        }

        private bool fmZedGraphControl_MouseDownEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            isClick = true;
            rect = new RectangleF(e.X, e.Y, 0, 0);
            cPnt = pnt = new Point(e.X, e.Y);
            if(IsEnableSelection && IsCancelSelections)
            {
                CancelSelections();
            }
            return true;
        }

        private bool fmZedGraphControl_MouseUpEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            
            rect = ArrangeRectangle(Point.Round(rect.Location), e.Location);
            cPnt = e.Location;
            Rectangle r = CalcScreenRect(pnt, cPnt);
            ControlPaint.DrawReversibleFrame( r, BackColor, FrameStyle.Dashed );
            
            isClick = false;
            
            return true;
        }
        public static RectangleF ArrangeRectangle(Point p1, Point p2)
        {
            if (p1.X > p2.X) 
            {
                int x = p1.X;
                p1.X = p2.X;
                p2.X = x;
            }
            if( p1.Y>p2.Y)
            {
                int y = p1.Y;
                p1.Y = p2.Y;
                p2.Y = y;
            }
            Size size = new Size(p2.X - p1.X, p2.Y - p1.Y);
            return new RectangleF(p1,size);
        }

        public Rectangle CalcScreenRect( Point mousePt1, Point mousePt2 )
        {
            Size size = new Size(mousePt2.X - mousePt1.X, mousePt2.Y - mousePt1.Y);
            Point screenPt = PointToScreen(mousePt1);
            return new Rectangle(screenPt, size);
		}

        private void CancelSelections()
        {
            if (selectedCurves.Count!=0)
            {
                foreach (CurveItem c in selectedCurves)
                {
                    GraphPane.CurveList.Remove(c);
                }
                selectedPoints.Clear();
                selectedCurves.Clear();
                Invalidate();
            }
        }

        private void fmZedGraphControl_KeyDown(object sender, KeyEventArgs e)
        {
           if (e.Modifiers==Keys.Control)
           {
               IsCancelSelections = false;
           }
        }

        private void fmZedGraphControl_KeyUp(object sender, KeyEventArgs e)
        {
           if (e.KeyValue==17)//control
           {
               IsCancelSelections = true;
           }
        }

        public void Delete()
        {
            selectedPoints.Sort();
            for (int i=selectedPoints.Count-1; i>=0; i--)
            {
                selectedPoints[i].Curve.RemovePoint(selectedPoints[i].PointInx);
            }
            CancelSelections();
            Invalidate();
        }
        public LineItem AddCurve(string label, double[] x, double[] y, Color color)
        {
            LineItem curve = this.GraphPane.AddCurve(label, x, y, color);
            curve.Line.IsAntiAlias = true;
            originalCurvesCount += 1;
            return curve;
        }
        public LineItem AddCurve(string label, double[] x, double[] y, Color color, SymbolType symbolType)
        {
            LineItem curve = this.GraphPane.AddCurve(label, x, y, color, symbolType);
            curve.Line.IsAntiAlias = true;
            originalCurvesCount += 1;
            return curve;
        }
        public LineItem AddCurve(string label, IPointList points, Color color)
        {
            LineItem curve = this.GraphPane.AddCurve(label, points, color);
            curve.Line.IsAntiAlias = true;
            originalCurvesCount += 1;
            return curve;
        }
        public LineItem AddCurve(string label, IPointList points, Color color, SymbolType symbolType)
        {
            LineItem curve = this.GraphPane.AddCurve(label, points, color, symbolType);
            curve.Line.IsAntiAlias = true;
            originalCurvesCount += 1;
            return curve;
        }

        #endregion
    }
}
