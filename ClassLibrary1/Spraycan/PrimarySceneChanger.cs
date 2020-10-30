using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace BreakdanceBeach.Spraycan
{
    /// <summary>
    /// Only exists to change the active scene. This was an attempt to get the skybox/lighting to change.
    /// </summary>
    public class ActiveSceneChanger : MonoBehaviour
    {

        static void Awake()
        {
        }

        public IEnumerator WaitForSceneLoad(Scene scene, GameObject Parent)
        {
            UnityEngine.Debug.Log("Gotten to the load scene");
            while (!scene.isLoaded)
            {
                yield return null;
            }
            Debug.Log("Setting active scene..");
            bool success = UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);
            if (!success)
            {
                UnityEngine.Debug.LogError("Scene " + scene.path + " was not loaded correctly");
            }
            else
            {
                UnityEngine.Debug.LogWarning("Scene " + scene.path + " set as the active scene!");
            }

            DynamicGI.UpdateEnvironment();

            Destroy(Parent);
        }

        /// <summary>
        /// Set this scene as the active scene at the next possible frame.   
        /// </summary>
        /// <param name="scene"></param>
        public void SetActiveSceneNextFrame(Scene scene, GameObject Parent)
        {
            Debug.Log("Holding scene.");
            StartCoroutine(WaitForSceneLoad(scene, Parent));
        }


    }
}
