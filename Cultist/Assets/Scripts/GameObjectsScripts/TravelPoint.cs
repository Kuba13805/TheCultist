using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelPoint : BaseInteractableObject
{
   private enum pointType
   {
      Local,
      Global
   };

   private enum objectType
   {
      Door,
      Ladder,
      Stairs
   }

   [SerializeField] private pointType PointType;
   [SerializeField] private objectType ObjectType;
   public Scene destinantionScene;
   

}
