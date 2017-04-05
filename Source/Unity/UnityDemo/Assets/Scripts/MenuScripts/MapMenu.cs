using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RedditSharp.Things;
using Graph;
using System.Net;
using UnityEngine.EventSystems;

public class MapMenu : Menu<MapMenu>, LoginObserver, IScrollHandler {

	//Map
    public GameObject nodePrefab;
	public GameObject linePrefab;

	//how far we an zoom out/in
	private readonly float minMapScale = 0.03f;
	private readonly float maxMapScale = 1;

	//Subscriptions
	public GameObject subscriptionPanel;
	public GameObject subscriptionContent;
	public GameObject subscriptionButtonPrefab;

	//Navigation
	public InputField inputField;
	public GameObject homeButton;
	public GameObject selectedNodeButton;

	private List<GameObject> lines = new List<GameObject> ();
	private Transform content;



	public float maxNodeSize{ get { return 3; } }

	//Adds this object to the LoginObservers and then draws the graph.
	void Start () {
		notify (GameInfo.instance.reddit.User != null);
		GameInfo.instance.redditRetriever.register (this);
        DrawGraph();
	}

	/// <summary>
	/// Unregisters this menu from the LoginObervers.
	/// </summary>
	void OnDestroy()
	{
		GameInfo.instance.redditRetriever.unRegister (this);
	}

	/// <summary>
	/// Changes the map state based on whether or not they are logged in.
	/// </summary>
	/// <param name="login">If set to <c>true</c> login.</param>
	public void notify(bool login)
	{

		if (login) {
			initializeSubscriptions ();
			homeButton.SetActive (true);
		} else {
			subscriptionPanel.SetActive (false);
			homeButton.SetActive (false);
		}

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
    /// Given the data located in MapInfo, draws the graph..
    /// </summary>
	void DrawGraph()
    {

         content = instance.transform.FindChild("Scroll View/Viewport/Content/");

        if(content==null)
        {
            GameInfo.instance.menuController.GetComponent<ErrorMenu>().loadMenu("Internal Error: Could not find Content on map viewport");
            return;
        }

        Vector2 center = new Vector2(0, 0);
        RectTransform rectangle = instance.transform.FindChild("Scroll View/Viewport/Content/").GetComponent<RectTransform>();

		rectangle.sizeDelta = new Vector2(ForceDirectedLayout.maxPosition*2+maxNodeSize, ForceDirectedLayout.maxPosition*2+maxNodeSize);


		foreach (Node<Subreddit> node in GameInfo.instance.map)
        {
			
            GameObject button = (GameObject)Instantiate(nodePrefab);
            button.transform.SetParent(content, false);

			button.transform.GetComponent<RectTransform>().anchoredPosition= new Vector2(node.position.x+center.x, node.position.y+center.y);

            var name = button.transform.Find("Name").GetComponent<Text>();
			name.text = node.Value.DisplayName;

			float size = maxNodeSize;

			//The multireddits have no full name or subscriber count
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
	/// Makes lines appear to the the neighbors. 
	/// </summary>
	/// <param name="node">Node.</param>
	public void clickOnNode(Node<Subreddit> node)
	{

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

		string name = node.Value.DisplayName;
		selectedNodeButton.SetActive (true);
		selectedNodeButton.GetComponentInChildren<Text> ().text = name;
		selectedNodeButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
		selectedNodeButton.GetComponent<Button> ().onClick.AddListener (() => goToSubreddit (name));
	}

	/// <summary>
	/// Goes to the subreddit in the input field.
	/// </summary>
	public void goToSubreddit()
	{
		goToSubreddit (inputField.text);
	}




	public void goToSubreddit(string sub)
	{
		//activateLoadingScreen();SceneManager.LoadScene("SubredditDome");
		SubredditDometoSubredditDomeTransition transition = GetComponent<SubredditDometoSubredditDomeTransition>();

		unLoadMenu();
		transition.goToDome(sub);
	}

	#region navigation bar



	public void goToFront()
	{
		//activateLoadingScreen();SceneManager.LoadScene("SubredditDome");
		SubredditDometoSubredditDomeTransition transition = GetComponent<SubredditDometoSubredditDomeTransition>();

		unLoadMenu();
		transition.goToFront ();

	}

	/// <summary>
	/// Goes to all.
	/// </summary>
	public void goToAll()
	{
		//activateLoadingScreen();SceneManager.LoadScene("SubredditDome");
		SubredditDometoSubredditDomeTransition transition = GetComponent<SubredditDometoSubredditDomeTransition>();

		unLoadMenu();
		transition.goToAll();

	}

	/// <summary>
	/// Goes home.
	/// </summary>
	public void goToHouse()
	{
		SubredditDometoSubredditDomeTransition transition = GetComponent<SubredditDometoSubredditDomeTransition>();

		unLoadMenu();
		transition.goToHouse ();

	}

	#endregion


	#region scrolling
	public void OnScroll(PointerEventData eventData)
	{
		
		float scrollSpeed = 0.01f;
		Vector2 scrollDelta = eventData.scrollDelta;

		float newScale;
		newScale = (scrollDelta.y * scrollSpeed) + content.localScale.x;
		Debug.Log (newScale);
		if (newScale <= maxMapScale && newScale >= minMapScale)
			content.localScale = new Vector3 (newScale, newScale,1);


	}
	#endregion

	public void close()
	{
		unLoadMenu ();
	}

}
