
using System.Collections;

public class User
{
    private string _name;
    private Token _token;
    private ArrayList _listOfBranch;

    public User()
        {
            listOfBranch = new ArrayList();
        }

    public User(string name)
    {
        listOfBranch = new ArrayList();
        this.name = name;
        this.token = new Token(GenerateToken());
    }

    //GETTERS AND SETTERS
    public string name
    {
        get { return _name; }
        set { this._name = value; }
    }

    public Token token
    {
        get { return _token; }
        set { this._token = value; }
    }

    public ArrayList listOfBranch
    {
        get { return _listOfBranch; }
        set { this._listOfBranch = value; }
    }

    //ADD NEW BRANCH TO USER
    public Branch addBranch(string branchName)
    {
        Branch branch = new Branch(branchName, this);
        listOfBranch.Add(branch);

        return branch;
    }

    //GENERATE A TOKEN FOR USER (FAKE SECURITY)
    public static string GenerateToken()
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 10)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }


}