// By Nerun
// version 1.0
using System;
using System.IO;
using System.Text;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Engines.CannedEvil;

namespace Server.Scripts.Commands
{
	public class GenChampionSpawn
	{
		public static void Initialize()
		{
			Server.Commands.Register( "GenChampions" , AccessLevel.Administrator, new CommandEventHandler( Champ_OnCommand ) );
		}

		[Usage( "GenChampions" )]
		[Description( "Spawns Champion Spawns for all maps." )]
		public static void Champ_OnCommand( CommandEventArgs e )
		{
			Map map1 = Map.Ilshenar;
			Map map2 = Map.Felucca;
			//Map map3 = Map.Maps[4];

			e.Mobile.SendMessage( "Generating Champions spawns..." );

// -ILSHENAR-----------------------------------------------------------
			ChampionSpawn valor = new ChampionSpawn();
			ChampionSpawn humility = new ChampionSpawn();
			ChampionSpawn forest = new ChampionSpawn();
// -FELUCCA------------------------------------------------------------
			ChampionSpawn Despise = new ChampionSpawn();
			ChampionSpawn Deceit = new ChampionSpawn();
			ChampionSpawn Destard = new ChampionSpawn();
			ChampionSpawn Fire = new ChampionSpawn();
			ChampionSpawn TerathanKeep = new ChampionSpawn();
			ChampionSpawn LostLands1 = new ChampionSpawn();
			ChampionSpawn LostLands2 = new ChampionSpawn();
			ChampionSpawn LostLands3 = new ChampionSpawn();
			ChampionSpawn LostLands4 = new ChampionSpawn();
			ChampionSpawn LostLands5 = new ChampionSpawn();
			ChampionSpawn LostLands6 = new ChampionSpawn();
			ChampionSpawn LostLands7 = new ChampionSpawn();
			ChampionSpawn LostLands8 = new ChampionSpawn();
			ChampionSpawn LostLands9 = new ChampionSpawn();
			ChampionSpawn LostLands10 = new ChampionSpawn();
			ChampionSpawn LostLands11 = new ChampionSpawn();
			ChampionSpawn LostLands12 = new ChampionSpawn();
// -TOKUNO-------------------------------------------------------------
			//ChampionSpawn isamu = new ChampionSpawn();
// --------------------------------------------------------------------

			valor.RandomizeType = true;
			valor.Active = true;
			valor.SpawnRange = 24;
			valor.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			valor.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			valor.MoveToWorld( new Point3D( 382, 328, -30 ), map1 );

			humility.RandomizeType = true;
			humility.Active = true;
			humility.SpawnRange = 24;
			humility.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			humility.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			humility.MoveToWorld( new Point3D( 462, 926, -67 ), map1 );

			forest.Active = true;
			forest.SpawnRange = 24;
			forest.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			forest.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			forest.RandomizeType = false;
			forest.Type = ChampionSpawnType.ForestLord;
			forest.MoveToWorld( new Point3D( 1645, 1107, 8 ), map1 );

			e.Mobile.SendMessage( "Ilshenar Champions Complete." );

			Despise.Active = true;
			Despise.SpawnRange = 24;
			Despise.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			Despise.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			Despise.RandomizeType = false;
			Despise.Type = ChampionSpawnType.VerminHorde;
			Despise.MoveToWorld( new Point3D( 5557, 824, 65 ), map2 );

			Deceit.Active = true;
			Deceit.SpawnRange = 24;
			Deceit.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			Deceit.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			Deceit.RandomizeType = false;
			Deceit.Type = ChampionSpawnType.UnholyTerror;
			Deceit.MoveToWorld( new Point3D( 5178, 708, 20 ), map2 );

			Destard.Active = true;
			Destard.SpawnRange = 24;
			Destard.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			Destard.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			Destard.RandomizeType = false;
			Destard.Type = ChampionSpawnType.ColdBlood;
			Destard.MoveToWorld( new Point3D( 5259, 837, 61 ), map2 );

			Fire.Active = true;
			Fire.SpawnRange = 24;
			Fire.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			Fire.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			Fire.RandomizeType = false;
			Fire.Type = ChampionSpawnType.Abyss;
			Fire.MoveToWorld( new Point3D( 5814, 1350, 2 ), map2 );

			TerathanKeep.Active = true;
			TerathanKeep.SpawnRange = 24;
			TerathanKeep.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			TerathanKeep.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			TerathanKeep.RandomizeType = false;
			TerathanKeep.Type = ChampionSpawnType.Arachnid;
			TerathanKeep.MoveToWorld( new Point3D( 5190, 1605, 20 ), map2 );

			e.Mobile.SendMessage( "Felucca Dungeon Champions complete" );

			LostLands1.Active = true;
			LostLands1.SpawnRange = 24;
			LostLands1.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			LostLands1.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			LostLands1.RandomizeType = true;
			LostLands1.MoveToWorld( new Point3D( 5511, 2360, 40 ), map2 );

			LostLands2.Active = true;
			LostLands2.SpawnRange = 24;
			LostLands2.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			LostLands2.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			LostLands2.RandomizeType = true;
			LostLands2.MoveToWorld( new Point3D( 6038, 2400, 45 ), map2 );

			LostLands3.Active = true;
			LostLands3.SpawnRange = 24;
			LostLands3.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			LostLands3.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			LostLands3.RandomizeType = true;
			LostLands3.MoveToWorld( new Point3D( 5549, 2640, 18 ), map2 );

			LostLands4.Active = true;
			LostLands4.SpawnRange = 24;
			LostLands4.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			LostLands4.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			LostLands4.RandomizeType = true;
			LostLands4.MoveToWorld( new Point3D( 5636, 2916, 38 ), map2 );

			LostLands5.Active = true;
			LostLands5.SpawnRange = 24;
			LostLands5.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			LostLands5.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			LostLands5.RandomizeType = true;
			LostLands5.MoveToWorld( new Point3D( 6035, 2943, 50 ), map2 );

			LostLands6.Active = true;
			LostLands6.SpawnRange = 24;
			LostLands6.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			LostLands6.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			LostLands6.RandomizeType = true;
			LostLands6.MoveToWorld( new Point3D( 5265, 3171, 107 ), map2 );

			LostLands7.Active = true;
			LostLands7.SpawnRange = 24;
			LostLands7.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			LostLands7.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			LostLands7.RandomizeType = true;
			LostLands7.MoveToWorld( new Point3D( 5282, 3368, 50 ), map2 );

			LostLands8.Active = true;
			LostLands8.SpawnRange = 24;
			LostLands8.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			LostLands8.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			LostLands8.RandomizeType = true;
			LostLands8.MoveToWorld( new Point3D( 5954, 3475, 25 ), map2 );

			LostLands9.Active = true;
			LostLands9.SpawnRange = 24;
			LostLands9.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			LostLands9.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			LostLands9.RandomizeType = true;
			LostLands9.MoveToWorld( new Point3D( 5207, 3637, 20 ), map2 );

			LostLands10.Active = true;
			LostLands10.SpawnRange = 24;
			LostLands10.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			LostLands10.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			LostLands10.RandomizeType = true;
			LostLands10.MoveToWorld( new Point3D( 5559, 3757, 21 ), map2 );

			LostLands11.Active = true;
			LostLands11.SpawnRange = 24;
			LostLands11.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			LostLands11.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			LostLands11.RandomizeType = true;
			LostLands11.MoveToWorld( new Point3D( 5982, 3882, 20 ), map2 );

			LostLands12.Active = true;
			LostLands12.SpawnRange = 24;
			LostLands12.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			LostLands12.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			LostLands12.RandomizeType = true;
			LostLands12.MoveToWorld( new Point3D( 5724, 3991, 41 ), map2 );

			e.Mobile.SendMessage( "Felucca - Lost Lands Champions complete" );
/*
			isamu.Active = true;
			isamu.SpawnRange = 24;
			isamu.RestartDelay = TimeSpan.FromMinutes( 10.0 );
			isamu.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
			isamu.RandomizeType = false;
			isamu.Type = ChampionSpawnType.?;
			isamu.MoveToWorld( new Point3D( 948, 434, 29 ), map3 );

			e.Mobile.SendMessage( "Tokuno Champion complete" );
*/
			e.Mobile.SendMessage( "Generation Complete." );
		}
	}
}