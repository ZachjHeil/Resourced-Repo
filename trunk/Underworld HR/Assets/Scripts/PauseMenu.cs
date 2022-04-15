using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private VisualElement root,warningBox;
    private Player p1;
    private bool isOpen = false;

    private SettingsData newSettings; // unapplied changes
    private SettingsData settings; // applied changes

    private SettingsManager manager; // script that puts these settings into effect

	public GameObject gameOverUI;

	public ExitGate exit;
	public bool exitcheck;

	// Start is called before the first frame update
	void Start()
    {
		exit = GameObject.Find("Exit Gate").GetComponent<ExitGate>();
		gameOverUI = GameObject.Find("GameOver");
	manager = GetComponent<SettingsManager>();
	//Debug.Log("STARTING PAUSE MENU!!!!");
        root = GetComponent<UIDocument>().rootVisualElement;
	warningBox = root.Q<VisualElement>("warning-dialogue");

	// hide the exit warning
	warningBox.style.display = DisplayStyle.None;
	HidePauseMenu();

	root.Q<Label>("exit-button").RegisterCallback<ClickEvent>(ev => ExitGame());
	InitLevelButton("menu-button","Main_menu_V2");
	root.Q<Label>("resume-button").RegisterCallback<ClickEvent>(ev => HidePauseMenu());
	InitLevelButton("restart-button",SceneManager.GetActiveScene().name);

	// setup Options Menu buttons
	FocusOnMenu("pause-screen");
	root.Q<Label>("options-button").RegisterCallback<ClickEvent>(ev => FocusOnMenu("options-screen"));
	root.Q<Label>("options-back-button").RegisterCallback<ClickEvent>(ev => FocusOnMenu("pause-screen"));

	root.Q<Label>("options-restore-button").RegisterCallback<ClickEvent>(ev => OnDefaultSettingsButton());

	// Audio Settings Menu
	root.Q<Label>("audio-button").RegisterCallback<ClickEvent>(ev => OpenAudioMenu());
	root.Q<Label>("audio-apply-button").RegisterCallback<ClickEvent>(ev => OnApplyButton("options-screen"));
	root.Q<Label>("audio-cancel-button").RegisterCallback<ClickEvent>(ev => OnCancelButton("options-screen"));
	
	root.Q<SliderInt>("mastervol-slider").RegisterValueChangedCallback(ev => OnMasterVolumeChanged(ev.previousValue, ev.newValue));
	root.Q<SliderInt>("musicvol-slider").RegisterCallback<ChangeEvent<int>>(ev => OnMusicVolumeChanged(ev.previousValue, ev.newValue));
	root.Q<SliderInt>("sfxvol-slider").RegisterCallback<ChangeEvent<int>>(ev => OnSfxVolumeChanged(ev.previousValue, ev.newValue));
	root.Q<SliderInt>("voicevol-slider").RegisterCallback<ChangeEvent<int>>(ev => OnVoiceVolumeChanged(ev.previousValue, ev.newValue));

	// Gameplay Settings Menu
	root.Q<Label>("gameplay-button").RegisterCallback<ClickEvent>(ev => OpenGameplayMenu());
	root.Q<Label>("gameplay-apply-button").RegisterCallback<ClickEvent>(ev => OnApplyButton("options-screen"));
	root.Q<Label>("gameplay-cancel-button").RegisterCallback<ClickEvent>(ev => OnCancelButton("options-screen"));

	root.Q<Toggle>("gameplay-grid-toggle").RegisterValueChangedCallback(OnGridChanged);

	// Graphics Settings Menu
	root.Q<Label>("graphics-button").RegisterCallback<ClickEvent>(ev => OpenGraphicsMenu());
	VisualElement graphics = root.Q<VisualElement>("graphics-screen");

	root.Q<Toggle>("graphics-fullscreen-toggle").RegisterValueChangedCallback(OnFullscreenChanged);
	root.Q<Label>("graphics-apply-button").RegisterCallback<ClickEvent>(ev => OnApplyButton("options-screen"));
	root.Q<Label>("graphics-cancel-button").RegisterCallback<ClickEvent>(ev => OnCancelButton("options-screen"));
	
	// ResolutionList resList = new ResolutionList();
	// ShiftMenu resShift = new ShiftMenu("graphics-resolution",resList.AsStringArray());
	// root.Q<VisualElement>("GraphicsButtons").Add(resShift);
	// resShift.PlaceBehind(root.Q<Toggle>("graphics-fullscreen-toggle"));
	// resShift.RegisterCallback<ChangeEvent<int>>( ev => OnResolutionChanged(ev.previousValue, ev.newValue));

	// DropdownMenu resMenu = new DropdownMenu();
	// graphics.Add(resMenu);

	// check if a settings file already exists
	if(SettingsSave.SaveExists()){
	    //Debug.Log("SAVE EXISTS!!!");
	    settings = SettingsSave.LoadSettings();
	    //Debug.Log(settings == null);
	    newSettings = settings.Clone();
	}else{
	    //Debug.Log("NO SAVE :(");
	    // create new settings file with default settings
	    settings = new SettingsData();
	    newSettings = settings.Clone();
	    SettingsSave.SaveSettings(settings);
	    //Debug.Log("Settings: "+settings);
	    //Debug.Log("New Settings " + newSettings);
	}

	manager.ApplySettings(settings);
    }
	private void Update()
	{
		exitcheck = exit.levelComplete;
	}

	// is the menu open and visible to the user?
	public bool IsOpen(){
	return isOpen;
    }

    public void ShowPauseMenu(){
	root.style.display = DisplayStyle.Flex;
	isOpen = true;
    }

    public void HidePauseMenu(){
	root.style.display = DisplayStyle.None;
		p1 = GameObject.Find("Player").GetComponent<Player>();
		exit = GameObject.Find("Exit Gate").GetComponent<ExitGate>();
		exitcheck = exit.levelComplete;
		if (exitcheck)
		{
			Debug.Log("entering freeze condition");
			p1.Freeze();
		}
		else
		{
			Debug.Log("entering unfreeze condition");
			p1.Unfreeze();
		}
		//Debug.Log("Hiding Pause Menu");
		isOpen = false;
    }

    private void ExitGame(){
	Application.Quit();
    }

    private void InitLevelButton(string elementName, string levelName){
	root.Q<Label>(elementName).RegisterCallback<ClickEvent>(ev => LoadLevel(levelName));
    }

    private void LoadLevel(string levelName){
	SceneManager.LoadScene(levelName);
    }

    private void OnApplyButton(string prevMenu){
	ApplyChanges();
	FocusOnMenu(prevMenu);
    }

    private void OnCancelButton(string prevMenu){
	DiscardChanges();
	FocusOnMenu(prevMenu);
    }

    private void ApplyChanges(){
	settings = newSettings.Clone();
	SettingsSave.SaveSettings(settings);
	manager.ApplySettings(settings);
    }

    private void DiscardChanges(){
	newSettings = settings.Clone();
    }

    private void FocusOnMenu(string menuName){
	VisualElement menus = root.Q<VisualElement>("menu-screens");
	for(int i = 0; i < menus.childCount;i++){
	    menus[i].style.display = DisplayStyle.None;
	}
	menus.Q<VisualElement>(menuName).style.display = DisplayStyle.Flex;
    }

    // ========== Audio Settings ========== //

    private void OpenAudioMenu(){
	FocusOnMenu("audio-screen");
	root.Q<SliderInt>("mastervol-slider").value = settings.masterVolume;
	root.Q<SliderInt>("musicvol-slider").value = settings.musicVolume;
	root.Q<SliderInt>("sfxvol-slider").value = settings.sfxVolume;
	root.Q<SliderInt>("voicevol-slider").value = settings.voiceVolume;
    }

    private void OnMasterVolumeChanged(int oldVal, int newVal){
	//Debug.Log("Volume: " + newVal.ToString());
	
	newSettings.masterVolume = newVal;
    }

    private void OnMusicVolumeChanged(int oldVal,int newVal){
	newSettings.musicVolume = newVal;
    }

    private void OnSfxVolumeChanged(int oldVal, int newVal){
	newSettings.sfxVolume = newVal;
    }

    private void OnVoiceVolumeChanged(int oldVal, int newVal){
	newSettings.voiceVolume = newVal;
    }


    // ========== Gameplay Settings ========== //

    private void OpenGameplayMenu(){
	FocusOnMenu("gameplay-screen");
	root.Q<Toggle>("gameplay-grid-toggle").value = settings.showGrid;
    }

    // Show grid toggle changed
    private void OnGridChanged(ChangeEvent<bool> ev) {
	newSettings.showGrid = ev.newValue;
    }


    // ========== Graphics Settings ========== //

    private void OpenGraphicsMenu(){
	FocusOnMenu("graphics-screen");
	root.Q<Toggle>("graphics-fullscreen-toggle").value = settings.fullscreen;
	//root.Q<ShiftMenu>("graphics-resolution").SetIndex(settings.resolutionIndex);
    }

    // private void OnResolutionChanged(int previousValue, int newValue ){
    // 	newSettings.resolutionIndex = newValue;
    // }

    private void OnFullscreenChanged(ChangeEvent<bool> ev){
	newSettings.fullscreen = ev.newValue;
    }

    

    private void OnDefaultSettingsButton(){
	// give warning
	// warningBox.style.display = DisplayStyle.Flex;
	// warningBox.Q<Label>("warning-title").text = "Restoring Default Settings";
	// warningBox.Q<Label>("warning-message").text = "Are you sure?";


	settings = new SettingsData();
	newSettings = settings.Clone();
	SettingsSave.SaveSettings(settings);
	manager.ApplySettings(settings);
	}
    

    
}
