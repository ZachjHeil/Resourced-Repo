using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class LoadScreen : MonoBehaviour
{
	private VisualElement root, background;
	public Texture2D LSImage;
	private GameObject loadScreen;
    // Start is called before the first frame update
    void Start()
    {
		root = GetComponent<UIDocument>().rootVisualElement;
		background = root.Q<VisualElement>("LoadImage");
		background.style.backgroundImage = LSImage;
		loadScreen = gameObject;
		StartCoroutine(loadScreenTimer());
    }

	IEnumerator loadScreenTimer()
	{
		yield return new WaitForSeconds(2);
		loadScreen.SetActive(false);
	}
}
