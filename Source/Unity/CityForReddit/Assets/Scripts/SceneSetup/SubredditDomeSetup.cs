using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using Graph;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Handles creation and management of the SubredditDome.
/// </summary>
public class SubredditDomeSetup : SceneSetUp, LoginObserver{

	//Constant values
	public readonly float innerCircleSize = 60;
	public readonly float outerCircleSize = 100;
	public readonly float domeSize = 250;
	public readonly int buildingFootprint = 30;
	public readonly int minPathWidth = 30;
	public readonly int maxPathWidth = 30;
	public readonly int innerBuildNum = 6;
	public readonly int outerBuildNum = 9;


	//Objects in the world
    private GameObject center;
    private GameObject buildingParent;
	private GameObject pathParent;
	public GameObject dome;

	//Prefabs
    public GameObject buildingPrefabSmall;
    public GameObject buildingPrefabMedium;
    public GameObject buildingPrefabLarge;
	public GameObject frontPrefab;
	public GameObject pathPrefab;

	public GameObject housePrefab;

	#region LoginObserver

	/// <summary>
	/// Registers with the redditRetriever.
	/// </summary>
	new void Start()
	{
		base.Start ();
		WorldState.instance.redditRetriever.register (this);
	}
		

	/// <summary>
	/// Resets the value of the reddit object in the subreddits.
	/// </summary>
	/// <param name="login">If set to <c>true</c> login.</param>
	public void notify(bool login)
	{
		foreach (Transform building in buildingParent.transform) {
			building.gameObject.GetComponentInChildren<BuildingInfo> ().subreddit.UpdateReddit (WorldState.instance.reddit);
		}
	
	}

	/// <summary>
	/// Unregisters from the redditRetriever.
	/// </summary>
	void OnDestroy()
	{
		WorldState.instance.redditRetriever.unRegister (this);
	}
	#endregion


