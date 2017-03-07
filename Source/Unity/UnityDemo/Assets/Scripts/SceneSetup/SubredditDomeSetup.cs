using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using GenericGraph;

public class SubredditDomeSetup : SceneSetUp{

    public GameObject center;
    public GameObject buildingParent;
	public GameObject pathParent;

    public GameObject buildingPrefabSmall;
    public GameObject buildingPrefabMedium;
    public GameObject buildingPrefabLarge;

    public Material lowReadingMaterial;
    public Material mediumReadingMaterial;
    public Material highReadingMaterial;


    protected override void setUpScene()
    {

		Subreddit centerSub = SubredditDomeState.instance.center.Value;
        instantiateBuilding(center.transform, centerSub);

		int i = 0;
		foreach(Node<Subreddit> node in SubredditDomeState.instance.buildings){

			if (node.Value.FullName == SubredditDomeState.instance.center.Value.FullName)
				continue;
				

			Transform placeHolder;
			try{
				placeHolder = buildingParent.transform.GetChild (i);
			}
			catch(UnityException e) {
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
	/// Instantiates the path.
	/// </summary>
	/// <param name="path">Path.</param>
	/// <param name="sub">The Center Node.</param>
	/// <param name="neighbor">The index of the path.</param>
	public void instantiatePath(Transform path, Node<Subreddit> sub,int neighbor)
    {
		float weight = sub.Costs [neighbor]; //Cost of going to the ith neigbor

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
        GameInfo.instance.player.transform.position = new Vector3(10, 1, 0);
    }

    


}
