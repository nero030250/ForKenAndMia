using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PersonType {
	Man,
	Woman,
}

public class TalkConfig {
	public PersonType Person { get; private set; }
	public string Content {get; private set; }

	public TalkConfig (PersonType person, string content) {
		Person = person;
		Content = content;
	}
}

public class DialogConfig {
	public TalkConfig [] TalkArr { get; private set; }

	public DialogConfig (params TalkConfig [] talk) {
		TalkArr = talk;
	}
}