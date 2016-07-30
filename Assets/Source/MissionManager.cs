using UnityEngine;
using System.Collections;

public class MissionManager : UISingleton <MissionManager> {
	public int CurMission { get; private set; }

	protected override void Awake () {
		base.Awake ();
		CurMission = PlayerPrefs.GetInt ("Mission", 1);
	}
}