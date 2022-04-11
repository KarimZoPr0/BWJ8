using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitor : MonoBehaviour {
	public static bool restart;
	private       int  targetScene;
	public Animation loadScreen;


	private void Awake()
	{
		Reference.transitor = this;
	}

	private void Start() {
		LoadScreen("Out_In");
	}

	public bool LoadScene(int sceneNum) {
		if (sceneNum == 0 && restart) return true;
		restart     = true;
		targetScene = sceneNum;
		StartCoroutine(Transition());

		if (sceneNum == 0) return false;

		return true;
	}

	public void LoadDay() {
		StartCoroutine(Transition());
	}

	public void LoadNext(string music) {
		//Reference.audio.Play(music);
		StartCoroutine(Transition());
	}

	public void Fade() {
		StartCoroutine(fade());
	}

	public void ReloadScene() {
		targetScene = SceneManager.GetActiveScene().buildIndex;
		StartCoroutine(Transition());
	}

	private IEnumerator Transition() {
		LoadScreen("In_Out");
		yield return new WaitForSecondsRealtime(.6f);
		SceneManager.LoadScene(targetScene);
		Time.timeScale = 1;
	}

	private IEnumerator fade() {
		LoadScreen("OpenRight");
		yield return new WaitForSecondsRealtime(0.45f);
		Time.timeScale = 1;
	}
	
	
	public void LoadScreen(string anim) {
		loadScreen.Play("LoadScreen_" + anim);
	}
}