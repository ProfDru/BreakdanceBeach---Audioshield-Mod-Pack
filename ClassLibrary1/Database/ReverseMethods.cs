using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;

namespace BreakdanceBeach.Database
{
	[HarmonyPatch]
	class ReverseMethods
	{
		[HarmonyReversePatch]
		[HarmonyPatch(typeof(ASDatabase), "ExtractSongsFromQuery")]
		public static List<Song> ExtractSongsFromQuery(object instance, SQLiteQuery sql)
		{
			return new List<Song>();
		}
	}
}
