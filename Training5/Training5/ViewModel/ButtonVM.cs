using GalaSoft.MvvmLight;

namespace Training5.ViewModel
{
    public class ButtonVM:ViewModelBase //very important! don't forget to make it public and inherit from ViewModelBase
    {

        int id; //id of the toggle button
        bool state = true; //state of the ellipse (green shall be true, red shall be false)

        public ButtonVM(int id)
        {
            this.Id = id; //create a new pair of one button and one ellipse with the given id for the button
        }

        public int Id { get => id; set => id = value; } //to use the ID in the view, we need to have it as property
        public bool State //state also has to be a property
        {
            get { return state; }
            set
            {
                state = value;
                RaisePropertyChanged(); //if the property has changed, you RaisePropertyChanged()
            }
        }
    }
}
