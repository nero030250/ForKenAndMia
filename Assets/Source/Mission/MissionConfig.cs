using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionConfig {
	public KeyType[] Order;
	public KeyType[] Key;

	public void SetOrder (params KeyType[] order) {
		Order = order;
	}

	public void SetKey (params KeyType[] key) {
		Key = key;
	}
}