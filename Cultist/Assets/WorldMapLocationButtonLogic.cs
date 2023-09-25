using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapLocationButtonLogic : MonoBehaviour
{
    [SerializeField][Scene] private string locationToLoad;

    private void LoadLocation()
    {
        SceneManager.LoadSceneAsync(locationToLoad);
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
