using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RedditSharp.Things;

public class MapMenu : Menu<MapMenu> {

    public GameObject nodePrefab;

	// Use this for initialization
	void Start () {

        DrawGraph();
		
	}
	
    /// <summary>
    /// TODO: Given the data located in MapInfo, draws the graph using somesort of force directed graph algorithm.
    /// </summary>
	void DrawGraph()
    {

        Transform content = instance.transform.FindChild("Scroll View/Viewport/Content/");

        if(content==null)
        {
            GameInfo.instance.menuController.GetComponent<ErrorMenu>().loadMenu("Internal Error: Could not find Content on map viewport");
            return;
        }

        Vector2 center = new Vector2(1000, 800);
        RectTransform rectangle = instance.transform.FindChild("Scroll View/Viewport/Content/").GetComponent<RectTransform>();

        rectangle.sizeDelta = new Vector2(2000, 1000);

        {
            GameObject button = (GameObject)Instantiate(nodePrefab);
            button.transform.SetParent(content, false);
            button.transform.position = new Vector3(center.x, center.y, 1);

            var name = button.transform.Find("Name").GetComponent<Text>();
			name.text = SubredditDomeState.instance.centerBuilding.DisplayName;

            float size = 1+((float)SubredditDomeState.instance.centerBuilding.Subscribers) / 10000000;
            button.transform.localScale = new Vector3(size, size,1);

            setColor(button.GetComponent<Image>(), SubredditDomeState.instance.centerBuilding);

            Subreddit sub = SubredditDomeState.instance.centerBuilding;
            button.GetComponent<Button>().onClick.AddListener(() => goToSubreddit(sub));
        }

        float innerAngle = 2 * Mathf.PI / 13;
        for (int i = 0; i < 13; i++)
        {
            GameObject button = (GameObject)Instantiate(nodePrefab);
            button.transform.SetParent(content, false);

            float x = 90 * Mathf.Cos(innerAngle * i) + center.x;
            float y = 90 * Mathf.Sin(innerAngle * i) + center.y;
            button.transform.position = new Vector3(x, y, 1);

            var name = button.transform.Find("Name").GetComponent<Text>();
			name.text = SubredditDomeState.instance.innerBuildings [i].DisplayName;

			float size = 1+((float)SubredditDomeState.instance.innerBuildings[i].Subscribers) / 10000000;
            button.transform.localScale = new Vector3(size, size, 1);

            setColor(button.GetComponent<Image>(), SubredditDomeState.instance.innerBuildings[i]);

            Subreddit sub = SubredditDomeState.instance.innerBuildings[i];
            button.GetComponent<Button>().onClick.AddListener(() => goToSubreddit(sub));
        }

        float outerAngle = 2 * Mathf.PI / 12;
        for (int i = 0; i < 12; i++)
        {
            GameObject button = (GameObject)Instantiate(nodePrefab);
            button.transform.SetParent(content, false);

            float x = 180 * Mathf.Cos(outerAngle * i)+center.x;
            float y = 180 * Mathf.Sin(outerAngle * i)+center.y;
            button.transform.position = new Vector3(x, y, 1);

            var name = button.transform.Find("Name").GetComponent<Text>();
			name.text = SubredditDomeState.instance.outerBuildings[i].DisplayName;

			float size = 1+((float)SubredditDomeState.instance.outerBuildings[i].Subscribers) / 10000000;
            button.transform.localScale = new Vector3(size, size, 1);

            setColor(button.GetComponent<Image>(), SubredditDomeState.instance.outerBuildings[i]);

            Subreddit sub = SubredditDomeState.instance.outerBuildings[i];
            button.GetComponent<Button>().onClick.AddListener(() => goToSubreddit(sub));
        }
    
        
    }

    public void setColor(Image image,Subreddit sub)
    {
		/*
        if(sub.<5)
        {
            image.color = Color.red;
        }
        else if(sub.lexil<6)
        {
            image.color = Color.yellow;
        }
        else
        {
            image.color = Color.blue;
        }
        */
    }

    public void goToSubreddit(Subreddit sub)
    {
        //activateLoadingScreen();SceneManager.LoadScene("SubredditDome");

        SubredditDometoSubredditDomeTransition transition = GetComponent<SubredditDometoSubredditDomeTransition>();
 
        unLoadMenu();
        transition.goToDome(sub);
    }
}
