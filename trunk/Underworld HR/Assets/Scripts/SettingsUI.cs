using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsUI : MonoBehaviour
{
    private VisualElement root;

    private SettingsData settings, newSettings;

    private Back_btn_main_menu backBtn;

    void Awake(){
	
    }
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
	// root.Q<Button>("options-apply-button").RegisterCallback<ClickEvent>(ev => ApplySettings() );
	// root.Q<Button>("options-back-button").RegisterCallback<ClickEvent>(ev => DiscardSettings() );
	GameObject back = GameObject.Find("Back_button");

	root.Q<SliderInt>("mastervol-slider").RegisterCallback<ChangeEvent<int>>(ev => OnVolumeChanged(0,ev.previousValue, ev.newValue));
	root.Q<SliderInt>("musicvol-slider").RegisterCallback<ChangeEvent<int>>(ev => OnVolumeChanged(1,ev.previousValue, ev.newValue));
	root.Q<SliderInt>("sfxvol-slider").RegisterCallback<ChangeEvent<int>>(ev => OnVolumeChanged(2,ev.previousValue, ev.newValue));
	root.Q<SliderInt>("voicevol-slider").RegisterCallback<ChangeEvent<int>>(ev => OnVolumeChanged(3,ev.previousValue, ev.newValue));


	// setup Options Menu buttons
	FocusOnMenu("options-screen");
	//root.Q<Label>("options-button").RegisterCallback<ClickEvent>(ev => FocusOnMenu("options-screen"));
	root.Q<Label>("options-back-button").RegisterCallback<ClickEvent>(ev => ApplySettings());

	root.Q<Label>("options-restore-button").RegisterCallback<ClickEvent>(ev => OnDefaultSettingsButton());

	// Audio Settings Menu
	root.Q<Label>("audio-button").RegisterCallback<ClickEvent>(ev => OpenAudioMenu());
	root.Q<Label>("audio-apply-button").RegisterCallback<ClickEvent>(ev => OnApplyButton("options-screen"));
	root.Q<Label>("audio-cancel-button").RegisterCallback<ClickEvent>(ev => OnCancelButton("options-screen"));

	// Gameplay Settings Menu
	//root.Q<Label>("gameplay-button").RegisterCallback<ClickEvent>(ev => OpenGameplayMenu());
	root.Q<Label>("gameplay-apply-button").RegisterCallback<ClickEvent>(ev => OnApplyButton("options-screen"));
	root.Q<Label>("gameplay-cancel-button").RegisterCallback<ClickEvent>(ev => OnCancelButton("options-screen"));

	//root.Q<Toggle>("gameplay-grid-toggle").RegisterValueChangedCallback(OnGridChanged);

	// Graphics Settings Menu
	root.Q<Label>("graphics-button").RegisterCallback<ClickEvent>(ev => OpenGraphicsMenu());
	VisualElement graphics = root.Q<VisualElement>("graphics-screen");

	root.Q<Toggle>("graphics-fullscreen-toggle").RegisterValueChangedCallback(OnFullscreenChanged);
	root.Q<Label>("graphics-apply-button").RegisterCallback<ClickEvent>(ev => OnApplyButton("options-screen"));
	root.Q<Label>("graphics-cancel-button").RegisterCallback<ClickEvent>(ev => OnCancelButton("options-screen"));

	


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
	
	if(back != null){
	    backBtn = back.GetComponent<Back_btn_main_menu>();
	}
	
	Hide();
    }

    void Update(){
	if(root == null){
	    root = GetComponent<UIDocument>().rootVisualElement;
	    // root.Q<Button>("options-apply-button").RegisterCallback<ClickEvent>(ev => ApplySettings() );
	    // root.Q<Button>("options-back-button").RegisterCallback<ClickEvent>(ev => DiscardSettings() );
	    GameObject back = GameObject.Find("Back_button");

	    root.Q<SliderInt>("mastervol-slider").RegisterCallback<ChangeEvent<int>>(ev => OnVolumeChanged(0,ev.previousValue, ev.newValue));
	    root.Q<SliderInt>("musicvol-slider").RegisterCallback<ChangeEvent<int>>(ev => OnVolumeChanged(1,ev.previousValue, ev.newValue));
	    root.Q<SliderInt>("sfxvol-slider").RegisterCallback<ChangeEvent<int>>(ev => OnVolumeChanged(2,ev.previousValue, ev.newValue));
	    root.Q<SliderInt>("voicevol-slider").RegisterCallback<ChangeEvent<int>>(ev => OnVolumeChanged(3,ev.previousValue, ev.newValue));
	
	    if(back != null){
		backBtn = back.GetComponent<Back_btn_main_menu>();
	    }
	}
    }

    private void ApplyChanges(){
	settings = newSettings.Clone();
	SettingsSave.SaveSettings(settings);
	//manager.ApplySettings(settings);
    }

    private void DiscardChanges(){
	newSettings = settings.Clone();
    }

    public void Show(){
	root.style.display = DisplayStyle.Flex;
        if(SettingsSave.SaveExists()){
	    settings = SettingsSave.LoadSettings();
	    //newSettings = settings.Clone();
	}else{
	    settings = new SettingsData();
	    //newSettings = settings.Clone();
	    SettingsSave.SaveSettings(settings);
	}
	root.Q<SliderInt>("mastervol-slider").value = settings.masterVolume;
	root.Q<SliderInt>("musicvol-slider").value = settings.musicVolume;
	root.Q<SliderInt>("sfxvol-slider").value = settings.sfxVolume;
	root.Q<SliderInt>("voicevol-slider").value = settings.voiceVolume;
    }

    public void Hide(){
	if(root == null){
	    root = GetComponent<UIDocument>().rootVisualElement;
	}
	root.style.display = DisplayStyle.None;
    }

    public bool IsShown(){
	return root.style.display == DisplayStyle.Flex;
    }

    private void ApplySettings(){
	Debug.Log("Applying Settings!");
	//SettingsSave.SaveSettings(settings);
	backBtn.SettingsToTitle();
    }

    private void DiscardSettings(){
	Debug.Log("Discard Settings!");
	backBtn.SettingsToTitle();
	//backBtn.MenuToTitle();
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

    // private void OnDefaultSettingsButton(){
    // 	settings = new SettingsData();
    // 	SettingsSave.SaveSettings(settings);
	
    // 	root.Q<SliderInt>("mastervol-slider").value = settings.masterVolume;
    // 	root.Q<SliderInt>("musicvol-slider").value = settings.musicVolume;
    // 	root.Q<SliderInt>("sfxvol-slider").value = settings.sfxVolume;
    // 	root.Q<SliderInt>("voicevol-slider").value = settings.voiceVolume;
    // }

    // private void OnMusicVolumeChanged(int oldVal,int newVal){
    // 	newSettings.musicVolume = newVal;
    // }

    // private void OnSfxVolumeChanged(int oldVal, int newVal){
    // 	newSettings.sfxVolume = newVal;
    // }

    // private void OnVoiceVolumeChanged(int oldVal, int newVal){
    // 	newSettings.voiceVolume = newVal;
    // }


    // ========== Gameplay Settings ========== //

    // private void OpenGameplayMenu(){
    // 	FocusOnMenu("gameplay-screen");
    // 	root.Q<Toggle>("gameplay-grid-toggle").value = settings.showGrid;
    // }

    // // Show grid toggle changed
    // private void OnGridChanged(ChangeEvent<bool> ev) {
    // 	newSettings.showGrid = ev.newValue;
    // }


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
	//manager.ApplySettings(settings);
	}

    private void FocusOnMenu(string menuName){
	VisualElement menus = root.Q<VisualElement>("menu-screens");
	for(int i = 0; i < menus.childCount;i++){
	    menus[i].style.display = DisplayStyle.None;
	}
	menus.Q<VisualElement>(menuName).style.display = DisplayStyle.Flex;
    }

    private void OpenAudioMenu(){
	FocusOnMenu("audio-screen");
	root.Q<SliderInt>("mastervol-slider").value = settings.masterVolume;
	root.Q<SliderInt>("musicvol-slider").value = settings.musicVolume;
	root.Q<SliderInt>("sfxvol-slider").value = settings.sfxVolume;
	root.Q<SliderInt>("voicevol-slider").value = settings.voiceVolume;
    }

    private void OnApplyButton(string prevMenu){
	ApplyChanges();
	FocusOnMenu(prevMenu);
    }

    private void OnCancelButton(string prevMenu){
	DiscardChanges();
	FocusOnMenu(prevMenu);
    }
    
}
