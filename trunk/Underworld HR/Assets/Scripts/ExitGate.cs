using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ExitGate : MonoBehaviour
{
    // the name of the next level scene file
    public string nextSceneName;

    
    public int threeStarEnergy;
    public int twoStarEnergy;

    private VisualElement root;

    private StarRating starScore;

	public AudioClip clip;

    public string[] successTips;

	public bool levelComplete = false;

    
    // Start is called before the first frame update
    void Start()
    {
		root = GetComponent<UIDocument>().rootVisualElement;
		root.style.display = DisplayStyle.None;

		root.Q<Button>("next-button").RegisterCallback<ClickEvent>(ev => LevelTransition() );
		root.Q<Button>("menu-button").RegisterCallback<ClickEvent>(ev => MenuButton() );
		root.Q<Button>("retry-button").RegisterCallback<ClickEvent>(ev => RetryButton() );

		if(successTips.Length > 0){
		    string msg = successTips[Random.Range(0,successTips.Length)];
		    root.Q<Label>("successtip").text = msg;
		}

		root.Q<Label>("dateS").text = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy");
		

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col){
	// check if object that triggered is the player
	Player ply = col.GetComponent<Player>();
	ply.Freeze();
	levelComplete = true;
	FindObjectOfType<AudioManager>().PlaySound(clip);
	int plyEnergy = (int)(ply.energy);


	
	if(ply != null){

	    // don't let the player move
	    ply.Freeze();

	    // decide how many stars the player gets
	    if(plyEnergy >= threeStarEnergy){
		starScore = StarRating.THREE;
	    }else if(plyEnergy >= twoStarEnergy){
		starScore = StarRating.TWO;
	    }else {
		starScore = StarRating.ONE;
	    }

	    // get the container to fill stars with
	    VisualElement container = root.Q<VisualElement>("star-container");
	    
	    // add stars to the star container
	    for(int i = 0; i < 3;i++){
		VisualElement star = new VisualElement();
		
		if((int)starScore > i){
		    star.AddToClassList("star-on");
		}else{
		    star.AddToClassList("star-off");
		}
		container.Add(star);
	    }

	    
	    ProgressSave(ply);
	    
	    

	    //Debug.Log("Level Complete! " + ((int)rating) + " star(s).");
	    
	    // Load next level
	    if(root != null)
		root.style.display = DisplayStyle.Flex;
	    //LevelTransition();
	}
    }

    private void MenuButton(){
	//root.Q<VisualElement>("page-background").style.display = DisplayStyle.None;
	SceneManager.LoadScene("Main_Menu_V2");
    }

    private void RetryButton(){
	//root.Q<VisualElement>("page-background").style.display = DisplayStyle.None;
	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ProgressSave(Player ply){

	// If the save file is missing, create a new one.
	// Only intended for developers as all players are taken
	// to the main menu first (where save files are automatically made)
	if(!SaveSystem.SaveExists()){
	    SaveSystem.SaveGame(new SaveData());
	}
	
	if(SaveSystem.SaveExists() && threeStarEnergy > 0){
	    
	    SaveData data = SaveSystem.LoadGame(); // Get the data from our previous save
	    LevelName thisLevel = SaveSystem.GetLevelIndex(SceneManager.GetActiveScene().name);
	    bool fileChange = false;

	    // notify our save that we've progressed
	    if(((int)data.levelProgress) < ((int)thisLevel) + 1 ){
		//data.levelProgress++;
		data.levelProgress = thisLevel + 1;
		fileChange = true;
		Debug.Log("new level progress " + data.levelProgress.ToString());
	    }

	    // update star score for this level in our save
	    if(((int)starScore) > ((int)data.levelStars[(int)thisLevel])){
		data.levelStars[(int)thisLevel] = starScore;
		fileChange = true;
	    }

	    // save the updated data to our save file.
	    if(fileChange){
		SaveSystem.SaveGame(data);
	    }

	    // Show that the file has been saved on the UI
	    //root.Q<Label>("save-message").text = "Saved Game!";
	}else {
	    Debug.LogWarning("No Save file found! Visit the main menu first to create a new save.");
	}
    }

    // loads nextLevel scene
    private void LevelTransition(){
	// hide page UI for feedback
	//root.Q<VisualElement>("page-background").style.display = DisplayStyle.None;
	SceneManager.LoadScene(nextSceneName);
    }
}
