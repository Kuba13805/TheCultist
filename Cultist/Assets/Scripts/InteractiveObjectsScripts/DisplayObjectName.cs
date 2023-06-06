using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisplayObjectName : MonoBehaviour
{
    private string _objectNameToDisplay;
    private GameObject _toolbarCanvas;
    private GameObject _panelPrefab;
    private GameObject _panelInstance;
    private bool _hasNameBeenDisplayed;

    private void Start()
    {
        _hasNameBeenDisplayed = false;

        _objectNameToDisplay = HandleObjectName();

        InputManager.Instance.PlayerInputActions.Player.DisplayInteractablesInfo.started += DisplayObjectNamePanel;
        
        InputManager.Instance.PlayerInputActions.Player.DisplayInteractablesInfo.canceled += DestroyObjectNamePanel;
        
        _toolbarCanvas = GameObject.Find("CommentsCanvas");

        _panelPrefab = LoadPanelPrefab();
    }

    private void OnDestroy()
    {
        InputManager.Instance.PlayerInputActions.Player.DisplayInteractablesInfo.started -= DisplayObjectNamePanel;
        
        InputManager.Instance.PlayerInputActions.Player.DisplayInteractablesInfo.canceled -= DestroyObjectNamePanel;
    }

    private void DisplayObjectNamePanel(InputAction.CallbackContext obj)
    {
        if (_hasNameBeenDisplayed)
        {
            Destroy();
        }
        
        var uiPosition = transform.position;
        uiPosition.y += 1;
        
        _panelInstance = Instantiate(_panelPrefab, _toolbarCanvas.transform);

        _panelInstance.transform.position = uiPosition;
        

        _panelInstance.GetComponentInChildren<TextMeshProUGUI>().text = _objectNameToDisplay;
        _hasNameBeenDisplayed = true;
    }
    private void DestroyObjectNamePanel(InputAction.CallbackContext obj)
    {
        Destroy();
    }

    private void Destroy()
    {
        _hasNameBeenDisplayed = false;
        Destroy(_panelInstance.gameObject);
    }

    private static GameObject LoadPanelPrefab()
    {
        return Resources.Load<GameObject>("InteractableInfoPanel");
    }

    private string HandleObjectName()
    {
        var nameToDisplay = "";
        if (GetComponent<Container>())
        {
            nameToDisplay = GetComponent<Container>().objectName;
        }
        else if (GetComponent<CollectableObject>())
        {
            nameToDisplay = GetComponent<CollectableObject>().objectName;
        }
        else if (GetComponent<InteractableCharacter>())
        {
            nameToDisplay = GetComponent<InteractableCharacter>().objectName;
        }
        else if (GetComponent<EnvironmentObject>())
        {
            nameToDisplay = GetComponent<EnvironmentObject>().objectName;
        }
        else if (GetComponent<TravelPoint>())
        {
            nameToDisplay = GetComponent<TravelPoint>().objectName;
        }

        return nameToDisplay ??= gameObject.name;
    }
}
