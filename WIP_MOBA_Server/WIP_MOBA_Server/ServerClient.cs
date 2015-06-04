using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIP_MOBA_Server.Data;
using WIP_MOBA_Server.Communication;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WIP_MOBA_Server
{
    public partial class ServerClient : Form
    {
        private Thread tComm;
        private DataShare dataShare;
        private DataQue dataQue;

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, Object propertyValue);

        public ServerClient()
        {
            InitializeComponent();

            Items _items = new Items();
            _items.LoadItems();

            dataShare = new DataShare();
            dataQue = new DataQue();

            IPHostEntry host;
            String address = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            if (host.AddressList[0].AddressFamily == AddressFamily.InterNetwork)
            {
                address = host.AddressList[0].ToString();
            }

            tComm = new Thread(() => Connection.StartControlComms(this, 11000, address, dataShare, dataQue));
            tComm.Start();
            
        }


        #region Thread Safety Methods

        public void SetControlPropertyThreadSafe(Control control, string propertyName, Object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new Object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new Object[] { propertyValue });
            }
        }

        #endregion

        private void bStart_Click(object sender, EventArgs e)
        {
            dataQue.SetData("Start Game");
            bStart.Text = "Stop";
        }

    }
}
