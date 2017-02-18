using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubredditDomeState : SceneState<SubredditDomeState> {



    public GameObject centerBuilding;

    private List<Subreddit> innerBuildings;
    private List<Subreddit> outerBuildings;


    public override void clear()
    {
        if (centerBuilding != null)
            centerBuilding.SetActive(false);
    }

    public override void reset()
    {
        if (centerBuilding != null)
            GameObject.Destroy(centerBuilding);
        if(innerBuildings!=null)
            innerBuildings.Clear();
        if(outerBuildings!=null)
            outerBuildings.Clear();
    }

    public void activateCenterBuilding()
    {
        if(centerBuilding!=null)
        {
            centerBuilding.SetActive(true);
        }
    }
}


	
	

