using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Back_btn_main_menu : MonoBehaviour
{
	private GameObject menuCam, menuUI, backbtn, clipboard, settingsUI;
	private Animator cam;
	private Animator cb;
	private VisualElement root, back;
	public bool menuUIactive;
	MenuCamera clickselector;
	// Start is called before the first frame update
	void Awake()
	{
		root = GetComponent<UIDocument>().rootVisualElement;
		back = root.Q<VisualElement>("Back");
		menuCam = GameObject.Find("MenuCamera");
		clipboard = GameObject.Find("Clipboard_title_v1");
		menuUI = GameObject.Find("MenuUI");
		settingsUI = GameObject.Find("SettingsUI");
		backbtn = GameObject.Find("Back_button");
		if (menuCam != null)
		{
			cam = menuCam.GetComponent<Animator>();
			clickselector = menuCam.GetComponent<MenuCamera>();
		}
		if (clipboard != null)
		{
			cb = clipboard.GetComponent<Animator>();
		}
		root.Q<Label>("Backbtn").RegisterCallback<ClickEvent>(ev => MenuToTitle());
	}


	public void SettingsToTitle(){
	    SettingsUI setUI = settingsUI.GetComponent<SettingsUI>();
	    if(setUI.IsShown()){
		setUI.Hide();
		backbtn.SetActive(false);
		if(cb != null){
		    cb.Play("Blurr_ClipBoard");
		}
		clickselector.titlescrn = true;
	    }
	}


	void MenuToTitle()
	{
		menuUI = GameObject.Find("MenuUI");
		settingsUI = GameObject.Find("SettingsUI");
		if (menuUI != null)
		{

			if (menuUI.activeSelf)
			{
				Debug.Log("Main menu active");
				menuUI.GetComponent<MainMenu>().Hide();
				backbtn.SetActive(false);
				if (cam != null)
				{
					cam.Play("Blurr_Main_menu");
				}
				clickselector.titlescrn = true;
			}
		}
		else if (settingsUI != null)
		{
		    SettingsToTitle();
			// if (settingsUI.activeSelf)
			// {
			// 	settingsUI.SetActive(false);
			// 	backbtn.SetActive(false);
			// 	if (cb != null)
			// 	{
			// 		cb.Play("Blurr_ClipBoard");
			// 	}
			// 	clickselector.titlescrn = true;
			// }
		}
		else if (backbtn.activeSelf)
		{
			Debug.Log("Credits menu active");
			backbtn.SetActive(false);
			if (cam != null)
			{
				cam.Play("Blurr_Credits");
			}
			clickselector.titlescrn = true;
		}
	}

    // Update is called once per frame
    void Update()
    {
		root = GetComponent<UIDocument>().rootVisualElement;
		back = root.Q<VisualElement>("Back");
		menuCam = GameObject.Find("MenuCamera");
		clipboard = GameObject.Find("Clipboard_title_v1");
		menuUI = GameObject.Find("MenuUI");
		settingsUI = GameObject.Find("SettingsUI");
		backbtn = GameObject.Find("Back_button");
		if (menuCam != null)
		{
			cam = menuCam.GetComponent<Animator>();
			clickselector = menuCam.GetComponent<MenuCamera>();
		}
		if (clipboard != null)
		{
			cb = clipboard.GetComponent<Animator>();
		}
		root.Q<Label>("Backbtn").RegisterCallback<ClickEvent>(ev => MenuToTitle());
	}
}
