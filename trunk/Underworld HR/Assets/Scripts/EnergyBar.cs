using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
	//Player variable declare
	Player p1;
    public Slider slider;
    public int energy;

    public void Set_Max_Energy() {
		p1 = GameObject.Find("Player").GetComponent<Player>();
		slider.maxValue = p1.max_energy;//assigned the value get_max_energy form player function
        slider.value = p1.energy;//assigned the value get_max_energy form player function
	}

    void Update() {
		p1 = GameObject.Find("Player").GetComponent<Player>();
		slider.value = p1.energy;
    }
    // Start is called before the first frame update
    
}
