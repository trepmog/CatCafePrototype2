[System.Serializable]
public class CatDatabase
{
    public Cat[] cats;
}

[System.Serializable]
public class Cat
{
    public string id;
    public string name;
    public string gender;
    public string bodyType;
    public string fur;
    public string breed;
    public string[] character;
}

