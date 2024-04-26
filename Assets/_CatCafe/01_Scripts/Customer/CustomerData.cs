using System;

[System.Serializable]
public class CustomerEntry
{
    public string id;
    public string name;
    public string occupation;
    public ConversationText[] conversationTexts;
}

[System.Serializable]
public class ConversationText
{
    public string speaker;
    public string text;
}

[System.Serializable]
public class CustomerDatabase
{
    public CustomerEntry[] customers;
}
 
 