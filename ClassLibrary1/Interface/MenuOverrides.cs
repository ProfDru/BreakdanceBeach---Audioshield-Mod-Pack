﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using BreakdanceBeach.Interface;

namespace BreakdanceBeach.Interface
{
	class MenuOverrides
	{
		/// <summary>
		/// 
		/// </summary>
		[HarmonyPatch]
		[HarmonyPatch(typeof(SongSelector_SongPane), "BuildList_FromSocialBoard")]
		public class Overrider
		{
			const string GetRecentlyAddedSongQuery = "SELECT * FROM songs ORDER BY songs.songid DESC LIMIT 24";
			const string GetRandomUnplayedSongQuery = "SELECT * FROM songs WHERE songs.lastplaytime IS NULL ORDER BY random() LIMIT 24";
			const string GetNewestUnplayedQuery = "SELECT * FROM songs WHERE songs.lastplaytime IS NULL ORDER BY songs.songid DESC LIMIT 24";

			static Dictionary<SocialState, string> state_to_query = new Dictionary<SocialState, string>()
			{
				{ SocialState.POPULAR_MY_FRIENDS, GetRecentlyAddedSongQuery },
				{ SocialState.SAMPLE_SONGS, GetNewestUnplayedQuery},
				{ SocialState.POPULAR_OTHER_GAME, GetRandomUnplayedSongQuery},
			};

			public static bool Prefix(
				SongSelector_SongPane __instance,
				List<SocialContent.SocialboardEntry> socialBoard,
				bool streamingCheck = false
			)
			{
				// Change the query based on the tracker state. This tells us what menu was clicked
				// on before this function was called
				string query = GetRandomUnplayedSongQuery;
				var tracker_state = SongGridTracker.GetSocialState();
				if (state_to_query.ContainsKey(tracker_state))
					query = state_to_query[tracker_state];

				// Set the next query to this, then load the songs
				BreakdanceBeach.Database.DBRedirector.SetNextDBString(query);
				var songs = ASDatabase.instance.GetFavoriteSongs(10, true);
				__instance.StartCoroutine(ReverseMethods.DisplaySongs(__instance, songs));
				return false;
			}
		}
	}
}
