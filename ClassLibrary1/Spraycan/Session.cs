using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakdanceBeach.Spraycan
{
    /// <summary>
    /// Manages the state of the game. Sets up when a new round starts, end when a round ends.
    /// </summary>
    public static class Session
    {
        static AssetBundleHandler Bundler;

        /// <summary>
        /// A new session started at the given directory
        /// </summary>
        /// <param name="directory"></param>
        public static void NewSessionStart(String directory)
        {
            Bundler = new AssetBundleHandler(directory);
        }

        public static void SessionEnd()
        {
            Bundler.Cleanup();
        }

    }
}
