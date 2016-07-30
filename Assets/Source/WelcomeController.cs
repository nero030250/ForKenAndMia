using UnityEngine;
using System.Collections;

public class WelcomeController : UISingleton <WelcomeController> {
	private const string UI_PATH = "Prefab/WelcomePanel";
	public static GameObject Create () {
		return UIHelper.Create (UI_PATH);
	}

	protected override void OnDestroy () {
		base.OnDestroy ();
	}
}