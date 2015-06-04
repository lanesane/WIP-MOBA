using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIP_MOBA_Server.Data
{
    public class DataQue
    {
        private Boolean isQued = false;
        private String data = "";
        private Int32 time = 3;
        private Boolean hasStopped = false;

        public void SetData(String _data)
        {
            isQued = true;
            data = _data;
        }

        public String GetData()
        {
            if (isQued)
            {
                isQued = false;
                String temp = data;
                data = "";
                return temp;
            }
            if (hasStopped)
            {
                hasStopped = false;
                return "Stop";
            }
            return null;
        }

        public Int32 GetTimerValue()
        {
            return time;
        }

        public void SetTimerValue(Int32 _time)
        {
            time = _time;
        }

        public void SetHasStopped()
        {
            hasStopped = true;
        }
    }
}
