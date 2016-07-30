using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BoxItem : UICollectController {
	private UISprite icon;
	private UIButton btn;
	public KeyType Type { get; private set; }
	private bool isPlaying;
	public bool IsPlaying { get { return isPlaying; } }

	protected override void Awake () {
		base.Awake ();
		icon = GetSprite ("Icon");
		btn = GetComponent<UIButton> ();
	}

	public void Init (KeyType type) {
		Type = type;
		icon.spriteName = GlobalFormat.GetKeySpriteName (Type);
	}

	public void OnBoxClik () {
		if (isPlaying)
			return;
		MissionController.Instance.OnBoxClick (this);
	}

	public void Front (bool anim = true) {
		icon.gameObject.SetActive (true);
	}

	public void Back (bool anim = true) {
		icon.gameObject.SetActive (false);
	}
}