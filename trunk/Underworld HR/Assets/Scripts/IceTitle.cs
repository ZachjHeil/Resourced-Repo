using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTitle : MonoBehaviour
{
	private GameObject p1;
	private Player player;
	private Inventory inv;
    private float velocity = 1f;
	private float speed = 5f;
	public bool inIceTiles;
	public AudioClip clip;
	public AudioSource source;





	void Start()
	{
		p1 = GameObject.Find("Player");
		inIceTiles = false;
		inv = p1.GetComponent<Inventory>();
	}

	private void Update()
	{
		/*Ray Player_Ray = new Ray(p1.transform.position + new Vector3(0, 0.25f, 0), p1.transform.forward);
		RaycastHit hit;
		if (Physics.Raycast(transform.position + new Vector3(0, 0.25f, 0), transform.forward, out hit, 2f))
		{
			if (hit.collider.tag == "IceTiles")
			{
				player.slide = false;
				player.stop = false;
			}
		}*/
	}



	private void OnTriggerEnter(Collider other)
	{			
		if (other.name == "Player")
		{
			player = GameObject.Find("Player").GetComponent<Player>();
			player.control = false;
			if (inv.IsFull)
			{
				player.energy--;
			}
			player.isInIcetiles = true;
        }
	}
	private void OnTriggerStay(Collider other)
	{
		FindObjectOfType<AudioManager>().PlaySound(clip);
		inIceTiles = true;
		if (other.name == "Player")
		{
			player = GameObject.Find("Player").GetComponent<Player>();
			player.stop = false;
			player.slide = false;
			Ray Player_Ray = new Ray(p1.transform.position + new Vector3(0, 0f, 0), p1.transform.forward);
			RaycastHit hit;
			if (Physics.Raycast(Player_Ray, out hit, 1f))
			{
				if (hit.collider.tag == "Wall")
				{
					player.control = true;
				}
				else
				{
					player.control = false;
				}
			}
			else
			{
				player.control = false;
			}
		}
	}

	private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
		{
			player = GameObject.Find("Player").GetComponent<Player>();
            player.control = true;
			player.slide = true;
			player.stop = true;
			player.isInIcetiles = false;
        }
    }

    // Update is called once per frame
  
}
