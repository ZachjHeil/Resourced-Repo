using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceDoor : MonoBehaviour {
    //public SequenceSwitch[] switchSequence;
    public List<SequenceSwitch> switchSequence = new List<SequenceSwitch>();

    private int sequenceIndex = 0; // where are we in the sequence?

    private bool sequenceSolved = false;

	private GameObject slidingDoor;

    void Start(){
        foreach(SequenceSwitch s in switchSequence){
	    s.SetMaster(this);
			slidingDoor = transform.Find("SlidingDoors_Textured_v1").gameObject;
	}
    }


    public void SwitchTriggered(SequenceSwitch s){
	// if the sequence hasn't been solved already, continue
	if(sequenceSolved)
	    return;
	
	if(switchSequence[sequenceIndex] == s){
	    RightSwitch();
	    
	}else{
	    WrongSwitch();
	}
    }

    // if the player gets the order wrong
    private void WrongSwitch(){
	// reset the sequence
	sequenceIndex = 0;

	// reset all switches to off position
	foreach(SequenceSwitch s in switchSequence){
	    s.ResetSwitch();
	}

	// PLAY SEQUENCE FAIL SOUND HERE //

	Debug.Log("INCORRECT SEQUENCE! TRY AGAIN.");
    }

    private void RightSwitch(){
	// proceed to next switch in the sequence
	sequenceIndex++;
	if(sequenceIndex == switchSequence.Count) {
	    SequenceSuccess();
	}else{
	    Debug.Log("CORRECT SWITCH, KEEP GOING!");
	}
	
    }

    // when the player gets the correct order of all the switches
    private void SequenceSuccess(){
		// PLAY SEQUENCE SUCCESS SOUND HERE //
		if (slidingDoor != null)
		{
			slidingDoor.transform.Find("Door").gameObject.transform.position += new Vector3(0f, 2.25f, 0f);
		}
		else
		{
			this.gameObject.SetActive(false); // maybe change this if it cuts off the sound
		}
	Debug.Log("SEQUENCE SUCCESS! DOOR OPENED!");
    }

    
}
