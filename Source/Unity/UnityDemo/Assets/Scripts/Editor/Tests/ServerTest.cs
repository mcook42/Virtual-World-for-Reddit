using System;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json.Linq;


public class ServerTest 
{
	[MenuItem("ServerTest/test")]
	public static void testServer ()
	{
		JObject askscienceJson = JObject.Parse (@"{ ""center"" : ""s1"", ""nodes"": [""s1"",""s2"",""s3"",""s4"",""sn""], ""edges"" : [ [ ""s1"",""s3"",5 ], [ ""s2"", ""s7"", 6 ] ] }");

		Debug.Log (askscienceJson ["center"]);
		foreach(var node in askscienceJson ["nodes"])
		{
			Debug.Log (node);
		}
		foreach (var edge in askscienceJson ["edges"]) {
			foreach (var thing in edge) {
				Debug.Log (thing);
			}
		}
		Debug.Log("here");
	}
}


