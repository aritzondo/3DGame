using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;


[ExecuteInEditMode]
public class LevelOrganizer : MonoBehaviour
{
#if UNITY_EDITOR

    private Scene sceneOrganizer;
    private string[] scene_paths;

    void Start()
    {
        sceneOrganizer = EditorSceneManager.GetSceneByName("Organizer");


        if (!Application.isPlaying && EditorSceneManager.GetActiveScene() == sceneOrganizer)
        {
            int number_scenes = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
            Debug.Log(number_scenes);

            scene_paths = new string[number_scenes];

            for (int i = 0; i < number_scenes; i++)
            {
                scene_paths[i] = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);
                string toLoadScene_path = scene_paths[i];

                loadScene(toLoadScene_path);
            }

        }
    }

    private void loadScene(string path)
    {
        Scene loaded = EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
        Debug.Log("LOAD Scene: " + loaded.name);
    }

#endif
}

