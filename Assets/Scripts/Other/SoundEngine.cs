using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundEnginePluginWrapper;

public class SoundEngine : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		SoundEngineWrapper.Initialize();
		SoundEngineWrapper.LoadSound("boulder_crumble", false, false, false);
		SoundEngineWrapper.LoadSound("boulder_roll", false, false, false);
		SoundEngineWrapper.LoadSound("boulder_shoot", false, false, false);
		SoundEngineWrapper.LoadSound("event_start", false, false, false);
		SoundEngineWrapper.LoadSound("event_win", false, false, false);
		SoundEngineWrapper.LoadSound("fireball_explode", false, false, false);
		SoundEngineWrapper.LoadSound("fireball_fly", false, false, false);
		SoundEngineWrapper.LoadSound("fireball_shoot", false, false, false);
		SoundEngineWrapper.LoadSound("firerun_alive", false, true, false);
		SoundEngineWrapper.LoadSound("hailstorm", false, false, false);
		SoundEngineWrapper.LoadSound("lightning", false, false, false);
		SoundEngineWrapper.LoadSound("music", false, false, true);
		SoundEngineWrapper.LoadSound("player_dash", false, false, false);
		SoundEngineWrapper.LoadSound("player_dash_hit", false, false, false);
		SoundEngineWrapper.LoadSound("player_die", false, false, false);
		SoundEngineWrapper.LoadSound("player_hurt", false, false, false);
		SoundEngineWrapper.LoadSound("player_pickup", false, false, false);
		SoundEngineWrapper.LoadSound("player_respawn", false, false, false);
		SoundEngineWrapper.LoadSound("shield_activate", false, false, false);
		SoundEngineWrapper.LoadSound("shield_active", false, false, false);
		SoundEngineWrapper.LoadSound("shield_deactivate", false, false, false);
		SoundEngineWrapper.LoadSound("wall_create", false, false, false);
		SoundEngineWrapper.LoadSound("wall_destroy", false, false, false);

		//SoundEngineWrapper.PlayASound("music", 0, false, 0);

		SoundEngineWrapper.SetNumSoundsToPlay(5);
	}
	
	// Update is called once per frame
	void Update ()
	{
		SoundEngineWrapper.UpdateSE();
	}

	void OnApplicationQuit()
	{
		SoundEngineWrapper.StopAllChannels();
		SoundEngineWrapper.ClearQueue();
		SoundEngineWrapper.UnloadAllSounds();
	}
}
