using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace BreakdanceBeach.Breezy
{
    /// <summary>
    /// Move air debris based on intensity. Still WIP. 
    /// </summary>
    public static class Breezer
    {
        static UnityEngine.ParticleSystem ps;
        static void InitBreezer()
        {
            ps = new UnityEngine.ParticleSystem();
            ps.transform.position = new Vector3(0, 1000, 0);
            ps.transform.forward = new Vector3(0, -1, 0);

            var main = ps.main;
            main.maxParticles= 500;
            main.playOnAwake = true;
            
        }
        static void UpdateVelocity()
        {
            var main = ps.main;
        }
    }
}
