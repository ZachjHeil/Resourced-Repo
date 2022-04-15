using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
	private GameObject mainMenu, settingsMenu;
	private GameObject backBtn;
	public Animator camAnim;
	private Animator cbAnim;
	public bool titlescrn;
	// Start is called before the first frame update
	void Start()
	{
		titlescrn = true;
		mainMenu = GameObject.Find("MenuUI");
		settingsMenu = GameObject.Find("SettingsUI");
		//settingsMenu.SetActive(false);
		if(settingsMenu != null)
		    settingsMenu.GetComponent<SettingsUI>().Hide();
		
		mainMenu.GetComponent<MainMenu>().Hide();
		backBtn = GameObject.Find("Back_button");
		backBtn.SetActive(false);
		camAnim = gameObject.GetComponent<Animator>();
		cbAnim = GameObject.Find("Clipboard_title_v1").GetComponent<Animator>();
	}

	// Update is called once per frame

	IEnumerator menuDelay()
	{
		yield return new WaitForSeconds(4);
		titlescrn = false;
		mainMenu.GetComponent<MainMenu>().Show();
		backBtn.SetActive(true);
	}
	IEnumerator creditDelay()
	{
		titlescrn = false;
		yield return new WaitForSeconds(4);
		backBtn.SetActive(true);
	}

	IEnumerator settingsDelay()
	{
	    SettingsUI setUI = settingsMenu.GetComponent<SettingsUI>();
	    if(!setUI.IsShown()){
		titlescrn = false;
		yield return new WaitForSeconds(4);
		//settingsMenu.SetActive(true);
		setUI.Show();
		
		//backBtn.SetActive(true);
		}
		
	}
 
	void Update()
	{
	    if(settingsMenu == null){
		settingsMenu = GameObject.Find("SettingsUI");
		settingsMenu.GetComponent<SettingsUI>().Hide();
	    }
		if (titlescrn)
		{
			if (Input.GetMouseButtonDown(0))
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit, 100.0f))
				{
					Debug.Log(hit);
					if (hit.transform != null)
					{
						Debug.Log(hit.transform.gameObject);
						if (hit.transform.gameObject.tag == "Laptop")
						{
							camAnim.Play("Focus_Main_menu");
							StartCoroutine(menuDelay());

						}
						if (hit.transform.gameObject.tag == "Bulletin")
						{
							camAnim.Play("Focus_Credits");
							StartCoroutine(creditDelay());
						}
						if (hit.transform.gameObject.tag == "ClipBoard")
						{
							cbAnim.Play("Focus_ClipBoard");
							StartCoroutine(settingsDelay());
						}
					}
				}
			}
		}
	}
}
