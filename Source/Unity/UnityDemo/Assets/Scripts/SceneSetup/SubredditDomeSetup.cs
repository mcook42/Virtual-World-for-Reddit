using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubredditDomeSetup : SceneSetUp{

    public GameObject center;
    public GameObject innerBuildingParent;
    public GameObject outerBuildingParent;
    public GameObject innerPathParent;
    public GameObject outerPathParent;

    public GameObject buildingPrefab;

    protected override void setUpScene()
    {
        

        SubredditDomeState.instance.centerBuilding.transform.rotation=center.transform.rotation;
        SubredditDomeState.instance.centerBuilding.transform.position = center.transform.position + new Vector3(0, 5, 0);


        int i = 0;
        foreach (Transform placeHolder in innerBuildingParent.transform)
        {
            GameObject building = Instantiate(buildingPrefab) as GameObject;
            building.transform.rotation = placeHolder.rotation;
            building.transform.position = placeHolder.position+new Vector3(0, 5, 0);

            //give building values using the Building script
            var buildingAttributes = building.GetComponent<Subreddit>();
            buildingAttributes.subredditId = "";
            buildingAttributes.subredditName = "Building: "+i;

            //set the name on the front of the building
            var name = building.transform.Find("Name").GetComponent<TextMesh>();
            name.text = buildingAttributes.subredditName;

            i++;
        }

        foreach(Transform placeHolder in outerBuildingParent.transform)
        {
            GameObject building = Instantiate(buildingPrefab) as GameObject;
            building.transform.rotation = placeHolder.rotation;
            building.transform.position = placeHolder.position+new Vector3(0,5,0);

            //give building values using the Building script
            var buildingAttributes = building.GetComponent<Subreddit>();
            buildingAttributes.subredditId = "";
            buildingAttributes.subredditName = "Building: " + i;

            //set the name on the front of the building
            var name = building.transform.Find("Name").GetComponent<TextMesh>();
            name.text = buildingAttributes.subredditName;
            i++;
        }

        //TODO: For each path, chance size appropriately

    }

    protected override void setPlayerState()
    {
        GameInfo.instance.player.transform.position = new Vector3(0, 1, 10);
    }

    


}
