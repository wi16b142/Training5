using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Training5.ViewModel
{
    public class ButtonVM:ViewModelBase
    {

        int id;
        bool state = true;

        public ButtonVM(int id)
        {
            this.Id = id;
        }

        public int Id { get => id; set => id = value; }
        public bool State
        {
            get { return state; }
            set
            {
                state = value;
                RaisePropertyChanged();
            }
        }
    }
}
