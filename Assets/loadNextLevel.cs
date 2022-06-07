using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadNextLevel : MonoBehaviour
{
    private int currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        
    }
    

    public void LoadLevel()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex + 1;

        StartCoroutine(delay());
        
        //SceneManager.LoadScene(currentLevel, LoadSceneMode.Single);
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(5);
        Reference.transitor.LoadScene(currentLevel);
    }
}
