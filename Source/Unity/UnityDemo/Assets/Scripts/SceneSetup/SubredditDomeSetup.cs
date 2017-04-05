using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using Graph;

public class SubredditDomeSetup : SceneSetUp{

	//Constant values
	public static readonly float innerCircleSize = 40;
	public static readonly float outerCircleSize = 60;
	public static readonly int buildingFootprint = 5;
	public static readonly int minPathWidth = 10;
	public static readonly int maxPathWidth = 30;
	public static readonly int innerBuildNum = 13;
	public static readonly int outerBuildNum = 12;


    public GameObject center;
    public GameObject buildingParent;
	public GameObject pathParent;

    public GameObject buildingPrefabSmall;
    public GameObject buildingPrefabMedium;
    public GameObject buildingPrefabLarge;

	public GameObject housePrefab;

    public Material lowReadingMaterial;
    public Material mediumReadingMaterial;
    public Material highReadingMaterial;

	protected override void setCurrentState ()
	{
		GameInfo.instance.currentState = SubredditDomeState.instance;
	}

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
        if (sub.Subscribers<1000000)
        {
            building = Instantiate(buildingPrefabSmall) as GameObject;
        }
        else if(sub.Subscribers<10000000)
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
        building.transform.rotation = placeHolder.rotation;
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
