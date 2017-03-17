using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RedditSharp.Things;
using Graph;

public class MapMenu : Menu<MapMenu> {

    public GameObject nodePrefab;

	public InputField inputField;

	public float maxNodeSize{ get { return 3; } }

	// Use this for initialization
	void Start () {
        DrawGraph();
		GameInfo.instance.keyController.SetActive (false);
		
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


		foreach (Node<Subreddit> node in GameInfo.instance.map)
        {
			
            GameObject button = (GameObject)Instantiate(nodePrefab);
            button.transform.SetParent(content, false);

			button.transform.position = new Vector3(node.position.x+center.x, node.position.y+center.y,1);

            var name = button.transform.Find("Name").GetComponent<Text>();
			name.text = node.Value.DisplayName;

			float size = 1+((float)node.Value.Subscribers) / 10000000;
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
	public void clickOnNode<T>(Node<T> node)
	{
		Debug.Log (node.Value);


	}

	struct GUILine{

		public Vector2 startPt;
		public Vector2 endPt;
	}

	void DrawLine(Vector2 pointA, Vector2 pointB)
	{
		Texture2D lineTex = new Texture2D (1, 1);
		Matrix4x4 matrixBackup = GUI.matrix;
		float width = 8.0f;
		GUI.color = Color.black;
		float angle = Mathf.Atan2 (pointB.y - pointA.y, pointB.x - pointA.x) * 180f / Mathf.PI;

		GUIUtility.RotateAroundPivot (angle, pointA);
		GUI.DrawTexture (new Rect (pointA.x, pointA.y, (pointB-pointA).magnitude, width), lineTex);

	}

    public void goToSubreddit(string sub)
    {
        //activateLoadingScreen();SceneManager.LoadScene("SubredditDome");
		Debug.Log (sub);
        SubredditDometoSubredditDomeTransition transition = GetComponent<SubredditDometoSubredditDomeTransition>();
 
        unLoadMenu();
        transition.goToDome(sub);
    }


	void unLoadMenu()
	{
		GameInfo.instance.keyController.SetActive (true);
		base.unLoadMenu ();


	}
}
