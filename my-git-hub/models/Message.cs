class Message {
    private string _text;

    public string text
    {
        get { return _text; }
        set { this._text = value; }
    }

    public Message(string text) {
        this.text = text;
    }
}