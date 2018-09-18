using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class ControllerPluginWrapper : MonoBehaviour
{
	const string DLL_NAME = "ControllerPlugin";

	[DllImport(DLL_NAME)]
	public static extern void SetIndex(int index);

	[DllImport(DLL_NAME)]
	public static extern void UpdateController();

	[DllImport(DLL_NAME)]
	public static extern void RefreshState();

	[DllImport(DLL_NAME)]
	public static extern bool LStick_InDeadZone();

	[DllImport(DLL_NAME)]
	public static extern bool RStick_INDeadZone();

	[DllImport(DLL_NAME)]
	public static extern float LeftStick_X();

	[DllImport(DLL_NAME)]
	public static extern float LeftStick_Y();

	[DllImport(DLL_NAME)]
	public static extern float RightStick_X();

	[DllImport(DLL_NAME)]
	public static extern float RightStick_Y();

	[DllImport(DLL_NAME)]
	public static extern float LeftTrigger();

	[DllImport(DLL_NAME)]
	public static extern float RightTrigger();

	[DllImport(DLL_NAME)]
	public static extern bool GetButtonPressed(int button);

	[DllImport(DLL_NAME)]
	public static extern bool GetPrevButtonDown(int button);

	[DllImport(DLL_NAME)]
	public static extern bool GetPrevLeftStickMoveX(float threshold);

	[DllImport(DLL_NAME)]
	public static extern bool GetPrevLeftStickMoveY(float threshold);

	[DllImport(DLL_NAME)]
	public static extern bool GetPrevRightStickMoveX(float threshold);

	[DllImport(DLL_NAME)]
	public static extern bool GetPrevRightStickMoveY(float threshold);

	[DllImport(DLL_NAME)]
	public static extern bool GetButtonDown(int button);

	[DllImport(DLL_NAME)]
	public static extern int GetIndex();

	[DllImport(DLL_NAME)]
	public static extern bool Connected();

	[DllImport(DLL_NAME)]
	public static extern void Rumble(float leftMotor, float rightMotor);

	// Update is called once per frame
	void Update ()
	{
		
	}
}
