using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using System.Diagnostics;

namespace BreakdanceBeach.Database
{

	/// <summary>
	/// Handles everything related to the new information storesd in local scores
	/// </summary>
	[HarmonyPatch]
	class LocalScoreEnhancer
	{

		/// <summary>
		///  Runs after the database has initialized. Checks if the tables already have 
		///  our edits or not and alters them to add new fields
		/// </summary>
		[HarmonyPatch(typeof(ASDatabase), "OneTimeInit"), HarmonyPostfix]
		static void TableCreationHook(SQLiteDB ___db)
		{

			if (NeedToAlter(___db))
			{
				AddNewColumns(___db);

				// If this went through properly, we should no longer need to alter. 
				Debug.Assert(!NeedToAlter(___db));
			}
		}

		/// <summary>
		/// Checks if we need to alter a specific table
		/// </summary>
		static bool NeedToAlter(SQLiteDB db)
		{
			// This can be any new field. I'm using time for now
			string field_to_check_for = "time";


			SQLiteQuery tbl_info_query = new SQLiteQuery(db, "PRAGMA table_info(ls_rides)");

			// Step through the query and check if time exists
			while (tbl_info_query.Step())
			{
				string field_name = tbl_info_query.GetString("name");

				if (field_name == field_to_check_for)
					return false;
			}


			// If we got here, time doesn't exist in the fields so this table needs to be altered
			return true;
		}


		/// <summary>
		/// Adds new columns to the database. 
		/// </summary>
		static void AddNewColumns(SQLiteDB db)
		{
			String[] alter_stmnts =
			{
				"ALTER TABLE ls_rides ADD COLUMN technical REAL",
				"ALTER TABLE ls_rides ADD COLUMN artistic REAL",
				"ALTER TABLE ls_rides ADD COLUMN physical_activity REAL",
				"ALTER TABLE ls_rides ADD COLUMN time DATETIME",
			};

			// HACK HACK HACK 
			// No way to do transactions here. If this fails, there's no safety net.
			foreach (String statment in alter_stmnts)
			{
				SQLiteQuery qry = new SQLiteQuery(db, statment);
				qry.Step();
			}

		}

	}
}
