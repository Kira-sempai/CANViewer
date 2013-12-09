using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CANViewer
{
    public class ParsedCANMessage
    {
        public string Function
        {
            get;
            set;
        }
        public string Type
        {
            get;
            set;
        }

        public byte? Source
        {
            get;
            set;
        }
        public byte? Target
        {
            get;
            set;
        }

        public bool Response
        {
            get;
            set;
        }
        public bool Exception
        {
            get;
            set;
        }
        public byte Reserved
        {
            get;
            set;
        }

        public decimal? Temperature
        {
            get;
            set;
        }

        public byte? Priority
        {
            get;
            set;
        }
        public byte? Manager
        {
            get;
            set;
        }

        public byte? State
        {
            get;
            set;
        }

        public byte? BoilerType
        {
            get;
            set;
        }
        public int? Power
        {
            get;
            set;
        }
        public int? WorkTime
        {
            get;
            set;
        }

        public DateTime? Datetime
        {
            get;
            set;
        }

        public string Error
        {
            get;
            set;
        }
    }
}

