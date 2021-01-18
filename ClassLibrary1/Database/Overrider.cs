using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;

namespace BreakdanceBeach.Database
{
	/// <summary>
	/// 
	/// 
	/// </summary>
	[HarmonyPatch]
	[HarmonyPatch(typeof(ASDatabase), "GetFavoriteSongs")]
	public class DBRedirector
	{
		private static string NextDBString = "";

		public static void SetNextDBString(string str)
		{
			NextDBString = str;
		}

		private static bool HasPendingQuery()
		{
			UnityEngine.Debug.Log("Checking Pending Query: " + NextDBString.Length.ToString());
			return NextDBString.Length != 0;
		}

		private static void ResetDBString()
		{
			NextDBString = "";
		}

		public static bool Prefix(ASDatabase __instance, ref Song[] __result, ref SQLiteDB ___db)
		{
			if (HasPendingQuery())
			{
				UnityEngine.Debug.Log("Satisfying Pending Query");
				UnityEngine.Debug.Log(___db.ToString());

				// If we have a pending query, return that instead of the actual favorite songs
				SQLiteQuery query = new SQLiteQuery(___db, NextDBString);
				UnityEngine.Debug.Log("Running Extract");
				List<Song> songs = ReverseMethods.ExtractSongsFromQuery(__instance, query);
				UnityEngine.Debug.Log("Extract Success: " + songs.Count().ToString());
				__result = songs.ToArray();
				ResetDBString();

				return false;
			}
			else
			{
				// otherwise just run the original function
				UnityEngine.Debug.Log("Ignoring pending query");
				return true;
			}

		}
	}
}
