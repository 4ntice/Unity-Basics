using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //[SerializeField] bool isSceneLoaded = false;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject loadingSymbol;
    int currentLevelIndex;

    private void Start()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadLevel(int levelIndex)
    {
        StartCoroutine(LoadAsync(levelIndex));
    }

    IEnumerator LoadAsync (int sceneIndex)
    {
        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            loadingSymbol.transform.Rotate(0, 0, 100 * Time.deltaTime);
            yield return null;
        }

        operation = SceneManager.UnloadSceneAsync(currentLevelIndex);

        while (!operation.isDone)
        {
            loadingSymbol.transform.Rotate(0, 0, 100 * Time.deltaTime);
            yield return null;
        }

        loadingScreen.SetActive(false);
    }
}
