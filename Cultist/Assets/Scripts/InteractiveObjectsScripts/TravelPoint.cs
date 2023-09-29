using System;
using System.Collections;
using Events;
using NaughtyAttributes;
using UnityEngine;

public class TravelPoint : BaseInteractableObject
{
   #region Events

   public static event Action OnPlayerTravel;

   public static event Action OnPlayerTravelDone;

   public static event Action OnSceneToScenePlayerTravel;

   public static event Action OnGlobalMapPlayerTravel;

   public static event Action<Vector3> OnReportDefaultSpawnPoint;

   public static event Action<Vector3, int> OnReportTravelPointSpawn;
   

   #endregion
   
   
   public bool isDefaultSpawnPoint;

   public bool travelNotAllowed;
   
   [SerializeField] private CharacterTransitionType transitionType;
   
   [ShowIf("transitionType", CharacterTransitionType.Local)]
   [SerializeField] private TravelPoint travelDestination;

   [Foldout("Conditions")] [ShowIf("transitionType", CharacterTransitionType.SceneToScene)] [AllowNesting]
   public int newTravelPointId;

   [Foldout("Conditions")] [ShowIf("transitionType", CharacterTransitionType.SceneToScene)] [Scene]
   public string newSceneName;

   public override void Start()
   {
      base.Start();
      
      ReportSpawnPoint();

      ReportTravelPointPosition();
   }

   private void ReportTravelPointPosition()
   {
      OnReportTravelPointSpawn?.Invoke(interactor.transform.position, objectId);
   }
   private void ReportSpawnPoint()
   {
      if (isDefaultSpawnPoint)
      {
         OnReportDefaultSpawnPoint?.Invoke(interactor.transform.position);
      }
   }

   public override void Interact()
   {
      if (travelNotAllowed) return;
      
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
            HandleSceneToSceneTransition();
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
   private void HandleSceneToSceneTransition()
   {
      OnSceneToScenePlayerTravel?.Invoke();
      
      var change = new CallLocationChange();
      
      change.ChangeLocation(newSceneName, newTravelPointId);
   }
   private void Transition(Vector3 newPosition)
   {
      try
      {
         player.GetComponent<CharacterControllerScript>().SpawnPlayer(newPosition);
      }
      catch (Exception exception)
      {
         Debug.Log(exception + "Cannot find directed travel point");
         throw;
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
