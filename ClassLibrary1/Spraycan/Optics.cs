using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BreakdanceBeach.Spraycan
{

	/// <summary>
	/// Provides control for spectator cameras
	/// </summary>
	static class Optics
	{
		public static void SessionStart()
		{
			DestroySpectatorHud();
		}

		/// <summary>
		/// Delete the default spectator hud
		/// </summary>
		public static void DestroySpectatorHud()
		{
			var spec_hud = GameObject.Find("Canvas_SpectatorHUD");
			spec_hud.SetActive(false);
		}

	}
}
