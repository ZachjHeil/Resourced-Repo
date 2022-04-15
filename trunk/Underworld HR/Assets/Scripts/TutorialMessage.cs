using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessage : MonoBehaviour
{
    [TextArea(3,10)]
    public string message;
    private TutorialUI tutUI;
	private Player p;
    
    void Start()
    {
        // GameObject tut = GameObject.Find("TutorialUI");
	// if(tut != null){
	//     tutUI = tut.GetComponent<TutorialUI>();
	// }
	AssignUI();
    }

    private void AssignUI(){
	GameObject tut = GameObject.Find("TutorialUI");
	if(tut != null){
	    tutUI = tut.GetComponent<TutorialUI>();
	}
    }

    void OnTriggerEnter(Collider c){
	if(c.GetComponent<Player>() != null){
		p = c.GetComponent<Player>();
	    Debug.Log("Entered Message Trigger");
	    if(tutUI == null)
		AssignUI();
		p.Freeze();
	    tutUI.ShowMessage(this);
	}
    }

    void OnTriggerExit(Collider c){
		if(c.GetComponent<Player>() != null){
		p = c.GetComponent<Player>();
	    Debug.Log("Exited Message Trigger");
			if (tutUI == null)
			{
				AssignUI();
				tutUI.HideMessage();
				
			}

		}
		Destroy(this.gameObject);
	}

	public void OnExit(){
		tutUI.HideMessage();
		p.Unfreeze();
	}
}
