using System.Collections.Generic;
using System.Drawing;
using ZedGraph;
using System;
using System.Windows.Forms;

namespace fmZedGraph
{
    public delegate void HighlightPointsEventHandler(object sender, HighlightPointsEventArgs e);
    public partial class fmZedGraphControl : ZedGraphControl
    {
        private RectangleF rect;
        Point pnt, cPnt;
        private bool isClick = false;
        List<fmCurvePoint> selectedPoints = new List<fmCurvePoint>();
        List<CurveItem> selectedCurves = new List<CurveItem>();
        private bool IsCancelSelections = true;
        private int originalCurvesCount;
        private bool IsHighlightPoints = true;
        List<CurveItem> highLightedPoints;
        public event HighlightPointsEventHandler HighLightedPointsChanged;
       
        public List<fmCurvePoint> SelectedPoints
        {
            get { return selectedPoints;}
        }
        public List<CurveItem> SelectedCurves
        {
            get { return selectedCurves;}
        }
        
        public fmZedGraphControl()
        {
            InitializeComponent();

            GraphPane.XAxis.MajorGrid.Color = Color.LightGray;
            GraphPane.XAxis.MajorGrid.IsVisible = true;
            GraphPane.XAxis.MajorGrid.DashOff = 0;
            GraphPane.XAxis.MajorGrid.DashOn = 15;

            GraphPane.YAxis.MajorGrid.Color = Color.LightGray;
            GraphPane.YAxis.MajorGrid.IsVisible = true;
            GraphPane.YAxis.MajorGrid.DashOff = 0;
            GraphPane.YAxis.MajorGrid.DashOn = 15;

            GraphPane.Title.IsVisible = false;
            IsAntiAlias = true;

            ContextMenuBuilder += MyContextMenuBuilder;
        }

        private void MyContextMenuBuilder(ZedGraphControl control, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Name = "highlight_currentX_points_tag";
            item.Tag = "highlight_currentX_points_tag";
            item.Text = "Not show points in the diagram";
            item.Checked = !IsHighlightPoints;
            item.Click += new EventHandler(IsHighlightMenuItemClick);
            menuStrip.Items.Add(item);
        }

        void IsHighlightMenuItemClick(object sender, EventArgs e)
        {
            IsHighlightPoints = !IsHighlightPoints;
            if (IsHighlightPoints)
            {
                HighlightPoints();
            }
            else
            {
                RemoveHighLightedPoints();
                Refresh();
            }
        }
    }
}
