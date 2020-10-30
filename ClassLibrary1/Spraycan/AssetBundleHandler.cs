using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary>
/// Anything to do with custom stages/environments
/// </summary>
namespace BreakdanceBeach.Spraycan
{
    /// <summary>
    /// Handles all interactions with asset bundles
    /// </summary>
    class AssetBundleHandler
    {

        //string directory;                   // Where the asset bundles are held
        List<UnityEngine.AssetBundle> bundles; /// a list of loaded asset bundles
        List<GameObject> GameObjects;     // A list of loaded game objects
        List<UnityEngine.Object> Objects;     // A list of other objects

        /// <summary>
        /// Initialize Assetbundle handler, load, and instantiate all assetbundles from Directory
        /// </summary>
        /// <param name="Directory"></param>
        public AssetBundleHandler(string Directory)
        {
            bundles = new List<AssetBundle>();
            GameObjects = new List<GameObject>();
            Objects = new List<UnityEngine.Object>();
            LoadBundles(Directory);
        }

        /// <summary>
        /// Load and instantiate all asset bundles in directory
        /// </summary>
        /// <param name="Directory">The directory to load asset bundles from</param>
        public void LoadBundles(string Directory)
        {
            String[] filepaths = System.IO.Directory.GetFiles(Directory);

            if (filepaths.Length < 1)
            {
                Debug.LogWarning("No Asset Bundles found!");
                return;
            }
            foreach (String file in filepaths)
            {
                if (file.Contains("assetbundle"))
                    LoadAsset(file);
            }
        }

        /// <summary>
        /// Unload every contained bundle and destroy their objects
        /// </summary>
        public void Cleanup()
        {
            foreach (var bundle in bundles)
            {
                bundle.Unload(true);
            }

            bundles.Clear();
            GameObjects.Clear();
            Objects.Clear();
        }



        /// <summary>
        /// Load all game objects and subassets into our GameObject list and instantiate them
        /// </summary>
        /// <param name="absolute_path">Direct path to the asset bundle to load</param>
        private void LoadAsset(String absolute_path)
        {
            var asset_bundle = AssetBundle.LoadFromFile(absolute_path);

            // Bail early if this failed to load
            if (asset_bundle == null)
            {
                UnityEngine.Debug.LogError("Failed to load assetbundle " + absolute_path);
                return;
            }
            else
            {
                UnityEngine.Debug.Log("Loaded assetbundle successfully!");
            }

            bundles.Add(asset_bundle);
            if (!asset_bundle.isStreamedSceneAssetBundle)
            {

                var assets = asset_bundle.LoadAllAssets();
                UnityEngine.Debug.Log("Iterating through assets");
                foreach (UnityEngine.Object obj in assets)
                {
                    if (obj != null && obj is GameObject)
                    {
                        UnityEngine.Debug.Log("Loading " + obj.name + " from " + absolute_path);
                        var instantiated_obj = UnityEngine.Object.Instantiate(((GameObject)obj));
                        GameObjects.Add(instantiated_obj);
                    }
                    Objects.Add(obj);
                }
            }
            else
            {
                string[] scenepath = asset_bundle.GetAllScenePaths();
                foreach (var sp in scenepath)
                {
                    UnityEngine.Debug.Log("Loading Scene " + sp);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(sp, LoadSceneMode.Additive);
                    var loaded_scene = UnityEngine.SceneManagement.SceneManager.GetSceneByPath(sp);

                    if (!loaded_scene.IsValid())
                        UnityEngine.Debug.LogError("Scene " + sp + "  is not valid!");

                    UnityEngine.GameObject SceneManagerGO = new UnityEngine.GameObject();
                    var asc = SceneManagerGO.AddComponent<ActiveSceneChanger>();
                    asc.SetActiveSceneNextFrame(loaded_scene, SceneManagerGO);
                }
            }
        }
    }
}
