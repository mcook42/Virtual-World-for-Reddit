using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using Newtonsoft.Json.Linq;

public class SubredditDomeState : SceneState<SubredditDomeState> {

    public Subreddit centerBuilding=null;

    public List<Subreddit> innerBuildings;
    public List<Subreddit> outerBuildings;

    /// <summary>
    /// TODO: Gets the building information and stores it from the server. Does not activate the buildings yet.
    /// </summary>
    /// <param name="centerBuilding">The </param>
    public void loadBuildings(Subreddit centerBuilding)
    {
        this.centerBuilding = centerBuilding;

		List<String> outerBuildingNames = new List<String>();
		List<String> innerBuildingNames = new List<String> ();

		/**
		outerBuildingNames.Add ("/r/AskReddit");
		outerBuildingNames.Add("/r/funny"); 
		outerBuildingNames.Add("/r/conspiracy"); 
		outerBuildingNames.Add("/r/aww"); 
		outerBuildingNames.Add("/r/askhistorians"); 
		outerBuildingNames.Add("/r/android"); 
		outerBuildingNames.Add("/r/DIY"); 
		outerBuildingNames.Add("/r/astronomy");
		outerBuildingNames.Add("/r/AmISexy");
		outerBuildingNames.Add("/r/AdviceAnimals"); 
		outerBuildingNames.Add("/r/shittyaskhistory"); 
		outerBuildingNames.Add("/r/Games");
		outerBuildingNames.Add("/r/Games");
		outerBuildingNames.Add("/r/Games");
		outerBuildingNames.Add("/r/Games");
		*/


		innerBuildingNames.Add("/r/science"); 
		innerBuildingNames.Add("/r/news");
		innerBuildingNames.Add("/r/politics"); 
		innerBuildingNames.Add("/r/worldnews"); 
		innerBuildingNames.Add("/r/bestof"); 
		innerBuildingNames.Add("/r/explainitlikeimfive");
		innerBuildingNames.Add("/r/LifeProTips"); 
		innerBuildingNames.Add("/r/space"); 
		//innerBuildingNames.Add("/r/IAmA"); 
		//innerBuildingNames.Add("/r/atheism");
		//innerBuildingNames.Add("/r/todayILearned"); 
		//innerBuildingNames.Add("/r/technology");
		//innerBuildingNames.Add("/r/space"); 

		innerBuildings = GameInfo.instance.server.getSubreddits (innerBuildingNames);
		outerBuildings = GameInfo.instance.server.getSubreddits (outerBuildingNames);








    }

    public override void clear()
    {
        
    }

    public override void reset()
    {
        centerBuilding = null;
        if(innerBuildings!=null)
            innerBuildings.Clear();
        if(outerBuildings!=null)
            outerBuildings.Clear();
    }

}


	
	

