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
    public partial class uctPool : UserControl
    {
        public enum enOption { eStart=0,ePause=1}
        private enOption _Option = enOption.eStart;
        public enum enMaintance { eMaintance=0,eAvailable=1}
        private enMaintance _Maintance = enMaintance.eMaintance;

        public class TableCopeletedEventArge : EventArgs
        {
            public string TimeInText { get; }
            public int TimeInSeconds { get; }
            public float RatePerHour { get; }
            public float TotalFees { get; }

            public TableCopeletedEventArge(string TimeInText,int TimeInSeconds,float RatePerHour,float TotalFees)
            {
                this.TimeInText = TimeInText;
                this.TimeInSeconds = TimeInSeconds;
                this.RatePerHour = RatePerHour;
                this.TotalFees = TotalFees;
            }

        }

        public event EventHandler<TableCopeletedEventArge> onTableCopeleted;

        public void RaiseTableCopeleted(string TimeInText, int TimeInSeconds, float RatePerHour, float TotalFees)
        {
            RaiseTableCopeleted(new TableCopeletedEventArge(TimeInText, TimeInSeconds, RatePerHour, TotalFees));
        }

        protected virtual void RaiseTableCopeleted(TableCopeletedEventArge e)
        {
            onTableCopeleted?.Invoke(this, e);
        }
       
        public uctPool()
        {
            InitializeComponent();
        }

        private int _Seconds;
        private float _HourlyRate = 10.00f;
        private string _TableTitle = "Table";
        private string _PlayerName;

        [
            Category("Pool Config")
           ,Description("Table Name")
        ]
        public string TableTitle
        {
            get { return _TableTitle; }
            set
            {
                _TableTitle = value;
                gpName.Name = value;

                Invalidate();

            }
        }

        [
            Category("Pool Config")
            ,Description("Player Name")
        ]
        public string PlayerName
        {
            get
            {
                return _PlayerName;
            }
            set
            {
                _PlayerName = value;
                lbPlayerName.Text = value;

                Invalidate();
            }
        }

        [
         Category("Pool Config"),
         Description("Rate per hour")
        ]
        public float HourlyRate
        {
            get
            {
                return _HourlyRate;
            }
            set
            {
                _HourlyRate = value;

            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            switch (_Option)
            {
                case enOption.eStart:
                    btnStart.Text = "Pause";
                    picImage.Image = Properties.Resources.Playing;
                    _Option = enOption.ePause;
                    timer1.Start();
                    break;

                case enOption.ePause:
                    btnStart.Text = "Start";
                    picImage.Image = Properties.Resources.Pause;
                    _Option = enOption.eStart;
                    timer1.Stop();
                    break;

            } 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ++_Seconds;
            TimeSpan time = TimeSpan.FromSeconds(_Seconds);
            string format = time.ToString(@"hh\:mm\:ss");
            lbTime.Text = format;
            lbTime.Refresh();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            float TotalFees = ((float)_Seconds / 60 / 60) * _HourlyRate;
            RaiseTableCopeleted(lbTime.Text,_Seconds,_HourlyRate,TotalFees);

            lbTime.Text = @"00:00:00";
            _Seconds = 0;
            lbPlayerName.Text = "Player";
            btnStart.Text = "Start";
            gpName.Text = "Table";
            _Option = enOption.eStart;
            picImage.Image = Properties.Resources.Available;

        }

        private void btnMaintenance_Click(object sender, EventArgs e)
        {
            switch(_Maintance)
            {
                case enMaintance.eMaintance:
                    picImage.Image = Properties.Resources.Maintenance;
                    btnStart.Enabled = false;
                    btnEnd.Enabled = false;
                    btnMaintenance.Text = "Available";
                    _Maintance = enMaintance.eAvailable;
                    timer1.Stop();
                    break;

                case enMaintance.eAvailable:
                    picImage.Image = Properties.Resources.Available;
                    btnStart.Enabled = true;
                    btnEnd.Enabled = true;
                    btnMaintenance.Text = "Maintance";
                    _Maintance = enMaintance.eMaintance;
                    timer1.Start();
                    break;
            }
       

        }
    }
}
