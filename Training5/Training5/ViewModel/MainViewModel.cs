using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using Training5.Com;

namespace Training5.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        Communication com; //connection can be either server or client
        private ObservableCollection<ButtonVM> buttons; //obsrevable for the button/ellipse pairs = id+state
        private ObservableCollection<HistoryVM> history; //observable which stores the events with id+state+timestamp+sender(who did the change?)
        private RelayCommand listenBtnClicked; //property for the relaycommand to act as server
        private RelayCommand connectBtnClicked; //property for the relaycommand to act as client
        private RelayCommand<ButtonVM> toggleBtnClicked; //property for the relaycommand which is bound to a specific button/ellipse pair in the view
        bool isConnected = false; //flag if you are connected or not


        public MainViewModel()
        {
            History = new ObservableCollection<HistoryVM>(); //instantiate the history observable

            CreateButtons(6); //create six button/ellipse pairs (in the funciton) 

            ListenBtnClicked = new RelayCommand(()=> 
            {
                com = new Communication(GuiUpdate, true); //if listen button was clicked, instantiate the communication as server (2nd parameter is true)
                isConnected = true; //if connection is established, flag isconnected as true
            },()=> 
            {
                return !isConnected; //if you are connected, the button is not clickable, otherwise it is clickable
            } );

            ConnectBtnClicked = new RelayCommand(() =>
            {
                com = new Communication(GuiUpdate, false);//if connect button was clicked, instantiate the communication as server (2nd parameter is false)
                isConnected = true; //if connection is established, flag isconnected as true
            }, () =>
            {
                return !isConnected; //if you are connected, the button is not clickable, otherwise it is clickable
            });

            ToggleBtnClicked = new RelayCommand<ButtonVM>((param)=> //this relaycommand hands over a parameter (which is the button/ellipse pair that was toggled)
            {
                param.State = !param.State; //invert the state
                com.Send(ItemsToByte()); //convert the information stored in a ButtonVM into a byte array which can be sent/received by server/client
                History.Add(new HistoryVM(param.Id, param.State, "you")); //write into your own history
            });
        }

        //properties for the observables and the relaycommands
        public ObservableCollection<ButtonVM> Buttons { get => buttons; set => buttons = value;}
        public ObservableCollection<HistoryVM> History { get => history; set => history = value; }
        public RelayCommand ListenBtnClicked { get => listenBtnClicked; set => listenBtnClicked = value; }
        public RelayCommand ConnectBtnClicked { get => connectBtnClicked; set => connectBtnClicked = value; }
        public RelayCommand<ButtonVM> ToggleBtnClicked { get => toggleBtnClicked; set => toggleBtnClicked = value; }

        private byte[] ItemsToByte() //converter that extracts information of a buttonVM and stores it in a byte array
        {
            byte[] temp = new byte[Buttons.Count]; //create a temp byte array of the same size like the observable has
            int i = 0; //counter variable

            foreach(var item in Buttons) //for eacht button/ellipse pair stored in buttonVM
            {
                temp[i] = Convert.ToByte(item.State); //store the new state of the ellipses in the byte array
                i++;
            }

            return temp; //return the array of states
        }

        private void GuiUpdate(byte[] buffer, string sender) //function to update the gui, used via delegat by the connection
        {
            App.Current.Dispatcher.Invoke(() => //switch to gui thread
            {
                int i = 0; //counter variable
                foreach(var item in Buttons) //for each button/ellipse pair  
                {
                    if(item.State != Convert.ToBoolean(buffer[i])) //if the current state is not the state that was sent by the connection
                    {
                        History.Add(new HistoryVM(item.Id, item.State, sender)); //add a new entry about the change to the history observable
                    }
                    item.State = Convert.ToBoolean(buffer[i]); //and change the state property of that specific buttonVM (==button/ellipse pair)
                    i++;
                }
            });
        }

        private void CreateButtons(int number) //function to generate button/ellipse pairs
        {
            Buttons = new ObservableCollection<ButtonVM>(); //instantiate the buttons observable
            for (int i = 0; i <= number; i++) //for loop to create a s many button/ellipse pairs as given in the parameter
            {
                Buttons.Add(new ButtonVM(i)); //add a new button/ellipse pair to the buttons observable
            }
        }

    }
}