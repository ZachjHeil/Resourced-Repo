using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapPlayer : MonoBehaviour
{
	public bool state;
	private Player player;
	// Start is called before the first frame update
	void Start()
	{
		state = true;
	}

	// Update is called once per frame
	void Update()
	{

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "KillTrap" && state)
		{
			player = gameObject.GetComponent<Player>();
			FindObjectOfType<AudioManager>().Play("SpikeTrap");
			player.Game_over();
		}
		else if (other.tag =="KillTrap")
		{
			player = gameObject.GetComponent<Player>();
			Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
		}

	}
	public void updateState()
	{
		state=!state;
	}
}
