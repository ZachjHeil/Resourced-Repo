using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
	private Inventory inv;
	private bool val;
	Collider col;
	public Dialogue_trigger dt1;
	private GameObject narUI;
	// Start is called before the first frame update
	private void Awake()
	{
		dt1 = GetComponent<Dialogue_trigger>();
		narUI = GameObject.Find("NarrativeUI");
	}
	// Update is called once per frame
	void Update()
    {

    }
	public bool check_Inv()
	{
		return val;
	}


	void OnTriggerEnter(Collider enemy)
	{
		
		if (enemy.name == "Player")
		
		{
			FindObjectOfType<AudioManager>().Play("Paper");
			inv = enemy.gameObject.GetComponent<Inventory>();

			// checks if the player's inventory item belongs to this enemy
			if (!inv.IsFull)
			{
				narUI.SetActive(true);
				dt1.TriggerDialogue();
			}
			if (inv.IsItemForEnemy(this))
			{
			    //Destroy(gameObject);
			    dt1.TriggerSuccessDialogue();
				
				inv.Item_Use();
				val = true;
				//GameObject.Find("Dialogue").GetComponent<TextMeshProUGUI>().text= "<Empty>";
				FindObjectOfType<AudioManager>().Play("Paper");
			}
			else
			{
				narUI.SetActive(true);
				dt1.TriggerDialogue();
				val = false;
			}

		}
	}

	private void OnTriggerExit(Collider enemy)
	{
		if (enemy.name == "Player")
		{
			FindObjectOfType<AudioManager>().Play("Paper");
			if (!inv.IsFull)
			{
				dt1.EndDialogue();
			}
		}
	}

	/*private void OnTriggerExit(Collider other)
	{
		if (other.name == "Player")
		{
			GameObject.Find("Dialogue_box").SetActive(false);
		}
	}*/

	public void ResolveEnemy(){
	    Destroy(gameObject);
	}

}
