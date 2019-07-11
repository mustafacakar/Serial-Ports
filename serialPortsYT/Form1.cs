using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
namespace serialPortsYT
{
    public partial class Form1 : Form
    {
        string dataOUt;
        string dataIn;

        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxCOMPORT.Items.AddRange(ports);

            chBoxDTREnable.Checked = false;
            serialPort1.DtrEnable = false;

            chBoxRTSEnable.Checked = false;
            serialPort1.RtsEnable = false;

            btnOpen.Enabled = true;
            btnClose.Enabled = false;

            chBoxAddToOldData.Checked = true;
            chBoxAlwaysUpdate.Checked = false;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cBoxCOMPORT.Text;
                serialPort1.BaudRate = Convert.ToInt32(cbBaudRate.Text);
                serialPort1.DataBits = Convert.ToInt32(cbDataBits.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbStopBits.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cbParityBits.Text);
                serialPort1.Open();
                progressBar1.Value = 100;
                btnOpen.Enabled = false;
                btnClose.Enabled = true;
                lblStatusCOM.Text = "AÇIK";



            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           ;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                progressBar1.Value = 0;
                btnOpen.Enabled = true;
                lblStatusCOM.Text = "KAPALI";
            }
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUt = txDataOut.Text;
                serialPort1.Write(dataOUt);
            }
        }
        //Data Terminal Ready (DTR) is a control signal in RS-232 serial communications
        //Veri Terminali Hazır(DTR), RS-232 seri iletişiminde bir kontrol sinyalidir
        //RTS Gönderme İsteği anlamına gelir ve bağlı cihaza veri göndermek istediğini belirtir.
        private void chBoxDTREnable_CheckedChanged(object sender, EventArgs e)
        {
            if(chBoxDTREnable.Checked)
            {
                serialPort1.DtrEnable = true;
            }
            else { serialPort1.DtrEnable = false; }
        }

        private void chBoxRTSEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (chBoxRTSEnable.Checked)
            {
                serialPort1.RtsEnable = true;

            }
            else { serialPort1.RtsEnable = false; }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if(txDataOut.Text != "")
            {
                txDataOut.Text = "";
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            dataIn = serialPort1.ReadExisting();
            this.Invoke(new EventHandler(ShowData));
        }

        private void ShowData(object sender, EventArgs e)
        {
            txDataIN.Text += dataIn;
            if (chBoxAlwaysUpdate.Checked)
            {
                txDataIN.Text = dataIn;
            }
            else if(chBoxAddToOldData.Checked)
            {
                txDataIN.Text = txDataIN.Text + dataIn;
            }
        }

        private void chBoxAlwaysUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (chBoxAlwaysUpdate.Checked)
            {
                chBoxAlwaysUpdate.Checked = true;
                chBoxAddToOldData.Checked = false;
            }
        }

        private void chBoxAddToOldData_CheckedChanged(object sender, EventArgs e)
        {
            if (chBoxAddToOldData.Checked)
            {
                chBoxAddToOldData.Checked = true;
                chBoxAlwaysUpdate.Checked = false;
            }
           
        }

        private void lblStatusCOM_Click(object sender, EventArgs e)
        {

        }
    }
}
