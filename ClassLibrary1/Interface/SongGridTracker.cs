using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;

namespace BreakdanceBeach.Interface
{
	public enum SocialState : int
	{
		SAMPLE_SONGS = 0,
		POPULAR_OTHER_GAME = 1,
		POPULAR_MY_FRIENDS = 2
	}

	/// <summary>
	/// Keeps track of which category was selected last in the social boards
	/// </summary>
	[HarmonyPatch]
	[HarmonyPatch(typeof(SongSelector_SongPane), "OnSongGrid_Show")]
	public class SongGridTracker
	{
		private static Dictionary<String, SocialState> state_translator = new Dictionary<string, SocialState>(){
			{"samplesongs", SocialState.SAMPLE_SONGS},
			{"popularothergame", SocialState.POPULAR_OTHER_GAME},
			{"popularwithfriends", SocialState.POPULAR_MY_FRIENDS}
		};

		private static SocialState last_state = SocialState.SAMPLE_SONGS;

		/// <summary>
		/// Get the social state of the last clicked on menu item 
		/// </summary>
		public static SocialState GetSocialState()
		{
			return last_state;
		}

		/// <summary>
		/// Before running this function, log the last social state 
		/// </summary>
		public static bool Prefix(string showCategory)
		{
			UnityEngine.Debug.Log("Updating tracker state.");

			if (state_translator.ContainsKey(showCategory))
				last_state = state_translator[showCategory];

			return true;
		}
	}
}
