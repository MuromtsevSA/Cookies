using Cookies.ViewModel;
using Microsoft.Data.SqlClient;

namespace Cookies.Model;

public class CookieModel: ViewModelBase
{
    private int _id;
    private string? _name;
    private int _price;
    private int? _personId;
    private int _countCookie;
    private Person _person;
    public int Id 
    {
        get { return _id; }
        set
        {
            _id = value;
            OnPropertyChanged("Id");
        }
    }

    public int CountCookie
    {
        get { return _countCookie; }
        set
        {
            _countCookie = value;
            OnPropertyChanged("Id");
        }
    }
    public string? Name
    { 
        get { return _name.ToString(); }
        set
        {
            _name = value.ToString();
            OnPropertyChanged("Name");
        }
    }
    public int Price
    { 
        get { return _price; }
        set
        {
            _price = value;
            OnPropertyChanged("Price");
        }
    
    }

    public int? PersonId 
    { 
        get { return _personId; }
        set
        {
            _personId = value;
            OnPropertyChanged("PersonId");
        }
    }
    public Person? Person 
    { 
        get { return _person; }
        set
        {
            _person = value;
            OnPropertyChanged("Person");
        }
    }

    public override string ToString()
    {
        return Name.ToString();
    }
}