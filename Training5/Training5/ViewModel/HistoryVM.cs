using GalaSoft.MvvmLight;
using System;

namespace Training5.ViewModel
{
    public class HistoryVM: ViewModelBase //very important! don't forget to make it public and inherit from ViewModelBase
    {
        int id; //id of the button pressed
        bool state; //new state of the corresponding ellipse
        DateTime timestamp; //timestamp when the button was pressed
        string sender; //name of the sender, can be client or server (on your own view it is "you")

        public HistoryVM(int id, bool state, string sender) //constructor
        {
            this.Id = id;
            this.State = state;
            Timestamp = DateTime.Now; //timestamp is created automatically
            this.sender = sender;
        }

        //set properties for binding in the view
        public int Id { get => id; set => id = value; }
        public bool State { get => state; set => state = value; }
        public DateTime Timestamp { get => timestamp; set => timestamp = value; }
        public string Sender { get => sender; set => sender = value; }
    }
}
