using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_OutpostBuild : MonoBehaviour {

    public int currentStage = 0;

    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;

    public int boards = 0;
    private int stage1boards = 4;
    private int stage2boards = 8;
    private int stage3boards = 12;

    public Collider transition;

    void Start()
    {
        SCRAPS_ObjectiveList.instance.CreateObjective("lvd_outpost", "Construct the Scrapper Outpost with Wood Boards.");
    }
	
	void OnTriggerEnter(Collider other)
    {
        /*if(other.GetComponent<Outpost_Board>() != null)
        {
            Destroy(other.gameObject);
            boards++;

            if (currentStage == 0)
            {
                if(boards < stage1boards)
                    SCRAPS_MessageSystem.instance.NewMessage("Outpost Progress", "Insert <b>" + (stage1boards - boards) + "</b> boards to complete <b>Stage 1</b>!", 3.0f);
                else
                {
                    SCRAPS_MessageSystem.instance.NewMessage("Outpost Progress", "Upgrade complete!", 3.0f);
                    stage1.SetActive(true);
                    currentStage++;
                }
            }
            else if (currentStage == 1)
            {
                if (boards < stage2boards)
                    SCRAPS_MessageSystem.instance.NewMessage("Outpost Progress", "Insert <b>" + (stage2boards - boards) + "</b> boards to complete <b>Stage 2</b>!", 3.0f);
                else
                {
                    SCRAPS_MessageSystem.instance.NewMessage("Outpost Progress", "Upgrade complete!", 3.0f);
                    stage2.SetActive(true);
                    currentStage++;
                }
            }
            else if (currentStage == 2)
            {
                if (boards < stage3boards)
                    SCRAPS_MessageSystem.instance.NewMessage("Outpost Progress", "Insert <b>" + (stage3boards - boards) + "</b> boards to complete <b>Stage 3</b>!", 3.0f);
                else
                {
                    SCRAPS_MessageSystem.instance.NewMessage("Outpost Progress", "Upgrade complete!", 3.0f);
                    stage3.SetActive(true);
                    SCRAPS_ObjectiveList.instance.CompleteObjective("lvd_outpost");
                    transition.enabled = true;
                    Destroy(gameObject);
                }
            }
        }*/
    }
}
