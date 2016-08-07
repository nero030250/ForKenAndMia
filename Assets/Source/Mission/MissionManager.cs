using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionManager : UISingleton <MissionManager> {
	private int curMission;
	public int CurMission {
		get { return curMission; }
		set { 
			if (value == curMission)
				return;
			curMission = value;
			PlayerPrefs.SetInt ("Mission", curMission);
		}
	}
	public Dictionary <int, MissionConfig> MissionDic { get; private set; }
	public Dictionary <int, DialogConfig> DialogDic { get; private set; }

	protected override void Awake () {
		base.Awake ();

		Random.seed = (int)System.DateTime.Now.Ticks;

		curMission = PlayerPrefs.GetInt ("Mission", 1);

		MissionDic = new Dictionary<int, MissionConfig> ();
		DialogDic = new Dictionary<int, DialogConfig> ();

		MissionDic.Add (1, new MissionConfig ());
		MissionDic [1].SetOrder (KeyType.Do);
		MissionDic[1].SetKey (KeyType.Do, KeyType.Do, KeyType.Do, KeyType.Ti_0, KeyType.Ti_1);

		DialogDic.Add (1, new DialogConfig (
			new TalkConfig (PersonType.Woman, "Hi."),
			new TalkConfig (PersonType.Man, "Hi.")));

		MissionDic.Add (2, new MissionConfig ());
		MissionDic [2].SetOrder (KeyType.Fa, KeyType.Fa, KeyType.Fa);
		MissionDic[2].SetKey(KeyType.Do, KeyType.Do, KeyType.Do, KeyType.Do, KeyType.Fa
			, KeyType.Fa, KeyType.Fa, KeyType.Fa, KeyType.Ti_0, KeyType.Ti_1);
		
		DialogDic.Add (2, new DialogConfig (
			new TalkConfig (PersonType.Man, "These are for you."),
			new TalkConfig (PersonType.Woman, "Wow..."),
			new TalkConfig (PersonType.Woman, "Thank you!")));
		
		MissionDic.Add (3, new MissionConfig ());
		MissionDic [3].SetOrder (KeyType.Do, KeyType.So, KeyType.Mi, KeyType.Fa);
		MissionDic [3].SetKey (KeyType.Do, KeyType.Do, KeyType.Do, KeyType.Do, KeyType.Do
			, KeyType.Mi, KeyType.Mi, KeyType.Fa, KeyType.Fa, KeyType.Fa
			, KeyType.So, KeyType.So, KeyType.So, KeyType.Ti_0, KeyType.Ti_1);
		
		DialogDic.Add (3, new DialogConfig (
			new TalkConfig (PersonType.Woman, "I am happy to be with you."),
			new TalkConfig (PersonType.Man, "I have the same feeling.")));

		MissionDic.Add (4, new MissionConfig ());
		MissionDic [4].SetOrder (KeyType.Do, KeyType.Fa, KeyType.Ti_0, KeyType.Ti_1, KeyType.La);
		MissionDic [4].SetKey (KeyType.Do, KeyType.Do, KeyType.Do, KeyType.Do, KeyType.Do
			, KeyType.Do, KeyType.Re, KeyType.Re, KeyType.Mi, KeyType.Mi
			, KeyType.Mi, KeyType.Fa, KeyType.Fa, KeyType.Fa, KeyType.So
			, KeyType.So, KeyType.So, KeyType.La, KeyType.Ti_0, KeyType.Ti_1);
		DialogDic.Add (4, new DialogConfig (
			new TalkConfig (PersonType.Man, "Will you…marry me?"),
			new TalkConfig (PersonType.Woman, "I do.")));

		MissionDic.Add (5, new MissionConfig ());
		MissionDic [5].SetOrder (KeyType.So, KeyType.Fa, KeyType.Mi, KeyType.Fa, KeyType.So, KeyType.So);
		MissionDic [5].SetKey (KeyType.Do, KeyType.Do, KeyType.Do, KeyType.Do, KeyType.Do
			, KeyType.Do, KeyType.Do, KeyType.Re, KeyType.Re, KeyType.Re
			, KeyType.Re, KeyType.Mi, KeyType.Fa, KeyType.Fa, KeyType.So
			, KeyType.So, KeyType.So, KeyType.La, KeyType.La, KeyType.La
			, KeyType.Ti_0, KeyType.Ti_1, KeyType.DoU, KeyType.DoU, KeyType.DoU);
		DialogDic.Add (5, new DialogConfig ());
	}

	public bool IsMissionUnlock (int id) {
		return CurMission >= id;
	}

	public void MissionComplete (int missionId) {
		if (missionId == curMission)
			CurMission++;
	}
}