using System;
using System.Collections.Generic;
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
        }

		private void GameLoop_GameLaunched( object sender, GameLaunchedEventArgs e )
		{
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
