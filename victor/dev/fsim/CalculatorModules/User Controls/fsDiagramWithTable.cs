using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Value;
using ZedGraph;

namespace CalculatorModules.User_Controls
{
    public partial class fsDiagramWithTable : UserControl
    {
        private readonly List<fsNamedArray> m_yAxis = new List<fsNamedArray>();
        private readonly List<fsNamedArray> m_y2Axis = new List<fsNamedArray>();
        private fsNamedArray m_xAxis;

        public fsDiagramWithTable()
        {
            InitializeComponent();
        }

        public void SetXAxis(fsNamedArray xAxis)
        {
            m_xAxis = xAxis;
        }

        public void ClearYAxis()
        {
            m_yAxis.Clear();
        }

        public void AddYAxis(fsNamedArray yAxis)
        {
            m_yAxis.Add(yAxis);
        }

        public void ClearY2Axis()
        {
            m_y2Axis.Clear();
        }

        public void AddY2Axis(fsNamedArray yAxis)
        {
            m_y2Axis.Add(yAxis);
        }

        public void Redraw()
        {
            RefreshCurves();
            RefreshTable();
        }

        private void RefreshTable()
        {
            int selectedRowIndex = 0;
            int selectedColIndex = 0;
            if (table.CurrentCell != null)
            {
                selectedRowIndex = table.CurrentCell.RowIndex;
                selectedColIndex = table.CurrentCell.ColumnIndex;
            }

            table.ColumnCount = 1;
            table.RowCount = m_xAxis.Array.Length;
            WriteArrayToLastColumnInTable(m_xAxis);
            foreach (fsNamedArray curve in m_yAxis)
            {
                AddArrayToTable(curve);
            }
            foreach (fsNamedArray curve in m_y2Axis)
            {
                AddArrayToTable(curve);
            }

            if (selectedRowIndex >= table.RowCount)
            {
                selectedRowIndex = table.RowCount - 1;
            }
            if (selectedColIndex >= table.ColumnCount)
            {
                selectedColIndex = table.ColumnCount - 1;
            }
            table.CurrentCell = table[selectedColIndex, selectedRowIndex];
        }

        private void AddArrayToTable(fsNamedArray curve)
        {
            table.ColumnCount++;
            WriteArrayToLastColumnInTable(curve);
        }

        private void WriteArrayToLastColumnInTable(fsNamedArray curve)
        {
            table.RowCount = Math.Max(table.RowCount, curve.Array.Length);
            DataGridViewColumn column = table.Columns[table.ColumnCount - 1];
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.HeaderCell.Value = curve.Name;
            for (int row = 0; row < curve.Array.Length; ++row)
            {
                table[table.ColumnCount - 1, row].Value = curve.Array[row].ToString();
            }
        }

        private void RefreshCurves()
        {
            fmZedGraphControl1.GraphPane.CurveList.Clear();
            fmZedGraphControl1.GraphPane.XAxis.Title.Text = m_xAxis.Name;
            fmZedGraphControl1.GraphPane.YAxis.IsVisible = false;
            fmZedGraphControl1.GraphPane.Y2Axis.IsVisible = false;

            RefreshYAxis(m_yAxis, false);
            RefreshYAxis(m_y2Axis, true);

            fmZedGraphControl1.GraphPane.AxisChange();
            fmZedGraphControl1.Refresh();
        }

        private void RefreshYAxis(List<fsNamedArray> yAxis, bool isY2Axis)
        {
            if (yAxis.Count > 0)
            {
                Color color;
                Axis graphYAxis;
                if (isY2Axis)
                {
                    color = Color.Green;
                    graphYAxis = fmZedGraphControl1.GraphPane.Y2Axis;
                }
                else
                {
                    color = Color.Blue;
                    graphYAxis = fmZedGraphControl1.GraphPane.YAxis;
                }
                graphYAxis.IsVisible = true;
                graphYAxis.Color = color;
                graphYAxis.Title.Text = "";

                var maxValues = new double[yAxis.Count];
                for (int i = 0; i < maxValues.Length; ++i)
                {
                    maxValues[i] = 0;
                    foreach (double value in yAxis[i].GetDoublesArray())
                    {
                        maxValues[i] = Math.Max(maxValues[i], Math.Abs(value));
                    }
                }

                for (int i = 0; i < yAxis.Count; ++i)
                {
                    fsNamedArray curve = yAxis[i];
                    string name = curve.Name;
                    var pointList = new PointPairList();
                    double scale = Math.Pow(10, Math.Round(Math.Log10(maxValues[0] / maxValues[i])));
                    for (int pointIndex = 0; pointIndex < curve.Array.Length; ++pointIndex)
                    {
                        if (curve.Array[pointIndex].Defined)
                        {
                            pointList.Add(m_xAxis.Array[pointIndex].Value, curve.Array[pointIndex].Value * scale);
                        }
                    }
                    string curveName = name;
                    if (scale != 1.0)
                    {
                        curveName += " * " + scale;
                    }
                    LineItem zedCurve = fmZedGraphControl1.GraphPane.AddCurve(curveName, pointList, color, SymbolType.None);
                    zedCurve.IsY2Axis = isY2Axis;
                    zedCurve.Line.IsAntiAlias = true;
                }
            }
        }

        #region Nested type: fsNamedArray

        public class fsNamedArray
        {
            public string Name { get; set; }
            public fsValue[] Array { get; set; }

            public double[] GetDoublesArray()
            {
                var result = new double[Array.Length];
                for (int i = 0; i < result.Length; ++i)
                {
                    result[i] = Array[i].Value;
                }
                return result;
            }
        }

        #endregion
    }
}