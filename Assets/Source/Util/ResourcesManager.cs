using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourcesManager : UISingleton <ResourcesManager> {
	public GameObject CreateGameObject (string path) {
		return Resources.Load<GameObject> (path);
	}

	public string[] ReadConfigFile (string fileName) {
		string path = string.Format ("Config/{0}", fileName);
		TextAsset textAsset = Resources.Load <TextAsset> (path);
		Debug.LogWarning (textAsset.text);
		string text = textAsset.text;
		text.Replace ("\r\n", "\n");
		string[] split = text.Split ('\n');
		return split;
	}
}