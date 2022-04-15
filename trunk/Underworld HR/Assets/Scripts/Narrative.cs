using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Narrative : MonoBehaviour
{
	private bool EOScheck;
	private Label character_name;
	private Label character_dialogue;
	// [Temporary for Alpha] 1 image for both characters
	//private VisualElement character_image;
	public VisualElement leftPortrait;
	//public  VisualElement rightPortrait;
	private VisualElement radGlow;
	private Button next_button, skip_button;
	private Dialogue_Manager diamngr;

	private AudioSource voiceChannel;

	private VisualElement root;

	// holds the conversation
	//private Dialogue dialogue;
	private Conversation convo;
	private int dialogueIndex = 0;
	
	public string line;
	// Start is called before the first frame update
	public GameObject referenceObject, p1;
	//	dialogue_textbox_BG.style.backgroundImage = TxtBoxBg;

	public ConversationEvent onEndConversation;

    void Start()
    {
		EOScheck = false;
		root = GetComponent<UIDocument>().rootVisualElement;
	
		line = "asasasdasdad";
		diamngr = GetComponent<Dialogue_Manager>();

		character_name = root.Q<Label>("Name_label");
		character_dialogue = root.Q<Label>("Dialogue_label");
		character_dialogue.text = line;
		//character_image = root.Q<VisualElement>("DialogueImage");

		leftPortrait = root.Q<VisualElement>("AgnesImg");
		//rightPortrait = root.Q<VisualElement>("EmployeeImg");
		radGlow = root.Q<VisualElement>("RadialGlow");

		
		next_button = root.Q<Button>("Next_button");
		next_button.clicked += NextButton;

		skip_button = root.Q<Button>("skip-button");

		skip_button.RegisterCallback<ClickEvent>(ev => SkipButton());
		
		Hide();

		voiceChannel = gameObject.AddComponent<AudioSource>();

		// This needs to be adjustable (options menu?)
		voiceChannel.volume = 0.5f;
		
    }

    // Update is called once per frame
    void Update()
    {
		p1 = GameObject.Find("Player");
		// var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
		// character_name = rootVisualElement.Q<Label>("Name_label");
		// character_dialogue = rootVisualElement.Q<Label>("Dialogue_label");
		// character_dialogue.text = $"{line}";
		// next_button = rootVisualElement.Q<Button>("Next_button");
		// next_button.clicked += diamngr.Display_next;
	}


    

	public void Line_update(string new_line)
	{
		line = new_line;
	}

	private void NextButton(){
	    //if(dialogue.sentences.Length < dialogueIndex+1){
	    if(convo.lines.Length < dialogueIndex+1){
			if (!EOScheck)
			{
				EndConversation();
			}
			else
			{
				EndEOSConversation();
			}
		}

		else{
		NextLine();
	    }
	}

	private void SkipButton(){
	    Debug.Log("Skipping, EOS: "+EOScheck.ToString());
	    if(!EOScheck){
		EndConversation();
	    }else{
		EndEOSConversation();
	    }
	}

	private void NextLine(){
	    if(convo == null)
		return;

	    if(convo.lines.Length == 0){
		Debug.LogWarning("Enemy Conversation is Empty!");
		return;
	    }
	    
	    DialogueData speech = convo.lines[dialogueIndex];
	    //character_dialogue.text = diamngr.GetLine();
	    //Debug.Log(character_dialogue);
	    //character_dialogue.text = dialogue.sentences[dialogueIndex];
	    character_dialogue.text = speech.line;
	    character_name.text = speech.name;
	    // if(speech.dialogueImage != null){
	    // 	character_image.style.backgroundImage = speech.dialogueImage;
	    // }
		//rightPortrait.style.opacity = 0.5f;
		
		Color tintColor = new Color32( 200, 200, 200, 128);

		if(speech.agnesSpeaking == true){
			//rightPortrait.style.unityBackgroundImageTintColor = tintColor;//Darken right image
			//leftPortrait.style.unityBackgroundImageTintColor = Color.white;//reset left image
		}
		if(speech.agnesSpeaking == false){
			//rightPortrait.style.unityBackgroundImageTintColor = Color.white;//reset right image
			//leftPortrait.style.unityBackgroundImageTintColor = tintColor;//Darken left image
		}

	    if(speech.leftPortrait != null){
			leftPortrait.style.backgroundImage = speech.leftPortrait;
		}
	    if(speech.rightPortrait != null){
			//rightPortrait.style.backgroundImage = speech.rightPortrait;     // ADDS A PORTRAIT ON THE RIGHT
		}
		

		if(speech.name == "Agnes"){
			leftPortrait.style.backgroundImage = convo.agnesPortrait;
			radGlow.style.unityBackgroundImageTintColor = new Color(0f/255f, 173f/255f, 255f/255f, 255f/255f);

		}
		if(speech.name == "Hathor"){
			leftPortrait.style.backgroundImage = convo.hathorPortrait;
			radGlow.style.unityBackgroundImageTintColor = new Color(255f/255f, 194f/255f, 0f/255f, 1f);
		}
		if(speech.name == "Anubis"){
			leftPortrait.style.backgroundImage = convo.anubisPortrait;
			radGlow.style.unityBackgroundImageTintColor = new Color(255f/255f, 194f/255f, 0f/255f, 1f);
		}
		if(speech.name == "Dionysus"){
			leftPortrait.style.backgroundImage = convo.dioPortrait;
			radGlow.style.unityBackgroundImageTintColor = new Color(255f/255f, 0f/255f, 184f/255f, 1f);
		}
		if(speech.name == "Hypnos"){
			leftPortrait.style.backgroundImage = convo.hypnosPortrait;
			radGlow.style.unityBackgroundImageTintColor = new Color(255f/255f, 0f/255f, 184f/255f, 1f);
		}
		if(speech.name == "Loki"){
			leftPortrait.style.backgroundImage = convo.lokiPortrait;
			radGlow.style.unityBackgroundImageTintColor = new Color(0f/255f, 255f/255f, 75f/255f, 1f);
		}
		if(speech.name == "Idun"){
			leftPortrait.style.backgroundImage = convo.idunPortrait;
			radGlow.style.unityBackgroundImageTintColor = new Color(0f/255f, 255f/255f, 75f/255f, 1f);
		}
		if(speech.name != "Agnes" && speech.name != "Hathor" && speech.name != "Anubis" && speech.name != "Dionysus" && speech.name != "Hypnos" && speech.name != "Loki" && speech.name != "Idun")
		{
			leftPortrait.style.backgroundImage = convo.enemyPortrait;
			radGlow.style.unityBackgroundImageTintColor = new Color(0f/255f, 173f/255f, 255f/255f, 255f/255f);
		}


	    dialogueIndex++;

	    voiceChannel.Stop();

	    if(speech.voiceAudio != null){
		
		voiceChannel.PlayOneShot(speech.voiceAudio,voiceChannel.volume);
	    }
	}

	
	public void Show(){
	    root.style.display = DisplayStyle.Flex;
	}

	public void Hide(){
	    root.style.display = DisplayStyle.None;
	}

	public void StartEOSConversation(Conversation c, Texture2D bg){
	    EOScheck = true;
	    StartConversation( c, bg );
	}
	
	public void StartConversation(Conversation c, Texture2D bg){
	    convo = c;
		p1.GetComponent<Player>().freeze = true;
	    //this.gameObject.SetActive(true);
		root.Q<VisualElement>("Narrative_box").style.backgroundImage = bg;
	    Show();
	    Debug.Log("Start of Convo");
	    // freeze player
	    dialogueIndex = 0;
	    NextLine();
	}

	public void EndConversation(){
	    onEndConversation.Invoke(convo);
	    //this.gameObject.SetActive(false);
	    Hide();
		p1.GetComponent<Player>().freeze = false;
		voiceChannel.Stop();
	    // unfreeze player
	    Debug.Log("End of Convo");
	    convo = null;
	}

	public void EndEOSConversation()
	{
	    Debug.Log("END EOS CONVERSATION");
		convo = null;
		//this.gameObject.SetActive(false);
		Hide();
		p1.GetComponent<Player>().freeze = false;
		voiceChannel.Stop();
		// unfreeze player
		Debug.Log("End of Convo");
		Destroy(referenceObject);
		referenceObject = null;
		EOScheck = false;
	}
}
