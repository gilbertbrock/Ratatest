using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthDisplay : MonoBehaviour {

	[SerializeField]
	private Health CurHealth;

	[SerializeField]
	private Text myGuiText;

    [SerializeField]
    private Image myBar;

	void Start ()
	{
		if(myGuiText == null)
		{
			Debug.LogWarning("No \"text\" component assigned to Health Display for Player!");
		}

        if (myBar == null)
        {
            Debug.LogWarning("No \"image\" component assigned to Health Display for Player!");
        }
    }

	void Update() {
		if(myGuiText!=null)
		{
			myGuiText.text = "" + CurHealth.currentHealth + "/" +CurHealth.maxHealth;
		}

        if(myBar!=null)
        {
            myBar.fillAmount = ((float)CurHealth.currentHealth / CurHealth.maxHealth);
        }
	}
}

