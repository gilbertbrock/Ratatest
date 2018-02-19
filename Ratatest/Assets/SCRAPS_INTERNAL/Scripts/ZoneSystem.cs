using UnityEngine;
using UnityEngine.UI;

public class ZoneSystem : MonoBehaviour {

    public Text msgBody;

    public void NewMessage(string body)
    {
        //fix new lines for Windows
        body = body.Replace("/n", "\n");
        msgBody.text = body.ToUpper();
    }
}
