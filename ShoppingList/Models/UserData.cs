namespace ShoppingList.Models;

public class UserDataCollection
{
    public UserData[] UserDataItems { get; set; }
}


public class UserData
{
    public string dataID { get; set; }
    public string dataValue { get; set; }
    public string userkey { get; set; }

    public UserData(string dataID, string dataValue, string userkey)
    {
        this.dataID = dataID;
        this.dataValue = dataValue;
        this.userkey = userkey;
    }
}