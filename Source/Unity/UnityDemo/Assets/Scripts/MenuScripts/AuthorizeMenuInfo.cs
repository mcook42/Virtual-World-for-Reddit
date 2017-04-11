using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class AuthorizeMenuInfo : MonoBehaviour {

	public JToken token {get; set;}
	public string postParams {get; set;}
	public GameObject scopeList;
	public GameObject title;

	public void init(JToken token, string postParams,string userName)
	{
		this.token = token;
		this.postParams = postParams;

		Dictionary<string,string> scopes = GameInfo.instance.redditRetriever.getAppScopeDescriptions ();

		Text text = scopeList.GetComponent<Text> ();
		foreach (KeyValuePair<string,string> scope in scopes) {
			text.text += " - " + scope.Value + "\n\n";
		}

		title.GetComponent<Text> ().text = "Hey "+userName+"! City For Reddit would like to connect with your reddit account.";
	}
}
