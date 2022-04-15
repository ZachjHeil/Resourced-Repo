using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSwitch : MonoBehaviour
{
    
    public GameObject lockedDoor, slidingDoor, turnStand, newLever; // temporary
    
    private GameObject offHandle, onHandle, handleNew;

    protected  bool switchActivated = false;

    public bool isToggleSwitch;

	public AudioClip clip;

	public AudioSource source;
	float smooth= 5.0f;
    protected void Start()
    {
        offHandle = transform.Find("HandleOFF").gameObject;
		onHandle = transform.Find("HandleON").gameObject;
		newLever = transform.Find("Lever_Textured_v1").gameObject;
		if (transform.Find("Lever_Textured_v1") != null)
		{
			handleNew = transform.Find("Lever_Textured_v1").transform.Find("Lever").gameObject;
		}
		
		slidingDoor = lockedDoor.transform.Find("SlidingDoors_Textured_v1").gameObject;
		turnStand= lockedDoor.transform.Find("RotatingDoor_Textured_v1").gameObject;

		// set placeholder lever switch to up position (deactive)
		offHandle.SetActive(true);
	onHandle.SetActive(false);
    }

    protected void FlipSwitch(bool onPosition) {
		// visual switch change for placeholder lever
		if (handleNew != null)
		{
			if (onPosition)
			{
				handleNew.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
			}
			if (!onPosition)
			{
				handleNew.transform.Rotate(new Vector3(0, 0, 0) * Time.deltaTime);
			}
		}
		else if (onHandle != null || offHandle != null)
		{
			Debug.Log("second condition found");
			Debug.Log(onPosition);
			offHandle.SetActive(!onPosition);
			onHandle.SetActive(onPosition);
		}
		else
		{
			Debug.Log("not found");
		}
    }

    // called when the player flips the switch on
    virtual protected void OnSwitchedOn(){
	// temporary
	if(lockedDoor != null){
			// 'open' the door
			if (newLever != null)
			{
				newLever.GetComponent<Animator>().Play("Lever_open");
			}
			if (slidingDoor != null)
			{
				slidingDoor.transform.Find("Door").gameObject.transform.position += new Vector3(0f, 2.25f, 0f);
			}
			else if (turnStand != null)
			{
				turnStand.GetComponent<Animator>().Play("Door_open");
			}
			else if (onHandle != null || offHandle != null)
			{
				lockedDoor.SetActive(false);
			}
	}
    }

    // called when the player flips the switch off (only if this switch is toggleable)
    virtual protected void OnSwitchedOff(){
		Debug.Log("entering default off condition");
    }

    protected void SetSwitchOn(){
	switchActivated = true;
	FlipSwitch(true);
	OnSwitchedOn();
    }

    
    protected void SetSwitchOff(){
	switchActivated = false;
	FlipSwitch(false);
	OnSwitchedOff();
    }

    protected void OnTriggerEnter(Collider c){
	Player player = c.GetComponent<Player>();
	FindObjectOfType<AudioManager>().Play("Lever");

	// if the object that entered this trigger isn't a player,
	// don't continue
	if(player == null)
	    return;

	if(isToggleSwitch){
	    if(switchActivated){
		SetSwitchOff();
				Debug.Log("this is the switch activated message"+switchActivated);
		// switchActivated = false;
		// FlipSwitch(false);
		// OnSwitchedOff();
	    }else{
		// switchActivated = true;
		// FlipSwitch(true);
		// OnSwitchedOn();
		SetSwitchOn();
				Debug.Log("this is the switch on activated message" + switchActivated);
			}
	}else if(!switchActivated){
	    // switchActivated = true;
	    // FlipSwitch(true);
	    // OnSwitchedOn();
	    SetSwitchOn();
			Debug.Log("this is the switch on activated 2nd message" + switchActivated);
			Debug.Log(isToggleSwitch);
		}
	
	

	
    }
}
