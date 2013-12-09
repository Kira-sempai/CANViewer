using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Windows.Threading;
using System.ComponentModel;

namespace CANViewer
{
    public class SerialLog : INotifyPropertyChanged
    {
        private SerialPort port = null;
        private ObservableCollection<RawCanMessage> data = new ObservableCollection<RawCanMessage>();

        private string constructor = "";
        DateTime receivedTimestamp;

        private Dispatcher dispatcher;

        public SerialLog()
        {
            dispatcher = Dispatcher.CurrentDispatcher;

            // TODO: make configurable
            port = new SerialPort("COM6", 115200, Parity.None, 8, StopBits.One);
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

            port.Handshake = Handshake.None;
            port.ReadTimeout = 1;
        }

        ~SerialLog()
        {
            if (port != null && port.IsOpen)
            {
                port.Close();
            }
        }

        public void Start()
        {
            if (!port.IsOpen)
            {
                port.Open();
            }

            OnPropertyChanged("IsWorking");
        }

        public bool IsWorking
        {
            get
            {
                return port != null && port.IsOpen;
            }
        }

        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (constructor.Length == 0)
            {
                receivedTimestamp = DateTime.Now;
            }
            string chunk = port.ReadExisting();

            constructor = constructor + chunk;

            do
            {
                int start = constructor.IndexOf('T');
                if (start == -1)
                {
                    // TODO: broken message, no beginning
                    start = 0;
                }

                int end = constructor.IndexOf('\r', start);
                if (end == -1)
                {
                    break;
                }

                chunk = constructor.Substring(start + 1, end - start - 1);
                constructor = constructor.Substring(end + 1);

                dispatcher.BeginInvoke(new Action(() =>
                {
                    data.Add(new RawCanMessage(receivedTimestamp, chunk));
                }));
            }
            while (constructor.Length != 0);
        }

        public void Stop()
        {
            port.Close();
            OnPropertyChanged("IsWorking");
        }

        public ObservableCollection<RawCanMessage> Data
        {
            get
            {
                return data;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
