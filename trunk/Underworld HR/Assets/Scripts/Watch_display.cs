using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements; //DON'T FORGET: using the ui toolkit


public class Watch_display : MonoBehaviour
{
    private Label stepsLabel;
    private Label energyLabel;

    private Label itemLabel;
    private VisualElement itemVisual;
    private Player p1;
    private Inventory invScript;
    
    
    private ItemInfo holdItem;

    public Texture2D itemImage;


    
    private int steps;
    public string heldItem;
    private string maxEnergy;
    private string currentEnergy;
 
    private int count = 1;


    private void start()
    {
    }

    private void Update()
    {    
    	p1 = GameObject.Find("Player").GetComponent<Player>();
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

		// goes and grabs the main visual element from the scene's UI document ("WatchUI")

        stepsLabel = rootVisualElement.Q<Label>("StepsText");
        energyLabel = rootVisualElement.Q<Label>("EnergyText");

        itemLabel = rootVisualElement.Q<Label>("ItemText");
        itemVisual = rootVisualElement.Q<VisualElement>("ItemImage");

		steps = p1.get_footstep_counter();
        //stepsLabel.text = $"{steps}/45"; REMOVED STEPS FROM DESIGN AND UI - Josh

        invScript = p1.GetComponent<Inventory>();
        heldItem = invScript.itemName;
        itemLabel.text = $"{heldItem}";
        if(count == 1)
        {
            itemLabel.transform.rotation *= Quaternion.Euler(0f, 0f, 8f);
            //itemVisual.transform.rotation *= Quaternion.Euler(0f, 0f, 8f);
            count++;
        }
        
        //itemVisual.text = invScript.markerColor;

        maxEnergy = p1.max_energy.ToString();
        currentEnergy = p1.energy.ToString();

        energyLabel.text = $"{currentEnergy} / {maxEnergy}";

        if(invScript.holdItem != null){
            itemImage = invScript.holdItem.uiImage;
        }
        if(invScript.holdItem == null){
            itemImage = null;
        }
        
        itemVisual.style.backgroundImage = itemImage;

        /*
        if(steps > 45){             //REPLACE 45 WITH VARIABLE FOR LEVEL PAR

            stepsLabel.style.color = new Color(255,0,0);
            //stepsLabel.style.backgroundColor = Color.green;
            //stepsLabel.style.backgroundImage = myImage;

        }
        if(steps < 45){
            stepsLabel.style.color = Color.blue;
        }*/
    }
}
