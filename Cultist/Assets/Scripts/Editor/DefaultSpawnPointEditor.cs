using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(TravelPoint))]
public class DefaultSpawnPointEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TravelPoint travelPoint = (TravelPoint)target;
        
        if (IsObjectInActiveScene(travelPoint.gameObject))
        {
            if (travelPoint.isDefaultSpawnPoint)
            {
                GUIStyle greenTextStyle = new GUIStyle();
                greenTextStyle.normal.textColor = Color.green;
                
                GUILayout.Label("Default spawn point", greenTextStyle);

                
                TravelPoint[] allTravelPoints = FindObjectsOfType<TravelPoint>();
                foreach (TravelPoint otherTravelPoint in allTravelPoints)
                {
                    if (otherTravelPoint != travelPoint && otherTravelPoint.isDefaultSpawnPoint)
                    {
                        otherTravelPoint.isDefaultSpawnPoint = false;
                        EditorUtility.SetDirty(otherTravelPoint);
                    }
                }
            }
        }

        DrawDefaultInspector();
    }

    // Funkcja sprawdzająca, czy obiekt jest w aktywnej scenie
    private bool IsObjectInActiveScene(GameObject obj)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        return obj.scene == activeScene;
    }
}
