using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


// Functions used to save and load option menu settings
public static class SettingsSave {
    // where the settings file gets saved/loaded from
    public static string savePath = Application.persistentDataPath + "/settings.dat";

    // saves 'data' to the settings file
    public static void SaveSettings(SettingsData data){
	BinaryFormatter bf = new BinaryFormatter();

	FileStream stream = new FileStream(SettingsSave.savePath, FileMode.Create);

	bf.Serialize(stream, data);

	stream.Close();
    }

    // checks to see if the settings file exists
    public static bool SaveExists(){
	return File.Exists(SettingsSave.savePath);
    }

    // loads and returns data from the settings file
    public static SettingsData LoadSettings(){
	if(SettingsSave.SaveExists()){
	    BinaryFormatter bf = new BinaryFormatter();
	    FileStream stream = new FileStream(SettingsSave.savePath, FileMode.Open);

	    
	    SettingsData data = (SettingsData)bf.Deserialize(stream);
	    stream.Close();
	    return data;
	} else {
	    Debug.LogError("Settings file not found in " + SettingsSave.savePath);
	    return null;
	}
    }
}


[System.Serializable]
public class SettingsData {
    
    // Audio Settings
    public int masterVolume; // volume of all sounds in game
    public int musicVolume;
    public int sfxVolume;
    public int voiceVolume;

    // Gameplay Settings
    public bool showGrid; // show a visual grid during the game

    // Graphics Settings
    public bool fullscreen; // the game is in fullscreen (as opposed to windowed)
    //public int resolutionIndex; // index of chosen resolution in ResolutionList class
    

    // Default settings
    public SettingsData(){
	//masterVolume = 1.0f;
	masterVolume = 50;
	musicVolume = 50;
	sfxVolume = 50;
	voiceVolume = 50;
	
	showGrid = true;
	fullscreen = true;
	//resolutionIndex = 15;
    }

    // returns a copy of this object
    public SettingsData Clone(){
	return (SettingsData)this.MemberwiseClone();
    }
    
}
