using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//attached to an enemy
public class Dialogue_trigger : MonoBehaviour
{

    [System.Obsolete("'dialogue' is now obsolete, please use 'convo' instead")]
	[HideInInspector]
	public Dialogue dialogue;//variable of Dialogue class defined separately
	public Conversation convo;

	[TooltipAttribute("Dialogue for when the player brings the correct item to this employee.")]
	public Conversation successDialogue;
	public Texture2D TxtBoxBg;
	public GameObject currentObject;


	public void Start()
	{
		currentObject = gameObject;
		
	}

	public void TriggerEOSDialogue(){
	    Narrative nar = FindObjectsOfType<Narrative>()[0];
	    if(nar == null){
		Debug.LogWarning("Could not find NarrativeUI!");
		return;
	    }

	    //nar.StartConversation(dialogue);
	    nar.referenceObject = currentObject;
	    nar.StartEOSConversation(convo, TxtBoxBg);
		
	}

	public void TriggerDialogue()
	{
		//FindObjectOfType<Dialogue_Manager>().Start_dialogue(dialogue);
		Narrative nar = FindObjectsOfType<Narrative>()[0];
	    if(nar == null){
		Debug.LogWarning("Could not find NarrativeUI!");
		return;
	    }

	    //nar.StartConversation(dialogue);
	    nar.StartConversation(convo, TxtBoxBg);
		// nar.referenceObject = currentObject;
		
	}

	// called when a conversation ends
	public void OnDialogueEnded(Conversation c){
		Narrative nar = FindObjectsOfType<Narrative>()[0];
		if (c == successDialogue)
		{			
			Debug.Log("SUCCESS! Resolve this enemy");
		}
		if(nar != null){
			nar.onEndConversation.RemoveListener(OnDialogueEnded);
				GetComponent<Enemy>().ResolveEnemy();

	    }
		
	}

	public void TriggerSuccessDialogue(){
	    Narrative nar = FindObjectsOfType<Narrative>()[0];
	    
	    if(nar == null){
		Debug.LogWarning("Could not find NarrativeUI!");
		return;
	    }
	    nar.onEndConversation.AddListener(OnDialogueEnded);
		nar.StartConversation(successDialogue, TxtBoxBg);
	}
	
	public void EndDialogue()
	{
		Narrative nar = FindObjectOfType<Narrative>();
		nar.EndConversation();
	
	}
}
	
