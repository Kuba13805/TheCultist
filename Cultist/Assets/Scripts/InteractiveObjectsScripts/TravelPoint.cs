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
      GameManager.Instance.PlayerData.lastLocation = objectId + " " + SceneManager.GetActiveScene().name;
      if (isLocal)
      {
         LoadLocalScene();
      }
      else
      {
         LoadGlobalScene();
      }
   }
   private void LoadLocalScene()
   {
      if (destinantionName != null)
      {
         Debug.Log("Player moved to: " + destinantionName);
         try
         {
            NavMeshAgent playerNavMeshAgent = player.GetComponent<NavMeshAgent>();
            
            playerNavMeshAgent.Warp(GameObject.Find(destinantionName + "TravelPoint")
               .GetComponent<TravelPoint>().interactor.interactorPosition);
            playerNavMeshAgent.transform.Rotate(0f,180f, 0f);
            
         }
         catch (Exception exception)
         {
            Debug.Log(exception + "Cannot find directed travel point");
            throw exception;
         }
      }
   }

   private void LoadGlobalScene()
   {
      Debug.Log("Scene changed to: Global Scene ");
      SceneManager.LoadScene("GlobalScene", LoadSceneMode.Single);
   }
}
