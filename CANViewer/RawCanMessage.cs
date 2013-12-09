using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CANViewer
{
    public class RawCanMessage
    {
        private DateTime datetime;
        private string message;

        private ParsedCANMessage parsed;

        public RawCanMessage(DateTime datetime, string message)
        {
            this.datetime = datetime;
            this.message = message;

            parsed = MessageParser.Parse(message);
        }

        public RawCanMessage(RawCanMessage source) : this(source.DateTime, (string)source.Message.Clone())
        {
        }

        public DateTime DateTime
        {
            get
            {
                return datetime;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }
        }

        public ParsedCANMessage Parsed
        {
            get
            {
                return parsed;
            }
        }
    }
}
