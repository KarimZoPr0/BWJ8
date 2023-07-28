using UnityEngine;

/// <summary>
/// This class contains the function to call when play button is pressed
/// </summary>

public class StartGame : MonoBehaviour
{
	public LoadEventChannelSO onPlayButtonPress;
	public ActiveSceneCollectionSO locationsToLoad;
	public bool showLoadScreen;

	public void OnLoadLevel()
	{
		onPlayButtonPress.RaiseEvent(locationsToLoad, showLoadScreen);
	}
}