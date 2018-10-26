using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace SoundEnginePluginWrapper
{
	public class SoundEngineWrapper : MonoBehaviour
	{
		const string DLL_NAME = "SoundEnginePlugin";

		[DllImport(DLL_NAME)]
		public static extern void Initialize();

		[DllImport(DLL_NAME)]
		public static extern void UpdateSE();

		[DllImport(DLL_NAME)]
		public static extern void LoadSound(string name, bool threeD, bool looping, bool stream);

		[DllImport(DLL_NAME)]
		public static extern void UnloadSound(string name);

		[DllImport(DLL_NAME)]
		public static extern void Set3DListenerAttributes(int listenerID,
			float posX, float posY, float posZ,
			float velX, float velY, float velZ,
			float fwdX, float fwdY, float fwdZ,
			float upX, float upY, float upZ);

		[DllImport(DLL_NAME)]
		public static extern void PlaySounds(string name,
			float posX, float posY, float posZ,
			float volume);

		[DllImport(DLL_NAME)]
		public static extern void StopChannel(int channel);

		[DllImport(DLL_NAME)]
		public static extern void StopAllChannels();

		[DllImport(DLL_NAME)]
		public static extern void SetChannelPosition(int channelID,
			float posX, float posY, float posZ);

		[DllImport(DLL_NAME)]
		public static extern void SetChannelVolume(int channelID, float volume);

		[DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern System.IntPtr IsPlaying(int channel);

		[DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern System.IntPtr DBToVolume(float dB);

		[DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern System.IntPtr VolumeTodB(float volume);

		[DllImport(DLL_NAME)]
		public static extern void SetSound3DMinMaxDistance(string name, float min, float max);

		[DllImport(DLL_NAME)]
		public static extern void PlayASound(string name, int channelGroup, bool paused, int channelID);

		[DllImport(DLL_NAME)]
		public static extern void SetChannel3DAttributes(int channelID,
			float posX, float posY, float posZ,
			float velX, float velY, float velZ,
			float altX = 0, float altY = 0, float altZ = 0);

		[DllImport(DLL_NAME)]
		public static extern void SetPaused(int channelID, bool isPaused);

		[DllImport(DLL_NAME)]
		public static extern void SetChannelRolloff(int channelID, string type);
	}
}