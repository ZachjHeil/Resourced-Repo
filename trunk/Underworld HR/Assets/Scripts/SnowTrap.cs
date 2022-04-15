using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTrap : MonoBehaviour
{
    public int energyDamage = 1;
    public AudioClip clip;
	public AudioSource source;
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider c){
	Player player = c.GetComponent<Player>();
	FindObjectOfType<AudioManager>().PlaySound(clip);
	FindObjectOfType<AudioManager>().Play("SnowTrap");
	if(player == null)
	    return;

	if(player.energy - energyDamage < 0){
	    player.energy = 0;
	}else {
	    player.energy -= energyDamage;
	}
    }
}
