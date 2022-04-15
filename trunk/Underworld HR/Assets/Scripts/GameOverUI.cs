using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
	private VisualElement root;
	private Label retry;
	private Label menu;
    public Label date;
    public Label tip;
    public int msg;
    public AudioClip clip;
    
    // Start is called before the first frame update
    void Start()
    {

        
		root = GetComponent<UIDocument>().rootVisualElement;
		retry= root.Q<Label>("Retry");
		menu = root.Q<Label>("Menu");
        date = root.Q<Label>("DateReal");
        tip = root.Q<Label>("GameOverTips");

        msg = Random.Range(1,5);
        {
            if (msg == 1)
            {
                tip.text = ("Section 34 Article 6 of the GR Employee Handbook states: To best conserve your energy, pay attention to what coffee supplements are nearby and use them accordingly.");
            }
            if (msg == 2)
            {
                tip.text = ("Section 4 Article 678 of the GR Employee Handbook states: To avoid back injury, consider only lifting items once you know where their destination is.");
            }
            if (msg == 3)
            {
                tip.text = ("Section 3 Article 12 of the GR employee handbook states: Absolutely NO QUITTING of any kind shall be tolerated, if losing is inevitable dust yourself off and try again!");
            }
            if (msg == 4)
            {
                tip.text = ("Remember: Success is not everything.. It is the ONLY thing. So, get up and hustle!");
            }
        }
        
		//menu.RegisterCallback<ClickEvent>(ev => SceneManager.LoadScene("Main_Menu"));
		menu.RegisterCallback<ClickEvent>(ev => MainMenu());
		retry.RegisterCallback<ClickEvent>(ev => Retry());
        
		Hide();
        

        root.Q<Label>("DateReal").text = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy");

        //string time = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy");
        //print(time);
        //date.text = time;
    }

    void Retry(){
	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    
    }

    void MainMenu(){
	SceneManager.LoadScene("Main_menu_V2");
    }

    public void Show(){
        FindObjectOfType<AudioManager>().PlaySound(clip);
	    root.style.display = DisplayStyle.Flex;

    }

    public void Hide(){
	root.style.display = DisplayStyle.None;
    }
	
    // Update is called once per frame
    void Update()
    {
        
    }
}
