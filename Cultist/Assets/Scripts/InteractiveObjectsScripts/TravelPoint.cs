using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class TravelPoint : BaseInteractableObject
{
   #region Events

   public static event Action OnPlayerTravel;

   public static event Action OnPlayerTravelDone;

   #endregion

   private enum objectType
   {
      Door,
      Ladder,
      Stairs
   }

   public bool isLocal;
   [SerializeField] private objectType ObjectType;
   [EnableIf("isLocal")]
   public string destinantionName;

   [Foldout("Conditions")] public string pointIdFromWhichPlayerComes;
   [Foldout("Conditions")] public string sceneNameFromWhichPlayerComes;

   public override void Interact()
   {
      DeterminLoad();
   }
   private void DeterminLoad()
   {
      if (isLocal)
      {
         LoadLocalScene();
      }
      else
      {
         StartCoroutine(WaitFor(LoadGlobalScene));
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
      if (destinantionName == null) return;
      try
      {
         var playerNavMeshAgent = player.GetComponent<NavMeshAgent>();

         playerNavMeshAgent.Warp(GameObject.Find(destinantionName + "TravelPoint")
            .GetComponent<TravelPoint>().interactor.interactorPosition);
         playerNavMeshAgent.transform.Rotate(0f, 180f, 0f);
      }
      catch (Exception exception)
      {
         Debug.Log(exception + "Cannot find directed travel point");
         throw;
      }
      OnPlayerTravelDone?.Invoke();
   }

   private static void LoadGlobalScene()
   {
      // SceneManager.LoadScene("GlobalScene", LoadSceneMode.Additive);
   }
}
