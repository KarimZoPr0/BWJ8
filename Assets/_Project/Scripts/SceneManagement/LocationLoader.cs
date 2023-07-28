using System;
using System.Collections;
using System.Collections.Generic;
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
	[SerializeField] private string _initializationScene;
	[Header("Load on start")]
	[SerializeField] private ActiveSceneCollectionSO mainMenuScenesCollection;

	[Header("Loading Screen")] 
	[SerializeField] private Ease loadingEase;
	[SerializeField] private Transform loadingIcon;
	[Header("Load Event")]
	//The load event we are listening to
	[SerializeField] private LoadEventChannelSO _loadEventChannel = default;

	[Header("Others")] 
	[SerializeField] private GameObject eventSystem;

	//[SerializeField] private InputReader inputReader;
	
	
	//List of the scenes to load and track progress
	private List<AsyncOperation> _scenesToLoadAsyncOperations = new List<AsyncOperation>();
	//List of scenes to unload
	private List<Scene> _ScenesToUnload = new List<Scene>();
	//Keep track of the scene we want to set as active (for lighting/skybox)
	public ActiveSceneCollectionSO currentSceneCollection;
	public Blinders blinders;
	
	private float _totalSceneProgress;
	public Image progressbar;
	
	private void Start()
	{
		if (SceneManager.GetActiveScene().name == _initializationScene)
		{
			LoadMainMenu();
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			ReloadScenes();
		}
	}

	private void OnEnable()
	{
		_loadEventChannel.onLoadingActiveRequested += LoadScenes;
		// inputReader.RestartEvent += HandleRestart;
	}
	
	private void OnDisable()
	{
		_loadEventChannel.onLoadingActiveRequested -= LoadScenes;
		// inputReader.RestartEvent -= HandleRestart;
	}

	public void ReloadScenes()
	{
		foreach (var activeScene in currentSceneCollection.activeScenes)
		{
			SceneManager.UnloadSceneAsync(activeScene);
			SceneManager.LoadSceneAsync(activeScene, LoadSceneMode.Additive);
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
		StartCoroutine(PrepareLoading(locationsToLoad));
	}

	private IEnumerator PrepareLoading(ActiveSceneCollectionSO locationsToLoad)
	{
		PrepareTransition();
		yield return new WaitForSeconds(blinders.GetDuration);

		AddAllScenesToUnload();
		UnloadScenes();
		SetupScenesToLoad(locationsToLoad);
		StartCoroutine(TrackLoadingProgress());
	}

	private void SetupScenesToLoad(ActiveSceneCollectionSO locationsToLoad)
	{
		currentSceneCollection = locationsToLoad;

		foreach (string currentSceneName in locationsToLoad.finalScenes)
		{
			_scenesToLoadAsyncOperations.Add(SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive));
		}

		_scenesToLoadAsyncOperations[0].completed += SetActiveScene;
	}

	private void PrepareTransition()
	{
		progressbar.fillAmount = 0f;
		eventSystem.SetActive(false);
		blinders.Close();
	}
	
	private IEnumerator TrackLoadingProgress()
	{
		foreach (var sceneToLoad in _scenesToLoadAsyncOperations)
		{
			while (!sceneToLoad.isDone)
			{
				GetLoadingProgress();
				yield return null;
			}
		}

		if (blinders.camera.isActiveAndEnabled)
		{
			yield return new WaitForSeconds(.5f);
			blinders.camera.gameObject.SetActive(false);
		}

		LoadingCompleted();
	}

	private void LoadingCompleted()
	{
		blinders.Open();
		_scenesToLoadAsyncOperations.Clear();
		eventSystem.SetActive(true);
	}

	private void GetLoadingProgress()
	{
		_totalSceneProgress = 0;
		foreach (var scenesToLoadAsyncOperation in _scenesToLoadAsyncOperations)
		{
			_totalSceneProgress += scenesToLoadAsyncOperation.progress;
		}

		progressbar.fillAmount = _totalSceneProgress / _scenesToLoadAsyncOperations.Count;
	}

	private void SetActiveScene(AsyncOperation asyncOp)
	{
		var scenePath = currentSceneCollection.finalScenes[0].ScenePath;
		var scene = SceneManager.GetSceneByPath(scenePath);
		SceneManager.SetActiveScene(scene);
	}
	

	private void AddAllScenesToUnload()
	{
		for (var i = 0; i < SceneManager.sceneCount; ++i)
		{
			var scene = SceneManager.GetSceneAt(i);
			if (scene.name != _initializationScene)
			{
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
		_ScenesToUnload?.Clear();
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

	private void HandleRestart(bool restart)
	{
		if (restart)
		{
			ReloadScenes();
		}
	}
}