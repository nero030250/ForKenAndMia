﻿using UnityEngine;
using System.Collections;

public class SystemDelegate {
	public delegate void VoidDelegate ();
	public delegate void KeyCodeDelegate (KeyCode key);
	public delegate void OnItemInitilaze (GameObject obj, int index);
	public delegate bool BoolIsTarget (GameObject obj);
	public delegate bool BoolIsTarget<T> (T obj);
	public delegate void VoidBoolDelegate (bool value);
	public delegate void ColliderDelegate (Collider e);
}