using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Holoville.HOTween;

public class BoxItem : UICollectController {

	private static long commonClickTime;
	private UISprite icon;
	private UIButton btn;
	private GameObject bg;
	public KeyType Type { get; private set; }
	private bool isFront = true;

	protected override void Awake () {
		base.Awake ();
		icon = GetSprite ("Icon");
		bg = GetGameObject ("BG");
		btn = GetComponent <UIButton> ();
		EventDelegate.Add (btn.onClick, OnBoxClik);
	}

	public void Init (KeyType type) {
		Type = type;
		icon.spriteName = GlobalFormat.GetKeySpriteName (Type);
		Back ();
	}

	public void OnBoxClik () {
		if (isFront)
			return;
		Debug.LogWarning (GlobalFormat.GetCurTick ());
		if (commonClickTime + 2 > GlobalFormat.GetCurTick ())
			return;
		commonClickTime = GlobalFormat.GetCurTick ();
		SoundManager.Instance.Play (Type.ToString ());
		StartCoroutine (_WaitForSec (0.5f, () => {
			Front (() => MissionController.Instance.OnBoxClick (this));
		}));
	}

	private IEnumerator _WaitForSec (float delay, SystemDelegate.VoidDelegate onTime) {
		yield return new WaitForSeconds (delay);
		onTime ();
	}
	public void Front (SystemDelegate.VoidDelegate onComplete = null) {
		isFront = true;
		icon.gameObject.SetActive (true);
		bg.SetActive (false);
		if (onComplete != null)
			StartCoroutine (_WaitForSec (1f, onComplete));
	}

	public void Back () {
		if (!isFront)
			return;
		isFront = false;
		icon.gameObject.SetActive (false);
		bg.SetActive (true);
	}
}