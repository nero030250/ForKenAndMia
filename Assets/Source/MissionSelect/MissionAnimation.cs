using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class MissionAnimation : UICollectController {
	private static int lastMissionID = 0;

	private UISpriteAnimation boy;
	private UISpriteAnimation girl;

	private GameObject marriedObj;
	private GameObject edObj;
	private GameObject conObj;

	private const float distance = 483;

	protected override void Awake () {
		base.Awake ();
		boy = GetChildComponent <UISpriteAnimation> ("Boy");
		boy.Pause ();
		girl = GetChildComponent <UISpriteAnimation> ("Girl");
		girl.Pause ();

		marriedObj = GetGameObject ("Married");
		edObj = GetGameObject ("ED");
		EventDelegate.Add (GetButton ("ED").onClick, OnEdClick);
		conObj = GetGameObject ("Con");
	}

	void OnDestroy () {
		SoundManager.Instance.Stop ("ED");
	}

	private bool isEd = false;
	private void OnEdClick () {
		if (isEd)
			return;
		isEd = true;
		edObj.SetActive (true);
		SoundManager.Instance.Stop ("Select");
		SoundManager.Instance.Play ("ED");
		conObj.SetActive (true);
	}

	public void Init (int missionId) {
		if (missionId == 6) {
			boy.gameObject.SetActive (false);
			girl.gameObject.SetActive (false);
			marriedObj.SetActive (true);
			edObj.SetActive (true);
		} else if (lastMissionID != missionId - 1) {
			boy.transform.localPosition = PersonPos (PersonType.Man, missionId);
			girl.transform.localPosition = PersonPos (PersonType.Woman, missionId);
		} else {
			boy.transform.localPosition = PersonPos (PersonType.Man, missionId - 1);
			boy.Play ();
			HOTween.To (boy.transform, 2f, new TweenParms ().Prop ("localPosition", PersonPos (PersonType.Man, missionId)).Ease (EaseType.Linear).OnComplete (() => boy.Pause ()));

			girl.transform.localPosition = PersonPos (PersonType.Woman, missionId - 1);
			girl.Play ();
			HOTween.To (girl.transform, 2f, new TweenParms ().Prop ("localPosition", PersonPos (PersonType.Woman, missionId)).Ease (EaseType.Linear).OnComplete (() => girl.Pause ()));
		}
		lastMissionID = missionId;
	}

	private Vector3 PersonPos (PersonType person, int misId) {
		float dis = (5f - misId) / 4f * distance;
		return new Vector3 (person == PersonType.Man ? -dis : dis, 0, 0);
	}

	public int testId;
	[ContextMenu ("Test")]
	public void Test () {
		Init (testId);
	}
}