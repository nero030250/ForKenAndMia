using UnityEngine;
using System.Collections;

public class WelcomeController : UISingleton <WelcomeController> {
	private const string UI_PATH = "Prefab/WelcomePanel";
	private const string ABOUT_PATH = "Prefab/AboutPanel";
	public static GameObject Create () {
		return UIHelper.Create (UI_PATH);
	}

	private UIButton newGameBtn;
	private UIButton continueBtn;
	private UIButton aboutBtn;

	protected override void Awake () {
		base.Awake ();
		newGameBtn = GetButton ("NewGameBtn");
		continueBtn = GetButton ("ContinueBtn");
		aboutBtn = GetButton ("AboutBtn");
		EventDelegate.Add (newGameBtn.onClick, OnNewGameClick);
		EventDelegate.Add (continueBtn.onClick, EnterSelectPanel);
		EventDelegate.Add (aboutBtn.onClick, OnAboutClick);

		UIEventListener.Get (newGameBtn.gameObject).onHover += OnBtnHower;
		UIEventListener.Get (continueBtn.gameObject).onHover += OnBtnHower;
		UIEventListener.Get (aboutBtn.gameObject).onHover += OnBtnHower;

		SoundManager.Instance.Play ("OP");
	}

	protected override void OnDestroy () {
		base.OnDestroy ();
		SoundManager.Instance.Stop ("OP");
	}

	private void OnNewGameClick () {
		MissionManager.Instance.CurMission = 1;
		EnterSelectPanel ();
	}

	private void EnterSelectPanel () {
		SoundManager.Instance.Play ("Click");
		Destroy (gameObject);
		MissionSelectController.Create ();
	}

	private void OnAboutClick () {
		SoundManager.Instance.Play ("Click");
		Destroy (gameObject);
		UIHelper.Create (ABOUT_PATH);
	}

	private void OnBtnHower (GameObject btn, bool status) {
		if (status)
			SoundManager.Instance.Play ("Hover");
		btn.transform.FindChild ("Flower").gameObject.SetActive (status);
	}
}