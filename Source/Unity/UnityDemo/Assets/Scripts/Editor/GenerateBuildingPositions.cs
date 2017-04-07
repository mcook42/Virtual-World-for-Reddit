﻿/**GenerateBuildingPositions.cs
* Caleb Whitman
* February 5, 2017
*/



using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// An editor script that creates objects to hold the building positions in the SubredditDome scene.
/// </summary>
public class GenerateBuildingPositions : MonoBehaviour {



	[MenuItem("GenerateBuildings/GenerateBuildings")]
    public static void generateBuildings()
    {

        GameObject buildingParent = new GameObject("buildingParent");
		GameObject pathParent = new GameObject ("pathParent");

        GameObject center = new GameObject("center");
        center.transform.position = new Vector3(0, 0, 0);

		float innerRadius = (SubredditDomeSetup.minPathWidth*SubredditDomeSetup.innerBuildNum/(Mathf.PI*2))+(SubredditDomeSetup.buildingFootprint/2)+SubredditDomeSetup.buildingFootprint / (2 * Mathf.Tan(180 / SubredditDomeSetup.innerBuildNum));
        
		if(innerRadius<SubredditDomeSetup.innerCircleSize)
        {
			innerRadius = SubredditDomeSetup.innerCircleSize;
        }
        
		float innerAngle = 2*Mathf.PI / SubredditDomeSetup.innerBuildNum;

        UnityEngine.Object pathPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Path.prefab", typeof(GameObject));

		for (int i=0; i<SubredditDomeSetup.innerBuildNum;i++)
        {
            GameObject building = new GameObject("Inner:" + i);
            float x = innerRadius * Mathf.Cos(innerAngle * i);
            float z =  innerRadius * Mathf.Sin(innerAngle * i);
            building.transform.position = new Vector3(x, 0, z);
            building.transform.eulerAngles = new Vector3(0,180+-innerAngle*i/(Mathf.PI*2)*360, 0);
            building.transform.SetParent(buildingParent.transform);

            GameObject path = Instantiate(pathPrefab) as GameObject;
			path.transform.localScale = new Vector3(0.5f, 1, (innerRadius-2*SubredditDomeSetup.buildingFootprint)/10);
			Vector3 middle = building.transform.position - new Vector3(3*SubredditDomeSetup.buildingFootprint * Mathf.Cos(innerAngle * i), 0.0f, 3*SubredditDomeSetup.buildingFootprint* Mathf.Sin(innerAngle * i));
            path.transform.position = new Vector3(middle.x, 0.01f, middle.z);
            path.transform.LookAt(building.transform.position);
            path.transform.SetParent(pathParent.transform);
            path.name = "InnerPath:" + i;
			path.SetActive (false);


        }
        
		float outerRadius=3*SubredditDomeSetup.buildingFootprint+ SubredditDomeSetup.buildingFootprint / (2 * Mathf.Tan(180 / SubredditDomeSetup.outerBuildNum));
        
		if (outerRadius<SubredditDomeSetup.outerCircleSize)
        {

			outerRadius = SubredditDomeSetup.outerCircleSize;
        }
        if (outerRadius < innerRadius)
        {
			outerRadius = innerRadius + 2 * SubredditDomeSetup.buildingFootprint;
        }
        
		float outerAngle= 2 * Mathf.PI / SubredditDomeSetup.outerBuildNum;

		for (int i = 0; i < SubredditDomeSetup.outerBuildNum; i++)
        {
            GameObject building = new GameObject("Outer: " + i);
            float x = outerRadius * Mathf.Cos(outerAngle * i);
            float z = outerRadius * Mathf.Sin(outerAngle * i);
            building.transform.position = new Vector3(x, 0, z);
			building.transform.localScale = new Vector3(SubredditDomeSetup.buildingFootprint, 5, SubredditDomeSetup.buildingFootprint);
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




    }
}
