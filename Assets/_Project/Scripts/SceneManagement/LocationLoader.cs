using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// This class manages the scenes loading and unloading
/// </summary>
public class LocationLoader : MonoBehaviour
{

	[Header("Initialization Scene")]
	[SerializeField] private SceneAsset _initializationScene = default;
	[Header("Load on start")]
	[SerializeField] private ActiveSceneCollectionSO mainMenuScenesCollection = default;

	[Header("Loading Screen")] 
	[SerializeField] private Ease loadingEase;
	[SerializeField] private Transform loadingIcon;
	[Header("Load Event")]
	//The load event we are listening to
	[SerializeField] private LoadEventChannelSO _loadEventChannel = default;

	[Header("Others")] 
	[SerializeField] private GameObject eventSystem; 
	
	//List of the scenes to load and track progress
	private List<AsyncOperation> _scenesToLoadAsyncOperations = new List<AsyncOperation>();
	//List of scenes to unload
	private List<Scene> _ScenesToUnload = new List<Scene>();
	//Keep track of the scene we want to set as active (for lighting/skybox)
	public ActiveSceneCollectionSO currentSceneCollection;
	public Blinders blinders;

	public void ReloadScenes()
	{
		foreach (var activeScene in currentSceneCollection.activeScenes)
		{
			Debug.Log("Adding scene to unload: " + activeScene.name);
			SceneManager.UnloadSceneAsync(activeScene.name);
			SceneManager.LoadSceneAsync(activeScene.name, LoadSceneMode.Additive);
		}
	}
	
	private void OnEnable()
	{
		
		_loadEventChannel.onLoadingActiveRequested += LoadScenes;
	}

	private void OnDisable()
	{
		_loadEventChannel.onLoadingActiveRequested -= LoadScenes;
	}

	private void Start()
	{
		if (SceneManager.GetActiveScene().name == _initializationScene.name)
		{
			LoadMainMenu();
		}
	}

	private void LoadMainMenu()
	{
		LoadScenes(mainMenuScenesCollection, false);
	}

	/// <summary> This function loads the scenes passed as array parameter </summary>
	public void LoadScenes(ActiveSceneCollectionSO locationsToLoad, bool showLoadingScreen)
	{
		//Add all current open scenes to unload list
		StartCoroutine(StartTOLaod(locationsToLoad));
	}

	private IEnumerator StartTOLaod(ActiveSceneCollectionSO locationsToLoad)
	{
		progressbar.fillAmount = 0f;
		eventSystem.SetActive(false);
		blinders.Close();
		yield return new WaitForSeconds(blinders.GetDuration);

		AddAllScenesToUnload();
		
		

		currentSceneCollection = locationsToLoad;

		for (int i = 0; i < locationsToLoad.finalScenes.Count; ++i)
		{
			String currentSceneName = locationsToLoad.finalScenes[i].name;
			_scenesToLoadAsyncOperations.Add(SceneManager.LoadSceneAsync(currentSceneName,
				LoadSceneMode.Additive));
		}

		_scenesToLoadAsyncOperations[0].completed += SetActiveScene;
		

		StartCoroutine(TrackLoadingProgress());
		UnloadScenes();
	}

	private float totalSceneProgress;
	public Image progressbar;
	private IEnumerator TrackLoadingProgress()
	{
		foreach (var sceneToLoad in _scenesToLoadAsyncOperations)
		{
			while (!sceneToLoad.isDone)
			{
				totalSceneProgress = 0;
				foreach (var scenesToLoadAsyncOperation in _scenesToLoadAsyncOperations)
				{
					totalSceneProgress += scenesToLoadAsyncOperation.progress;

				}
				progressbar.fillAmount = totalSceneProgress / _scenesToLoadAsyncOperations.Count;
				
				yield return null;
			}
		}

		if (blinders.camera.isActiveAndEnabled)
		{
			yield return new WaitForSeconds(.5f);
			blinders.camera.gameObject.SetActive(false);
		}

		blinders.Open();
		
		//Clear the scenes to load
		_scenesToLoadAsyncOperations.Clear();
		
		eventSystem.SetActive(true);
	}
	private void SetActiveScene(AsyncOperation asyncOp)
	{
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentSceneCollection.finalScenes[0].name));
	}
	

	private void AddAllScenesToUnload()
	{
		for (int i = 0; i < SceneManager.sceneCount; ++i)
		{
			Scene scene = SceneManager.GetSceneAt(i);
			if (scene.name != _initializationScene.name)
			{
				//Debug.Log("Added scene to unload = " + scene.name);
				_ScenesToUnload.Add(scene);
			}
		}
	}
	

	private void UnloadScenes()
	{
		if (_ScenesToUnload != null)
		{
			foreach (var scene in _ScenesToUnload)
			{
				SceneManager.UnloadSceneAsync(scene);
			}
		}
		_ScenesToUnload.Clear();
	}

	/// <summary> This function checks if a scene is already loaded </summary>
	private bool CheckLoadState(String sceneName)
	{
		for (int i = 0; i < SceneManager.sceneCount; ++i)
		{
			Scene scene = SceneManager.GetSceneAt(i);
			if (scene.name == sceneName)
			{
				return true;
			}
		}
		return false;
	}

	/// <summary> This function updates the loading progress once per frame until loading is complete </summary>
	
	private void ExitGame()
	{
		Application.Quit();
		Debug.Log("Exit!");
	}

}