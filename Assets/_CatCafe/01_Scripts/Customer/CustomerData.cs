using System;

[System.Serializable]
public class CustomerEntry
{
    public string id;
    public string name;
    public string occupation;
    public string[] desiredTraits;
	public Conversation[] conversations;
}

[System.Serializable]
public class Conversation
{
	public string desiredTrait;
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
 
 