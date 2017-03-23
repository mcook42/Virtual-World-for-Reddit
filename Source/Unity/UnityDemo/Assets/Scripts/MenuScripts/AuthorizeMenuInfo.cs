using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class AuthorizeMenuInfo : MonoBehaviour {

	public JToken token {get; set;}
	public string postParams {get; set;}

	public void init(JToken token, string postParams)
	{
		this.token = token;
		this.postParams = postParams;
	}
}
