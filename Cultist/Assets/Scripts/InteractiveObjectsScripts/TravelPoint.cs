using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class TravelPoint : BaseInteractableObject
{
   #region Events

   public static event Action OnPlayerTravel;

   public static event Action OnPlayerTravelDone;

   public static event Action<int> OnSceneToScenePlayerTravel;

   public static event Action OnGlobalMapPlayerTravel;

   #endregion
   
   [SerializeField] private CharacterTransitionType transitionType;
   
   [ShowIf("transitionType", CharacterTransitionType.Local)]
   [SerializeField] private TravelPoint travelDestination;

   [Foldout("Conditions")][ShowIf("transitionType", CharacterTransitionType.SceneToScene)] 
   public int newTravelPointId;

   [Foldout("Conditions")] [ShowIf("transitionType", CharacterTransitionType.SceneToScene)] [Scene]
   public string newSceneName;

   private void OnEnable()
   {
      OnSceneToScenePlayerTravel += MakePlayerTransition;
   }

   public override void Interact()
   {
      DetermineLoad();
   }
   private void DetermineLoad()
   {
      switch (transitionType)
      {
         case CharacterTransitionType.Local:
            LoadLocalScene();
            break;
         case CharacterTransitionType.SceneToScene:
            StartCoroutine(HandleSceneToSceneTransition());
            break;
         case CharacterTransitionType.GlobalMap:
            HandleGlobalMapTransition();
            break;
      }
   }
   private void LoadLocalScene()
   {
      OnPlayerTravel?.Invoke();

      StartCoroutine(WaitFor(HandleLocalTransition));
   }

   private static IEnumerator WaitFor(Action method)
   {
      yield return new WaitForSeconds(1.5f);

      method();
   }
   private void HandleLocalTransition()
   {
      Transition(travelDestination.interactor.interactorPosition);
      
      OnPlayerTravelDone?.Invoke();
   }

   private void Transition(Vector3 newPosition)
   {
      try
      {
         var playerNavMeshAgent = player.GetComponent<NavMeshAgent>();

         playerNavMeshAgent.Warp(newPosition);
         playerNavMeshAgent.transform.Rotate(0f, 180f, 0f);
      }
      catch (Exception exception)
      {
         Debug.Log(exception + "Cannot find directed travel point");
         throw;
      }
   }

   private IEnumerator HandleSceneToSceneTransition()
   {
      var activeScene = SceneManager.GetActiveScene();
      
      var newLoadSceneAsync = SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive);

      while (!newLoadSceneAsync.isDone)
      {
         yield return null;
      }
      
      OnSceneToScenePlayerTravel?.Invoke(objectId);

      SceneManager.SetActiveScene(SceneManager.GetSceneByName(newSceneName));
      
      SceneManager.UnloadSceneAsync(activeScene);
   }
   private void MakePlayerTransition(int travelPointId)
   {
      if (travelPointId == objectId)
      {
         Transition(interactor.interactorPosition);
      }

      if (travelPointId == objectId)
      {
         Debug.Log("Point found!");
      }
   }
   private static void HandleGlobalMapTransition()
   {
       OnGlobalMapPlayerTravel?.Invoke();
   }
}
public enum CharacterTransitionType
{
   Local,
   SceneToScene,
   GlobalMap,
}
