using UnityEngine;
using System.Collections;

public class MissionItem : UICollectController {
	private UIButton btn;
	private TweenAlpha unlockAnim;

	private int missionId;
	private bool isUnlock;

	protected override void Awake () {
		base.Awake ();
		unlockAnim = GetChildComponent <TweenAlpha> ("Lock");
		btn = GetComponent<UIButton> ();
		EventDelegate.Add (btn.onClick, OnBtnClick);
	}

	public void Init (int mId, bool anim) {
		missionId = mId;
		isUnlock = MissionManager.Instance.IsMissionUnlock (missionId);
		if (isUnlock) {
			if (anim) {
				unlockAnim.enabled = true;
				unlockAnim.PlayForward ();
			} else {
				unlockAnim.GetComponent<UIWidget> ().alpha = 0f;
			}
		}
	}

	private void OnBtnClick () {
		if (!isUnlock)
			return;
		MissionSelectController.Instance.OnItemClick (missionId);
	}
}