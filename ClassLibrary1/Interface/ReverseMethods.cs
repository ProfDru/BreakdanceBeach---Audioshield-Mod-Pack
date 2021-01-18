using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;

namespace BreakdanceBeach.Interface
{

	[HarmonyPatch]
	class ReverseMethods
	{
		[HarmonyReversePatch]
		[HarmonyPatch(typeof(SongSelector_SongPane), "DisplaySongsCO")]
		public static System.Collections.IEnumerator DisplaySongs(object instance, Song[] songs)
		{
			yield break;
		}
	}

}
