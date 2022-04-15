using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Updates the current level with settings from the option menu
public class SettingsManager : MonoBehaviour
{
    void Start()
    {
        
    }

    // apply settings data to the current scene
    public void ApplySettings(SettingsData settings){
	// Graphics
	//SetFullscreen(settings.fullscreen);
	//SetResolutionFromIndex(settings.resolutionIndex,settings.fullscreen);
	
	if(GameObject.Find("AudioManager") != null){
	    AudioManager am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	    am.SetMasterVolume(settings.masterVolume/100f);
	    am.SetSFXVolume(settings.sfxVolume/100f);
	    am.SetMusicVolume(settings.musicVolume/100f);
	    am.SetVoiceVolume(settings.voiceVolume/100f);
	    SetFullscreen(settings.fullscreen);
	}
    }

    public void SetFullscreen(bool fullscreen){
	if(Screen.fullScreen != fullscreen){
	    Screen.fullScreen = fullscreen;
	}
    }

    // public void SetResolutionFromIndex(int index,bool fullscreen){
    // 	ResolutionList list = new ResolutionList();
    // 	ResValue res = list.Get(index);
    // 	Screen.SetResolution(res.width,res.height,Screen.fullScreen);
    // }

    // public void SetResolution(int width, int height){
    // 	Screen.SetResolution(width,height,Screen.fullScreen);
    // }
}

public struct ResValue {
    public string name;
    public int width;
    public int height;

    public ResValue(string n, int w, int h){
	name = n;
	width = w;
	height = h;
    }

    public ResValue(int w, int h){
	width = w;
	height = h;
	name = w+" x "+h;
    }
};

public class ResolutionList {
    private List<ResValue> resOptions = new List<ResValue>();

    public ResolutionList(){
	this.Add( 640, 480);
	this.Add( 720, 400);
	this.Add( 800, 600);
	this.Add( 832, 624);
	this.Add( 1024, 768);
	this.Add( 1152, 864);
	this.Add( 1152, 872);
	this.Add( 1280, 720);
	this.Add( 1280, 800);
	this.Add( 1280, 1024);
	this.Add( 1366, 768);
	this.Add( 1400, 1050);
	this.Add( 1440, 900);
	Add(1600,900);
	Add(1680,1050);
	Add(1920,1080);
    }

    public string[] AsStringArray(){
	string[] arr = new string[resOptions.Count];
	int i = 0;
	foreach(ResValue r in resOptions){
	    arr[i] = r.name;
	    i++;
	}
	return arr;
    }

    public void Add(int width, int height){
	resOptions.Add(new ResValue(width,height));
    }

    public ResValue Get(int index){
	return resOptions[index];
    }
}
