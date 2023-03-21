using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
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
   public string destinantionSceneName;

   [Foldout("Conditions")] public string pointIdFromWhichPlayerComes;
   [Foldout("Conditions")] public string sceneNameFromWhichPlayerComes;

   public override void Interact()
   {
      DeterminLoad();
   }
   private void DeterminLoad()
   {
      player.GetComponent<PlayerScript>().PlayerData.lastLocation = objectId + " " + SceneManager.GetActiveScene().name;
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
      if (destinantionSceneName != null)
      {
         Debug.Log("Scene changed to: " + destinantionSceneName);
         SceneManager.LoadScene(destinantionSceneName, LoadSceneMode.Single);
      }
   }

   private void LoadGlobalScene()
   {
      Debug.Log("Scene changed to: Global Scene ");
      SceneManager.LoadScene("GlobalScene", LoadSceneMode.Single);
   }

   private void CheckWhichPointToUse()
   {
      string[] array = new string[2];
      array = player.GetComponent<PlayerScript>().PlayerData.lastLocation.Split(' ');
      string receivedId = array[0];
      string receivedSceneName = array[1];
      MovePlayerToLoadedPoint(receivedId, receivedSceneName);
   }

   private void MovePlayerToLoadedPoint(string receivedId, string receivedSceneName)
   {
      if (receivedId == pointIdFromWhichPlayerComes && receivedSceneName == sceneNameFromWhichPlayerComes)
      {
         player.transform.position = interactor.interactorPosition;
      }
   }

   private void Awake()
   {
      //CheckWhichPointToUse();
   }
}
