using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InteractiveObjectsScripts;
using ModestTree;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private float detectionRadius;
    private bool _playerDetected;

    [SerializeField] private List<PlayerDetectionAction> _detectionActions;

    #region Events

    public static event Action<string, GameObject> OnCallCommentOnObject; 

    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void Start()
    {
        GetComponent<SphereCollider>().radius = detectionRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerDetected = true;
            
            Debug.Log("Player detected!");
            
            DoAction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player no longer in range!");
            _playerDetected = false;
        }
    }

    private void DoAction()
    {
        switch (_detectionActions.First().actionType)
        {
            case ActionType.CallForNarrativeEvent:
                CallNarrativeEvent();
                break;
            case ActionType.CallForDialogue:
                CallDialogue();
                break;
            case ActionType.CallForTimeline:
                CallTimeline();
                break;
            case ActionType.CallForComment:
                CallComment();
                break;
            case ActionType.CallForTest:
                CallPlayerTest();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (_detectionActions.First().oneTimeAction)
        {
            _detectionActions.Remove(_detectionActions.First());
        }

        
    }

    private void CallNarrativeEvent()
    {
        _detectionActions.First().narrativeEvent.CallForEvent();
    }

    private void CallDialogue()
    {
        _detectionActions.First().dialogue.InteractWithObject();
    }

    private void CallTimeline()
    {
        
    }

    private void CallComment()
    {
        OnCallCommentOnObject?.Invoke(_detectionActions.First().comment, _detectionActions.First().commentOrigin);
    }

    private void CallPlayerTest()
    {
        var playerTest = new PlayerEvents();
        
        playerTest.TestStat(_detectionActions.First().skillToTest, _detectionActions.First().testDifficulty);
    }
}
