using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using Graph;

/// <summary>
/// Handles creation of the Subreddit Dome.
/// </summary>
public class SubredditDomeSetup : SceneSetUp, LoginObserver{

	//Constant values
	public static readonly float innerCircleSize = 80;
	public static readonly float outerCircleSize = 160;
	public static readonly int buildingFootprint = 40;
	public static readonly int minPathWidth = 10;
	public static readonly int maxPathWidth = 30;
	public static readonly int innerBuildNum = 6;
	public static readonly int outerBuildNum = 9;


    public GameObject center;
    public GameObject buildingParent;
	public GameObject pathParent;

    public GameObject buildingPrefabSmall;
    public GameObject buildingPrefabMedium;
    public GameObject buildingPrefabLarge;
	public GameObject frontPrefab;

	public GameObject housePrefab;

    public Material lowReadingMaterial;
    public Material mediumReadingMaterial;
    public Material highReadingMaterial;


	#region LoginObserver

	/// <summary>
	/// Registers with the redditRetriever.
	/// </summary>
	new void Start()
	{
		base.Start ();
		GameInfo.instance.redditRetriever.register (this);
	}

	/// <summary>
	/// Resets the value of the reddit object in the subreddits.
	/// </summary>
	/// <param name="login">If set to <c>true</c> login.</param>
	public void notify(bool login)
	{
		SubredditDomeState.instance.center.Value.Reddit = GameInfo.instance.reddit;
		foreach (Node<Subreddit> node in SubredditDomeState.instance.center.ToNeighbors) {

			node.Value.Reddit = GameInfo.instance.reddit;
		}
	}

	/// <summary>
	/// Unregisters from the redditRetriever.
	/// </summary>
	void OnDestroy()
	{
		GameInfo.instance.redditRetriever.unRegister (this);
	}
	#endregion


	/// <summary>
	/// Stores the appropriate state for the scene in GameInfo.
	/// </summary>
	protected override void setCurrentState ()
	{
		GameInfo.instance.currentState = SubredditDomeState.instance;
	}
	/// <summary>
	/// Loads and instantiates all objects required for the scene.
	/// </summary>
    protected override void setUpScene()
    {

		if (SubredditDomeState.instance.house) {
			instantiateHouse (center.transform);
		} else {
			Subreddit centerSub = SubredditDomeState.instance.center.Value;
			instantiateBuilding (center.transform, centerSub);
		}

		int i = 0;
		foreach(Node<Subreddit> node in SubredditDomeState.instance.center.ToNeighbors){


			Transform placeHolder;
			try{
				placeHolder = buildingParent.transform.GetChild (i);
			}
			catch(UnityException) {
				break;
			}
			Subreddit sub = node.Value;
			instantiateBuilding(placeHolder, sub);
			instantiatePath(pathParent.transform.GetChild(i),SubredditDomeState.instance.center,i);
			i++;
		}
			


    }

	/// <summary>
	/// Instantiates the building.
	/// </summary>
	/// <param name="placeHolder">Position and rotation of the new building.</param>
	/// <param name="sub">Subreddit attached to the building.</param>
    private void instantiateBuilding(Transform placeHolder, Subreddit sub)
    {

        GameObject building; 

		if (sub.Url == null)
			sub = GameInfo.instance.reddit.GetSubreddit (sub.DisplayName);

		//determining the model to use
		if (sub.DisplayName == "Front Page" || sub.DisplayName == "/r/all") {
			building = Instantiate(frontPrefab) as GameObject;
		}
        else if (sub.Subscribers<15000000)
        {
            building = Instantiate(buildingPrefabSmall) as GameObject;
        }
        else if(sub.Subscribers<16000000)
        {
            building = Instantiate(buildingPrefabMedium) as GameObject;
        }
        else
        {
            building = Instantiate(buildingPrefabLarge) as GameObject;
        }
			
        //adding the correct material

		/**
        Renderer renderer = building.GetComponent<Renderer>();


        if(sub.lexil<5)
        {
            renderer.material = lowReadingMaterial;
        }
        else if(sub.lexil <6)
        {
            renderer.material = mediumReadingMaterial;
        }
        else
        {
            
            renderer.material = highReadingMaterial;
        }
        */

        float height = building.GetComponent<BuildingInfo>().height;
		building.transform.localEulerAngles = new Vector3(building.transform.localEulerAngles.x,building.transform.localEulerAngles.y+placeHolder.rotation.eulerAngles.y,building.transform.localEulerAngles.z);
        building.transform.position = placeHolder.position + new Vector3(0, height/2, 0);

        //give building values using the Building script
        building.GetComponent<BuildingInfo>().subreddit=sub;

        //set the name on the front of the building
        var name = building.transform.Find("Name").GetComponent<TextMesh>();
		name.text = sub.DisplayName;
    }

	/// <summary>
	/// Instantiates the house.
	/// </summary>
	/// <param name="placeholder">The placeholder holding where the house will be located.</param>
	public void instantiateHouse(Transform placeholder)
	{
		Instantiate (housePrefab);
	}

	/// <summary>
	/// Instantiates the path.
	/// </summary>
	/// <param name="path">Path.</param>
	/// <param name="sub">The Center Node.</param>
	/// <param name="neighbor">The index of the path.</param>
	public void instantiatePath(Transform path, Node<Subreddit> sub,int neighbor)
    {
		float weight = sub.Costs [neighbor]; //Cost of going to the ith neigbor
		path.gameObject.SetActive(true);

        if (weight <=1)
        {
            path.localScale = new Vector3(path.localScale.x / 4, path.localScale.y, path.localScale.z);
        }
        else if (weight <= 2)
        {
            path.localScale = new Vector3(path.localScale.x / 2, path.localScale.y, path.localScale.z);
        }
        else
        {
            path.localScale = new Vector3(path.localScale.x, path.localScale.y, path.localScale.z);
        }
    }

    protected override void setPlayerState()
    {
		
		GameInfo.instance.player.transform.position = SubredditDomeState.instance.playerSpawnPoint;
		GameInfo.instance.player.transform.localRotation = SubredditDomeState.instance.playerSpawnRotation;
    }

    


}
