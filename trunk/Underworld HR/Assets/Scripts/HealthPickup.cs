using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private Player p1;

	public float health_amount; 

	public AudioClip clip;
	
    void OnTriggerEnter(Collider other) {
		p1 = other.gameObject.GetComponent<Player>();
		//FindObjectOfType<AudioManager>().Play("Latte");
		FindObjectOfType<AudioManager>().PlaySound(clip);
		if ((p1.energy + health_amount) > p1.max_energy) {
			Destroy(gameObject);
			p1.energy = p1.max_energy;
		}
		else {
			Destroy(gameObject);
			p1.energy += health_amount;

		}
    }


}
