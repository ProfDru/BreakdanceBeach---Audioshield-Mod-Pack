using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakdanceBeach
{
	[BepInPlugin("com.prof.dru", "HarmonyInitializer", "1.0.0.0")]
	public class HarmonyInitializer : BaseUnityPlugin
	{
		Harmony h;

		public void Awake()
		{
			Harmony.DEBUG = true;
			h = new Harmony("com.prof.trueharmony");
			h.PatchAll();

			UnityEngine.Debug.Log("[Dru] Harmony patches have been (hopefully) applied! RIGHT?!");
		}
	}

}
