using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using Training5.Com;

namespace Training5.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        Communication com;
        private ObservableCollection<ButtonVM> buttons;
        private ObservableCollection<HistoryVM> history;
        private RelayCommand listenBtnClicked;
        private RelayCommand connectBtnClicked;
        private RelayCommand<ButtonVM> toggleBtnClicked;
        bool isConnected = false;


        public MainViewModel()
        {
            //Buttons = new ObservableCollection<ButtonVM>();
            History = new ObservableCollection<HistoryVM>();

            CreateButtons(6);

            ListenBtnClicked = new RelayCommand(()=> 
            {
                com = new Communication(GuiUpdate, true);
                isConnected = true;
            },()=> 
            {
                return !isConnected;
            } );

            ConnectBtnClicked = new RelayCommand(() =>
            {
                com = new Communication(GuiUpdate, false);
                isConnected = true;
            }, () =>
            {
                return !isConnected;
            });

            ToggleBtnClicked = new RelayCommand<ButtonVM>((param)=> 
            {
                param.State = !param.State;
                com.Send(ItemsToByte());
                History.Add(new HistoryVM(param.Id, param.State, "you"));
            });




        }

        public ObservableCollection<ButtonVM> Buttons { get => buttons; set => buttons = value;}
        public ObservableCollection<HistoryVM> History { get => history; set => history = value; }
        public RelayCommand ListenBtnClicked { get => listenBtnClicked; set => listenBtnClicked = value; }
        public RelayCommand ConnectBtnClicked { get => connectBtnClicked; set => connectBtnClicked = value; }
        public RelayCommand<ButtonVM> ToggleBtnClicked { get => toggleBtnClicked; set => toggleBtnClicked = value; }

        private byte[] ItemsToByte()
        {
            byte[] temp = new byte[Buttons.Count];
            int i = 0;

            foreach(var item in Buttons)
            {
                temp[i] = Convert.ToByte(item.State);
                i++;
            }

            return temp;
        }

        private void GuiUpdate(byte[] buffer, string sender)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                int i = 0;
                foreach(var item in Buttons)
                {
                    if(item.State != Convert.ToBoolean(buffer[i]))
                    {
                        History.Add(new HistoryVM(item.Id, item.State, sender));
                    }
                    item.State = Convert.ToBoolean(buffer[i]);
                    i++;
                }
            });
        }

        private void CreateButtons(int number)
        {
            Buttons = new ObservableCollection<ButtonVM>();
            for (int i = 0; i <= number; i++)
            {
                Buttons.Add(new ButtonVM(i));
            }
        }

    }
}