using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MissionController : UISingleton <MissionController> {
	private const string UI_PATH = "Prefab/MissionPanel";
	public static GameObject Create (int id) {
		GameObject obj = UIHelper.Create (UI_PATH);
		obj.GetComponent <MissionController> ().Init (id);
		return obj;
	}

	private GameObject prepareObj;
	private UILabel cdLabel;

	private UIGridEx orderGrid;

	private GameObject successfulObj;

	private UIGridEx boxGrid;
	private List<BoxItem> boxItemList = new List<BoxItem> ();

	private TweenAlpha leftTalk;
	private UILabel leftTalkLabel;

	private TweenAlpha rightTalk;
	private UILabel rightTalkLabel;

	private int missionId;

	private List<KeyType> orderList = new List<KeyType> ();
	private int orderIndex = 0;
	private List<KeyType> boxTypeList = new List<KeyType> ();

	protected override void Awake () {
		base.Awake ();

		prepareObj = GetGameObject ("Prepare");
		cdLabel = GetLabel ("Prepare_CD");

		EventDelegate.Add (GetButton ("BackBtn").onClick, Back);

		boxGrid = GetGridEx ("BoxGrid");
		boxGrid.OnInitializeItem = OnInitializeBoxItem;

		orderGrid = GetGridEx ("OrderGrid");
		orderGrid.OnInitializeItem = OnInitializeOrderItem;

		successfulObj = GetGameObject ("Successful");

		leftTalk = GetChildComponent <TweenAlpha> ("LeftTalk");
		leftTalkLabel = GetLabel ("LeftTalk_Label");

		rightTalk = GetChildComponent <TweenAlpha> ("RightTalk");
		rightTalkLabel = GetLabel ("RightTalk_Label");
	}

	public void Init (int id) {
		missionId = id;

		// 随机
		List <KeyType> keyList = MissionManager.Instance.MissionDic [missionId].Key.ToList ();
		while (keyList.Count < 25)
			keyList.Add (KeyType.TiD);
		while (keyList.Count > 0) {
			int index = Random.Range (0, keyList.Count);
			boxTypeList.Add (keyList [index]);
			keyList.RemoveAt (index);
		}
		orderList = MissionManager.Instance.MissionDic [missionId].Order.ToList ();
		boxGrid.Resize (25);
		StartCoroutine (_StartCD ());
	}

	private IEnumerator _StartCD () {
		float time = 3f;
		prepareObj.SetActive (true);
		while (time > 0) {
			cdLabel.text = Mathf.CeilToInt (time).ToString ();
			time -= Time.deltaTime;
			yield return 0;
		}
		prepareObj.SetActive (false);
		foreach (BoxItem item in boxItemList)
			item.Front ();
		yield return new WaitForSeconds (5f);
		CloseAllBox ();
		yield return new WaitForSeconds (1f);
		orderGrid.Resize (orderList.Count, true);
	}

	private void Back () {
		Destroy (gameObject);
		MissionSelectController.Create ();
	}

	private void OnInitializeBoxItem (GameObject obj, int index) {
		BoxItem item = obj.GetComponent<BoxItem> ();
		boxItemList.Add (item);
		item.Init (boxTypeList [index]);
	}

	private void OnInitializeOrderItem (GameObject obj, int index) {
		obj.GetComponent<OrderItem> ().Init (orderList [index]);
	}

	public void OnBoxClick (BoxItem item) {
		if (orderList.Count <= orderIndex)
			return;
		if (item.Type == orderList [orderIndex]) {
			orderIndex++;
		} else {
			orderIndex = 0;
			CloseAllBox ();
		}
		if (orderList.Count == orderIndex) {
			StartCoroutine (_ShowCompletedAnimation ());
		}
	}

	private IEnumerator _ShowCompletedAnimation () {
		MissionManager.Instance.MissionComplete (missionId);
		foreach (BoxItem item in boxItemList) {
			item.GetComponentInChildren <BoxCollider> ().enabled = false;
		}
//		yield return new WaitForSeconds (0.5f);
		successfulObj.SetActive (true);
		SoundManager.Instance.Play ("Successful");
		yield return new WaitForSeconds (6f);
		DialogConfig dConfig = MissionManager.Instance.DialogDic [missionId];
		for (int index = 0; index < dConfig.TalkArr.Length; index++) {
			TalkConfig talk = dConfig.TalkArr [index];
			if (talk.Person == PersonType.Man) {
				leftTalkLabel.text = talk.Content;
				leftTalk.PlayForward ();
				rightTalk.ResetToBeginning ();
			} else {
				rightTalkLabel.text = talk.Content;
				rightTalk.PlayForward ();
				leftTalk.ResetToBeginning ();
			}
			yield return new WaitForSeconds (3f);
		}
		Destroy (gameObject);
		MissionSelectController.Create ();
	}
		
	private void CloseAllBox () {
		foreach (BoxItem item in boxItemList)
			item.Back ();
	}
}