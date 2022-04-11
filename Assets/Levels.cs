using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
	public int sceneNum;
	public void MouseOn() {
		transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.5f);
		print("mouse on");
	}

	public void MouseOff() {
		transform.DOScale(new Vector3(1f, 1f, 1), 0.5f);
		print("mouse off");
	}

	public void LoadLevel()
	{
		//sceneNum = SceneManager.GetActiveScene().buildIndex + 1;
		Reference.transitor.LoadScene(sceneNum);
		//SceneManager.LoadScene(sceneNum, LoadSceneMode.Single);
	}
}
