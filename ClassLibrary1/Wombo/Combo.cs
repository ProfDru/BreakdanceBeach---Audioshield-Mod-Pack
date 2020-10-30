using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BreakdanceBeach.Wombo
{

    /// <summary>
    /// A way to record hits for potential logging to file later
    /// </summary>
    public struct Hit
    {
        Vector3 pos;              // Position of the hit
        Vector3 direction;        // The direction of the punch
        float power;              // how strong the punch was
        Vector3 rotational_speed; // How much the person's hand was rotating
        float time;               //Seconds since song start that the hit occured
    }
    /// <summary>
    /// Keeps track of the user's current combo, hit percentage, etc.
    /// </summary>
    public static class ComboManager
    {
        static int hits;          // Total number of hits
        static int misses;        // Total number of misses
        static float hype;        // A measure of how exciting the current song is at the current moment
        static float performance; // A measure of how well the player is doing

        /// <summary>
        /// Reset hit and missses. Start fresh
        /// </summary>
        public static void StartNewSong()
        {
            hits = 0;
            misses = 0;
            hype = 0;
            performance = 0;
        }
        /// <summary>
        /// Record a hit during the current song
        /// </summary>
        /// <param name="hit"></param>
        public static void RecordHit(VR_ControllerShields.ImpactType it, Vector3 pos, Vector3 forward, float punchStrength, Vector3 rotationalSpeed)
        {
            hits++;
        } 

        /// <summary>
        /// Record a miss in the current song
        /// </summary>
        public static void RecordMiss()
        {
            misses++;
        }

        /// <summary>
        /// Eh, play something fun
        /// </summary>
        public static void PlayFanfare()
        {
            var SEM = SoundEffectManager.instance;
            SEM.PlaySound("crowdroar", 1f, 1f, 0f);
        }
    }
}
