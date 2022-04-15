using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Animation_to_scene : MonoBehaviour
{
	public string scene_name;

	private void OnEnable()
	{
		SceneManager.LoadScene(scene_name);
	}
}
