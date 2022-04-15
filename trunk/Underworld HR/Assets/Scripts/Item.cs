using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string name;

	private Player player;

    // Temporary: used to see which item goes to which item;
    public Material markerMaterial;

    // The enemy that this item is for
    public Enemy enemyRef;

    //The original item Prefab to spawn
    public GameObject itemPrefab;

    
    public Texture2D uiImage;

	public GameObject dropItemPrefab;
    // Start is called before the first frame update

	public AudioClip clip;

	public AudioSource source;
    


    void Start()
    {

	// Temporary
	if(markerMaterial != null){ // just in case markerMaterial is not assigned in the inspector
	    // set this and the enemy's material to markerMaterial
	    GetComponent<Renderer>().material = markerMaterial;
	    if(enemyRef != null)
		enemyRef.GetComponent<Renderer>().material = markerMaterial;
	}
	
    }

    public void LoadInfo(ItemInfo info){
		
		this.name = info.name;
		if (info.markerMaterial != null)
		{
			markerMaterial = info.markerMaterial;
		}
		enemyRef = info.enemyRef;
		itemPrefab = info.itemPrefab;
		dropItemPrefab = info.dropItemPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Generate an ItemInfo from this object
    public ItemInfo GetItemInfo(){
	return new ItemInfo(this);
	
    }

    // called by Inventory when picked up
    public void Destroy(){
	Destroy(this.gameObject);
	
    }

    // Called when something with a collider overlaps this object's collider (trigger)
    void OnTriggerEnter(Collider other){

	// check if 'other' is an Inventory
	Inventory inv = other.gameObject.GetComponent<Inventory>();
	if(inv != null){
		FindObjectOfType<AudioManager>().PlaySound(clip);
	    inv.PickUp(this);
	}
		
    }
	
	

}

// held by Inventory.
// this will contain data needed from the original Item
// like the item name, UI image, enemy this item is linked to, etc...
public class ItemInfo {
    public string name;

    // UI Image Variable
    public Texture2D uiImage;

    // Item's enemy reference
    public Enemy enemyRef;

    // (Temporary) UI Color
    public Material markerMaterial;
    public Color32 markerColor;

    public GameObject itemPrefab;

	public GameObject dropItemPrefab;

	
    

	//function to assign translate item information to inventory
	public ItemInfo(Item item){
		
		name = item.name;
		enemyRef = item.enemyRef;
		if(item.markerMaterial != null){
			markerMaterial = item.markerMaterial;
			markerColor = markerMaterial.color;
		}
		else {
			markerColor = Color.white;
		}

		if(item.uiImage != null){
			uiImage = item.uiImage;
		}

		itemPrefab = item.itemPrefab;
		if (item.dropItemPrefab != null)
		{
			dropItemPrefab = item.dropItemPrefab;
		}
    }
	

}
