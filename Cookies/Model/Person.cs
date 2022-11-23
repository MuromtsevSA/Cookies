using Cookies.ViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cookies.Model;

public class Person: ViewModelBase
{
    private int _id;
    private string _name;
    private int _balance;
    private List<CookieModel> _cookies;

    [Key]
    public int Id
    { 
        get { return _id; }
        set { 
            _id = value; 
            OnPropertyChanged("Id");
        }
    }
    public string? Name 
    { 
        get { return _name; }
        set
        {
            _name = value;
            OnPropertyChanged("Name");
        }
    }
    public int Balance
    { 
        get { return _balance; }
        set
        {
            _balance = value;
            OnPropertyChanged("Balance");
        }
    }

    public List<CookieModel>? Cooks
    { 
        get { return _cookies; }
        set
        {
            _cookies = value;
            OnPropertyChanged("Cookie");
        }
    }

    public override string ToString()
    {
        return Name.ToString();
    }
}