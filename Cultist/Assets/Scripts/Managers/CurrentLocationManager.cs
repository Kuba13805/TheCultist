using System;
using System.Collections;
using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentLocationManager : MonoBehaviour
{
    private Scene _currentLocation;

    private string _locationToLoad;

    public static event Action<string, int> CallForTravelPointTransition;

    public static event Action OnCallForDefaultSpawnPoint;

    public static event Action<Vector3> OnSpawnPlayerAtDefaultSpawnPoint; 
    private void OnEnable()
    {
        CallLocationChange.OnChangeLocation += OnChangeLocation;

        CallLocationChange.OnChangeLocationOnTravelPoint += OnChangeLocation;

        TravelPoint.OnReturnDefaultSpawnPoint += SpawnPlayerAtDefaultSpawnPoint;
    }

    #region LocationChangeAndSceneLoad
    private void OnChangeLocation(string scene, bool setActive)
    {
        _currentLocation = SceneManager.GetActiveScene();
        
        _locationToLoad = scene;
        
        UnloadScene();

        StartCoroutine(LoadSceneAsync(setActive));
        
        CallForDefaultSpawnPoint();
    }
    private void OnChangeLocation(string scene, int travelPointId)
    {
        _currentLocation = SceneManager.GetActiveScene();
        
        _locationToLoad = scene;
        
        UnloadScene();
        
        StartCoroutine(LoadSceneAsync(true));
        
        
        CallForTravelPointTransition?.Invoke(scene, travelPointId);
    }

    private IEnumerator LoadSceneAsync(bool setActive)
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync(_locationToLoad, LoadSceneMode.Additive);

        while (!loadSceneAsync.isDone)
        {
            yield return null;
        }
        
        if (setActive)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_locationToLoad));
        }
    }

    private void UnloadScene()
    {
        SceneManager.UnloadSceneAsync(_currentLocation);
    }
    #endregion

    #region PlayerTransition

    private void CallForDefaultSpawnPoint()
    {
        OnCallForDefaultSpawnPoint?.Invoke();
    }

    private void SpawnPlayerAtDefaultSpawnPoint(TravelPoint defaultSpawnPoint)
    {
        OnSpawnPlayerAtDefaultSpawnPoint?.Invoke(defaultSpawnPoint.interactor.interactorPosition
        );
    }

    #endregion
}
