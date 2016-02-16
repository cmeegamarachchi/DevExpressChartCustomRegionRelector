using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DevExpressChartCustomRegionRelector
{
    public partial class frmMain : Form
    {
        readonly DataPointList _dataSource = new DataPointList();
        readonly List<DataPoint> _selectedPoints = new List<DataPoint>();

        public frmMain()
        {
            InitializeComponent();
        }

        private void RefreshChart()
        {
            _dataSource.Populate();

            chartControl1.Series[0].DataSource = _dataSource;
            chartControl1.RefreshData();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshChart();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            RefreshChart();
        }
    }
}