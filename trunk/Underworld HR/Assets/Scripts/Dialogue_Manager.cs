using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// attached to the Dialogue_Manager in-game object
public class Dialogue_Manager : MonoBehaviour
{
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dia;


	private Queue<string> sentences;
	private GameObject narUI;
	// Start is called before the first frame update
	void Start()
    {
		sentences = new Queue<string>();
		narUI = GameObject.Find("NarrativeUI");
	}

	public void Start_dialogue(Dialogue dialogue)
	{
		Debug.Log("starting convo with" + dialogue.name);

		
		sentences.Clear();
		foreach (string line in dialogue.sentences)
		{
			sentences.Enqueue(line);
		}

		Display_next();
	}
	public void Display_next()
	{
		if (sentences.Count == 0)
		{
			End_dialogue();
			return;
		}
		// string line = sentences.Dequeue();
		// FindObjectOfType<Narrative>().Line_update(line);
	}

	// returns the current line
	public string GetLine(){
	    return sentences.Dequeue();
	}

	void End_dialogue()
	{
		narUI.SetActive(false);
		Debug.Log("end of convo");
	}
}
