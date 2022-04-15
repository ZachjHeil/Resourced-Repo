using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private AudioSource clipSource;

    private float masterVolume, sfxVolume, musicVolume, voiceVolume;
     

    void Awake()
    {
	masterVolume = 1f;
	sfxVolume = 1.0f;
	musicVolume = 1.0f;
	voiceVolume = 1.0f;
	
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
        }
        clipSource = gameObject.AddComponent<AudioSource>();

    }

   /** void Start () {
        Play("Theme"); 
    }**/

    public void Play (string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
	if(s != null){
	    s.source.volume = s.volume * sfxVolume * masterVolume;
	    s.source.Play();
	}else{
	    Debug.LogWarning("AudioManager could not find sound '"+name+"'. Make sure it has been added to the AudioManager.");
	}
        
    }

    // public float GetVolumeOfType(SoundType type){
    // 	switch(type){
    // 	case SoundType.SFX:
    // 	    return sfxVolume;
    // 	case SoundType.MUSIC:
    // 	    return musicVolume;
    // 	case SoundType.VOICE:
    // 	    return voiceVolume;
    // 	}
    // 	return sfxVolume;
    // }

    public void PlaySound(AudioClip clip){
        clipSource.PlayOneShot(clip,sfxVolume*masterVolume);
    }

    public void SetMasterVolume(float value){
	masterVolume = value;
	
    }

    public void SetSFXVolume(float value){
	sfxVolume = Mathf.Clamp(value,0,1);
    }

    public void SetMusicVolume(float value){
	musicVolume = Mathf.Clamp(value,0,1);
	GameObject plyCam = GameObject.Find("Player Camera");
	if(plyCam != null){
	    plyCam.GetComponent<AudioSource>().volume = musicVolume*masterVolume;
	}
    }

    public void SetVoiceVolume(float value){
	voiceVolume = Mathf.Clamp(value,0,1);

        GameObject nar = GameObject.Find("NarrativeUI");
	if(nar != null){
	    nar.GetComponent<AudioSource>().volume = voiceVolume*masterVolume;
	}
	
    }

}
