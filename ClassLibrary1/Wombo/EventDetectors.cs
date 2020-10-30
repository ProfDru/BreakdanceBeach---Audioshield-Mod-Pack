using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;
namespace BreakdanceBeach
{
	namespace Wombo
	{
		[HarmonyPatch(typeof(ShieldSparksManager))]
		[HarmonyPatch("FireSparks")]
		[HarmonyPatch(MethodType.Normal)]
		[HarmonyPatch(new Type[] { typeof(VR_ControllerShields.ImpactType), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(Vector3) })]
		/// <summary>
		/// Where there's sparks, there's a hit. Record when hits occur
		/// </summary>
		class HitDetector
		{
			public static void Prefix(VR_ControllerShields.ImpactType it, Vector3 pos, Vector3 forward, float punchStrength, Vector3 rotationalSpeed)
			{
				ComboManager.RecordHit(it, pos, forward, punchStrength, rotationalSpeed);
			}
		}

		[HarmonyPatch(typeof(VR_ControllerShields))]
		[HarmonyPatch("OnBatchRenderedImpact")]
		[HarmonyPatch(MethodType.Normal)]
		[HarmonyPatch(new Type[] { typeof(BatchRenderEveryFrame.BREImpactInfo) })]
		/// <summary>
		/// Detect when misses occur
		/// </summary>
		class MissDetector
		{
			public static void Prefix(BatchRenderEveryFrame.BREImpactInfo breInfo)
			{
				ComboManager.RecordMiss();
			}
		}
	}
}
