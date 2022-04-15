using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EOS_Narrative : MonoBehaviour
{
	private Dialogue_trigger dt1;
	private GameObject narUI;
	private Narrative nar;
	private void Awake()
	{
		dt1 = GetComponent<Dialogue_trigger>();
		narUI = GameObject.Find("NarrativeUI");
		nar = narUI.GetComponent<Narrative>();
	}
	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player")
		{
			FindObjectOfType<AudioManager>().Play("Paper");
			narUI.SetActive(true);
			//nar.EOScheck = true;
			dt1.TriggerEOSDialogue();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.name == "Player")
		{
			FindObjectOfType<AudioManager>().Play("Paper");
			dt1.EndDialogue();
		}
	}
}
