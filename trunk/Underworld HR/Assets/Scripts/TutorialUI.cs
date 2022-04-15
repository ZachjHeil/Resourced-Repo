using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialUI : MonoBehaviour
{
    public VisualElement root;
    public Label msgLabel;
    private Player p;
    

    
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
	msgLabel = root.Q<Label>("tutorial-message");
    //root.Q<Button>("Next").RegisterCallback<ClickEvent>(ev => Exit());
    //p.Freeze();


	// hide the UI
	root.style.display = DisplayStyle.None;
    
    }

    private void Exit(){
	//SceneManager.LoadScene("Tutorial");
    //player.Unfreeze();
	root.style.display = DisplayStyle.None;
    //p.Unfreeze();
    }

    public void ShowMessage(TutorialMessage message){
	root.style.display = DisplayStyle.Flex;
	msgLabel.text = message.message;
    root.Q<Button>("Next").RegisterCallback<ClickEvent>(ev => message.OnExit());
    }

    public void HideMessage(){
	root.style.display = DisplayStyle.None;
    }

  
}
