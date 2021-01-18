using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;

namespace BreakdanceBeach.Interface
{
	class MenuOverrides
	{

		/// <summary>
		/// 
		/// 
		/// </summary>
		[HarmonyPatch]
		[HarmonyPatch(typeof(SongSelector_SongPane), "BuildList_FromSocialBoard")]
		public class Overrider
		{
			const string GetRandomSongQuery = "SELECT * FROM songs ORDER BY random() LIMIT 25";
			const string GetRecentlyAddedSongQuery = "SELECT * FROM songs ORDER BY songs.songid DESC LIMIT 25";

			public static bool Prefix(
				SongSelector_SongPane __instance,
				List<SocialContent.SocialboardEntry> socialBoard,
				bool streamingCheck = false
			)
			{
				BreakdanceBeach.Database.DBRedirector.SetNextDBString(GetRecentlyAddedSongQuery);

				var songs = ASDatabase.instance.GetFavoriteSongs(10, true);
				__instance.StartCoroutine(ReverseMethods.DisplaySongs(__instance, songs));
				return false;
			}


			public void LoadRecentlyAddedSongs()
			{

			}

		}


	}
}
