using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;

public class SubredditDomeSetup : SceneSetUp{

    public GameObject center;
    public GameObject innerBuildingParent;
    public GameObject outerBuildingParent;
    public GameObject innerPathParent;
    public GameObject outerPathParent;

    public GameObject buildingPrefabSmall;
    public GameObject buildingPrefabMedium;
    public GameObject buildingPrefabLarge;

    public Material lowReadingMaterial;
    public Material mediumReadingMaterial;
    public Material highReadingMaterial;


    protected override void setUpScene()
    {

        Subreddit centerSub = SubredditDomeState.instance.centerBuilding;
        instantiateBuilding(center.transform, centerSub);


		for(int i=0; i<SubredditDomeState.instance.innerBuildings.Count;i++){
			Transform placeHolder;
			try{
				placeHolder = innerBuildingParent.transform.GetChild (i);
			}
			catch(UnityException e) {
				break;
			}
			Subreddit sub = SubredditDomeState.instance.innerBuildings[i];
			instantiateBuilding(placeHolder, sub);
			instantiatePath(innerPathParent.transform.GetChild(i),sub);
		}

		for(int i=0; i<SubredditDomeState.instance.outerBuildings.Count;i++){

			Transform placeHolder;
			try{
			placeHolder = outerBuildingParent.transform.GetChild (i);
			}
			catch(UnityException e) {
				break;
			}

			Subreddit sub = SubredditDomeState.instance.outerBuildings[i];
			instantiateBuilding(placeHolder, sub);
			instantiatePath(outerPathParent.transform.GetChild(i),sub);
		}
        

       


    }

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

    public void instantiatePath(Transform path, Subreddit sub)
    {
		/*
        if (sub.weight < 0.3)
        {
            path.localScale = new Vector3(path.localScale.x / 4, path.localScale.y, path.localScale.z);
        }
        else if (sub.weight < 0.6)
        {
            path.localScale = new Vector3(path.localScale.x / 2, path.localScale.y, path.localScale.z);
        }
        else
        {
            path.localScale = new Vector3(path.localScale.x, path.localScale.y, path.localScale.z);
        }
        */
    }

    protected override void setPlayerState()
    {
        GameInfo.instance.player.transform.position = new Vector3(10, 1, 0);
    }

    


}
