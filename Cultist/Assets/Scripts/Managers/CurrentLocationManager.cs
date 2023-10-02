using System;
using System.Collections;
using System.Net.NetworkInformation;
using Events;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CurrentLocationManager : MonoBehaviour
{
    private Scene _currentLocation;

    private string _locationToLoad;

    private int _travelPointIdToSpawnAt;

    private Vector3 _defaultSpawnPoint;

    private Vector3 _newSpawnPoint;

    private bool _pointFound;

    [SerializeField][Scene] private string mainMenuScene;
    
    [SerializeField][Scene] private string playerAndUI;
    
    [SerializeField][Scene] private string sceneManager;

    [SerializeField] private GameObject loadingScreen;

    [SerializeField] private Image loadingBar;

    public static event Action<Vector3> OnSpawnPlayerAtPosition;

    public static event Action OnSceneLoaded;

    public static event Action<int> OnFindTravelPoint;

    public static event Action OnFindDefaultSpawnPoint;
    private void OnEnable()
    {
        CallLocationChange.OnChangeLocation += OnChangeLocation;

        CallLocationChange.OnChangeLocationOnTravelPoint += OnChangeLocation;

        TravelPoint.OnReportDefaultSpawnPoint += SetNewDefaultSpawnPoint;

        TravelPoint.OnReportTravelPointSpawn += SetPlayerAtTravelPoint;

        CameraController.OnCameraMovementDone += CloseLoadingScreen;

        //CharacterControllerScript.OnPlayerSpawnDone += UnloadScene;
    }


    private void Start()
    {
        StartCoroutine(LoadMainMenuOnStart());
    }
    private void SetPlayerAtTravelPoint(Vector3 interactorPosition)
    {
        _newSpawnPoint = interactorPosition;
        
        _pointFound = true;
        
        SpawnPlayerAtPosition(_newSpawnPoint);
        
        Debug.Log("Travel point set: " + _newSpawnPoint);
    }
    private void SetNewDefaultSpawnPoint(Vector3 interactorPosition)
    {
        _defaultSpawnPoint = interactorPosition;
        
        _pointFound = true;
        
        SpawnPlayerAtPosition(_defaultSpawnPoint);
        
        Debug.Log("Default point set: " + _defaultSpawnPoint);
    }
    private void CloseLoadingScreen()
    {
        loadingScreen.SetActive(false);
        
        OnSceneLoaded?.Invoke();
    }

    private IEnumerator LoadMainMenuOnStart()
    {
        var operation = SceneManager.LoadSceneAsync(mainMenuScene, LoadSceneMode.Additive);
        
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            loadingBar.fillAmount = progressValue;
            
            yield return null;
        }
        
        loadingScreen.SetActive(false);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(mainMenuScene));
    }

    #region LocationChangeAndSceneLoad
    private void OnChangeLocation(string scene, bool setActive)
    {
        _currentLocation = SceneManager.GetActiveScene();
        
        _locationToLoad = scene;
        
        _pointFound = false;
        
        
        StartCoroutine(LoadSceneAsync(_locationToLoad, setActive));
        
        UnloadScene();
    }
    private void OnChangeLocation(string scene, int travelPointId)
    {
        _currentLocation = SceneManager.GetActiveScene();
        
        _locationToLoad = scene;
        
        _travelPointIdToSpawnAt = travelPointId;
        
        _pointFound = false;
        
        StartCoroutine(LoadSceneAsync(_locationToLoad ,true));
        
        UnloadScene();
    }

    private IEnumerator LoadSceneAsync(string newScene, bool setActive)
    {
        loadingScreen.SetActive(true);
        
        var loadSceneAsync = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        
        Debug.Log("Start scene load: " + newScene);
        
        while (!loadSceneAsync.isDone)
        {
            float progressValue = Mathf.Clamp01(loadSceneAsync.progress / 0.9f);

            loadingBar.fillAmount = progressValue;
            
            yield return null;
        }
        
        Debug.Log("Scene loaded:" + newScene);
        
        OnFindDefaultSpawnPoint?.Invoke();
        OnFindTravelPoint?.Invoke(_travelPointIdToSpawnAt);
        if (!setActive) yield break;
            
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_locationToLoad));
    }

    private void UnloadScene()
    {
        if(_currentLocation.name == sceneManager) return;
        
        Debug.Log("Previous scene unloading: " + _currentLocation.name);
        
        SceneManager.UnloadSceneAsync(_currentLocation);
    }
    #endregion

    #region PlayerTransition
    private static void SpawnPlayerAtPosition(Vector3 newPlayerPosition)
    {
        OnSpawnPlayerAtPosition?.Invoke(newPlayerPosition);
    }

    #endregion
}
