using System.Windows;
using System.Windows.Input;
using Cookies.Command;
using Cookies.Model;

namespace Cookies.ViewModel;

public class AddViewModel: ViewModelBase
{
        private Context _db;
        private ICommand _addPerson;
        private ICommand _addCookie;
        private string? _personName;
        private string? _cookieName;
        private string _balance;
        private string _price;

        public ICommand AddPerson => _addPerson ?? (_addPerson = new RelayCommand(AddPersons));
        public ICommand AddCokie => _addCookie ?? (_addCookie = new RelayCommand(AddCookies));
        public string PersonName
        {
            get { return _personName; }
            set
            {
                _personName = value;
                OnPropertyChanged("Name");
            }
        }
        public string CookieName
        { 
            get { return _cookieName; }
            set
            {
                _cookieName = value;
                OnPropertyChanged("CookieName");
            }
        }
        public string Balance
        { 
            get { return _balance; }
            set
            {
                _balance = value;
                OnPropertyChanged("Balance");
            }
        }

        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Balance");
            }
        }

        public AddViewModel(Context db)
        {
            _db = db;
        }

        public void AddPersons()
        {
            if (_personName != string.Empty && int.Parse(_balance) != 0)
            {
                Person person = new Person { Name = _personName, Balance = int.Parse(_balance) };
                _db.Persons.Add(person);
                _db.SaveChanges();
                
            }
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }

        public void AddCookies()
        {
            if (_cookieName != string.Empty && int.Parse(_price) != 0)
            {
                CookieModel cook = new CookieModel() { Name = _cookieName, Price = int.Parse(_price) };
                _db.Cookies.Add(cook);
                _db.SaveChangesAsync();
            }
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
    }
}