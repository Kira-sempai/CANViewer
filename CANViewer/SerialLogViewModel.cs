using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CANViewer
{
    public class SerialLogViewModel : INotifyPropertyChanged
    {
        private SerialLog log;

        public SerialLogViewModel(SerialLog log)
        {
            this.log = log;

            log.Data.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Data_CollectionChanged);
            log.PropertyChanged += new PropertyChangedEventHandler(log_PropertyChanged);
        }

        void log_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsWorking":
                    OnPropertyChanged("IsWorking");
                    break;

            }
        }

        void Data_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Data");
        }

        public ObservableCollection<RawCanMessage> Data
        {
            get
            {
                return log.Data;
            }
        }

        public bool IsWorking
        {
            get
            {
                return log.IsWorking;
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
