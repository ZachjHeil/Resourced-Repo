using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Footstep_counter : MonoBehaviour
{
	
	private Player p1;
	private int steps;
	private TextMeshProUGUI step_counter;

    // Update is called once per frame
    void Update()
    {
		p1 = GameObject.Find("Player").GetComponent<Player>();
		steps = p1.get_footstep_counter();
		step_counter = gameObject.GetComponent<TextMeshProUGUI>();
		step_counter.text = "Steps: " + steps;
	}
}
