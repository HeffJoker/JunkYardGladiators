using UnityEngine;
using System.Collections;
using InControl;

public class MouseKeyboardMapping : CustomInputDeviceProfile {

	public MouseKeyboardMapping()
	{
		Name = "Mouse/Keyboard mapping";
		Sensitivity = 1f;
		LowerDeadZone = 0f;
		UpperDeadZone = 1f;

		ButtonMappings = new InputControlMapping[]
		{
			new InputControlMapping()
			{
				Handle = "Attack - Left Hand",
				Target = InputControlType.LeftTrigger,
				Source = new UnityMouseButtonSource(0)
			},
			new InputControlMapping()
			{
				Handle = "Attack - Right Hand",
				Target = InputControlType.RightTrigger,
				Source = new UnityMouseButtonSource(1)
			},
			new InputControlMapping()
			{
				Handle = "Drop Weapon - Left Hand",
				Target = InputControlType.LeftBumper,
				Source = new UnityKeyCodeSource(KeyCode.LeftControl)
			},
			new InputControlMapping()
			{
				Handle = "Drop Weapon - Right Hand",
			    Target = InputControlType.RightBumper,
				Source = new UnityKeyCodeSource(KeyCode.Space)
			},
			new InputControlMapping()
			{
				Handle = "Pause Button",
				Target = InputControlType.Start,
				Source = new UnityKeyCodeSource(KeyCode.Escape)
			}
		};

		AnalogMappings = new InputControlMapping[]
		{
			new InputControlMapping()
			{
				Handle = "Move Left X",
				Target = InputControlType.LeftStickLeft,
				Source = new UnityKeyCodeSource(KeyCode.A)
			},
			new InputControlMapping()
			{
				Handle = "Move Right X",
				Target = InputControlType.LeftStickRight,
				Source = new UnityKeyCodeSource(KeyCode.D)
			},
			new InputControlMapping()
			{
				Handle = "Move Up Y",
				Target = InputControlType.LeftStickUp,
				Source = new UnityKeyCodeSource(KeyCode.W)
			},
			new InputControlMapping()
			{
				Handle = "Move Down Y",
				Target = InputControlType.LeftStickDown,
				Source = new UnityKeyCodeSource(KeyCode.S)
			},
			/*
			new InputControlMapping()
			{
				Handle = "Look X",
				Target = InputControlType.RightStickLeft | InputControlType.RightStickRight,
				Source = MouseXAxis,
			},
			new InputControlMapping()
			{
				Handle = "Look Y",
				Target = InputControlType.RightStickUp | InputControlType.RightStickDown,
				Source = MouseYAxis,
			}*/
		};
	}
}
