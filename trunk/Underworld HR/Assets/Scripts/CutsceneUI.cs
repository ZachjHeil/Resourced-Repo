using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class CutsceneUI : MonoBehaviour
{
    private VisualElement root;
    public string scene_name;
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
	root.Q<Button>("skip-button").RegisterCallback<ClickEvent>(ev => SkipIntro());
    }

    private void SkipIntro(){
	SceneManager.LoadScene(scene_name);
    }

    
}
