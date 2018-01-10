using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training5.ViewModel
{
    public class HistoryVM:ViewModelBase
    {
        int id;
        bool state;
        DateTime timestamp;
        string sender;

        public HistoryVM(int id, bool state, string sender)
        {
            this.Id = id;
            this.State = state;
            Timestamp = DateTime.Now;
            this.sender = sender;
        }

        public int Id { get => id; set => id = value; }
        public bool State { get => state; set => state = value; }
        public DateTime Timestamp { get => timestamp; set => timestamp = value; }
        public string Sender { get => sender; set => sender = value; }
    }
}
