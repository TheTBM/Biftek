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
		SoundEngineWrapper.LoadSound("music", false, false, true);
		SoundEngineWrapper.LoadSound("fireball_shoot", false, false, false);
		SoundEngineWrapper.LoadSound("fireball_fly", false, false, false);
		SoundEngineWrapper.LoadSound("fireball_explode", false, false, false);
		//SoundEngineWrapper.PlayASound("music", 0, false, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		SoundEngineWrapper.UpdateSE();
	}

	void OnApplicationQuit()
	{
		SoundEngineWrapper.StopAllChannels();
		SoundEngineWrapper.UnloadSound("music");
		SoundEngineWrapper.UnloadSound("fireball_shoot");
		SoundEngineWrapper.UnloadSound("fireball_fly");
		SoundEngineWrapper.UnloadSound("fireball_explode");
	}
}
