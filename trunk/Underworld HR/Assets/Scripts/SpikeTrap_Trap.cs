using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap_Trap : MonoBehaviour
{
	SpikeTrapPlayer player;
	private bool status;
	public Transform [] models;
	GameObject open;
	GameObject closed;

	public AudioClip clip;
	public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.Find("Player").GetComponent<SpikeTrapPlayer>();
		models = gameObject.GetComponentsInChildren<Transform>();
		
    }

	void OnTriggerEnter(Collider other){
		FindObjectOfType<AudioManager>().PlaySound(clip);
	}

    // Update is called once per frame
    void Update()
    {
		open = models[1].gameObject;
		closed = models[2].gameObject;
		status = player.state;
		
		if (!status)
		{
			open.SetActive(true);
			closed.SetActive(false);
		}
		if (status)
		{
			open.SetActive(false);
			closed.SetActive(true);
		}
    }
}
