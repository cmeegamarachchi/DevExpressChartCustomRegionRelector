using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.XtraCharts;

namespace DevExpressChartCustomRegionSelector
{
    public partial class frmMain : Form
    {
        readonly DataPointList _dataSource = new DataPointList();
        readonly List<DataPoint> _selectedPoints = new List<DataPoint>();

        static readonly Color SelectionRectColor = Color.FromArgb(89, Color.FromArgb(0xf1ea6f));
        static readonly Color SelectionRectBorderColor = Color.FromArgb(89, Color.FromArgb(0xbaa500));

        Point _firstSelectionCorner = Point.Empty;
        Point _lastSelectionCorner = Point.Empty;
        Rectangle _selectionRectangle = Rectangle.Empty;


        XYDiagram XyDiagram => (chart.Diagram as XYDiagram);

        Rectangle CalculateDiagramBounds()
        {
            Point p1 = XyDiagram.DiagramToPoint((double)XyDiagram.AxisX.VisualRange.MinValue, (double)XyDiagram.AxisY.VisualRange.MinValue).Point;
            Point p2 = XyDiagram.DiagramToPoint((double)XyDiagram.AxisX.VisualRange.MaxValue, (double)XyDiagram.AxisY.VisualRange.MaxValue).Point;
            return DiagramToPointHelper.CreateRectangle(p1, p2);
        }


        public frmMain()
        {
            InitializeComponent();
        }

        private void RefreshChart()
        {
            _dataSource.Populate();

            chart.Series[0].DataSource = _dataSource;
            chart.RefreshData();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshChart();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            RefreshChart();
        }

        private void chart_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle bounds = CalculateDiagramBounds();
            if (bounds.Contains(e.Location))
            {
                _selectionRectangle = Rectangle.Empty;
                _firstSelectionCorner = _lastSelectionCorner = e.Location;
            }
        }

        private void chart_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_firstSelectionCorner.IsEmpty)
            {
                _lastSelectionCorner = DiagramToPointHelper.GetLastSelectionCornerPosition(e.Location, CalculateDiagramBounds());
                if (!_lastSelectionCorner.IsEmpty && _firstSelectionCorner != _lastSelectionCorner)
                {
                    _selectionRectangle = DiagramToPointHelper.CreateRectangle(_firstSelectionCorner, _lastSelectionCorner);
                    chart.Invalidate();
                }
            }
        }

        private void chart_MouseUp(object sender, MouseEventArgs e)
        {
            //if (!_selectionRectangle.IsEmpty)
            //{
            //    ProcessManualPoints();
            //}
            _firstSelectionCorner = _lastSelectionCorner = Point.Empty;
            _selectionRectangle = Rectangle.Empty;
        }

        private void chart_CustomPaint(object sender, CustomPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SetClip(CalculateDiagramBounds());
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (!_selectionRectangle.IsEmpty)
            {
                g.FillRectangle(new SolidBrush(SelectionRectColor), _selectionRectangle);
                g.DrawRectangle(new Pen(SelectionRectBorderColor), _selectionRectangle);
            }
        }
    }
}