using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterMood {
    NEUTRAL,
    HAPPY,
    ANGRY
};

[CreateAssetMenu]
public class DialogueCharacter : ScriptableObject
{
    public new string name;
    public Texture2D imageNeutral;
    public Texture2D imageHappy;
    public Texture2D imageAngry;
}
