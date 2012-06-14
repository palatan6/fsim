using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace CurveTestBuilder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DrawGraph()
        {
            GraphPane pane = fmZedGraphControlMain.GraphPane;
            pane.CurveList.Clear();
            PointPairList list = new PointPairList();

            double xmin = -50;
            double xmax = 50;

            for (double x = xmin; x <= xmax; x += 0.01)
            {
                if (listBoxGraficsChoose.SelectedIndex != -1)
                {
                    if (listBoxGraficsChoose.SelectedIndex == 0)
                        list.Add(x, Math.Sin(x));
                    if (listBoxGraficsChoose.SelectedIndex == 1)
                        list.Add(x, Math.Sqrt(x));
                    if (listBoxGraficsChoose.SelectedIndex == 2)
                        list.Add(x, Math.Pow(x, 2));
                }
            }

            LineItem myCurve = pane.AddCurve("Curve", list, Color.Blue, SymbolType.None);

            fmZedGraphControlMain.AxisChange();

            fmZedGraphControlMain.Invalidate();
        }

        private void listBoxGraficsChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawGraph();
        }
    }
}
