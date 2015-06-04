using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace WIP_MOBA_Server.Data
{
    public class DataShare
    {
        public Boolean GameRunning = false;

        private Boolean client1Computer = false;
        private Boolean client2Computer = false;

        public Boolean GetClient1Connected()
        {
            return client1Computer;
        }

        public Boolean GetClient2Connected()
        {
            return client2Computer;
        }

        public void Client1Connected()
        {
            client1Computer = true;
        }

        public void Client1Disconnected()
        {
            client1Computer = false;
        }

        public void Client2Connected()
        {
            client2Computer = true;
        }

        public void Client2Disconnected()
        {
            client2Computer = false;
        }
    }
}
