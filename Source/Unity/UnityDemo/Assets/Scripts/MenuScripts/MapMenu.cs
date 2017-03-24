using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RedditSharp.Things;
using Graph;
using System.Net;

public class MapMenu : Menu<MapMenu> {

    public GameObject nodePrefab;
	public GameObject linePrefab;

	public GameObject subscriptionPanel;
	public GameObject subscriptionContent;
	public GameObject subscriptionButtonPrefab;

	public InputField inputField;

	private List<GameObject> lines = new List<GameObject> ();
	private Transform content;

	public float maxNodeSize{ get { return 3; } }

	// Use this for initialization
	void Start () {
		if (GameInfo.instance.reddit.User != null) {
			initializeSubscriptions ();
		}
        DrawGraph();
		
	}

	/// <summary>
	/// Initializes the subscriptions.
	/// </summary>
	void initializeSubscriptions()
	{

		subscriptionPanel.SetActive (true);

		try{

	
			bool noSubs = true;
			foreach(Subreddit subscription in GameInfo.instance.reddit.User.SubscribedSubreddits)
			{
				noSubs = false;
				initializeSubreddit(subscription);

			}

			if(noSubs)
			{
				var subs = GameInfo.instance.reddit.GetDefaultSubreddits();
				foreach(var subscription in subs)
				{
					initializeSubreddit(subscription);
				}
			}


		}
		catch(WebException w) {
			GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Web Error: " + w.Message);
		}

	}

	/// <summary>
	/// Initializes the subreddit and adds it to the subsciption list. 
	/// </summary>
	/// <param name="subscription">Subreddit</param>
	private void initializeSubreddit(Subreddit subscription)
	{
		GameObject button = Instantiate(subscriptionButtonPrefab);
		button.transform.SetParent(subscriptionContent.transform,true);
		var name = button.GetComponent<Button>().transform.Find("Text").GetComponent<Text>().text = subscription.DisplayName;
		button.GetComponent<Button>().onClick.AddListener(()=>goToSubreddit(name));

	}
    /// <summary>
    /// TODO: Given the data located in MapInfo, draws the graph using somesort of force directed graph algorithm.
    /// </summary>
	void DrawGraph()
    {

         content = instance.transform.FindChild("Scroll View/Viewport/Content/");

        if(content==null)
        {
            GameInfo.instance.menuController.GetComponent<ErrorMenu>().loadMenu("Internal Error: Could not find Content on map viewport");
            return;
        }

        Vector2 center = new Vector2(1000, 800);
        RectTransform rectangle = instance.transform.FindChild("Scroll View/Viewport/Content/").GetComponent<RectTransform>();

        rectangle.sizeDelta = new Vector2(2000, 1000);


		foreach (Node<Subreddit> node in GameInfo.instance.map)
        {
			
            GameObject button = (GameObject)Instantiate(nodePrefab);
            button.transform.SetParent(content, false);

			button.transform.position = new Vector3(node.position.x+center.x, node.position.y+center.y,1);

            var name = button.transform.Find("Name").GetComponent<Text>();
			name.text = node.Value.DisplayName;

			float size = maxNodeSize;

			//The front page has no full name.
			if(node.Value.FullName!=null)
				size = 1+((float)node.Value.Subscribers) / 10000000;
			size = (size > maxNodeSize) ? maxNodeSize : size;
            button.transform.localScale = new Vector3(size, size, 1);

			setColor(button.GetComponent<Image>(), node.Value);


			button.GetComponent<Button>().onClick.AddListener(() => clickOnNode(node));
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

	/// <summary>
	/// Goes to the subreddit in the input field.
	/// </summary>
	public void goToSubreddit()
	{
		goToSubreddit (inputField.text);
	}

	/// <summary>
	/// Makes lines appear to the the neighbors. 
	/// </summary>
	/// <param name="node">Node.</param>
	public void clickOnNode(Node<Subreddit> node)
	{
		Debug.Log (node.Value);

		foreach (GameObject line in lines) {
			Destroy (line);
		}
		lines.Clear ();

		foreach (Node<Subreddit> neighbor in node.ToNeighbors) {
			var line = Instantiate (linePrefab);
			line.transform.SetParent (content,false);
			line.GetComponent<Line> ().Init (node,neighbor);
			lines.Add (line);

		}

	}
		

    public void goToSubreddit(string sub)
    {
        //activateLoadingScreen();SceneManager.LoadScene("SubredditDome");
        SubredditDometoSubredditDomeTransition transition = GetComponent<SubredditDometoSubredditDomeTransition>();
 
        unLoadMenu();
        transition.goToDome(sub);
    }


}
