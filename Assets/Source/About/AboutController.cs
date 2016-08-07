using UnityEngine;
using System.Collections;

public class AboutController : UISingleton <AboutController> {
	private const string UI_PATH = "Prefab/AboutPanel";
	public static GameObject Create () {
		return UIHelper.Create (UI_PATH);
	}

	protected override void Awake () {
		base.Awake ();
		EventDelegate.Add (GetButton ("CloseBtn").onClick, () => Destroy (gameObject));
	}

	protected override void OnDestroy () {
		base.OnDestroy ();
		WelcomeController.Create ();
	}
}