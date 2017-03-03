using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;
using RedditSharp.Things;

class SubredditDometoSubredditDomeTransition : SceneTransition
{
    Subreddit newCenter;
    public void goToDome(Subreddit newCenter)
    {
        this.newCenter = newCenter;
        transferInfo();
    }
    protected override void transferInfo()
    {
        activateLoadingScreen();
        SubredditDomeState.instance.loadBuildings(newCenter);
        SceneManager.LoadScene("SubredditDome");


    }
}
