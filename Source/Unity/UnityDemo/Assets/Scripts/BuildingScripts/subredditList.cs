using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubredditList : MonoBehaviour {

    public GameObject viewportContent;
    public GameObject buttonPrefab;

    void OnTriggerEnter(Collider other)
    {
        generateSubredditList();
    }

    void generateSubredditList()
    {
        for(int i=0; i<20;i++)
        {
            GameObject button = Instantiate(buttonPrefab) as GameObject;
            button.transform.SetParent(viewportContent.transform, false);
            button.transform.localScale = new Vector3(1, 1, 1);

        }
    }


}
