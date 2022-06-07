using DG.Tweening;
using UnityEngine;

public class UIScaler : MonoBehaviour
{
	public int sceneNum;

	private Vector3 scale;

	public void MouseOn() => transform.DOScale(new Vector3(1.6f,1.6f,1.6f), 0.5f);
	public void MouseOff() => transform.DOScale(new Vector3(1.526917f,1.526917f,1.526917f), 0.5f);

	public void LoadLevel()
	{
		Reference.transitor.LoadScene(sceneNum);
	}
}
