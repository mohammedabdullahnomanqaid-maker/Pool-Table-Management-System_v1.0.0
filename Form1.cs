using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pool_v1._0._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void uctPool1_onTableCopeleted(object sender, uctPool.TableCopeletedEventArge e)
        {
            string TimeInText = e.TimeInText;
            string TimeInsconds = e.TimeInSeconds.ToString();
            string RatePerHour = e.RatePerHour.ToString();
            string TotalFees = e.TotalFees.ToString();

            string message = $"Time Consumed :{TimeInText} ,Total Seconds : {TimeInsconds} ,Hourly Rate : {RatePerHour} , Total Fees : {TotalFees}";

            MessageBox.Show(message,"Output",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
