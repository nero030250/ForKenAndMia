using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum KeyType {
	TiD,
	Do,
	Re,
	Mi,
	Fa,
	So,
	La,
	Ti_0,
	Ti_1,
	DoU,
}

public class KeyConfig {
	public KeyType Type { get; private set; }
	public string Sound { get; private set; }
	public string Icon { get; private set; }

	public KeyConfig (KeyType type, string sound, string icon) {
		Type = type;
		Sound = sound;
		Icon = icon;
	}
}