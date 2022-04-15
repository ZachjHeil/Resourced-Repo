using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	private SaveData globalData;
	MenuBG bg;
    private VisualElement root, mainMenu, optionsMenu;

    private SettingsData settings, newSettings;

    // Start is called before the first frame update
    void Start()
    {
		if (GameObject.Find("Main_menu_Bg") != null)
		{
			bg = GameObject.Find("Main_menu_Bg").GetComponent<MenuBG>();
		}
        root = GetComponent<UIDocument>().rootVisualElement;
	mainMenu = root.Q<VisualElement>("main-menu");
	optionsMenu = root.Q<VisualElement>("options-menu");

	root.Q<Label>("new-game-button").RegisterCallback<ClickEvent>(ev => NewGame());
	root.Q<Label>("level-select-button").RegisterCallback<ClickEvent>(ev => LevelSelect());

	root.Q<Label>("options-button").RegisterCallback<ClickEvent>(ev => Options());
	root.Q<Label>("exit-button").RegisterCallback<ClickEvent>(ev => ExitGame());

	
	//optionsMenu.Q<Button>("options-back-button").RegisterCallback<ClickEvent>(ev => BackToMain());
	root.Q<Button>("level-back-button").RegisterCallback<ClickEvent>(ev => BackToMain());

	//InitLevelButton("new-game-button", "Prologue_video");

	InitLevelButton("prologue-button", "Prologue_video", LevelName.TUTORIAL);
	InitLevelButton("tutorial-button","Tutorial", LevelName.TUTORIAL);
	InitLevelButton("stage1-level1-button","Stage1_Level1", LevelName.STAGE1_LEVEL1);
	InitLevelButton("stage1-level2-button","Stage1_Level2", LevelName.STAGE1_LEVEL2);
	InitLevelButton("stage2-level1-button","Stage2_Level1", LevelName.STAGE2_LEVEL1);
	InitLevelButton("stage2-level2-button","Stage2_Level2", LevelName.STAGE2_LEVEL2);
	InitLevelButton("stage3-level1-button","Stage3_Level1", LevelName.STAGE3_LEVEL1);
	InitLevelButton("stage3-level2-button","Stage3_Level2", LevelName.STAGE3_LEVEL2);
	InitLevelButton("stage4-level1-button","Final_Stage", LevelName.FINAL_LEVEL);
	InitLevelButton("epilogue-button", "Epilogue_video", LevelName.TUTORIAL);

		//background changes
		if (bg != null)
		{
			root.Q<Label>("tutorial-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(0));
			root.Q<Label>("stage1-level1-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(1));
			root.Q<Label>("stage1-level2-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(1));
			root.Q<Label>("stage2-level1-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(2));
			root.Q<Label>("stage2-level2-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(2));
			root.Q<Label>("stage3-level1-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(3));
			root.Q<Label>("stage3-level2-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(3));
			root.Q<Label>("stage4-level1-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(3));
		}
		// VisualElement starHolder = new VisualElement();
		// VisualElement starTest = new VisualElement();

		// starHolder.AddToClassList("star-holder");
		// starTest.AddToClassList("star-off");

		// starHolder.Add(starTest);

		// root.Q<Label>("stage1-level1-button").Add(starHolder);

		//PopulateLevelStars();
		//Debug.Log(SaveSystem.savePath);
		LoadSaveData();

		// Settings Menu
		optionsMenu.Q<Button>("options-apply-button").RegisterCallback<ClickEvent>(ev => OptionsApplyChanges());
		optionsMenu.Q<Button>("options-back-button").RegisterCallback<ClickEvent>(ev => OptionsDiscardChanges());
		root.Q<SliderInt>("mastervol-slider").RegisterCallback<ChangeEvent<int>>(ev => OnVolumeChanged(0,ev.previousValue,ev.newValue));
		root.Q<SliderInt>("musicvol-slider").RegisterCallback<ChangeEvent<int>>(ev => OnVolumeChanged(1,ev.previousValue,ev.newValue));
		root.Q<SliderInt>("sfxvol-slider").RegisterCallback<ChangeEvent<int>>(ev => OnVolumeChanged(2,ev.previousValue,ev.newValue));
		root.Q<SliderInt>("voicevol-slider").RegisterCallback<ChangeEvent<int>>(ev => OnVolumeChanged(3,ev.previousValue,ev.newValue));

		root.Q<Label>("reset-settings-button").RegisterCallback<ClickEvent>(ev => OnDefaultSettingsButton());

	// screen1 = root.Q<VisualElement>("screen-1");
	FocusOnMenu("main-menu");

	if(SettingsSave.SaveExists()){
	    settings = SettingsSave.LoadSettings();
	    newSettings = settings.Clone();
	}else {
	    settings = new SettingsData();
	    newSettings = settings.Clone();
	    SettingsSave.SaveSettings(settings);
			
		}
		PopulateLevelStars(globalData);
	}

	void Update()
	{
		/*if (!gameObject.activeSelf)
		{
			if (GameObject.Find("Main_menu_Bg") != null)
			{
				bg = GameObject.Find("Main_menu_Bg").GetComponent<MenuBG>();
			}
			 root = GetComponent<UIDocument>().rootVisualElement;
			 mainMenu = root.Q<VisualElement>("main-menu");
			 optionsMenu = root.Q<VisualElement>("options-menu");

			 root.Q<Label>("new-game-button").RegisterCallback<ClickEvent>(ev => NewGame());
			 root.Q<Label>("level-select-button").RegisterCallback<ClickEvent>(ev => LevelSelect());

			 root.Q<Label>("options-button").RegisterCallback<ClickEvent>(ev => Options());
			 root.Q<Label>("exit-button").RegisterCallback<ClickEvent>(ev => ExitGame());
			 optionsMenu.Q<Label>("options-back-button").RegisterCallback<ClickEvent>(ev => BackToMain());
			 root.Q<Button>("level-back-button").RegisterCallback<ClickEvent>(ev => BackToMain());

			InitLevelButton("prologue-button", "Prologue_video", LevelName.TUTORIAL);
			InitLevelButton("tutorial-button", "Tutorial", LevelName.TUTORIAL);
			InitLevelButton("stage1-level1-button", "Stage1_Level1", LevelName.STAGE1_LEVEL1);
			InitLevelButton("stage1-level2-button", "Stage1_Level2", LevelName.STAGE1_LEVEL2);
			InitLevelButton("stage2-level1-button", "Stage2_Level1", LevelName.STAGE2_LEVEL1);
			InitLevelButton("stage2-level2-button", "Stage2_Level2", LevelName.STAGE2_LEVEL2);
			InitLevelButton("stage3-level1-button", "Stage3_Level1", LevelName.STAGE3_LEVEL1);
			InitLevelButton("stage3-level2-button", "Stage3_Level2", LevelName.STAGE3_LEVEL2);
			InitLevelButton("stage4-level1-button", "Final_Stage", LevelName.FINAL_LEVEL);

			//background changes
			if (bg != null)
			{
				root.Q<Label>("tutorial-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(0));
				root.Q<Label>("stage1-level1-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(1));
				root.Q<Label>("stage1-level2-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(1));
				root.Q<Label>("stage2-level1-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(2));
				root.Q<Label>("stage2-level2-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(2));
				root.Q<Label>("stage3-level1-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(3));
				root.Q<Label>("stage3-level2-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(3));
				root.Q<Label>("stage4-level1-button").RegisterCallback<MouseOverEvent>(ev => bg.backGroundChange(3));
			}
			 VisualElement starHolder = new VisualElement();
			 VisualElement starTest = new VisualElement();

			 starHolder.AddToClassList("star-holder");
			 starTest.AddToClassList("star-off");

			 starHolder.Add(starTest);

			 root.Q<Label>("stage1-level1-button").Add(starHolder);

			Debug.Log(SaveSystem.savePath);
			LoadSaveData();
			PopulateLevelStars(globalData);


		}
		// screen1 = root.Q<VisualElement>("screen-1");*/
	}
	public void Show()
	{
		LoadSaveData();
		if (root != null)
		{
			root.style.display = DisplayStyle.Flex;
		}
	}
        

	public void Hide()
	{
		FocusOnMenu("main-menu");
		if (root == null)
		{
			root = GetComponent<UIDocument>().rootVisualElement;
		}
		root.style.display = DisplayStyle.None;
	}

	private void LoadSaveData(){
	SaveData data;
	
	if(SaveSystem.SaveExists()){
	    Debug.Log("Found save game!");
	    data = SaveSystem.LoadGame();
			globalData = data;
	}else{
	    Debug.Log("No save game found, creating new save.");
	    // if there is no save yet, make create a blank one and use that
	    data = new SaveData();
	    SaveSystem.SaveGame(data);
		globalData = data;
	}
	
	InitLevelButton("continue-button",SaveSystem.GetLevelName(data.levelProgress), data.levelProgress);
	//PopulateLevelStars(data);
    }

    private void PopulateLevelStars(SaveData data){
        string[] levelButtons = new string[] {
	    "tutorial-button",
	    "stage1-level1-button",
	    "stage1-level2-button",
	    "stage2-level1-button",
	    "stage2-level2-button",
	    "stage3-level1-button",
	    "stage3-level2-button",
	    "stage4-level1-button",
	};

	// if(data.levelProgress < LevelName.STAGE1_LEVEL1)
	//     root.Q<Label>("egypt-label").text = "???";

	// if(data.levelProgress < LevelName.STAGE2_LEVEL1)
	//     root.Q<Label>("greek-label").text = "???";

	// if(data.levelProgress < LevelName.STAGE3_LEVEL1)
	//     root.Q<Label>("norse-label").text = "???";

	// if(data.levelProgress < LevelName.FINAL_LEVEL)
	//     root.Q<Label>("finale-label").text = "???";

	for(int i = 0; i < levelButtons.Length;i++){
	    bool levelLocked = false;
	    if(i > ((int)data.levelProgress)){
		root.Q<Label>(levelButtons[i]).text = "";
		levelLocked = true;
	    }
	    AddLevelStars(levelButtons[i],(int)data.levelStars[i],levelLocked);
	}
    }

    // adds 'starCount' stars to a level button (between 0-3)
    private void AddLevelStars(string elementName, int starCount,bool levelLocked){
	if(starCount > 3)
	    starCount = 3;
	if(starCount < 0)
	    starCount = 0;

	VisualElement levelButton = root.Q<Label>(elementName);

	// make star holder
	VisualElement holder = new VisualElement();
		
	holder.AddToClassList("star-holder");

	if(!levelLocked){
	    // create stars and add them to the holder
	    for(int i = 0; i < 3;i++){
		VisualElement star = new VisualElement();
		if(starCount > i){
		    star.AddToClassList("star-on");
		}else{
		    star.AddToClassList("star-off");
		}
		holder.Add(star);
	    }
	}else{
	    
	    VisualElement lockIcon = new VisualElement();
	    lockIcon.AddToClassList("lock");
	    holder.Add(lockIcon);
	    holder.ClearClassList();
	    holder.AddToClassList("lock-holder");
		}

	levelButton.Add(holder);
    }

    private void InitLevelButton(string elementName, string levelName, LevelName levelNum){
		
	root.Q<Label>(elementName).RegisterCallback<ClickEvent>(ev => LoadLevel(levelName, levelNum));
    }

    private void LoadLevel(string levelName, LevelName levelNum){
		if (globalData.levelProgress >= levelNum)
		{
			SceneManager.LoadScene(levelName);
		}
		else
		{
			return;
		}
    }

    private void FocusOnMenu(string menuName){
		if (root != null)
		{
			for (int i = 0; i < root.childCount; i++)
			{
				root[i].style.display = DisplayStyle.None;
			}

			root.Q<VisualElement>(menuName).style.display = DisplayStyle.Flex;
		}
    }

    private void ShowOptionsMenu(){
	mainMenu.style.display = DisplayStyle.None;
	optionsMenu.style.display = DisplayStyle.Flex;
	
	
    }

    private void ShowMainMenu(){
	mainMenu.style.display = DisplayStyle.Flex;
	optionsMenu.style.display = DisplayStyle.None;
    }

    private void NewGame(){
	Debug.Log("New Game Pressed!");
	SaveSystem.SaveGame(new SaveData());
	SceneManager.LoadScene("Tutorial");
    }

    private void ContinueGame(){
	
    }

    private void LevelSelect(){
	Debug.Log("Level Select Pressed!");
	FocusOnMenu("level-menu");
    }

    private void OptionsApplyChanges(){
	settings = newSettings.Clone();
	SettingsSave.SaveSettings(settings);
	FocusOnMenu("main-menu");
	Debug.Log("APPLIED CHANGES");
    }

    private void OptionsDiscardChanges(){
	newSettings = settings.Clone();
	FocusOnMenu("main-menu");
    }

    private void OnDefaultSettingsButton(){
	settings = new SettingsData();
	newSettings = settings.Clone();
	SettingsSave.SaveSettings(settings);
	
	root.Q<SliderInt>("mastervol-slider").value = settings.masterVolume;
	root.Q<SliderInt>("musicvol-slider").value = settings.musicVolume;
	root.Q<SliderInt>("sfxvol-slider").value = settings.sfxVolume;
	root.Q<SliderInt>("voicevol-slider").value = settings.voiceVolume;
    }

    private void Options(){
	FocusOnMenu("options-menu");
	root.Q<SliderInt>("mastervol-slider").value = settings.masterVolume;
	root.Q<SliderInt>("musicvol-slider").value = settings.musicVolume;
	root.Q<SliderInt>("sfxvol-slider").value = settings.sfxVolume;
	root.Q<SliderInt>("voicevol-slider").value = settings.voiceVolume;
    }

    // volumeIndex: 0 = master, 1 = music, 2 = sfx, 3 = voice
    private void OnVolumeChanged(int volumeIndex, int oldVal, int newVal){
	switch(volumeIndex){
	case 0:
	    newSettings.masterVolume = newVal;
	    break;
	case 1:
	    newSettings.musicVolume = newVal;
	    break;
	case 2:
	    newSettings.sfxVolume = newVal;
	    break;
	case 3:
	    newSettings.voiceVolume = newVal;
	    break;
	}
    }

    private void BackToMain(){
	Debug.Log("Back Pressed!");
	FocusOnMenu("main-menu");
    }

    private void ExitGame(){
	Debug.Log("Exit Pressed!");
	Application.Quit();
    }
}
