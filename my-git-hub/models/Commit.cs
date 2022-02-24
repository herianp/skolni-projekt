class Commit {
    private string _message;
    private DateTime _dateTime;

    public Commit(string msg) {
        this.message = msg;
    }

    public string message
    {
        get { return _message; }
        set { this._message = value; }
    }
    
    public DateTime dateTime
    {
        get { return _dateTime; }
        set { this._dateTime = value; }
    }

    
}