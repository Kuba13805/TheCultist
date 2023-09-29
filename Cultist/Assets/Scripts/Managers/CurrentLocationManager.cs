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

    private Vector3 _playerNewPosition;

    [SerializeField][Scene] private string mainMenuScene;
    
    [SerializeField][Scene] private string playerAndUI;

    [SerializeField] private GameObject loadingScreen;

    [SerializeField] private Image loadingBar;

    public static event Action<Vector3> OnSpawnPlayerAtPosition;

    public static event Action OnSceneLoaded;
    private void OnEnable()
    {
        CallLocationChange.OnChangeLocation += OnChangeLocation;

        CallLocationChange.OnChangeLocationOnTravelPoint += OnChangeLocation;

        TravelPoint.OnReportDefaultSpawnPoint += SetNewDefaultSpawnPoint;

        TravelPoint.OnReportTravelPointSpawn += SetPlayerAtTravelPoint;

        CharacterControllerScript.OnPlayerSpawnDone += UnloadScene;

        CameraController.OnCameraMovementDone += CloseLoadingScreen;
    }


    private void Start()
    {
        StartCoroutine(LoadMainMenuOnStart());
    }
    private void SetPlayerAtTravelPoint(Vector3 interactorPosition, int newTravelPointId)
    {
        if (newTravelPointId != _travelPointIdToSpawnAt) return;
        
        _playerNewPosition = interactorPosition;
            
        SpawnPlayerAtPosition(_playerNewPosition);

    }
    private void SetNewDefaultSpawnPoint(Vector3 interactorPosition)
    {
        _playerNewPosition = interactorPosition;
        
        SpawnPlayerAtPosition(_playerNewPosition);
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
        
        StartCoroutine(LoadSceneAsync(scene, setActive));
        
        loadingScreen.SetActive(false);
    }
    private void OnChangeLocation(string scene, int travelPointId)
    {
        _currentLocation = SceneManager.GetActiveScene();
        
        _locationToLoad = scene;
        
        _travelPointIdToSpawnAt = travelPointId;
        
        StartCoroutine(LoadSceneAsync(_locationToLoad ,true));
    }

    private IEnumerator LoadSceneAsync(string newScene, bool setActive)
    {
        loadingScreen.SetActive(true);
        
        var loadSceneAsync = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        
        while (!loadSceneAsync.isDone)
        {
            float progressValue = Mathf.Clamp01(loadSceneAsync.progress / 0.9f);

            loadingBar.fillAmount = progressValue;
            
            yield return null;
            
        }

        if (!setActive) yield break;
            
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_locationToLoad));
    }

    private void UnloadScene()
    {
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
