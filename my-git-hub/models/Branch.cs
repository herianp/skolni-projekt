using System.Collections;

public class Branch
{
    private string _name;
    private string _owner;
    private Token _token;

    private ArrayList _messages; //todo when checkout delete messages
    private ArrayList _commmitList;

    private ArrayList _addList;

    private ArrayList _branchData;

    public Branch(string name, User user)
    {
        this.name = name;
        this.owner = user.name;
        this.token = user.token;
        commmitList  = new ArrayList();
        messages  = new ArrayList();
        addList = new ArrayList();
        branchData = new ArrayList();
    }

    //GETTERS AND SETTERS
    public string name
    {
        get { return _name; }
        set { this._name = value; }
    }

    public string owner
    {
        get { return _owner; }
        set { this._owner = value; }
    }

 
    public Token token
    {
        get { return _token; }
        set { this._token = value; }
    }

    public ArrayList commmitList
    {
        get { return _commmitList; }
        set { this._commmitList = value; }
    }

    public ArrayList messages
    {
        get { return _messages; }
        set { this._messages = value; }
    }

    public ArrayList addList
    {
        get { return _addList; }
        set { this._addList = value; }
    }

    public ArrayList branchData
    {
        get { return _branchData; }
        set { this._branchData = value; }
    }

}