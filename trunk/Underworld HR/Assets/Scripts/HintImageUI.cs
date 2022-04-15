using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class HintImageUI : MonoBehaviour
{
    public GameObject employeeA, employeeB;
    private Button backButton;
    private VisualElement root;
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
	root = GetComponent<UIDocument>().rootVisualElement;
	root.style.display = DisplayStyle.None;
        backButton = root.Q<Button>("back-button");
	backButton.RegisterCallback<ClickEvent>(ev => ExitImage());
    }

    void OnTriggerEnter(Collider c){
	if(employeeA == null && employeeB == null){
	    player = c.GetComponent<Player>();
	    if(player != null){
		root.style.display = DisplayStyle.Flex;
		player.Freeze();
	    }
	}
    }

    void ExitImage(){
	if(player != null){
	    root.style.display = DisplayStyle.None;
	    player.Unfreeze();
	    player = null;
	}
    }
}
