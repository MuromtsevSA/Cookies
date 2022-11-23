using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Linq;
using Cookies.Command;
using Cookies.Model;
using Cookies.View;
using Microsoft.EntityFrameworkCore;

namespace Cookies.ViewModel;

public class MainViewModel : ViewModelBase
{
    private Context _db;
    private ICommand _openWindowCommand;
    private ICommand _saleCommand;
    private ICommand _salePersonCommand;
    private int _sumBalance;
    private int _countCookie;
    private int _totalAmount;
    private int _selectedValue;
    private int _selectedCookie;
    private ObservableCollection<Person> _person;

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

    public int SelectedValue
    {
        get{
            return _selectedValue;
        }
        set
        {
            _selectedValue = value;
            OnPropertyChanged("SelectNamePerson");
        }
    }
    
    public int TotalAmount
    {
        get { return _totalAmount; }
        set
        {
            _totalAmount = value;
            OnPropertyChanged("Total");
        }
    }

    public int CountCookie
    {
        get { return _countCookie; }
        set
        {
            _countCookie = value;
            OnPropertyChanged("Count");
        }
    }
    public int SumBalances
    {
        get { return _sumBalance; }
        set
        {
            _sumBalance = value;
            OnPropertyChanged("Sum");
        }
    }

    public ICommand OpenWindowCommand => _openWindowCommand ?? (_openWindowCommand = new RelayCommand(OpenWindow));
    public ICommand SaleCommand => _saleCommand ?? (_saleCommand = new RelayCommand(SumCookie));
    public ICommand SalePersonCommand => _salePersonCommand ?? (_salePersonCommand = new RelayCommand(OpenWindowSale));
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

    public MainViewModel(Context db)
    {
        _db = db;
        _db.Persons.Load();
        _db.Cookies.Load();
        Persons = _db.Persons.Local.ToObservableCollection();
        Cookies = _db.Cookies.Local.ToObservableCollection();
        SumBalance();
        //TotalSum();
        //CookiesAdd();
    }

    public override string ToString()
     {
        return _selectedValue.ToString();
    }

    private void SumBalance()
    {
        foreach (var person in Persons)
        {
            SumBalances += person.Balance;
        }
    }

    private int TotalSum()
    {
        if (CountCookie != null && _db.Cookies.FirstOrDefault(x => x.Id == _selectedCookie + 1).Price != null)
        {
            var cookie = _db.Cookies.FirstOrDefault(x => x.Id == _selectedCookie + 1).Price;
            _totalAmount = CountCookie * cookie;
        }

        return _totalAmount;
    }

    private void SumCookie()
    {
        if (_selectedCookie != null && _selectedValue != null)
        {
            var person = _db.Persons.FirstOrDefault(x => x.Id == _selectedValue + 1).ToString();
            if (person != null)
            {
                var cookie = _db.Cookies.FirstOrDefault(x => x.Id == _selectedCookie + 1).ToString();
                if (cookie != null)
                {   
                    var balancePerson = _db.Persons.FirstOrDefault(x => x.Id == _selectedValue + 1);

                    if (balancePerson != null)
                    {
                        balancePerson.Balance = balancePerson.Balance - TotalSum();
                        var per = _db.Persons.FirstOrDefault(x => x.Id == _selectedValue + 1).Id;
                        var cook = _db.Cookies.FirstOrDefault(x => x.PersonId == per);
                        if (cook != null)
                        {
                            cook.PersonId = _selectedValue + 1;
                            cook.CountCookie = _countCookie;
                        }
                        else
                        {
                            var price = _db.Cookies.FirstOrDefault(x => x.Id == _selectedCookie + 1);
                            var cookies = new CookieModel { PersonId = _selectedValue + 1, CountCookie = _countCookie, Name = cookie, Price = price.Price };
                            _db.Cookies.Add(cookies);
                        }
                        _db.SaveChangesAsync();
                    }
                }
            }
        }
    }

    private void OpenWindowSale()
    {
        SalePersonView ps = new SalePersonView(_db);
        ps.Show();
    }

    private void OpenWindow()
    {
        AddWindow edit = new AddWindow();
        edit.Show();
    }
}