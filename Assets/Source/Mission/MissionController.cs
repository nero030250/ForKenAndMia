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

	private UIGridEx boxGrid;
	private List<BoxItem> boxItemList = new List<BoxItem> ();

	private int missionId;

	private List<KeyType> orderList = new List<KeyType> ();
	private int orderIndex = 0;
	private List<KeyType> boxTypeList = new List<KeyType> ();

	protected override void Awake () {
		base.Awake ();

		prepareObj = GetGameObject ("Prepare");
		cdLabel = GetLabel ("Prepare_CD");

		EventDelegate.Add (GetButton ("BackBtn").onClick, Back);

		orderGrid = GetGridEx ("OrderGrid");
		orderGrid.OnInitializeItem = OnInitializeOrderItem;

		boxGrid = GetGridEx ("BoxGrid");
		boxGrid.OnInitializeItem = OnInitializeBoxItem;
	}

	public void Init (int id) {
		missionId = id;
		StartCoroutine (_StartCD ());

		// 随机
		List <KeyType> keyList = MissionManager.Instance.MissionDic [missionId].Key.ToList ();
		while (keyList.Count < 25)
			keyList.Add (KeyType.Empty);
		while (keyList.Count > 0) {
			int index = Random.Range (0, keyList.Count);
			boxTypeList.Add (keyList [index]);
			keyList.RemoveAt (index);
		}
		orderList = MissionManager.Instance.MissionDic [missionId].Order.ToList ();
		boxGrid.Resize (25);
		RefreshOrderGrid ();
	}

	private IEnumerator _StartCD () {
		float time = 3f;
		prepareObj.SetActive (true);
		while (time > 0) {
			cdLabel.text = Mathf.FloorToInt (time).ToString ();
			time -= Time.deltaTime;
			yield return 0;
		}
		prepareObj.SetActive (false);
	}

	private void Back () {
		Destroy (gameObject);
		MissionSelectController.Create ();
	}

	private void OnInitializeOrderItem (GameObject obj, int index) {
		obj.GetComponent<OrderItem> ().Init (orderList [index] + orderIndex);
	}

	private void OnInitializeBoxItem (GameObject obj, int index) {
		BoxItem item = obj.GetComponent<BoxItem> ();
		boxItemList.Add (item);
		item.Init (boxTypeList [index]);
	}

	private void RefreshOrderGrid () {
		orderGrid.Resize (orderList.Count);
	}

	public void OnBoxClick (BoxItem item) {
		if (orderList.Count <= orderIndex)
			return;
		if (item.Type == orderList [orderIndex]) {
			orderIndex++;
		} else {
			orderIndex = 0;
		}
		orderGrid.Resize (orderList.Count - orderIndex);
	}
}