using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MenuBG : MonoBehaviour
{
	public BGcontainer contBackGround;
	public GameObject[] backgrounds;
	public Vector3[] pos;
	private GameObject currentbg;
    // Start is called before the first frame update
    void Start()
    {
		backGroundChange(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void backGroundChange(int backg)
	{
		if (currentbg != null)
		{
			Destroy(currentbg);
		}
		GameObject bg;
		bg = GameObject.Find("Main_menu_Bg");
		if (bg != null)
		{
			currentbg=GameObject.Instantiate(backgrounds[backg],bg.transform.position+pos[backg],Quaternion.identity);
		}
	}
}
public class BGcontainer {
	public GameObject model;
	public Transform position;
}
