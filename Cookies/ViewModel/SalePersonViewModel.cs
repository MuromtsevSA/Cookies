using Cookies.Command;
using Cookies.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cookies.ViewModel
{
    public class SalePersonViewModel: ViewModelBase
    {
        private Context _db;
        private ObservableCollection<Person> _person;
        private ICommand _saleCommand;
        private int _selectedValue;
        private int _selectedValuePerson;
        private int _countCookie;
        private int _selectedCookie;
        private int _totalAmount;

        public ICommand SaleCommand => _saleCommand ?? (_saleCommand = new RelayCommand(SaleCommands));
        public int CountCookie
        {
            get { return _countCookie; }
            set
            {
                _countCookie = value;
                OnPropertyChanged("Count");
            }
        }

        public int SelectedCookie
        {
            get
            {
                return _selectedCookie;
            }
            set
            {
                _selectedCookie = value;
                OnPropertyChanged("selectedCookie");
            }
        }

        public int SelectedValuePerson
        {
            get
            {
                return _selectedValuePerson;
            }
            set
            {
                _selectedValuePerson = value;
                OnPropertyChanged("SelectNamePerson");
            }
        }

        public int SelectedValue
        {
            get
            {
                return _selectedValue;
            }
            set
            {
                _selectedValue = value;
                OnPropertyChanged("SelectNamePerson");
            }
        }
        public ObservableCollection<Person> Persons
        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged("Person");
            }

        }
        public ObservableCollection<CookieModel> Cookies { get; set; }
        public SalePersonViewModel(Context db)
        {
            _db = db;
            _db.Persons.Load();
            _db.Cookies.Load();
            Persons = _db.Persons.Local.ToObservableCollection();
            Cookies = _db.Cookies.Local.ToObservableCollection();
        }

        private int TotalSum()
        {
            if (CountCookie != null && _db.Cookies.FirstOrDefault(x => x.Id == _selectedCookie + 1).Price != null)
            {
                var cookie = _db.Cookies.FirstOrDefault(x => x.Id == _selectedCookie + 1).Price;
                _totalAmount = CountCookie * 10;
            }

            return _totalAmount;
        }

        private void SaleCommands()
        {
            if (_db != null)
            {
                if (_selectedCookie != null && _selectedValue != null && _selectedValuePerson != null)
                {
                    var person = _db.Persons.FirstOrDefault(x => x.Id == _selectedValue + 1).ToString();
                    if (person != null)
                    {
                        var personSale = _db.Persons.FirstOrDefault(x => x.Id == _selectedValuePerson + 1).ToString();
                        if (personSale != null)
                        {
                            var cookie = _db.Cookies.FirstOrDefault(x => x.Id == _selectedCookie + 1).ToString();
                            if (cookie != null)
                            {
                                var personsSale = _db.Persons.FirstOrDefault(x => x.Id == _selectedValuePerson + 1);
                                var persons = _db.Persons.FirstOrDefault(x => x.Id == _selectedValue + 1);
                                if (personsSale != null)
                                {
                                    personsSale.Balance = personsSale.Balance + TotalSum();
                                    var cooks = _db.Cookies.FirstOrDefault(x => x.Id == _selectedValuePerson + 1);
                                    var coo = cooks.CountCookie - _countCookie;
                                    cooks.CountCookie = coo;
                                    if (persons != null)
                                    {
                                        var bal = persons.Balance - _totalAmount;
                                        persons.Balance = bal;
                                        var cookSale = _db.Cookies.FirstOrDefault(x => x.Id == _selectedValue + 1);
                                        var count = (cookSale.CountCookie + _countCookie) > 0 ? cookSale.CountCookie + _countCookie : 0;
                                        cookSale.CountCookie += count;
                                        cookSale.Price = cooks.Price;
                                        cookSale.Name = cookie;
                                        _db.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        CookieModel c = new CookieModel { Name = cookie, PersonId = _selectedValue + 1, CountCookie = _countCookie, Price = cooks.Price };
                                        _db.Cookies.Add(c);
                                        _db.SaveChangesAsync();
                                    }
                                }
                                
                            }
                        }
                    }
                }
            }

            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }
    }
}
