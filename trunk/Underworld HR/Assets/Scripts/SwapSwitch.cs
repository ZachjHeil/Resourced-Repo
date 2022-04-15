using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "When One Door Closes, Another Opens"
public class SwapSwitch : LeverSwitch {
    public GameObject unlockedDoor, slidingDoor1, turnStand1, UnlockedBlock; // the 'door' that is closed when this lever is pulled
	[Tooltip("Switch between on and off everytime the player steps on this lever.")]
    public bool toggleSwitch;

    public bool allDoorsStartClosed;
    
    public void Start() {
		base.Start();
		isToggleSwitch = toggleSwitch;

		slidingDoor1= unlockedDoor.transform.Find("SlidingDoors_Textured_v1").gameObject;
		turnStand1 = unlockedDoor.transform.Find("RotatingDoor_Textured_v1").gameObject;
		if (unlockedDoor != null && !allDoorsStartClosed)
		{
			if (slidingDoor1 != null)
			{
				slidingDoor1.transform.Find("Door").gameObject.transform.position += new Vector3(0f, 2.25f, 0f);
				UnlockedBlock.transform.Find("Cube").gameObject.transform.position += new Vector3(0f, 2.25f, 0f);
			}
			if (turnStand1 != null)
			{
				turnStand1.GetComponent<Animator>().Play("Door_open");
			}
			else
			{
				unlockedDoor.SetActive(false); // Just in case
			}
		}
	}

    // void OnTriggerEnter(Collider c){
    // 	base.OnTriggerEnter(c);
    // 	if(unlockedDoor != null){
    // 	    unlockedDoor.SetActive(true);
    // 	}
    // }

    protected override void OnSwitchedOn(){
	Debug.Log("ON");
		if (newLever != null)
		{
			newLever.GetComponent<Animator>().Play("Lever_open");
		}
		if (unlockedDoor != null)
	    {
			if (slidingDoor1 != null)
			{
				slidingDoor1.transform.Find("Door").gameObject.transform.position -= new Vector3(0f, 2.25f, 0f);
			}
			else if (turnStand1 != null)
			{
				Debug.Log("It's the first turnstand");
				turnStand1.GetComponent<Animator>().Play("Door_close");
			}
			if (slidingDoor1 == null && turnStand1 == null)
			{
				Debug.Log("Entered default state 1");
				unlockedDoor.SetActive(true); // Just in case
			}
		}
		if (lockedDoor != null)
		{
			if (slidingDoor != null)
			{
				slidingDoor.transform.Find("Door").gameObject.transform.position += new Vector3(0f, 2.25f, 0f);
			}
			else if (turnStand != null)
			{
				Debug.Log("It's the second turnstand");
				turnStand.GetComponent<Animator>().Play("Door_open");
			}
			if (slidingDoor == null && turnStand == null)
			{
				Debug.Log("entered default state 2");
				lockedDoor.SetActive(false);
			}
		}
	}

    protected override void OnSwitchedOff(){
	Debug.Log("OFF");
		if (newLever != null)
		{
			newLever.GetComponent<Animator>().Play("Lever_close");
		}
		if (unlockedDoor != null)
		{
			if (slidingDoor1 != null)
			{
				slidingDoor1.transform.Find("Door").gameObject.transform.position += new Vector3(0f, 2.25f, 0f);
			}
			if (turnStand1 != null)
			{
				turnStand1.GetComponent<Animator>().Play("Door_open");
			}
			if (slidingDoor1 == null && turnStand1 == null)
			{
				Debug.Log("entered default state 3");
				unlockedDoor.SetActive(false); // Just in case
			}
		}
		if (lockedDoor != null)
		{
			if (slidingDoor != null)
			{
				slidingDoor.transform.Find("Door").gameObject.transform.position -= new Vector3(0f, 2.25f, 0f);
			}
			if (turnStand != null)
			{
				turnStand.GetComponent<Animator>().Play("Door_close");
			}
			if(slidingDoor==null && turnStand==null)
			{
				Debug.Log("entered default state 4");
				lockedDoor.SetActive(true);
			}
		}
	}
}
