using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// A special type of switch that connected to a sequence door. The sequence door controls
// what sequence the switches need to be pulled in to open it.
public class SequenceSwitch : LeverSwitch {
    
    public SequenceDoor sequenceMaster;
    
    void Start() {
	base.Start();
    }

	// void OnTriggerEnter(Collider c){
	// 	Player player = c.GetComponent<Player>();

	// 	if(player == null)
	// 	    return;

	// 	if(sequenceMaster != null){
	// 	    FlipSwitch(true);
	// 	    sequenceMaster.SwitchTriggered(this);
	// 	}
	// }
	protected override void OnSwitchedOff()
	{
		base.OnSwitchedOff();
		if (newLever != null)
		{
			newLever.GetComponent<Animator>().Play("Lever_close");
		}
	}
	protected override void OnSwitchedOn(){
	Debug.Log(this.name+" Switched On");
		if (newLever != null)
		{
			newLever.GetComponent<Animator>().Play("Lever_open");
		}
		sequenceMaster.SwitchTriggered(this);
    }

    public void ResetSwitch(){
	// switchActivated = false;
	// FlipSwitch(false);
	SetSwitchOff();
    }

    // called by the SequenceDoor that this switch opens
    public void SetMaster(SequenceDoor master){
	sequenceMaster = master;
    }
}
