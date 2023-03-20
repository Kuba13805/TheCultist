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
   public Scene sceneTest;

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
      Scene sceneToLoad = SceneManager.GetSceneByName("GlobalScene");
      SceneManager.LoadSceneAsync(sceneToLoad.name, LoadSceneMode.Single);
   }
}
