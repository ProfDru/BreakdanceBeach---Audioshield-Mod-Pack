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
				// If we have a pending query, return that instead of the actual favorite songs
				SQLiteQuery query = new SQLiteQuery(___db, NextDBString);
				List<Song> songs = ReverseMethods.ExtractSongsFromQuery(__instance, query);
				__result = songs.ToArray();
				ResetDBString();

				return false;
			}
			else
			{
				// otherwise just run the original function
				return true;
			}

		}
	}
}
