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
		static Camera spec_camera;
		public static void SessionStart()
		{
			DestroySpectatorHud();
			SetSpectatorCamera();
		}

		/// <summary>
		/// Delete the default spectator hud
		/// </summary>
		public static void DestroySpectatorHud()
		{
			var spec_hud = GameObject.Find("Canvas_SpectatorHUD");
			spec_hud.SetActive(false);
		}
		public static void SetSpectatorCamera()
		{
			var spec_cam_root = GameObject.Find("CameraHolder1");
			if (!spec_cam_root)
			{
				Debug.LogWarning("Spectator camera root couldn't be found");
				return;
			}

			var spec_container = spec_cam_root.transform.Find("SpectatorCam_VR").gameObject;
			if (!spec_container)
			{
				Debug.LogWarning("Spectator container couldn't be found.");
				return;
			}
			spec_container.SetActive(true);

			spec_camera = spec_container.GetComponent<Camera>();

			if (!spec_camera)
			{
				Debug.LogWarning("Spectator container couldn't be found.");
				return;
			}
			spec_camera.fieldOfView = 120f;
			spec_camera.cullingMask = spec_camera.cullingMask | 31 | 35 | 10;
		}
	}
}
