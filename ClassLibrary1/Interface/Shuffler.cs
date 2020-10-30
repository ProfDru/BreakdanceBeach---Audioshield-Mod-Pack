using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BreakdanceBeach.Interface
{
    /// <summary>
    /// </summary>
    /// 
    [HarmonyPatch]
    [HarmonyPatch(typeof(SongBrowser_FileSystem), "FinalizeLastExploreIntoFolderAsync")]

    public class FinalizeLastExploreWatcher
    {
        public static SongBrowser_FileSystem.FoldersFiles Postfix(SongBrowser_FileSystem.FoldersFiles __result)
        {
            __result.files = Shuffler.ShuffleList(__result.files);
            return __result;
        }
    }


    [HarmonyPatch]
    [HarmonyPatch(typeof(SongBrowser_FileSystem), "ExploreCurrentFolder")]

    public class ExploreCurrentFolderWatcher
    {
        public static SongBrowser_FileSystem.FoldersFiles Postfix(SongBrowser_FileSystem.FoldersFiles __result)
        {
            __result.files = Shuffler.ShuffleList(__result.files);
            return __result;
        }

    }


    /// <summary>
    /// </summary>
    /// 
    [HarmonyPatch]
    [HarmonyPatch(typeof(SongSelector), "UpdateSongBrowserView")]

    public class BeforeUpdateBrowser
    {
        public static void Prefix()
        {
        }

    }



    /// <summary>
    /// </summary>
    /// 
    [HarmonyPatch]
    [HarmonyPatch(typeof(SongSelector_SongPane), "ChangePaneDisplayTo")]

    public class SettingDirFiles
    {
        /// <summary>
        /// Tell session manager that a new session has started
        /// </summary>
        public static void Prefix(ref Song[] songs)
        {
            songs = Shuffler.ShuffleList(songs);
        }
    }

        /// <summary>
        /// Shuffles the list of songs obtained from disk
        /// </summary>
        static class Shuffler
    {


        public static string[] ShuffleList(string[] songs)
        {
            Random rnd = new Random();
            return songs.OrderBy(x => rnd.Next()).ToArray();
        }
        public static Song[] ShuffleList(Song[] songs)
        {
            Random rnd = new Random();
            return songs.OrderBy(x => rnd.Next()).ToArray();
        }

    }
}
