using System.Diagnostics;

namespace GestiunePrezente.Common
{
        public partial class FrmErr : Form
        {
            private readonly EventLog eventLog = new();
            public FrmErr(string _text, string _msg, string _err, int _idErr)
            {
                InitializeComponent();
                this.Text = " " + _text + " E" + _idErr.ToString();
                this.m_ebError.Text = _msg;
                this.richtb.Text = _err;
                writeEvent(_err, System.Diagnostics.EventLogEntryType.Error, _idErr);
                this.ShowDialog();
            }
            public FrmErr(string _text, string _msg, string _err, int _idErr, int TimeToShow)
            {
                InitializeComponent();
                this.Text = " " + _text + " E" + _idErr.ToString();
                this.m_ebError.Text = _msg;
                this.richtb.Text = _err;
                writeEvent(_err, System.Diagnostics.EventLogEntryType.Error, _idErr);
                this.TopMost = true;
                this.btndett.Visible = false;
                this.btnok.Visible = false;
                this.Show();
                Application.DoEvents();
                System.Threading.Thread.CurrentThread.Join(TimeToShow);
                this.TopMost = false;
                this.Close();
            }

            private void btnok_Click(object sender, EventArgs e)
            {
                this.Close();
            }

            private void btndett_Click(object sender, EventArgs e)
            {
                if (richtb.Visible)
                {
                    richtb.Visible = false;
                    this.Height = 186;
                }
                else
                {
                    richtb.Visible = true;
                    this.Height = 316;
                }
            }

            private void FrmErr_Load(object sender, EventArgs e)
            {
                Cursor.Current = Cursors.Default;
            }

            #region GESTIUNE EVENTLOG
            void writeEvent(string _message, System.Diagnostics.EventLogEntryType _type, int _id)
            {
                eventLog.Source = "IdealeExtensions";
                eventLog.Log = "EISA";
                eventLog.WriteEntry(_message, _type, _id);
            }
            #endregion
        }
    }
