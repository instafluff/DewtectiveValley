using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;

namespace DewtectiveValley
{
    public class ModEntry : Mod
    {
        public override void Entry( IModHelper helper )
        {
			helper.Events.GameLoop.GameLaunched += GameLoop_GameLaunched;
			helper.Events.Input.ButtonPressed += Input_ButtonPressed;
			helper.Events.Input.ButtonReleased += Input_ButtonReleased;
			helper.Events.GameLoop.UpdateTicking += GameLoop_UpdateTicking;
			helper.Events.GameLoop.UpdateTicked += GameLoop_UpdateTicked;

			//// create with random manifest data
			//IContentPack contentPack = this.Helper.ContentPacks.CreateFake(
			//   directoryPath: Path.Combine( this.Helper.DirectoryPath, "content-pack" ),
			//);

			//// create with given manifest fields
			//IContentPack contentPack = this.Helper.ContentPacks.CreateTemporary(
			//   directoryPath: Path.Combine( this.Helper.DirectoryPath, "content-pack" ),
			//   id: Guid.NewGuid().ToString( "N" ),
			//   name: "temporary content pack",
			//   description: "...",
			//   author: "...",
			//   version: new SemanticVersion( 1, 0, 0 )
			//);
		}

		string[] characters = new string[] { "Abigail", "Shane", "Willy", "Gus" };
		string[] numbers = new string[] { "Zero", "One", "Two", "Three", "Four", "Five" };
		Dictionary<string, string> dialogue = new Dictionary<string, string>();

		private void GameLoop_GameLaunched( object sender, GameLaunchedEventArgs e )
		{
			// TODO: Try setting a schedule and see if it can trigger a location-specific dialogue!

			Console.WriteLine( "Game Has Launched" );

			var api = this.Helper.ModRegistry.GetApi<IContentPatcherAPI>( "Pathoschild.ContentPatcher" );
			api.RegisterToken( this.ModManifest, "PlayerName", () =>
			{
				if( Context.IsWorldReady )
					return new[] { Game1.player.Name };
				if( SaveGame.loaded?.player != null )
					return new[] { SaveGame.loaded.player.Name }; // lets token be used before save is fully loaded
				return null;
			} );

			foreach( var character in characters )
			{
				dialogue.Add( character, string.Format( "TESTING {0} DIALOGUE", character ) );
				api.RegisterToken( this.ModManifest, character + "Intro", () => {
					var ch = character;
					if( Context.IsWorldReady )
						return new[] { dialogue[ ch ] + " Intro" };
					if( SaveGame.loaded?.player != null )
						return new[] { dialogue[ ch ] + " Intro" }; // lets token be used before save is fully loaded
					return null;
				} );
				api.RegisterToken( this.ModManifest, character + "Rain", () => {
					var ch = character;
					if( Context.IsWorldReady )
						return new[] { dialogue[ ch ] + " Rain" };
					if( SaveGame.loaded?.player != null )
						return new[] { dialogue[ ch ] + " Rain" }; // lets token be used before save is fully loaded
					return null;
				} );
				for( var i = 0; i < 6; i++ )
				{
					api.RegisterToken( this.ModManifest, character + numbers[ i ], () => {
						var ch = character;
						if( Context.IsWorldReady )
							return new[] { dialogue[ ch ] };
						if( SaveGame.loaded?.player != null )
							return new[] { dialogue[ ch ] }; // lets token be used before save is fully loaded
						return null;
					} );
				}
			}

			foreach( IContentPack contentPack in this.Helper.ContentPacks.GetOwned() )
			{
				this.Monitor.Log( $"Reading content pack: {contentPack.Manifest.Name} {contentPack.Manifest.Version} from {contentPack.DirectoryPath}" );
			}
		}

		private void Input_ButtonPressed( object sender, ButtonPressedEventArgs e )
		{
			if( Context.IsWorldReady && Context.CanPlayerMove )
			{
			}
		}

		private void Input_ButtonReleased( object sender, ButtonReleasedEventArgs e )
		{
			if( Context.IsWorldReady && Context.CanPlayerMove )
			{
			}
		}

		private void GameLoop_UpdateTicking( object sender, UpdateTickingEventArgs e )
		{
			if( Context.IsWorldReady && Context.CanPlayerMove )
			{
			}
		}

		private void GameLoop_UpdateTicked( object sender, UpdateTickedEventArgs e )
		{
			if( Context.IsWorldReady && Context.CanPlayerMove )
			{
			}
		}
	}
}
