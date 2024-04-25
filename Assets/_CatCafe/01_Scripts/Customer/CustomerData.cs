using System;

[System.Serializable]
public class CustomerEntry
{
    public string id;
    public string name;
    public string occupation;
    public string demands;
}

[System.Serializable]
public class CustomerDatabase
{
    public CustomerEntry[] customers;
}
 
 