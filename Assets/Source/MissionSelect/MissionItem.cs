using UnityEngine;
using System.Collections;

public class MissionItem : UICollectController {
	private UISprite icon;
	private UIButton btn;

	private int missionId;
	private bool isUnlock;

	protected override void Awake () {
		base.Awake ();
		icon = GetSprite ("Icon");
		btn = GetComponent<UIButton> ();
		EventDelegate.Add (btn.onClick, OnBtnClick);
	}

	public void Init (int mId) {
		missionId = mId;
		isUnlock = MissionManager.Instance.IsMissionUnlock (missionId);
		if (!isUnlock)
			icon.spriteName = "Lock";
	}

	private void OnBtnClick () {
		if (!isUnlock)
			return;
		MissionSelectController.Instance.OnItemClick (missionId);
	}
}