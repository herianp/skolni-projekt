public class Token
{
    private string _token;

    public string token
    {
        get { return _token; }
        set { this._token = value; }
    }

    public Token()
    {
    }

    public Token(string token)
    {
        this.token = token;
    }
}