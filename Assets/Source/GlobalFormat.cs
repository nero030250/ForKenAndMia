using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalFormat {
	public static string GetKeySpriteName (KeyType type) {
		return type.ToString ();
	}

	public static long GetCurTick () {
		return System.DateTime.Now.Ticks / 10000000;
	}
}