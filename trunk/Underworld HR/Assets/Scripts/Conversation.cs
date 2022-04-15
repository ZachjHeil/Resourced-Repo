using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public enum SpeakerLetter {
    A = 0,
    B = 1
};

[System.Serializable]
public class Conversation {
    //public DialogueCharacter[] speakers;
    //public DialogueCharacter speakerA,speakerB;
    public Color portraitRadial;
    public DialogueData[] lines;
    public Texture2D agnesPortrait;
    public Texture2D hathorPortrait;
    public Texture2D anubisPortrait;
    public Texture2D dioPortrait;
    public Texture2D hypnosPortrait;
    public Texture2D lokiPortrait;
    public Texture2D idunPortrait;
    public Texture2D enemyPortrait;

}

// public struct SpeechData {
//     public string speakerName;
//     public Texture2D speakerImage;
//     public string speakerDialogue;
    

//     public SpeechData(string name, Texture2D img, string dialogue){
// 	speakerName = name;
// 	speakerImage = img;
// 	speakerDialogue = dialogue;
//     }
// }

[System.Serializable]
public class ConversationEvent : UnityEvent<Conversation> {
    
}

[System.Serializable]
public class DialogueData {
    [InspectorName("Character Name")]
    public string name;
    
    public bool agnesSpeaking;

    [TextArea(3,10)]
    public string line;

    //public Texture2D dialogueImage;
    public Texture2D leftPortrait;
    public Texture2D rightPortrait;
    public AudioClip voiceAudio;

    /* Josh's expirimental code
    public Texture2D agnesPortrait;
    public Texture2D hathorPortrait;
    public Texture2D anubisPortrait;
    public Texture2D DioPortrait;
    public Texture2D HypnoPortrait;
    public Texture2D LokiPortrait;
    public Texture2D IdunPortrait;

    */
}
