/* GameInfo.cs
 * Author: Caleb Whitman
 * December 23, 2016
 * 
 * Stores data that exists between all scenes AND is not stored within any physical object.
 * Right now I plan to use this to keep track of the current Subreddit/Thread we are in. This may change in the future.
 * 
 * This is basically a singleton class.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {

	public static GameInfo info;

	public string currentSubreddit;
	public string currentThread;

	public Vector3 outsidePlayerPosition;
	public GameObject player;

	public int center_chunk_x, center_chunk_z;

	//called first thing no matter what
	void Awake () 
	{
		if (info == null) 
		{
			DontDestroyOnLoad (gameObject);
			info = this;
			outsidePlayerPosition=new Vector3(0,1,-4);
		} 
		else if (info != this) 
		{
			//ensures that only on object of this type is present at all times
			Destroy(gameObject);
		}
	}


}
