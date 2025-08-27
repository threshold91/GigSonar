namespace GigSonar;

public class Notification
{
    private int _id;
    private string _message;
    private string _timestamp;
    private string _status;
    private Event _eventData;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Message
    {
        get { return _message; }
        set { _message = value; }
    }

    public string Timestamp
    {
        get { return _timestamp; }
        set { _timestamp = value; }
    }

    public string Status
    {
        get { return _status; }
        set { _status = value; }
    }

    public Event EventData
    {
        get { return _eventData; }
        set { _eventData = value; }
    }
}