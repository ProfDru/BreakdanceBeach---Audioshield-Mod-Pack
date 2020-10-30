using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;
namespace BreakdanceBeach.Spraycan
{

	/// <summary>
	/// Triggers events when a new session starts or ends.
	/// </summary>
	/// 
	[HarmonyPatch]
	[HarmonyPatch(typeof(SceneManager), "SetScene")]

	public class SessionStartWatcher
	{

		/// <summary>
		/// Tell session manager that a new session has started
		/// </summary>
		public static void Postfix()
		{
			int length2 = Application.dataPath.LastIndexOf('/');
			var directory = Application.dataPath.Substring(0, length2) + "/" + RingDesignManager.selectedSkinRelativePath + "/";
			UnityEngine.Debug.LogWarning("New session started at " + directory);

			Session.NewSessionStart(directory);
		}

	}

	[HarmonyPatch(typeof(SceneManager), "OnEndCleanup")]
	public class SessionEndWatcher
	{
		/// <summary>
		/// Trigger cleanup when the session ends
		/// </summary>
		public static void Postfix()
		{
			UnityEngine.Debug.LogWarning("Session End! ");
			Session.SessionEnd();
		}
	}
}