	/// <summary>
	/// Loads and instantiates all objects required for the scene.
	/// </summary>
    protected override void setUpScene()
    {
		generateBuildingPositions ();

		if (SubredditDomeState.instance.house) {
			instantiateHouse (center.transform);
		} else {
			Subreddit centerSub = SubredditDomeState.instance.center.Value;
			var building = instantiateBuilding (center.transform, centerSub);
			if (SubredditDomeState.instance.loadNew) {
				SubredditDomeState.instance.playerSpawnPoint = new Vector3 (building.GetComponent<BuildingInfo> ().footprint + 5, 1, 1);
				SubredditDomeState.instance.loadNew = false;
			}
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
	/// Generates the building positions depending on the fields in the above script.
	/// </summary>
	public void generateBuildingPositions()
	{
		buildingParent = new GameObject("buildingParent");
		pathParent = new GameObject ("pathParent");

		//set center
		center = new GameObject("center");
		center.transform.position = new Vector3(0, 0, 0);

		//center inner buildings and paths
		float innerRadius = (minPathWidth*innerBuildNum/(Mathf.PI*2))+(buildingFootprint/2)+buildingFootprint / (2 * Mathf.Tan(180 / innerBuildNum));

		if(innerRadius<innerCircleSize)
		{
			innerRadius = innerCircleSize;
		}

		float innerAngle = 2*Mathf.PI / innerBuildNum;

		for (int i=0; i<innerBuildNum;i++)
		{
			GameObject building = new GameObject("Inner:" + i);
			float x = innerRadius * Mathf.Cos(innerAngle * i);
			float z =  innerRadius * Mathf.Sin(innerAngle * i);
			building.transform.position = new Vector3(x, 0, z);
			building.transform.eulerAngles = new Vector3(0,180+-innerAngle*i/(Mathf.PI*2)*360, 0);
			building.transform.SetParent(buildingParent.transform);

			GameObject path = Instantiate(pathPrefab) as GameObject;
			path.transform.localScale = new Vector3(0.5f, 1, (innerRadius-2*buildingFootprint)/10);
			Vector3 middle = building.transform.position - new Vector3(3*buildingFootprint * Mathf.Cos(innerAngle * i), 0.0f, 3*buildingFootprint* Mathf.Sin(innerAngle * i));
			path.transform.position = new Vector3(middle.x, 0.01f, middle.z);
			path.transform.LookAt(building.transform.position);
			path.transform.SetParent(pathParent.transform);
			path.name = "InnerPath:" + i;
			path.SetActive (false);


		}

		//set outer buildings and positions
		float outerRadius=3*buildingFootprint+ buildingFootprint / (2 * Mathf.Tan(180 / outerBuildNum));

		if (outerRadius<outerCircleSize)
		{

			outerRadius = outerCircleSize;
		}
		if (outerRadius < innerRadius)
		{
			outerRadius = innerRadius + 2 * buildingFootprint;
		}

		float outerAngle= 2 * Mathf.PI / outerBuildNum;

		for (int i = 0; i < outerBuildNum; i++)
		{
			GameObject building = new GameObject("Outer: " + i);
			float x = outerRadius * Mathf.Cos(outerAngle * i);
			float z = outerRadius * Mathf.Sin(outerAngle * i);
			building.transform.position = new Vector3(x, 0, z);
			building.transform.localScale = new Vector3(buildingFootprint, 5, buildingFootprint);
			building.transform.eulerAngles = new Vector3(0, 180+-outerAngle*i / (Mathf.PI * 2) * 360, 0);
			building.transform.SetParent(buildingParent.transform);

			GameObject path = Instantiate(pathPrefab) as GameObject;
			path.transform.localScale = new Vector3(0.5f, 1, (outerRadius-innerRadius) / 10);
			Vector3 middle = building.transform.position - new Vector3((innerRadius) * Mathf.Cos(outerAngle * i), 0.0f, (innerRadius) * Mathf.Sin(outerAngle * i));
			path.transform.position = new Vector3(building.transform.position.x-(middle.x/2), 0.01f, building.transform.position.z - (middle.z/2));
			path.transform.LookAt(building.transform.position);
			path.transform.SetParent(pathParent.transform);
			path.name = "OuterPath:" + i;
			path.SetActive (false);

		}

		//resize dome
		dome.transform.localScale = new Vector3 (domeSize, domeSize, domeSize);

	}


	/// <summary>
	/// Instantiates the building.
	/// </summary>
	/// <param name="placeHolder">Position and rotation of the new building.</param>
	/// <param name="sub">Subreddit attached to the building.</param>
	private GameObject instantiateBuilding(Transform placeHolder, Subreddit sub)
    {

        GameObject building; 

		if (sub.Url == null)
			sub = WorldState.instance.reddit.GetSubreddit (sub.DisplayName);

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
			

        float height = building.GetComponent<BuildingInfo>().height;
		building.transform.localEulerAngles = new Vector3(building.transform.localEulerAngles.x,building.transform.localEulerAngles.y+placeHolder.rotation.eulerAngles.y,building.transform.localEulerAngles.z);
        building.transform.position = placeHolder.position + new Vector3(0, height/2, 0);
		building.transform.SetParent (placeHolder);

        //give building values using the Building script
        building.GetComponent<BuildingInfo>().subreddit=sub;

        //set the name on the front of the building
        var name = building.transform.Find("Name").GetComponent<TextMesh>();
		name.text = sub.DisplayName;

		return building;
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

	/// <summary>
	/// Sets the players state once the scene is loaded.
	/// </summary>
    protected override void setPlayerState()
    {
		if(SubredditDomeState.instance.loadNew)
			SubredditDomeState.instance.playerSpawnPoint = new Vector3(buildingFootprint+6,1,1);
		else
			WorldState.instance.player.transform.position = SubredditDomeState.instance.playerSpawnPoint;
		WorldState.instance.player.transform.localRotation = SubredditDomeState.instance.playerSpawnRotation;
    }

    


}
