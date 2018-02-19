using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SCRAPS_MessageSystem : MonoBehaviour {

    /*public GameObject panel;
    public Text msgFrom;
    public Text msgBody;
    private bool show = false;
    private float showTime = 0;*/

    private static SCRAPS_MessageSystem _instance;
    public static SCRAPS_MessageSystem instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<SCRAPS_MessageSystem>();
            return _instance;
        }
    }

    public Text chatText;
    public int maxLines = 5;

    public enum msgType
    {
        standard,
        good,
        bad,
        system
    }

    public string standardHex;
    public string goodHex;
    public string badHex;
    public string systemHex;
    public bool debugSpam = false;

    List<string> messages = new List<string>();

    void Start()
    {
        NewMessage("", "Welcome to SCRAPS v" + INIWorker.IniReadValue(INIWorker.Sections.Version, INIWorker.Keys.value1), msgType.system);

        if(debugSpam == true)
        {
            NewMessage("SPAM", "This is debug spam, filling up space to test the message system.  This should wrap at the end of the window.", msgType.standard);
            NewMessage("SPAM", "This is debug spam, filling up space to test the message system.  This should wrap at the end of the window.", msgType.standard);
            NewMessage("SPAM", "This is debug spam, filling up space to test the message system.  This should wrap at the end of the window.", msgType.standard);
            NewMessage("SPAM", "This is debug spam, filling up space to test the message system.  This should wrap at the end of the window.", msgType.standard);
            NewMessage("SPAM", "This is debug spam, filling up space to test the message system.  This should wrap at the end of the window.", msgType.standard);
            NewMessage("SPAM", "This is debug spam, filling up space to test the message system.  This should wrap at the end of the window.", msgType.standard);
            NewMessage("SPAM", "This is debug spam, filling up space to test the message system.  This should wrap at the end of the window.", msgType.standard);
            NewMessage("SPAM", "This is debug spam, filling up space to test the message system.  This should wrap at the end of the window.", msgType.standard);
            NewMessage("SPAM", "This is debug spam, filling up space to test the message system.  This should wrap at the end of the window.", msgType.standard);
            NewMessage("SPAM", "This is debug spam, filling up space to test the message system.  This should wrap at the end of the window.", msgType.standard);
            NewMessage("SPAM", "This is debug spam, filling up space to test the message system.  This should wrap at the end of the window.", msgType.standard);
            NewMessage("SPAM", "This is debug spam, filling up space to test the message system.  This should wrap at the end of the window.", msgType.standard);
        }
    }

    public void NewMessage(string from, string body, msgType type)
    {
        //remove returns
        body = body.Replace("/n", "");
        body = body.Replace("\n", "");

        if (type == msgType.system)
            from = "SYSTEM";
        else if (type == msgType.good || type == msgType.bad)
            from = "PLAYER";

        //check for blank from
        if (from == "")
        {
            //format color
            from = "<color=" + systemHex + ">SYSTEM</color>";

            //format from
            from = "<b>" + from + "</b>:";

            messages.Add(from + "Error sending message, sender is null.");
            UpdateChat();
        }
        else
        {
            //color format
            if (type == msgType.standard)
                from = "<color=" + standardHex + ">" + from + "</color>";
            else if (type == msgType.good)
                from = "<color=" + goodHex + ">" + from + "</color>";
            else if (type == msgType.bad)
                from = "<color=" + badHex + ">" + from + "</color>";
            else if (type == msgType.system)
                from = "<color=" + systemHex + ">" + from + "</color>";

            //format from
            from = "<b>" + from + "</b>:";

            messages.Add(from + " " + body);
            UpdateChat();
        }
    }

    void UpdateChat()
    {
        if(messages.Count == maxLines)
        {
            messages.RemoveAt(0);
            messages.TrimExcess();
        }

        string[] chatMessages = messages.ToArray();
        string chatBox = "";

        foreach(string msg in chatMessages)
        {
            chatBox = chatBox + "\n" + msg;
        }

        chatText.text = chatBox;
    }
}
