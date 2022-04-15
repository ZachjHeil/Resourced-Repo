using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This would be attached to the player gameobject
public class Inventory : MonoBehaviour
{

    // position for a temporary inventory ui
    public Vector2 uiPos;

    // the item that this inventory is holding
    public ItemInfo holdItem; // begins null

    // returns true if we are holding an item
    public bool IsFull {
	get { return !(holdItem == null); }
    }
    
    public string itemName; //added by Josh

    void Start()
    {
        
    }

    
    void Update()
    {
        itemName = IsFull ? holdItem.name : "Empty";

    }

    // finds an empty tile to drop the item on
    // starts with the direction the player is facing and works clockwise
    private Vector3 FindEmptyDropSpace(){
	Vector3[] dirs = {
	    transform.forward,
	    transform.right,
	    -transform.forward,
	    -transform.right
	};

	for(int i = 0; i < dirs.Length;i++){
	    if(IsDirectionFree(dirs[i])){
		return transform.position + dirs[i];
	    }
	}

	// no free spaces available (shouldn't happen???)
	return Vector3.zero;
    }

    // casts a ray in a direction (dir)
    private bool IsDirectionFree(Vector3 dir){
	RaycastHit hit;

	if(Physics.Raycast(transform.position + new Vector3(0,0.25f,0),dir,out hit, 1.25f)){
	    string hitTag = hit.collider.tag;
	    if(hitTag == "Wall" || hitTag == "Enemy" || hitTag == "Item" || hitTag=="EnergyPickup" || hitTag=="KillTrap")
		return false;
	}
	return true;
    }


    // Puts the item in this inventory
    public void PickUp(Item item){
	holdItem = item.GetItemInfo();
	
	// is there a better way to do this?
	item.Destroy(); // custom function inside Item
	FindObjectOfType<AudioManager>().Play("Paper");
	
    }

    // drops the item into this world
    public void DropItem(){
	if(IsFull){
	    Vector3 dropSpot = FindEmptyDropSpace();
	    // spawn item (from holdItem) into the world
	
	    // Which tile will this item be dropped on [Design Team Question]
	    Item item = GameObject.Instantiate(holdItem.dropItemPrefab,dropSpot,Quaternion.identity).GetComponent<Item>();
	    item.LoadInfo(holdItem);
	    holdItem = null;
		
	}
    }

    // called by an enemy when they take the item
	public void Item_Use()
	{
		holdItem = null;
		
	}

	// checks to see whether holdItem is meant for 'enemy'
	public bool IsItemForEnemy(Enemy enemy){
	    // if the inventory is empty, there's nothing to compare
	    if(!IsFull)
		return false;

	    // return true if the item enemy ref 
	    return holdItem.enemyRef == enemy;
	}

	
    // a TEMPORARY ui solution
  //  void OnGUI(){
	
	// make this a bit easier
	//float x = uiPos.x;
	//float y = uiPos.y;

	
	// Display the item's name if we have an item, otherwise display 'Empty'
	//itemName = IsFull ? holdItem.name : "Empty";


	
//	GUI.color = Color.white;
//	GUI.Box(new Rect(x,y,100,50),"Inventory");

	// temporary: if the inventory is carrying an item, make the text the item color
	//if(IsFull){
	  //  GUI.color = holdItem.markerColor;
	//}
//	GUI.Label(new Rect(x + 15,y + 20,80,30),itemName);
   // }
}//}
