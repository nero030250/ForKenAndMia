using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class OrderItem : UICollectController {
	private UISprite icon;

	protected override void Awake () {
		base.Awake ();
		icon = GetSprite ("Icon");
	}

	public void Init (KeyType type) {
		icon.spriteName = GlobalFormat.GetKeySpriteName (type);
	}
}