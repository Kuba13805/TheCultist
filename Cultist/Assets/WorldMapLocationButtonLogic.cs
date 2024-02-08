using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapLocationButtonLogic : MonoBehaviour
{
    [SerializeField][Scene] private string locationToLoad;

    public void LoadLocation()
    {
        SceneManager.LoadSceneAsync(locationToLoad, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(locationToLoad);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
    }
}
