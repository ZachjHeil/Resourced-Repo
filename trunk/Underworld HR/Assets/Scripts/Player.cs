using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//Energy var
	private GameObject abc;
	public float energy;//energy of player
	private Renderer agnesRend;
	public float max_energy = 30;// max energy of player
	private int frame = 0, cframe=0;
	//Game over tracker
	public int game_over = 0;
	public SpikeTrapPlayer spikes;

	public bool stop;
	public IceTitle it1;

	public GameObject mainCharacter;
	public Animator agnesanim;
	private int step_counter = 0;// step counter for player
	private float rayLength = 1f;//length of ray

	//private float velocity = 1f;
	private float speed = 5f; //movement speed of character
	private bool can_Move;// boolean to check and stall movement after 1 step
	private GameObject narUI;
	private GameObject gameoverUI;
	public bool freeze, control;

	public bool slide;

	private Vector3 up = Vector3.zero,
		right = new Vector3(0, 90, 0),
		down = new Vector3(0, 180, 0),
		left = new Vector3(0, 270, 0),
		current_direction = Vector3.zero;

	Vector3 next_Pos, destination, direction;


	private PauseMenu pauseMenu;

	// reference to the player inventory
	private Inventory inventory;
	private bool iceTilesEnergyDecreased;
	public bool isInIcetiles; 


	/*---------------UNUSED VARIABLES--------------------------
	private ushort health;// health of Player
	private ushort max_health;//max health of player
	private int maxEnergy = 10;
	private int currentEnergy;
	private float maxHealth = 10;
	private float curHealth;
	*/


	// Start is called before the first frame update
	void Start()
	{
		isInIcetiles = false;
		iceTilesEnergyDecreased = true;
		mainCharacter = GameObject.Find("Agnes");

		slide = true;
		stop = true;

		control = true;
		if (mainCharacter != null)
			agnesanim = mainCharacter.GetComponent<Animator>();

		if (agnesanim != null)
		{
			StartCoroutine(agnesAnimTimer());
		}//needed for the delay between idle and walk animations for the character
		narUI = GameObject.Find("NarrativeUI");
		// narUI.SetActive(false);
		gameoverUI = GameObject.Find("GameOver");
		// gameoverUI.SetActive(false);
		freeze = false;
		current_direction = up;
		next_Pos = Vector3.forward;
		destination = transform.position;
		energy = max_energy;
		inventory = GetComponent<Inventory>();

		GameObject pMenu = GameObject.Find("PauseMenuUI");
		if (pMenu != null)
			pauseMenu = pMenu.GetComponent<PauseMenu>();
		/*currentEnergy = maxEnergy;
		EnergyBar.SetMaxEnergy(maxEnergy);*/
		spikes = GetComponent<SpikeTrapPlayer>();
	}


	//Creates a delay between the idle and walk animations to show the change
	IEnumerator agnesAnimTimer()
	{
		yield return new WaitForSeconds(1);
		agnesanim.Play("Idle");

		StartCoroutine(agnesAnimTimer());
	}
	IEnumerator spikeTimer()
	{
		yield return new WaitUntil(() => frame > 10);
		spikes.updateState();
	}
	private void AgnesShader()
	{
		float opacPerc = 1-(get_energy() / get_max_energy());
		agnesRend= GameObject.Find("Agnes").transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>();
		agnesRend.material.SetColor("_Color", new Color(1, 1, 1, opacPerc));
	}
	// Update is called once per frame
	void Update()
	{
		Move();
		cframe++;
		frame++;
		AgnesShader();
		item_Drop_Controls();
		narUI = GameObject.Find("NarrativeUI");
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PauseMenuControls();
		}
		if (energy == 0 && game_over != 1)
		{

			Game_over();
		}

		/*if (Input.GetKeyDown(KeyCode.D)){
			TakeDamage(2);
			EnergyBar ebar;
			ebar.SetMaxEnergy(currentEnergy);
		}*/
	}


	private void item_Drop_Controls() {

		if (Input.GetKeyDown(KeyCode.Space)) { // Spacebar to drop inventory item (We can change this)

			if (inventory.IsFull) { // only do this if we have an item in the inventory
				inventory.DropItem();
				FindObjectOfType<AudioManager>().Play("Paper");
				energy--;
			}
		}
	}



	private void PauseMenuControls() {
		if (game_over != 1) {
			if (pauseMenu != null) {
				if (pauseMenu.IsOpen())
				{
					pauseMenu.HidePauseMenu();
					freeze = false;
					// unfreeze player controls
				}
				else if(!freeze)
				{
					pauseMenu.ShowPauseMenu();

					freeze = true;
					// freeze player controls
				}
			}
		}
		if (game_over != 1 && !pauseMenu.IsOpen()) {

		    //freeze = false;
		}
	}

	/*private void checkNarUI()
	{
		if (narUI.activeSelf)
		{
			freeze = true;
		}
		else
		{
			freeze = false;
		}
	}
/**	public void slide()
	{
		if (collider_check())
					{
						destination = transform.position + next_Pos;
						direction = next_Pos;
						can_Move = true;
					}

	}**/

	IEnumerator controlTimer()
	{
		//yield return new WaitForSeconds(1);
		cframe = 0;
		yield return new WaitUntil(() => cframe >= 20);
		control=true;
	}
	private void Move()
	{
		if (!freeze){
			transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
			if (control)
			{
				
				if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
				{
					//Player.velocity = new Vector3(speed, 0);
					FindObjectOfType<AudioManager>().Play("walking");
					next_Pos = Vector3.forward;
					current_direction = up;
					if (game_over == 0)
					{
						if (!stop && inventory.IsFull)
						{
							if (collider_check())
							{
								if (isInIcetiles && iceTilesEnergyDecreased != true)
								{
									energy--;
									iceTilesEnergyDecreased = true;
								}

							}
							else
							{
								iceTilesEnergyDecreased = false;
							}
						}
						can_Move = true;
						if (agnesanim != null)
							agnesanim.Play("Walk");
						control = false;
						StartCoroutine(controlTimer());
					}
				}
				if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
				{
					//Player.velocity = new Vector3(speed, 0);
					FindObjectOfType<AudioManager>().Play("walking");
					next_Pos = Vector3.back;
					current_direction = down;
					if (game_over == 0)
					{
						if (!stop && inventory.IsFull)
						{
							if (collider_check())
							{
								if (isInIcetiles && iceTilesEnergyDecreased != true)
								{
									energy--;
									iceTilesEnergyDecreased = true;
								}

							}
							else
							{
								iceTilesEnergyDecreased = false;
							}
						}
						can_Move = true;
						if (agnesanim != null)
							agnesanim.Play("Walk");
						control = false;
						StartCoroutine(controlTimer());
					}
				}
				if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
				{
					//Player.velocity = new Vector3(speed, 0);
					FindObjectOfType<AudioManager>().Play("walking");
					next_Pos = Vector3.right;
					current_direction = right;
					if (game_over == 0)
					{
						if (!stop && inventory.IsFull)
						{
							if (collider_check())
							{
								if (isInIcetiles && iceTilesEnergyDecreased != true)
								{
									energy--;
									iceTilesEnergyDecreased = true;
								}

							}
							else
							{
								iceTilesEnergyDecreased = false;
							}
						}
						can_Move = true;
						if (agnesanim != null)
							agnesanim.Play("Walk");
						control = false;
						StartCoroutine(controlTimer());
					}
				}
				if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
				{
					//Player.velocity = new Vector3(speed, 0);
					FindObjectOfType<AudioManager>().Play("walking");
					next_Pos = Vector3.left;
					current_direction = left;
					if (game_over == 0)
					{
						if (!stop && inventory.IsFull)
						{
							if (collider_check())
							{
								if (isInIcetiles && iceTilesEnergyDecreased != true)
								{
									energy--;
									iceTilesEnergyDecreased = true;
								}

							}
							else
							{
								iceTilesEnergyDecreased = false;
							}
						}
						can_Move = true;
						if (agnesanim != null)
							agnesanim.Play("Walk");
						control = false;
						StartCoroutine(controlTimer());
					}
				}
			}
			/*else if (it1.inIceTiles)
			{
				if (!can_Move)
				{
					transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
					if (Input.GetKeyDown(KeyCode.W))
					{
						//Player.velocity = new Vector3(speed, 0);
						FindObjectOfType<AudioManager>().Play("walking");
						next_Pos = Vector3.forward;
						current_direction = up;
						if (game_over == 0)
						{
							can_Move = true;
							if (agnesanim != null)
								agnesanim.Play("Walk");
						}
					}
					if (Input.GetKeyDown(KeyCode.S))
					{
						//Player.velocity = new Vector3(speed, 0);
						FindObjectOfType<AudioManager>().Play("walking");
						next_Pos = Vector3.back;
						current_direction = down;
						if (game_over == 0)
						{
							can_Move = true;
							if (agnesanim != null)
								agnesanim.Play("Walk");
						}
					}
					if (Input.GetKeyDown(KeyCode.D))
					{
						//Player.velocity = new Vector3(speed, 0);
						FindObjectOfType<AudioManager>().Play("walking");
						next_Pos = Vector3.right;
						current_direction = right;
						if (game_over == 0)
						{
							can_Move = true;
							if (agnesanim != null)
								agnesanim.Play("Walk");
						}
					}
					if (Input.GetKeyDown(KeyCode.A))
					{
						//Player.velocity = new Vector3(speed, 0);
						FindObjectOfType<AudioManager>().Play("walking");
						next_Pos = Vector3.left;
						current_direction = left;
						if (game_over == 0)
						{
							can_Move = true;
							if (agnesanim != null)
								agnesanim.Play("Walk");
						}
					}
				}
			}*/
			if (Vector3.Distance(destination, transform.position) <= 0.000001f)
			{
				transform.localEulerAngles = current_direction;
				if (can_Move)
				{
					//checks if the movement is possible 
					if (collider_check())
					{
						destination = transform.position + next_Pos;
						direction = next_Pos;
						if (slide)
						{
							can_Move = false;
						}

						frame = 0;
						StartCoroutine(spikeTimer());
						step_counter += 1;
						if (inventory.IsFull && stop)
						{
							energy--;
						}
					}
				}
			}

		}
	}




	//function to check valid movement of player with relation to other in-game objects
	private bool collider_check()
	{
		Ray Player_Ray = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.forward);
		RaycastHit hit;

		Debug.DrawRay(Player_Ray.origin, Player_Ray.direction, Color.red);

		if (Physics.Raycast(Player_Ray, out hit, rayLength))
		{
			//implements collisons with wall
			if (hit.collider.tag == "Wall")
			{
				return false;
			}
			//implements collisions with enemy
			else if (hit.collider.tag == "Enemy")
			{
				Enemy e1 = gameObject.GetComponent<Enemy>();
				if (e1 != null)
				{//needed to avoid null exception error
					if (!e1.check_Inv())
					{
						return false;
					}
					else
					{
						return true;
					}
				}
			}
			//implements collisions with items
			else if (hit.collider.tag == "Item")
			{
				Inventory inv = this.GetComponent<Inventory>();
				if (inv.IsFull)
				{
					return false;
				}
				else
				{
					return true;
				}

			}
			else if (hit.collider.tag == "IceTiles")
			{
				slide = false;
				stop = false;
				return true;
			}
		}
		return true;
	}

	//returns the energy value of the player
	public float get_energy()
	{
		return energy;
	}

	//returns the max energy value of the player
	public float get_max_energy()
	{
		return max_energy;
	}

	//updates the value for max energy
	public void update_max_energy(float new_val)
	{
		max_energy = new_val;
	}


	//updates the value for energy
	public void update_energy(float new_val)
	{
		energy = new_val;
	}
	//returns the footstep count
	public int get_footstep_counter()
	{
		return step_counter;
	}
	public void Game_over()
	{
		game_over = 1;
		freeze = true;
		//gameoverUI.SetActive(true);
		if (gameoverUI != null)
			gameoverUI.GetComponent<GameOverUI>().Show();
	}
	public void updateFreeze()
	{
		freeze = !freeze;
	}

	// Prevents player controls from moving the player; can be used for UI
	public void Freeze(){
	    freeze = true;
	}

	// Allows player controls to move the player
	public void Unfreeze(){
	    freeze = false;
	}

	
	/*--------------UNUSED FUNCTIONS-----------------
	//returns the health value of the player
	public ushort get_health()
	{
		return health;
	}

	//returns the max health value of the player 
	public ushort get_max_health()
	{
		return max_health;
	}


	//updates the value for max health
	public void update_max_health(ushort new_val)
	{
		max_health = new_val;
	}


	//updates the value for health
	public void update_health(ushort new_val)
	{
		health = new_val;
	}

	void TakeDamage(int damage)
	{
		currentEnergy -= damage;
	}
	*/
}
	
