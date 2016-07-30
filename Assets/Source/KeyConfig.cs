using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum KeyType {
	Empty,
	Do,
	Re,
	Mi,
	Fa,
	So,
	La,
	Ti,
	DoU,
	TiD,
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