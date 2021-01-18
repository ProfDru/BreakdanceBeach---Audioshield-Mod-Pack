using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;

namespace BreakdanceBeach.Database
{
	class ReverseMethods
	{
		[HarmonyReversePatch]
		[HarmonyPatch(typeof(OriginalCode), "Test")]
		public static void MyTest(object instance, int counter, string name)
		{
			// its a stub so it has no initial content
			throw new NotImplementedException("It's a stub");
		}
	}
