using UnityEngine;
using System.Collections;

public class MissionSelectController : UISingleton <MissionSelectController> {
	private const string UI_PATH = "Prefab/MissionSelectPanel";

	public static GameObject Create () {
		return UIHelper.Create (UI_PATH);
	}

	private UIGridEx missionGrid;
	private MissionAnimation missionAnimation;
	private static int lastMissionID = 0;

	protected override void Awake () {
		base.Awake ();
		missionGrid = GetGridEx ("MissionGrid");
		missionGrid.OnInitializeItem = OnInitializeMissionItem;
		missionGrid.Resize (MissionManager.Instance.MissionDic.Count, true);
		missionAnimation = GetChildComponent <MissionAnimation> ("Animation");

		EventDelegate.Add (GetButton ("CloseBtn").onClick, () => {
			SoundManager.Instance.Play ("Click");
			Destroy (gameObject);
			WelcomeController.Create ();
		});

		lastMissionID = MissionManager.Instance.CurMission;
	}

	void Start () {
		missionAnimation.Init (MissionManager.Instance.CurMission);
		SoundManager.Instance.Play ("Select");
	}

	protected override void OnDestroy () {
		base.OnDestroy ();
		SoundManager.Instance.Stop ("Select");
	}

	private void OnInitializeMissionItem (GameObject obj, int index) {
		obj.GetComponent <MissionItem> ().Init (index + 1, (lastMissionID == index && lastMissionID == MissionManager.Instance.CurMission - 1));
	}

	public void OnItemClick (int id) {
		MissionController.Create (id);
		Destroy (gameObject);
	}
}