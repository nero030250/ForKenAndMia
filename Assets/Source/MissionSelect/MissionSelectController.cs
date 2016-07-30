using UnityEngine;
using System.Collections;

public class MissionSelectController : UISingleton <MissionSelectController> {
	private const string UI_PATH = "Prefab/MissionSelectPanel";

	public static GameObject Create () {
		return UIHelper.Create (UI_PATH);
	}

	private UIGridEx missionGrid;
	private MissionAnimation missionAnimation;

	protected override void Awake () {
		base.Awake ();
		missionGrid = GetGridEx ("MissionGrid");
		missionGrid.OnInitializeItem = OnInitializeMissionItem;
		missionGrid.Resize (MissionManager.Instance.MissionDic.Count);
		missionAnimation = GetChildComponent <MissionAnimation> ("Animation");
	}

	private void OnInitializeMissionItem (GameObject obj, int index) {
		obj.GetComponent <MissionItem> ().Init (index + 1);
	}

	public void OnItemClick (int id) {
		if (missionAnimation.IsPlaying)
			return;
	}
}