using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SCRAPS_ObjectiveList : MonoBehaviour {

    public Text objList;
    public Text objListTitle;
    private int compNum = 0;
    private int objNum = 0;
    List<Objective> objectives = new List<Objective>();

    private static SCRAPS_ObjectiveList _instance;
    public static SCRAPS_ObjectiveList instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<SCRAPS_ObjectiveList>();
            return _instance;
        }
    }

    void Update()
    {
        objList.text = "";

        Objective[] objArray = objectives.ToArray();

        for (int i=0; i < objectives.Count; i++)
        {
            Objective test = objectives[i];

            if(!test.completed)
                objList.text += (i+1)+ "." + objArray[i].objective + "\n";
            else
                objList.text += (i + 1) + ".<b><color="+SCRAPS_MessageSystem.instance.goodHex+">" + objArray[i].objective + "</color></b>\n";
        }

        if(objArray.Length > 0)
        {
            objListTitle.text = "OBJECTIVES (" + compNum + "/"+objNum+")";
        }
    }

    public void CreateObjective(string idName, string objectiveText)
    {
        objectives.Add(new Objective(idName, objectiveText, false));
        SCRAPS_MessageSystem.instance.NewMessage("", "<b>New Objective</b> - "+objectiveText, SCRAPS_MessageSystem.msgType.system);
        objectives.Sort();

        objNum++;
    }

    public void CompleteObjective(string idName)
    {
        Objective selected = objectives.Find(x => x.identifier.Contains(idName));
        selected.completed = true;
        SCRAPS_MessageSystem.instance.NewMessage("", "You've completed <b>" + selected.objective +"</b>!", SCRAPS_MessageSystem.msgType.good);
        compNum++;
    }

    public void RemoveObjective(string objName)
    {
        objectives.Remove(objectives.Find(x => x.identifier.Contains(objName)));
    }
}
