﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : UISingleton <UIManager> {
	public int InitDepth = 10;
	public int DepthIncrement = 10;

	public GameObject Mask;

	private int nextDepth = 0;

	private Stack <UIStackComponent> dialogStack = new Stack<UIStackComponent> ();

	protected override void Awake () {
		base.Awake ();
		nextDepth = InitDepth;
	}

	void Start () {
		WelcomeController.Create ();
	}

	public void Push (UIStackComponent component) {
		dialogStack.Push (component);
		UIPanel panel = component.GetComponent <UIPanel> ();
		panel.depth = nextDepth;
		nextDepth += DepthIncrement;

		NGUITools.AdjustDepth (component.gameObject, 1);
	}

	public void Pop () {
		dialogStack.Pop ();
		nextDepth -= DepthIncrement;
	}
}