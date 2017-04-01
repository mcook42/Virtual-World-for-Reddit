using System;
using System.Collections.Generic;
using RedditSharp.Things;
using Newtonsoft.Json.Linq;
using Graph;
using UnityEngine;

/// <summary>
/// Handles communication with the server.
/// All methods may throw a ServerDownExcpetion. The excpetion should be expected and handles appropriately.
/// </summary>
public class Server
{




	public Server ()
	{


	}
		

	/// <summary>
	/// A temporary method used to similate getting subreddits from a server.
	/// Always loads AskScience
	/// </summary>
	/// <returns>The subreddits.</returns>
	/// <param name="subreddits">The url field of each subreddit.</param>
	public Graph<Subreddit> getSubreddits(List<String> subreddits,String center)
	{
		Graph<Subreddit> returnGraph = new Graph<Subreddit> ();
		Subreddit tempCenter = new Subreddit ();
		tempCenter.Init(GameInfo.instance.reddit,askscienceJson,GameInfo.instance.webAgent);
		Node<Subreddit> centerNode = new Node<Subreddit>(tempCenter);
		returnGraph.AddNode (centerNode);

		foreach (String sub in subreddits) {

			var return_sub = new Subreddit ();


			switch(sub){

			case "/r/AskReddit":
				return_sub.Init (GameInfo.instance.reddit, AskRedditJson, GameInfo.instance.webAgent);

				break;
			case "/r/askscience":
				return_sub.Init (GameInfo.instance.reddit, askscienceJson, GameInfo.instance.webAgent);

				break;
			case "/r/science":
				return_sub.Init (GameInfo.instance.reddit, scienceJson, GameInfo.instance.webAgent);

				break;
			case "/r/politics":
				return_sub.Init (GameInfo.instance.reddit, politicsJson, GameInfo.instance.webAgent);

				break;
			case "/r/worldnews":
				return_sub.Init (GameInfo.instance.reddit, worldnewsJson, GameInfo.instance.webAgent);

				break;
			case "/r/bestof":
				return_sub.Init (GameInfo.instance.reddit, bestofJson, GameInfo.instance.webAgent);

				break;
			case "/r/explainitlikeimfive":
				return_sub.Init (GameInfo.instance.reddit, explainitlikeimfiveJson, GameInfo.instance.webAgent);

				break;
			case "/r/space":
				return_sub.Init (GameInfo.instance.reddit, spaceJson, GameInfo.instance.webAgent);

				break;
			
			default :
				return_sub.Init (GameInfo.instance.reddit, AskRedditJson, GameInfo.instance.webAgent);
				break;

			}
			System.Random random = new System.Random ();

			Node<Subreddit> node = new Node<Subreddit> (return_sub);
			returnGraph.AddNode(node);
			returnGraph.AddDirectedEdge (centerNode, node, Mathf.FloorToInt((float)random.NextDouble()*3));
			returnGraph.AddDirectedEdge (node, centerNode, Mathf.FloorToInt((float)random.NextDouble()*3));
		}

		return returnGraph;
	}

	/// <summary>
	/// Gets the subreddits.
	/// </summary>
	/// <returns>The subreddits if found, null otherwise</returns>
	/// <param name="subredditFullName">Subreddit full name.</param>
	public Graph<Subreddit> getSubreddits(String subredditFullName)
	{

		if (subredditFullName == "/r/askscience") {
			List<String> buildingNames = new List<String> ();

			buildingNames.Add ("/r/science"); 
			buildingNames.Add ("/r/news");
			buildingNames.Add ("/r/politics"); 
			buildingNames.Add ("/r/worldnews"); 
			buildingNames.Add ("/r/bestof"); 
			buildingNames.Add ("/r/explainitlikeimfive");
			buildingNames.Add ("/r/LifeProTips"); 
			buildingNames.Add ("/r/space"); 

			for (int i = 0; i < 100; i++) {
				buildingNames.Add ("/r/space"); 
			}

			return getSubreddits (buildingNames, subredditFullName);
		} else {

			return null;
		}

	}

	/// <summary>
	/// Gets the maps with the optional parameter of the center node.
	/// </summary>
	/// <param name="centerNode"> The Node that should be in the approximate center of the map </param>
	/// <returns>The map.</returns>
	public Graph<Subreddit> getMap(String centerNode)
	{
		//TODO: do stuff
		return new Graph<Subreddit>();
	}

	/// <summary>
	/// Gets the map.
	/// </summary>
	/// <returns>The map.</returns>
	public Graph<Subreddit> getMap()
	{
		return getMap (null);
	}

	private JObject askscienceJson = JObject.Parse (@"{
			""kind"": ""t5"",
			""data"": {
				""user_is_contributor"": null,
				""banner_img"": """",
				""submit_text_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;h2&gt;Please state your question succinctly in the title and include a question mark.&lt;/h2&gt;\n\n&lt;h2&gt;All submissions are placed in the spam filter for moderator review, so please be patient.&lt;/h2&gt;\n\n&lt;h1&gt;&lt;a href=\""http://www.reddit.com/r/askscience/wiki/index#wiki_asking_askscience\""&gt;Read our rules and guidelines before asking your question.&lt;/a&gt;&lt;/h1&gt;\n\n&lt;p&gt;Use the &lt;a href=\""http://www.reddit.com/r/askscience/search\""&gt;reddit search function&lt;/a&gt; and google &amp;#39;site:reddit.com&lt;a href=\""/r/askscience\""&gt;/r/askscience&lt;/a&gt;: search terms&amp;#39; before posting in order to avoid repeat questions.&lt;/p&gt;\n\n&lt;p&gt;&lt;strong&gt;Instructions for flairing your post:&lt;/strong&gt;&lt;/p&gt;\n\n&lt;p&gt;Once you click &amp;quot;submit,&amp;quot; there will be an option that is highlighted in yellow under the post that says &amp;quot;Please Click This To Categorize Your Post.&amp;quot; Once you click it, a menu will pop up and you will be able to choose what field your question falls under.&lt;/p&gt;\n\n&lt;p&gt;Thanks for asking your question in AskScience! :)&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""user_is_banned"": null,
				""wiki_enabled"": true,
				""show_media"": false,
				""id"": ""2qm4e"",
				""description"": ""### Please read our [**guidelines**](http://goo.gl/NZf5gP) and [**FAQ**](http://goo.gl/hfiJiJ) before posting\r\n\r\n* **Answer questions** with accurate, in-depth explanations, [including **peer-reviewed sources** where possible](/r/askscience/wiki/sources)\r\n* **Upvote** on-topic answers supported by reputable sources and scientific research\r\n* **Downvote** anecdotes, speculation, and jokes\r\n* **Report** comments that do not meet our [**guidelines**](http://goo.gl/NZf5gP), including [medical advice](http://goo.gl/UAVhTJ)\r\n* **Be civil:** [Remember the human](http://redd.it/1ytp7q) and follow [Reddiquette](http://www.reddit.com/wiki/reddiquette)\r\n\r\n\r\n\r\n###Features\r\n\r\n* [**Book List**](/r/askscience/wiki/booklist)\r\n* [**Mods' Choice**](http://goo.gl/lqBzfv): Outstanding posts recognized by the mod team\r\n* [**Weekly Features**](http://goo.gl/chKW86): Archives of AskAnything Wednesday, FAQ Fridays, and more!\r\n* [**FAQ**](http://goo.gl/hfiJiJ): In-depth answers to many popular questions\r\n* [**New**](http://goo.gl/s2ISrb) and [**gilded**](http://goo.gl/nNgvp7) posts\r\n* **/r/AskScienceDiscussion**: For open-ended and hypothetical questions \r\n\r\n### Filter by Field   \r\n\r\nTitle|Description\r\n:--|--:\r\n[Physics](/r/askscience/search?q=flair%3A%27Physics%27&amp;sort=new&amp;restrict_sr=on)|Theoretical Physics, Experimental Physics, High-energy Physics, Solid-State Physics, Fluid Dynamics, Relativity, Quantum Physics, Plasma Physics  \r\n[Mathematics](/r/askscience/search?q=flair%3A%27Maths%27&amp;sort=new&amp;restrict_sr=on)|Mathematics, Statistics, Number Theory, Calculus, Algebra\r\n[Astronomy](/r/askscience/search?q=flair%3A%27Astro%27&amp;sort=new&amp;restrict_sr=on)|Astronomy, Astrophysics, Cosmology, Planetary Formation  \r\n[Computing](/r/askscience/search?q=flair%3A%27Computing%27&amp;sort=new&amp;restrict_sr=on)|Computing, Artificial Intelligence, Machine Learning, Computability \r\n[Earth and Planetary Sciences](/r/askscience/search?q=flair%3A%27Geo%27&amp;sort=new&amp;restrict_sr=on)|Earth Science, Atmospheric Science, Oceanography, Geology  \r\n[Engineering](/r/askscience/search?q=flair%3A%27Eng%27&amp;sort=new&amp;restrict_sr=on)|Mechanical Engineering, Electrical Engineering, Structural Engineering, Computer Engineering, Aerospace Engineering\r\n[Chemistry](/r/askscience/search?q=flair%3A%27Chem%27&amp;sort=new&amp;restrict_sr=on)|Chemistry, Organic Chemistry, Polymers, Biochemistry\r\n[Social Sciences](/r/askscience/search?q=flair%3A%27Soc%27&amp;sort=new&amp;restrict_sr=on)|Social Science, Political Science, Economics, Archaeology, Anthropology, Linguistics  \r\n[Biology](/r/askscience/search?q=flair%3A%27Bio%27&amp;sort=new&amp;restrict_sr=on)|Biology, Evolution, Morphology, Ecology, Synthetic Biology, Microbiology, Cellular Biology, Molecular Biology, Paleontology   \r\n[Psychology](/r/askscience/search?q=flair%3A%27Psych%27&amp;sort=new&amp;restrict_sr=on)|Psychology, Cognitive Psychology, Developmental Psychology, Abnormal, Social Psychology\r\n[Medicine](/r/askscience/search?q=flair%3A%27Med%27&amp;sort=new&amp;restrict_sr=on)|Medicine, Oncology, Dentistry, Physiology, Epidemiology, Infectious Disease, Pharmacy, Human Body\r\n[Neuroscience](/r/askscience/search?q=flair%3A%27Neuro%27&amp;sort=new&amp;restrict_sr=on)|Neuroscience, Neurology, Neurochemistry, Cognitive Neuroscience   \r\n \r\n###Calendar\r\n##### [](/blank)\r\nDate|Description\r\n:--|:--\r\n1 Mar|Ask Anything Wednesday - Economics, Political science, Linguistics, Anthropology\n8 Mar|Ask Anything Wednesday - Physics, Astronomy, Earth and Planetary Science\n\r\n\r\n\r\n\r\n\r\n###Related subreddits\r\n\r\n* [Click here for a list of related subs!](/r/askscience/wiki/subreddit_links)\r\n\r\n### Are you a science expert?\r\n\r\n* **Looking for flair? [Sign up to be a panelist!](/r/askscience/comments/5rqtmc/askscience_panel_of_scientists_xvi/)** \r\n\r\n---\r\n[Header Information](http://goo.gl/cRQDGr) \r\n\r\n* [Switch to dark theme](http://goo.gl/wRzlXr)\r\n* [Switch to light theme](http://goo.gl/cYwdsu)\r\n\r\n[](#/RES_SR_Config/NightModeCompatible)\r\n\r\n1. For more open-ended questions, try /r/AskScienceDiscussion | [Sign up to be a panelist!](/r/askscience/comments/5rqtmc/askscience_panel_of_scientists_xvi/)\r\n\r\n\r\n\r\nWe make our world significant by the courage of our questions and by the depth of our answers. -Carl Sagan, *Cosmos*"",
				""submit_text"": ""##Please state your question succinctly in the title and include a question mark.\n\n##All submissions are placed in the spam filter for moderator review, so please be patient.\n\n#[Read our rules and guidelines before asking your question.](http://www.reddit.com/r/askscience/wiki/index#wiki_asking_askscience)\n\nUse the [reddit search function](http://www.reddit.com/r/askscience/search) and google 'site:reddit.com/r/askscience: search terms' before posting in order to avoid repeat questions.\n\n**Instructions for flairing your post:**\n\nOnce you click \""submit,\"" there will be an option that is highlighted in yellow under the post that says \""Please Click This To Categorize Your Post.\"" Once you click it, a menu will pop up and you will be able to choose what field your question falls under.\n\nThanks for asking your question in AskScience! :)"",
				""display_name"": ""askscience"",
				""header_img"": ""http://a.thumbs.redditmedia.com/KtS37yyOy-MnvMyMfKgjib6M07bHEZMAUu5lCIkEF40.png"",
				""description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;h3&gt;Please read our &lt;a href=\""http://goo.gl/NZf5gP\""&gt;&lt;strong&gt;guidelines&lt;/strong&gt;&lt;/a&gt; and &lt;a href=\""http://goo.gl/hfiJiJ\""&gt;&lt;strong&gt;FAQ&lt;/strong&gt;&lt;/a&gt; before posting&lt;/h3&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;strong&gt;Answer questions&lt;/strong&gt; with accurate, in-depth explanations, &lt;a href=\""/r/askscience/wiki/sources\""&gt;including &lt;strong&gt;peer-reviewed sources&lt;/strong&gt; where possible&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;strong&gt;Upvote&lt;/strong&gt; on-topic answers supported by reputable sources and scientific research&lt;/li&gt;\n&lt;li&gt;&lt;strong&gt;Downvote&lt;/strong&gt; anecdotes, speculation, and jokes&lt;/li&gt;\n&lt;li&gt;&lt;strong&gt;Report&lt;/strong&gt; comments that do not meet our &lt;a href=\""http://goo.gl/NZf5gP\""&gt;&lt;strong&gt;guidelines&lt;/strong&gt;&lt;/a&gt;, including &lt;a href=\""http://goo.gl/UAVhTJ\""&gt;medical advice&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;strong&gt;Be civil:&lt;/strong&gt; &lt;a href=\""http://redd.it/1ytp7q\""&gt;Remember the human&lt;/a&gt; and follow &lt;a href=\""http://www.reddit.com/wiki/reddiquette\""&gt;Reddiquette&lt;/a&gt;&lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;h3&gt;Features&lt;/h3&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;a href=\""/r/askscience/wiki/booklist\""&gt;&lt;strong&gt;Book List&lt;/strong&gt;&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""http://goo.gl/lqBzfv\""&gt;&lt;strong&gt;Mods&amp;#39; Choice&lt;/strong&gt;&lt;/a&gt;: Outstanding posts recognized by the mod team&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""http://goo.gl/chKW86\""&gt;&lt;strong&gt;Weekly Features&lt;/strong&gt;&lt;/a&gt;: Archives of AskAnything Wednesday, FAQ Fridays, and more!&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""http://goo.gl/hfiJiJ\""&gt;&lt;strong&gt;FAQ&lt;/strong&gt;&lt;/a&gt;: In-depth answers to many popular questions&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""http://goo.gl/s2ISrb\""&gt;&lt;strong&gt;New&lt;/strong&gt;&lt;/a&gt; and &lt;a href=\""http://goo.gl/nNgvp7\""&gt;&lt;strong&gt;gilded&lt;/strong&gt;&lt;/a&gt; posts&lt;/li&gt;\n&lt;li&gt;&lt;strong&gt;&lt;a href=\""/r/AskScienceDiscussion\""&gt;/r/AskScienceDiscussion&lt;/a&gt;&lt;/strong&gt;: For open-ended and hypothetical questions &lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;h3&gt;Filter by Field&lt;/h3&gt;\n\n&lt;table&gt;&lt;thead&gt;\n&lt;tr&gt;\n&lt;th align=\""left\""&gt;Title&lt;/th&gt;\n&lt;th align=\""right\""&gt;Description&lt;/th&gt;\n&lt;/tr&gt;\n&lt;/thead&gt;&lt;tbody&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/askscience/search?q=flair%3A%27Physics%27&amp;amp;sort=new&amp;amp;restrict_sr=on\""&gt;Physics&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""right\""&gt;Theoretical Physics, Experimental Physics, High-energy Physics, Solid-State Physics, Fluid Dynamics, Relativity, Quantum Physics, Plasma Physics&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/askscience/search?q=flair%3A%27Maths%27&amp;amp;sort=new&amp;amp;restrict_sr=on\""&gt;Mathematics&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""right\""&gt;Mathematics, Statistics, Number Theory, Calculus, Algebra&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/askscience/search?q=flair%3A%27Astro%27&amp;amp;sort=new&amp;amp;restrict_sr=on\""&gt;Astronomy&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""right\""&gt;Astronomy, Astrophysics, Cosmology, Planetary Formation&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/askscience/search?q=flair%3A%27Computing%27&amp;amp;sort=new&amp;amp;restrict_sr=on\""&gt;Computing&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""right\""&gt;Computing, Artificial Intelligence, Machine Learning, Computability&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/askscience/search?q=flair%3A%27Geo%27&amp;amp;sort=new&amp;amp;restrict_sr=on\""&gt;Earth and Planetary Sciences&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""right\""&gt;Earth Science, Atmospheric Science, Oceanography, Geology&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/askscience/search?q=flair%3A%27Eng%27&amp;amp;sort=new&amp;amp;restrict_sr=on\""&gt;Engineering&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""right\""&gt;Mechanical Engineering, Electrical Engineering, Structural Engineering, Computer Engineering, Aerospace Engineering&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/askscience/search?q=flair%3A%27Chem%27&amp;amp;sort=new&amp;amp;restrict_sr=on\""&gt;Chemistry&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""right\""&gt;Chemistry, Organic Chemistry, Polymers, Biochemistry&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/askscience/search?q=flair%3A%27Soc%27&amp;amp;sort=new&amp;amp;restrict_sr=on\""&gt;Social Sciences&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""right\""&gt;Social Science, Political Science, Economics, Archaeology, Anthropology, Linguistics&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/askscience/search?q=flair%3A%27Bio%27&amp;amp;sort=new&amp;amp;restrict_sr=on\""&gt;Biology&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""right\""&gt;Biology, Evolution, Morphology, Ecology, Synthetic Biology, Microbiology, Cellular Biology, Molecular Biology, Paleontology&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/askscience/search?q=flair%3A%27Psych%27&amp;amp;sort=new&amp;amp;restrict_sr=on\""&gt;Psychology&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""right\""&gt;Psychology, Cognitive Psychology, Developmental Psychology, Abnormal, Social Psychology&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/askscience/search?q=flair%3A%27Med%27&amp;amp;sort=new&amp;amp;restrict_sr=on\""&gt;Medicine&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""right\""&gt;Medicine, Oncology, Dentistry, Physiology, Epidemiology, Infectious Disease, Pharmacy, Human Body&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/askscience/search?q=flair%3A%27Neuro%27&amp;amp;sort=new&amp;amp;restrict_sr=on\""&gt;Neuroscience&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""right\""&gt;Neuroscience, Neurology, Neurochemistry, Cognitive Neuroscience&lt;/td&gt;\n&lt;/tr&gt;\n&lt;/tbody&gt;&lt;/table&gt;\n\n&lt;h3&gt;Calendar&lt;/h3&gt;\n\n&lt;h5&gt;&lt;a href=\""/blank\""&gt;&lt;/a&gt;&lt;/h5&gt;\n\n&lt;table&gt;&lt;thead&gt;\n&lt;tr&gt;\n&lt;th align=\""left\""&gt;Date&lt;/th&gt;\n&lt;th align=\""left\""&gt;Description&lt;/th&gt;\n&lt;/tr&gt;\n&lt;/thead&gt;&lt;tbody&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;1 Mar&lt;/td&gt;\n&lt;td align=\""left\""&gt;Ask Anything Wednesday - Economics, Political science, Linguistics, Anthropology&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;8 Mar&lt;/td&gt;\n&lt;td align=\""left\""&gt;Ask Anything Wednesday - Physics, Astronomy, Earth and Planetary Science&lt;/td&gt;\n&lt;/tr&gt;\n&lt;/tbody&gt;&lt;/table&gt;\n\n&lt;h3&gt;Related subreddits&lt;/h3&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;a href=\""/r/askscience/wiki/subreddit_links\""&gt;Click here for a list of related subs!&lt;/a&gt;&lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;h3&gt;Are you a science expert?&lt;/h3&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;strong&gt;Looking for flair? &lt;a href=\""/r/askscience/comments/5rqtmc/askscience_panel_of_scientists_xvi/\""&gt;Sign up to be a panelist!&lt;/a&gt;&lt;/strong&gt; &lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;hr/&gt;\n\n&lt;p&gt;&lt;a href=\""http://goo.gl/cRQDGr\""&gt;Header Information&lt;/a&gt; &lt;/p&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;a href=\""http://goo.gl/wRzlXr\""&gt;Switch to dark theme&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""http://goo.gl/cYwdsu\""&gt;Switch to light theme&lt;/a&gt;&lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;p&gt;&lt;a href=\""#/RES_SR_Config/NightModeCompatible\""&gt;&lt;/a&gt;&lt;/p&gt;\n\n&lt;ol&gt;\n&lt;li&gt;For more open-ended questions, try &lt;a href=\""/r/AskScienceDiscussion\""&gt;/r/AskScienceDiscussion&lt;/a&gt; | &lt;a href=\""/r/askscience/comments/5rqtmc/askscience_panel_of_scientists_xvi/\""&gt;Sign up to be a panelist!&lt;/a&gt;&lt;/li&gt;\n&lt;/ol&gt;\n\n&lt;p&gt;We make our world significant by the courage of our questions and by the depth of our answers. -Carl Sagan, &lt;em&gt;Cosmos&lt;/em&gt;&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""title"": ""AskScience: Got Questions? Get Answers."",
				""collapse_deleted_comments"": true,
				""public_description"": ""Ask a science question, get a science answer."",
				""over18"": false,
				""public_description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;p&gt;Ask a science question, get a science answer.&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""spoilers_enabled"": true,
				""icon_size"": null,
				""suggested_comment_sort"": null,
				""icon_img"": """",
				""header_title"": ""AskScience"",
				""display_name_prefixed"": ""r/askscience"",
				""user_is_muted"": null,
				""submit_link_label"": null,
				""accounts_active"": 4006,
				""public_traffic"": true,
				""header_size"": [
					70,
					72
				],
				""subscribers"": 12783978,
				""submit_text_label"": ""Ask a science question"",
				""key_color"": """",
				""lang"": ""en"",
				""whitelist_status"": ""all_ads"",
				""name"": ""t5_2qm4e"",
				""created"": 1220628764.0,
				""url"": ""/r/askscience/"",
				""quarantine"": false,
				""hide_ads"": false,
				""created_utc"": 1220599964.0,
				""banner_size"": null,
				""user_is_moderator"": null,
				""accounts_active_is_fuzzed"": false,
				""advertiser_category"": ""Lifestyles"",
				""user_sr_theme_enabled"": true,
				""allow_images"": true,
				""show_media_preview"": true,
				""comment_score_hide_mins"": 0,
				""subreddit_type"": ""public"",
				""submission_type"": ""self"",
				""user_is_subscriber"": null
			}
		}"
		);
	private JObject scienceJson = JObject.Parse (@"{
			""kind"": ""t5"",
			""data"": {
				""user_is_contributor"": null,
				""banner_img"": """",
				""submit_text_html"": null,
				""user_is_banned"": null,
				""wiki_enabled"": true,
				""show_media"": true,
				""id"": ""mouw"",
				""description"": ""# [Submission Requirements](http://www.reddit.com/r/science/wiki/rules#wiki_submission_rules)\r\n\r\n1. Directly link to published peer-reviewed research articles or a brief media summary\r\n2. No summaries of summaries, reviews or popular reposts (over 100 upvotes)\r\n3. Research must be less than 6 months old\r\n4. No sensationalized titles, all titles must include the model where applicable\r\n5. No blogspam, images, videos, infographics\r\n6. All submissions must be flaired and contain a link to the published article, either in the submission link or as a standalone comment.\r\n\r\n#[Comment Rules](http://www.reddit.com/r/science/wiki/rules#wiki_comment_rules)\r\n\r\n1. On-topic. No memes/jokes/etc.\r\n2. No abusive/offensive/spam comments.\r\n3. Non-professional personal anecdotes may be removed\r\n4. Arguments dismissing established scientific theories must contain substantial, peer-reviewed evidence\r\n5. No medical advice!\r\n6. Repeat or flagrant offenders may be banned.\r\n\r\n\r\n\r\n---\r\n\r\n## [Reddit Science AMA Submission Guide](https://drive.google.com/file/d/0B3fzgHAW-mVZdnBKaHhCM1RlMFU)\r\n\r\n## [New to reddit? Click here!](https://www.reddit.com/wiki/reddit_101)\r\n\r\n## [Get flair in /r/science](https://www.reddit.com/r/science/wiki/flair)\r\n\r\n## [Previous Science AMA's](https://www.reddit.com/r/science/search?q=flair%3A%27AMA%27&amp;sort=new&amp;restrict_sr=on)\r\n\r\n&gt; \r\n- **filter by field**\r\n- [Medicine](https://goo.gl/Fqh7eE)\r\n- [Epidemiology](https://goo.gl/7I7jJ5)\r\n- [Physics](https://goo.gl/OMXDb3)\r\n- [Computer Sci](https://goo.gl/df9L0Q)\r\n- [Astronomy](https://goo.gl/bAISyw)\r\n- [Mathematics](https://goo.gl/LhTCSL)\r\n- [Health](https://goo.gl/jj1HRl)\r\n- [Chemistry](https://goo.gl/XV3QOM)\r\n- [Nanoscience](https://goo.gl/AI5LfC)\r\n- [Biology](https://goo.gl/CVt9UW)\r\n- [Environment](https://goo.gl/bdAgJs)\r\n- [Animal Sci](https://goo.gl/ypqlBo)\r\n- [Neuroscience](https://goo.gl/4y4HDU)\r\n- [Psychology](https://goo.gl/iFyWz1)\r\n- [Cancer](https://goo.gl/3mKLIq)\r\n- [Soc Sciences](https://goo.gl/Kne3Ii)\r\n- [Anthropology](https://goo.gl/23Q3hu)\r\n- [Paleontology](https://goo.gl/SzET4J)\r\n- [Engineering](https://goo.gl/G4G6ES)\r\n- [Geology](https://goo.gl/A5QGPm)\r\n- [Sci Discussion](https://goo.gl/JcvrmP)\r\n\r\n---\r\n\r\n# Upcoming AMAs *(All times and dates are USA East Coast Time)*\r\n\r\nDate Time (Eastern Time - USA)|Person|Description\r\n:-------:|:--------:|:--------:\r\n1 Mar-1pm|PLOS Science Wednesday: Mike Snyder | Wearables\n1 Mar-4pm|Jennifer Watling Neal, Emily Durbin, Allison Gornik, &amp; Sharon Lo | Kids’ Personalities are Shaped by their Peers\n2 Mar-1pm|Portland State Prof. Emily Fitzgibbons Shafer | Marriage Surnames\n2 Mar-4pm|Bonnie Rochman | How Genetic Technologies Are Reshaping Families\n3 Mar-1pm|Dr. William Gahl, Dr. Cyndi Tifft and Chad Smith | The Undiagnosed Diseases Network and medical mysteries\n3 Mar-1pm|NOAA | Understanding the Lionfish Invasion\n6 Mar-1pm|American Geophysical Union AMA: Dr. Mike Brudzinski\n7 Mar-11am|American Chemical Society AMA\n\r\n\r\n######*Trending:* **[Science AMA Series: This is Dr. Jenna Watling Neal, Dr. Emily Durbin, Allison Gornik, &amp; Sharon Lo, psychologists at Michigan State University. We’re here to discuss how preschool kids’ social networks shape their personalities &amp; vice versa. Ask us anything!](https://redd.it/5wvz03)**\n\r\n[](#/RES_SR_Config/NightModeCompatible)\r\n"",
				""submit_text"": """",
				""display_name"": ""science"",
				""header_img"": ""http://b.thumbs.redditmedia.com/qzIP8EKeRG_kef7dVYI5ojcPj_V9kk5xyByof-Fe0eE.png"",
				""description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;h1&gt;&lt;a href=\""http://www.reddit.com/r/science/wiki/rules#wiki_submission_rules\""&gt;Submission Requirements&lt;/a&gt;&lt;/h1&gt;\n\n&lt;ol&gt;\n&lt;li&gt;Directly link to published peer-reviewed research articles or a brief media summary&lt;/li&gt;\n&lt;li&gt;No summaries of summaries, reviews or popular reposts (over 100 upvotes)&lt;/li&gt;\n&lt;li&gt;Research must be less than 6 months old&lt;/li&gt;\n&lt;li&gt;No sensationalized titles, all titles must include the model where applicable&lt;/li&gt;\n&lt;li&gt;No blogspam, images, videos, infographics&lt;/li&gt;\n&lt;li&gt;All submissions must be flaired and contain a link to the published article, either in the submission link or as a standalone comment.&lt;/li&gt;\n&lt;/ol&gt;\n\n&lt;h1&gt;&lt;a href=\""http://www.reddit.com/r/science/wiki/rules#wiki_comment_rules\""&gt;Comment Rules&lt;/a&gt;&lt;/h1&gt;\n\n&lt;ol&gt;\n&lt;li&gt;On-topic. No memes/jokes/etc.&lt;/li&gt;\n&lt;li&gt;No abusive/offensive/spam comments.&lt;/li&gt;\n&lt;li&gt;Non-professional personal anecdotes may be removed&lt;/li&gt;\n&lt;li&gt;Arguments dismissing established scientific theories must contain substantial, peer-reviewed evidence&lt;/li&gt;\n&lt;li&gt;No medical advice!&lt;/li&gt;\n&lt;li&gt;Repeat or flagrant offenders may be banned.&lt;/li&gt;\n&lt;/ol&gt;\n\n&lt;hr/&gt;\n\n&lt;h2&gt;&lt;a href=\""https://drive.google.com/file/d/0B3fzgHAW-mVZdnBKaHhCM1RlMFU\""&gt;Reddit Science AMA Submission Guide&lt;/a&gt;&lt;/h2&gt;\n\n&lt;h2&gt;&lt;a href=\""https://www.reddit.com/wiki/reddit_101\""&gt;New to reddit? Click here!&lt;/a&gt;&lt;/h2&gt;\n\n&lt;h2&gt;&lt;a href=\""https://www.reddit.com/r/science/wiki/flair\""&gt;Get flair in /r/science&lt;/a&gt;&lt;/h2&gt;\n\n&lt;h2&gt;&lt;a href=\""https://www.reddit.com/r/science/search?q=flair%3A%27AMA%27&amp;amp;sort=new&amp;amp;restrict_sr=on\""&gt;Previous Science AMA&amp;#39;s&lt;/a&gt;&lt;/h2&gt;\n\n&lt;blockquote&gt;\n&lt;ul&gt;\n&lt;li&gt;&lt;strong&gt;filter by field&lt;/strong&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/Fqh7eE\""&gt;Medicine&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/7I7jJ5\""&gt;Epidemiology&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/OMXDb3\""&gt;Physics&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/df9L0Q\""&gt;Computer Sci&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/bAISyw\""&gt;Astronomy&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/LhTCSL\""&gt;Mathematics&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/jj1HRl\""&gt;Health&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/XV3QOM\""&gt;Chemistry&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/AI5LfC\""&gt;Nanoscience&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/CVt9UW\""&gt;Biology&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/bdAgJs\""&gt;Environment&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/ypqlBo\""&gt;Animal Sci&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/4y4HDU\""&gt;Neuroscience&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/iFyWz1\""&gt;Psychology&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/3mKLIq\""&gt;Cancer&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/Kne3Ii\""&gt;Soc Sciences&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/23Q3hu\""&gt;Anthropology&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/SzET4J\""&gt;Paleontology&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/G4G6ES\""&gt;Engineering&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/A5QGPm\""&gt;Geology&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""https://goo.gl/JcvrmP\""&gt;Sci Discussion&lt;/a&gt;&lt;/li&gt;\n&lt;/ul&gt;\n&lt;/blockquote&gt;\n\n&lt;hr/&gt;\n\n&lt;h1&gt;Upcoming AMAs &lt;em&gt;(All times and dates are USA East Coast Time)&lt;/em&gt;&lt;/h1&gt;\n\n&lt;table&gt;&lt;thead&gt;\n&lt;tr&gt;\n&lt;th align=\""center\""&gt;Date Time (Eastern Time - USA)&lt;/th&gt;\n&lt;th align=\""center\""&gt;Person&lt;/th&gt;\n&lt;th align=\""center\""&gt;Description&lt;/th&gt;\n&lt;/tr&gt;\n&lt;/thead&gt;&lt;tbody&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;1 Mar-1pm&lt;/td&gt;\n&lt;td align=\""center\""&gt;PLOS Science Wednesday: Mike Snyder&lt;/td&gt;\n&lt;td align=\""center\""&gt;Wearables&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;1 Mar-4pm&lt;/td&gt;\n&lt;td align=\""center\""&gt;Jennifer Watling Neal, Emily Durbin, Allison Gornik, &amp;amp; Sharon Lo&lt;/td&gt;\n&lt;td align=\""center\""&gt;Kids’ Personalities are Shaped by their Peers&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;2 Mar-1pm&lt;/td&gt;\n&lt;td align=\""center\""&gt;Portland State Prof. Emily Fitzgibbons Shafer&lt;/td&gt;\n&lt;td align=\""center\""&gt;Marriage Surnames&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;2 Mar-4pm&lt;/td&gt;\n&lt;td align=\""center\""&gt;Bonnie Rochman&lt;/td&gt;\n&lt;td align=\""center\""&gt;How Genetic Technologies Are Reshaping Families&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;3 Mar-1pm&lt;/td&gt;\n&lt;td align=\""center\""&gt;Dr. William Gahl, Dr. Cyndi Tifft and Chad Smith&lt;/td&gt;\n&lt;td align=\""center\""&gt;The Undiagnosed Diseases Network and medical mysteries&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;3 Mar-1pm&lt;/td&gt;\n&lt;td align=\""center\""&gt;NOAA&lt;/td&gt;\n&lt;td align=\""center\""&gt;Understanding the Lionfish Invasion&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;6 Mar-1pm&lt;/td&gt;\n&lt;td align=\""center\""&gt;American Geophysical Union AMA: Dr. Mike Brudzinski&lt;/td&gt;\n&lt;td align=\""center\""&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;7 Mar-11am&lt;/td&gt;\n&lt;td align=\""center\""&gt;American Chemical Society AMA&lt;/td&gt;\n&lt;td align=\""center\""&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;/tbody&gt;&lt;/table&gt;\n\n&lt;h6&gt;&lt;em&gt;Trending:&lt;/em&gt; &lt;strong&gt;&lt;a href=\""https://redd.it/5wvz03\""&gt;Science AMA Series: This is Dr. Jenna Watling Neal, Dr. Emily Durbin, Allison Gornik, &amp;amp; Sharon Lo, psychologists at Michigan State University. We’re here to discuss how preschool kids’ social networks shape their personalities &amp;amp; vice versa. Ask us anything!&lt;/a&gt;&lt;/strong&gt;&lt;/h6&gt;\n\n&lt;p&gt;&lt;a href=\""#/RES_SR_Config/NightModeCompatible\""&gt;&lt;/a&gt;&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""title"": ""Science"",
				""collapse_deleted_comments"": true,
				""public_description"": ""The Science subreddit is a place to share new findings. Read about the latest advances in astronomy, biology, medicine, physics and the social sciences. Find and submit the best writeup on the web about a discovery, and make sure it cites its sources."",
				""over18"": false,
				""public_description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;p&gt;The Science subreddit is a place to share new findings. Read about the latest advances in astronomy, biology, medicine, physics and the social sciences. Find and submit the best writeup on the web about a discovery, and make sure it cites its sources.&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""spoilers_enabled"": false,
				""icon_size"": [
					256,
					256
				],
				""suggested_comment_sort"": ""confidence"",
				""icon_img"": ""http://b.thumbs.redditmedia.com/NYGpyQu_WzaA81QlfiZ4pKz_OTFI5Gz0tLF8CHOzl-I.png"",
				""header_title"": null,
				""display_name_prefixed"": ""r/science"",
				""user_is_muted"": null,
				""submit_link_label"": null,
				""accounts_active"": 1731,
				""public_traffic"": true,
				""header_size"": [
					28,
					24
				],
				""subscribers"": 15645720,
				""submit_text_label"": null,
				""key_color"": """",
				""lang"": ""en"",
				""whitelist_status"": ""all_ads"",
				""name"": ""t5_mouw"",
				""created"": 1161208466.0,
				""url"": ""/r/science/"",
				""quarantine"": false,
				""hide_ads"": false,
				""created_utc"": 1161179666.0,
				""banner_size"": null,
				""user_is_moderator"": null,
				""accounts_active_is_fuzzed"": false,
				""advertiser_category"": ""Lifestyles"",
				""user_sr_theme_enabled"": true,
				""allow_images"": false,
				""show_media_preview"": false,
				""comment_score_hide_mins"": 60,
				""subreddit_type"": ""public"",
				""submission_type"": ""link"",
				""user_is_subscriber"": null
			}
		}"
		);
	private JObject politicsJson = JObject.Parse (@"{
			""kind"": ""t5"",
			""data"": {
				""user_is_contributor"": null,
				""banner_img"": ""http://a.thumbs.redditmedia.com/kcKnQt4TInkTARtKtsyRhvs5g3bdkVXq8wSJF6gDH20.png"",
				""submit_text_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;ol&gt;\n&lt;li&gt;&lt;p&gt;Submissions must be for &lt;strong&gt;Current U.S. Political news &amp;amp; information only.&lt;/strong&gt;&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;&lt;strong&gt;Do not create your own title for link posts or they will be removed.&lt;/strong&gt; &lt;/p&gt;\n\n&lt;p&gt;Your title must match the article&amp;#39;s headline &lt;strong&gt;exactly&lt;/strong&gt;. Do not add or remove words.&lt;/p&gt;\n\n&lt;p&gt;&lt;strong&gt;Note: Using Reddit&amp;#39;s &amp;#39;Submit Title&amp;#39; does not always give the exact title, it&amp;#39;s not recommended to use this feature &amp;amp; instead copy/paste the title from the article.&lt;/strong&gt; &lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Do not use ALL CAPS, or use “BREAKING” in your titles.&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Do not submit links to wikis, images, memes. Political cartoons &amp;amp; detailed info-graphics should be linked to their original source.&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;&lt;strong&gt;Submit from the original source of the article.&lt;/strong&gt; &lt;a href=\""http://www.reddit.com/r/politics/wiki/rulesandregs#wiki_ensure_that_you_are_using_the_original_source\""&gt;Blogspam and rehosted content will be removed.&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;\n&lt;/ol&gt;\n\n&lt;p&gt;Posts that do not follow these rules are subject to removal.&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""user_is_banned"": null,
				""wiki_enabled"": true,
				""show_media"": true,
				""id"": ""2cneq"",
				""description"": ""## **Welcome to /r/Politics! Please read [the wiki](http://www.goo.gl/o8SOTy) before participating.**   \r\n\r\n/r/Politics is the subreddit for current and explicitly political U.S. news.\r\n\r\n### [Our full rules](https://www.reddit.com/r/politics/wiki/index) [Reddiquette](https://www.reddit.com/wiki/reddiquette)\r\n\r\n# [Comment Guidelines](http://goo.gl/RxDhbM):\r\n\r\n ||\r\n:-:|:-:\r\nBe civil|Treat others with basic decency. No personal attacks, shill accusations, hate-speech, flaming, baiting, trolling, witch-hunting, or unsubstantiated accusations. Threats of violence will result in a ban. [More Info.](/r/politics/wiki/rulesandregs#wiki_please_be_civil)\r\nDo not post users' personal information.|Users who violate this rule will be banned on sight. Witch-hunting and giving out private personal details of other people can result in unexpected and potentially serious consequences for the individual targeted. [More Info.](/r/politics/wiki/rulesandregs#wiki_no_threats.2C_witch_hunting.2C_or_personal_information.)\r\nVote based on quality, not opinion.|Political discussion requires varied opinions. Well written and interesting content can be worthwhile, even if you disagree with it. Downvote only if you think a comment/post does not contribute to the thread it is posted in or if it is off-topic in /r/politics. [More Info.](/r/politics/wiki/rulesandregs#wiki_voting)\r\nDo not manipulate comments and posts via group voting.|Manipulating comments and posts via group voting is against reddit TOS. [More Info.](/r/politics/wiki/rulesandregs#wiki_do_not_manipulate_comments_and_posts_via_group_voting.)\r\n\r\n\r\n# [Submission Guidelines:](http://goo.gl/rPD0Fa)\r\n\r\n ||\r\n:-:|:-:\r\nArticles must deal explicitly with US politics.|[See our on-topic statement here.](/r/politics/wiki/rulesandregs#wiki_the_.2Fr.2Fpolitics_on_topic_statement)\r\nArticles must be published within the last 31 days.|[More Info.](/r/politics/wiki/rulesandregs#wiki_the_.2Fr.2Fpolitics_on_topic_statement)\r\nPost titles must be the exact headline from the article.|Your headline must be comprised only of the **exact** copied and pasted headline of the article. [More Info.](/r/politics/wiki/rulesandregs#wiki_do_not_create_your_own_title)\r\nSubmissions must be an original source.|An article must contain significant analysis and original content--not just a few links of text among chunks of copy and pasted material. Content is considered rehosted when a publication takes the majority of their content from another website and reposts it in order to get the traffic and collect ad revenue. [More Info.](/r/politics/wiki/rulesandregs#wiki_ensure_that_you_are_using_the_original_source)\r\nArticles must be written in English|An article must be **primarily** written in English for us to be able to moderate it and enforce our rules in a fair and unbiased manner. [More Info.](https://www.reddit.com/r/politics/wiki/rulesandregs#wiki_all_submissions_must_be_primarily_written_in_the_english_language.)\r\nSpam is bad!|If 33% or more of your submissions are from a single website, you will be banned as a spammer. [More Info.](/r/politics/wiki/rulesandregs#wiki_spam_is_bad.21)\r\nSubmissions must be articles, videos or sound clips.|We disallow solicitation of users (petitions, polls, requests for money, etc.), personal blogs, satire, images, social media content (Facebook, twitter, tumblr, LinkedIn, etc.), wikis, memes, and political advertisements. More info: [Content type rules.](/r/politics/wiki/rulesandregs#wiki_submissions_must_be_articles.2C_videos.2C_or_sound_clips.)\r\nDo not use \""BREAKING\"" or ALL CAPS in titles.|The ALL CAPS and 'Breaking' rule is applied even when the actual title of the article is in all caps or contains the word 'Breaking'. This rule may be applied to other single word declarative and/or sensational expressions, such as 'EXCLUSIVE:' or 'HOT:'. [More Info.](/r/politics/wiki/rulesandregs#wiki_follow_reddiquette.27s_title_instructions.)\r\n\r\n#Events Calendar\r\n\r\n&gt; 3 Mar - 12am EST\n\n * /r/Politics monthly Meta Thread\n\n&gt; 8 Mar - 9am EST\n\n * AMA with Michael and Andrew from the Center for Investigative Reporting\n\n\r\n\r\n\r\n# Other Resources:\r\n\r\n#### [Full list of Related Subreddits](http://www.reddit.com/r/politics/wiki/relatedsubs)\r\n\r\n#### [Chat with us on OrangeChat/IRC](https://app.orangechat.io/r/politics)\r\n* Click [here](https://app.orangechat.io/#/r/politics) to join our channel via webchat with easy log-in. Alternatively, to connect via IRC, point your client to #politics on irc.snoonet.org, port 6667.\r\n\r\n#### [Follow us on Twitter](http://twitter.com/rSlashPolitics)\r\n\r\n#### [Events Calendar](https://goo.gl/AH1EPQ)\r\n\r\n#### [Apply to be a mod](http://redditpolitics.pythonanywhere.com/)\r\n\r\n#### [Register To Vote](http://goo.gl/XHLkS)\r\n\r\n[](#/RES_SR_Config/NightModeCompatible)"",
				""submit_text"": ""1. Submissions must be for **Current U.S. Political news &amp; information only.**\n\n2. **Do not create your own title for link posts or they will be removed.** \n\n    Your title must match the article's headline **exactly**. Do not add or remove words.\n    \n    **Note: Using Reddit's 'Submit Title' does not always give the exact title, it's not recommended to use this feature &amp; instead copy/paste the title from the article.** \n\n3. Do not use ALL CAPS, or use “BREAKING” in your titles.\n\n4. Do not submit links to wikis, images, memes. Political cartoons &amp; detailed info-graphics should be linked to their original source.\n\n5. **Submit from the original source of the article.** [Blogspam and rehosted content will be removed.](http://www.reddit.com/r/politics/wiki/rulesandregs#wiki_ensure_that_you_are_using_the_original_source)\n\nPosts that do not follow these rules are subject to removal."",
				""display_name"": ""politics"",
				""header_img"": ""http://b.thumbs.redditmedia.com/fREkTALOGCmOydgClyhaIy6k30jm0NdIVpAT0wi--QA.png"",
				""description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;h2&gt;&lt;strong&gt;Welcome to &lt;a href=\""/r/Politics\""&gt;/r/Politics&lt;/a&gt;! Please read &lt;a href=\""http://www.goo.gl/o8SOTy\""&gt;the wiki&lt;/a&gt; before participating.&lt;/strong&gt;&lt;/h2&gt;\n\n&lt;p&gt;&lt;a href=\""/r/Politics\""&gt;/r/Politics&lt;/a&gt; is the subreddit for current and explicitly political U.S. news.&lt;/p&gt;\n\n&lt;h3&gt;&lt;a href=\""https://www.reddit.com/r/politics/wiki/index\""&gt;Our full rules&lt;/a&gt; &lt;a href=\""https://www.reddit.com/wiki/reddiquette\""&gt;Reddiquette&lt;/a&gt;&lt;/h3&gt;\n\n&lt;h1&gt;&lt;a href=\""http://goo.gl/RxDhbM\""&gt;Comment Guidelines&lt;/a&gt;:&lt;/h1&gt;\n\n&lt;table&gt;&lt;thead&gt;\n&lt;tr&gt;\n&lt;th align=\""center\""&gt;&lt;/th&gt;\n&lt;th align=\""center\""&gt;&lt;/th&gt;\n&lt;/tr&gt;\n&lt;/thead&gt;&lt;tbody&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;Be civil&lt;/td&gt;\n&lt;td align=\""center\""&gt;Treat others with basic decency. No personal attacks, shill accusations, hate-speech, flaming, baiting, trolling, witch-hunting, or unsubstantiated accusations. Threats of violence will result in a ban. &lt;a href=\""/r/politics/wiki/rulesandregs#wiki_please_be_civil\""&gt;More Info.&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;Do not post users&amp;#39; personal information.&lt;/td&gt;\n&lt;td align=\""center\""&gt;Users who violate this rule will be banned on sight. Witch-hunting and giving out private personal details of other people can result in unexpected and potentially serious consequences for the individual targeted. &lt;a href=\""/r/politics/wiki/rulesandregs#wiki_no_threats.2C_witch_hunting.2C_or_personal_information.\""&gt;More Info.&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;Vote based on quality, not opinion.&lt;/td&gt;\n&lt;td align=\""center\""&gt;Political discussion requires varied opinions. Well written and interesting content can be worthwhile, even if you disagree with it. Downvote only if you think a comment/post does not contribute to the thread it is posted in or if it is off-topic in &lt;a href=\""/r/politics\""&gt;/r/politics&lt;/a&gt;. &lt;a href=\""/r/politics/wiki/rulesandregs#wiki_voting\""&gt;More Info.&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;Do not manipulate comments and posts via group voting.&lt;/td&gt;\n&lt;td align=\""center\""&gt;Manipulating comments and posts via group voting is against reddit TOS. &lt;a href=\""/r/politics/wiki/rulesandregs#wiki_do_not_manipulate_comments_and_posts_via_group_voting.\""&gt;More Info.&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;/tbody&gt;&lt;/table&gt;\n\n&lt;h1&gt;&lt;a href=\""http://goo.gl/rPD0Fa\""&gt;Submission Guidelines:&lt;/a&gt;&lt;/h1&gt;\n\n&lt;table&gt;&lt;thead&gt;\n&lt;tr&gt;\n&lt;th align=\""center\""&gt;&lt;/th&gt;\n&lt;th align=\""center\""&gt;&lt;/th&gt;\n&lt;/tr&gt;\n&lt;/thead&gt;&lt;tbody&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;Articles must deal explicitly with US politics.&lt;/td&gt;\n&lt;td align=\""center\""&gt;&lt;a href=\""/r/politics/wiki/rulesandregs#wiki_the_.2Fr.2Fpolitics_on_topic_statement\""&gt;See our on-topic statement here.&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;Articles must be published within the last 31 days.&lt;/td&gt;\n&lt;td align=\""center\""&gt;&lt;a href=\""/r/politics/wiki/rulesandregs#wiki_the_.2Fr.2Fpolitics_on_topic_statement\""&gt;More Info.&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;Post titles must be the exact headline from the article.&lt;/td&gt;\n&lt;td align=\""center\""&gt;Your headline must be comprised only of the &lt;strong&gt;exact&lt;/strong&gt; copied and pasted headline of the article. &lt;a href=\""/r/politics/wiki/rulesandregs#wiki_do_not_create_your_own_title\""&gt;More Info.&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;Submissions must be an original source.&lt;/td&gt;\n&lt;td align=\""center\""&gt;An article must contain significant analysis and original content--not just a few links of text among chunks of copy and pasted material. Content is considered rehosted when a publication takes the majority of their content from another website and reposts it in order to get the traffic and collect ad revenue. &lt;a href=\""/r/politics/wiki/rulesandregs#wiki_ensure_that_you_are_using_the_original_source\""&gt;More Info.&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;Articles must be written in English&lt;/td&gt;\n&lt;td align=\""center\""&gt;An article must be &lt;strong&gt;primarily&lt;/strong&gt; written in English for us to be able to moderate it and enforce our rules in a fair and unbiased manner. &lt;a href=\""https://www.reddit.com/r/politics/wiki/rulesandregs#wiki_all_submissions_must_be_primarily_written_in_the_english_language.\""&gt;More Info.&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;Spam is bad!&lt;/td&gt;\n&lt;td align=\""center\""&gt;If 33% or more of your submissions are from a single website, you will be banned as a spammer. &lt;a href=\""/r/politics/wiki/rulesandregs#wiki_spam_is_bad.21\""&gt;More Info.&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;Submissions must be articles, videos or sound clips.&lt;/td&gt;\n&lt;td align=\""center\""&gt;We disallow solicitation of users (petitions, polls, requests for money, etc.), personal blogs, satire, images, social media content (Facebook, twitter, tumblr, LinkedIn, etc.), wikis, memes, and political advertisements. More info: &lt;a href=\""/r/politics/wiki/rulesandregs#wiki_submissions_must_be_articles.2C_videos.2C_or_sound_clips.\""&gt;Content type rules.&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;Do not use &amp;quot;BREAKING&amp;quot; or ALL CAPS in titles.&lt;/td&gt;\n&lt;td align=\""center\""&gt;The ALL CAPS and &amp;#39;Breaking&amp;#39; rule is applied even when the actual title of the article is in all caps or contains the word &amp;#39;Breaking&amp;#39;. This rule may be applied to other single word declarative and/or sensational expressions, such as &amp;#39;EXCLUSIVE:&amp;#39; or &amp;#39;HOT:&amp;#39;. &lt;a href=\""/r/politics/wiki/rulesandregs#wiki_follow_reddiquette.27s_title_instructions.\""&gt;More Info.&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;/tbody&gt;&lt;/table&gt;\n\n&lt;h1&gt;Events Calendar&lt;/h1&gt;\n\n&lt;blockquote&gt;\n&lt;p&gt;3 Mar - 12am EST&lt;/p&gt;\n&lt;/blockquote&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;a href=\""/r/Politics\""&gt;/r/Politics&lt;/a&gt; monthly Meta Thread&lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;blockquote&gt;\n&lt;p&gt;8 Mar - 9am EST&lt;/p&gt;\n&lt;/blockquote&gt;\n\n&lt;ul&gt;\n&lt;li&gt;AMA with Michael and Andrew from the Center for Investigative Reporting&lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;h1&gt;Other Resources:&lt;/h1&gt;\n\n&lt;h4&gt;&lt;a href=\""http://www.reddit.com/r/politics/wiki/relatedsubs\""&gt;Full list of Related Subreddits&lt;/a&gt;&lt;/h4&gt;\n\n&lt;h4&gt;&lt;a href=\""https://app.orangechat.io/r/politics\""&gt;Chat with us on OrangeChat/IRC&lt;/a&gt;&lt;/h4&gt;\n\n&lt;ul&gt;\n&lt;li&gt;Click &lt;a href=\""https://app.orangechat.io/#/r/politics\""&gt;here&lt;/a&gt; to join our channel via webchat with easy log-in. Alternatively, to connect via IRC, point your client to #politics on irc.snoonet.org, port 6667.&lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;h4&gt;&lt;a href=\""http://twitter.com/rSlashPolitics\""&gt;Follow us on Twitter&lt;/a&gt;&lt;/h4&gt;\n\n&lt;h4&gt;&lt;a href=\""https://goo.gl/AH1EPQ\""&gt;Events Calendar&lt;/a&gt;&lt;/h4&gt;\n\n&lt;h4&gt;&lt;a href=\""http://redditpolitics.pythonanywhere.com/\""&gt;Apply to be a mod&lt;/a&gt;&lt;/h4&gt;\n\n&lt;h4&gt;&lt;a href=\""http://goo.gl/XHLkS\""&gt;Register To Vote&lt;/a&gt;&lt;/h4&gt;\n\n&lt;p&gt;&lt;a href=\""#/RES_SR_Config/NightModeCompatible\""&gt;&lt;/a&gt;&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""title"": ""Politics"",
				""collapse_deleted_comments"": true,
				""public_description"": ""/r/Politics is for news and discussion about U.S. politics."",
				""over18"": false,
				""public_description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;p&gt;&lt;a href=\""/r/Politics\""&gt;/r/Politics&lt;/a&gt; is for news and discussion about U.S. politics.&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""spoilers_enabled"": false,
				""icon_size"": [
					256,
					256
				],
				""suggested_comment_sort"": null,
				""icon_img"": ""http://a.thumbs.redditmedia.com/ZaSYxoONdAREm1_u_sid_fjcgvBTNeFQV--8tz6fZC0.png"",
				""header_title"": ""The Place for U.S. Politics"",
				""display_name_prefixed"": ""r/politics"",
				""user_is_muted"": null,
				""submit_link_label"": null,
				""accounts_active"": 21607,
				""public_traffic"": true,
				""header_size"": [
					21,
					16
				],
				""subscribers"": 3319459,
				""submit_text_label"": null,
				""key_color"": """",
				""lang"": ""en"",
				""whitelist_status"": ""all_ads"",
				""name"": ""t5_2cneq"",
				""created"": 1186406199.0,
				""url"": ""/r/politics/"",
				""quarantine"": false,
				""hide_ads"": false,
				""created_utc"": 1186377399.0,
				""banner_size"": [
					880,
					264
				],
				""user_is_moderator"": null,
				""accounts_active_is_fuzzed"": false,
				""advertiser_category"": ""Lifestyles"",
				""user_sr_theme_enabled"": true,
				""allow_images"": false,
				""show_media_preview"": false,
				""comment_score_hide_mins"": 480,
				""subreddit_type"": ""public"",
				""submission_type"": ""link"",
				""user_is_subscriber"": null
			}
		}"
		);
	private JObject worldnewsJson = JObject.Parse (@"{
			""kind"": ""t5"",
			""data"": {
				""user_is_contributor"": null,
				""banner_img"": """",
				""submit_text_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;h6&gt;&lt;a href=\""#h6-red\""&gt;&lt;/a&gt;&lt;/h6&gt;\n\n&lt;blockquote&gt;\n&lt;h1&gt;DISALLOWED SUBMISSIONS&lt;/h1&gt;\n\n&lt;ul&gt;\n&lt;li&gt;US internal news/US politics&lt;/li&gt;\n&lt;li&gt;Editorialized titles&lt;/li&gt;\n&lt;li&gt;Feature stories&lt;/li&gt;\n&lt;li&gt;Editorials, opinion, analysis&lt;/li&gt;\n&lt;li&gt;Non-English articles&lt;/li&gt;\n&lt;li&gt;Raw images, videos or audio clips&lt;/li&gt;\n&lt;li&gt;Petitions, advocacy, surveys&lt;/li&gt;\n&lt;li&gt;No all caps words in titles&lt;/li&gt;\n&lt;li&gt;Blogspam (if stolen content/direct copy)&lt;/li&gt;\n&lt;li&gt;Twitter, Facebook, Tumblr&lt;/li&gt;\n&lt;li&gt;Old news (≥1 week old) articles&lt;/li&gt;\n&lt;/ul&gt;\n&lt;/blockquote&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""user_is_banned"": null,
				""wiki_enabled"": true,
				""show_media"": false,
				""id"": ""2qh13"",
				""description"": ""&gt;&gt;&gt; - **Other Subs:**\n\n&gt;&gt;&gt; - [Related](http://goo.gl/he9fxO)\n&gt;&gt;&gt;     - /r/News\n&gt;&gt;&gt;     - /r/PoliticalDiscussion\n&gt;&gt;&gt;     - /r/WorldPolitics\n&gt;&gt;&gt;     - /r/WorldEvents\n&gt;&gt;&gt;     - /r/GeoPolitics\n&gt;&gt;&gt;     - /r/InternationalPolitics\n&gt;&gt;&gt;     - /r/InternationalBusiness\n&gt;&gt;&gt;     - /r/Business\n&gt;&gt;&gt;     - /r/Economics\n&gt;&gt;&gt;     - /r/Environment\n&gt;&gt;&gt;     - /r/History\n&gt;&gt;&gt;     - /r/WikiLeaks\n&gt;&gt;&gt;     - /r/HumanRights\n&gt;&gt;&gt;     - /r/NSALeaks\n&gt;&gt;&gt;     - /r/doctorswithoutborders\n&gt;&gt;&gt;     - /r/Features\n&gt;&gt;&gt;     - /r/UpliftingNews\n&gt;&gt;&gt;     - /r/globalhealth\n&gt;&gt;&gt;     - /r/modded\n&gt;&gt;&gt;     - /r/NewsOfTheWeird\n&gt;&gt;&gt;     - /r/Insidernews\n&gt;&gt;&gt;     - /r/FakeNews\n\n&gt;&gt;&gt; - [N. America](http://goo.gl/RV3yw6)\n&gt;&gt;&gt;     - /r/Politics\n&gt;&gt;&gt;     - /r/USA\n&gt;&gt;&gt;     - /r/USANews\n&gt;&gt;&gt;     - /r/Canada\n&gt;&gt;&gt;     - /r/Cuba\n&gt;&gt;&gt;     - /r/Mexico\n&gt;&gt;&gt;     - /r/PuertoRico\n\n&gt;&gt;&gt; - [S. America](http://goo.gl/5hxkxn)\n&gt;&gt;&gt;     - /r/Argentina/\n&gt;&gt;&gt;     - /r/Brasil\n&gt;&gt;&gt;     - /r/Chile\n&gt;&gt;&gt;     - /r/Colombia\n&gt;&gt;&gt;     - /r/Guyana\n&gt;&gt;&gt;     - [/r/Venezuela](/r/vzla)\n\n&gt;&gt;&gt; - [Europe](http://goo.gl/yoALC8)\n&gt;&gt;&gt;     - /r/Armenia \n&gt;&gt;&gt;     - /r/Azerbaijan\n&gt;&gt;&gt;     - /r/Belgium\n&gt;&gt;&gt;     - [/r/Bosnia](/r/BiH)\n&gt;&gt;&gt;     - /r/Bulgaria\n&gt;&gt;&gt;     - /r/Croatia\n&gt;&gt;&gt;     - /r/Denmark\n&gt;&gt;&gt;     - /r/Europe\n&gt;&gt;&gt;     - /r/France\n&gt;&gt;&gt;     - [/r/Georgia](/r/sakartvelo)\n&gt;&gt;&gt;     - /r/Germany\n&gt;&gt;&gt;     - /r/Greece\n&gt;&gt;&gt;     - /r/Hungary\n&gt;&gt;&gt;     - /r/Ireland\n&gt;&gt;&gt;     - /r/Italy\n&gt;&gt;&gt;     - /r/TheNetherlands\n&gt;&gt;&gt;     - /r/Poland\n&gt;&gt;&gt;     - /r/Portugal\n&gt;&gt;&gt;     - /r/Romania\n&gt;&gt;&gt;     - /r/Russia\n&gt;&gt;&gt;     - /r/Scotland\n&gt;&gt;&gt;     - /r/Serbia\n&gt;&gt;&gt;     - /r/Spain\n&gt;&gt;&gt;     - /r/Sweden\n&gt;&gt;&gt;     - /r/Switzerland \n&gt;&gt;&gt;     - /r/Turkey\n&gt;&gt;&gt;     - /r/UnitedKingdom\n&gt;&gt;&gt;     - /r/UKPolitics\n&gt;&gt;&gt;     - /r/Ukraina\n&gt;&gt;&gt;     - /r/Ukraine \n&gt;&gt;&gt;     - /r/UkrainianConflict\n\n&gt;&gt;&gt; - [Asia](http://goo.gl/gsotVH)\n&gt;&gt;&gt;     - /r/Afghanistan\n&gt;&gt;&gt;     - /r/Bangladesh\n&gt;&gt;&gt;     - /r/China\n&gt;&gt;&gt;     - /r/India\n&gt;&gt;&gt;     - /r/IndiaNews\n&gt;&gt;&gt;     - /r/Malaysia\n&gt;&gt;&gt;     - /r/NorthKoreaNews\n&gt;&gt;&gt;     - /r/Pakistan\n&gt;&gt;&gt;     - /r/Philippines\n&gt;&gt;&gt;     - /r/Singapore\n&gt;&gt;&gt;     - /r/Sino\n&gt;&gt;&gt;     - /r/Thailand\n&gt;&gt;&gt;     - /r/Turkey\n\n&gt;&gt;&gt; - [Middle East](http://goo.gl/i2FSG9)\n&gt;&gt;&gt;     - /r/Assyria\n&gt;&gt;&gt;     - /r/Iran\n&gt;&gt;&gt;     - /r/Iraq\n&gt;&gt;&gt;     - /r/Israel\n&gt;&gt;&gt;     - /r/Kurdistan\n&gt;&gt;&gt;     - /r/LevantineWar\n&gt;&gt;&gt;     - /r/MiddleEastNews\n&gt;&gt;&gt;     - /r/MideastPeace\n&gt;&gt;&gt;     - /r/Palestine\n&gt;&gt;&gt;     - /r/Syria\n&gt;&gt;&gt;     - /r/Yemen\n&gt;&gt;&gt;     - /r/YemeniCrisis\n\n&gt;&gt;&gt; - [Africa](http://goo.gl/DifApT)\n&gt;&gt;&gt;     - /r/Africa\n&gt;&gt;&gt;     - /r/SouthAfrica\n\n&gt;&gt;&gt; - [Oceania](http://goo.gl/k8cUqs)\n&gt;&gt;&gt;     - /r/Australia\n&gt;&gt;&gt;     - /r/NewZealand \n&gt;&gt;&gt;     - /r/Westpapua  \n&gt;&gt;&gt;     - /r/Fijian    \n\n## **Filter out dominant topics:**\n\n[Display Trump submissions](http://www.reddit.com/r/worldnews#nt)\n\n[&amp;nbsp; *NEW!* &amp;nbsp;&amp;nbsp;&amp;nbsp; Filter Trump &amp;nbsp;&amp;nbsp;&amp;nbsp; *NEW!* &amp;nbsp;](http://nt.reddit.com/r/worldnews#www)\n\n[Display Philippines submissions](http://www.reddit.com/r/worldnews#du)\n\n[Filter Philippines](http://du.reddit.com/r/worldnews#www)\n\n[Display Syria/Iraq submissions](http://www.reddit.com/r/worldnews#ii)\n\n[Filter Syria / Iraq](http://ii.reddit.com/r/worldnews#www)\n\n[Display Israel/Palestine submissions](http://www.reddit.com/r/worldnews#ni)\n\n[Filter Israel / Palestine](http://ni.reddit.com/r/worldnews#www)\n\n[Display all submissions](http://www.reddit.com/r/worldnews#iu)\n\n[Filter all dominant topics](http://iu.reddit.com/r/worldnews#www)\n\n#### [](#h4-green)\n&gt;# Welcome!\n&gt;\n&gt; /r/worldnews is for major news from around the world except US-internal news / US politics\n\n###### [](#h6-red)\n&gt;#Worldnews Rules\n&gt;\n&gt;\n&gt;### **Disallowed submissions**\n&gt;\n&gt; * US internal news/US politics\n&gt; * Editorialized titles\n&gt; * Misleading titles\n&gt; * Editorials, opinion, analysis\n&gt; * Feature stories\n&gt; * Non-English articles\n&gt; * Images, videos or audio clips\n&gt; * Petitions, advocacy, surveys\n&gt; * All caps words in titles\n&gt; * Blogspam (if stolen content/direct copy)\n&gt; * Twitter, Facebook, Tumblr\n&gt; * Old news (≥1 week old) articles\n\n&gt;[See the wiki](/r/worldnews/wiki/rules#wiki_disallowed_submissions) for details on each rule\n\n&gt;### **Disallowed comments**\n&gt;\n&gt; * Bigotry / Other offensive content\n&gt; * Personal attacks on other users\n&gt; * Memes/GIFs\n&gt; * Unlabeled NSFW images/videos\n&gt; * URL shorteners\n\n&gt;[See the wiki](/r/worldnews/wiki/rules#wiki_disallowed_comments) for details on each rule\n\n&gt; **Continued or outstandingly blatant violation** of the submission or commenting rules will result in you being temporarily **banned** from the subreddit without a warning.\n\n&gt;\n&gt;----\n&gt;\n&gt;**Please don't ever feed the trolls.**    \n&gt;Downvote, report and move on.\n&gt;\n&gt;----\n&gt;\n&gt;* [**What moderators do and can't do**](/r/worldnews/wiki/whatmodsdo)\n&gt;* [**Message the moderators**](http://www.reddit.com/message/compose?to=%2Fr%2Fworldnews)\n\n#### [](#h4-green)\n&gt;# Sticky Posts\n\n&gt; • [A list of all recent stickied posts.](/r/worldnews/search?q=author%3AWorldNewsMods&amp;sort=new&amp;restrict_sr=on&amp;t=all)"",
				""submit_text"": ""###### [](#h6-red)\n&gt;# DISALLOWED SUBMISSIONS\n&gt;\n&gt; * US internal news/US politics\n&gt; * Editorialized titles\n&gt; * Feature stories\n&gt; * Editorials, opinion, analysis\n&gt; * Non-English articles\n&gt; * Raw images, videos or audio clips\n&gt; * Petitions, advocacy, surveys\n&gt; * No all caps words in titles\n&gt; * Blogspam (if stolen content/direct copy)\n&gt; * Twitter, Facebook, Tumblr\n&gt; * Old news (≥1 week old) articles\n"",
				""display_name"": ""worldnews"",
				""header_img"": null,
				""description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;blockquote&gt;\n&lt;blockquote&gt;\n&lt;blockquote&gt;\n&lt;ul&gt;\n&lt;li&gt;&lt;p&gt;&lt;strong&gt;Other Subs:&lt;/strong&gt;&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;&lt;a href=\""http://goo.gl/he9fxO\""&gt;Related&lt;/a&gt;&lt;/p&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;a href=\""/r/News\""&gt;/r/News&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/PoliticalDiscussion\""&gt;/r/PoliticalDiscussion&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/WorldPolitics\""&gt;/r/WorldPolitics&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/WorldEvents\""&gt;/r/WorldEvents&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/GeoPolitics\""&gt;/r/GeoPolitics&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/InternationalPolitics\""&gt;/r/InternationalPolitics&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/InternationalBusiness\""&gt;/r/InternationalBusiness&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Business\""&gt;/r/Business&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Economics\""&gt;/r/Economics&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Environment\""&gt;/r/Environment&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/History\""&gt;/r/History&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/WikiLeaks\""&gt;/r/WikiLeaks&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/HumanRights\""&gt;/r/HumanRights&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/NSALeaks\""&gt;/r/NSALeaks&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/doctorswithoutborders\""&gt;/r/doctorswithoutborders&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Features\""&gt;/r/Features&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/UpliftingNews\""&gt;/r/UpliftingNews&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/globalhealth\""&gt;/r/globalhealth&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/modded\""&gt;/r/modded&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/NewsOfTheWeird\""&gt;/r/NewsOfTheWeird&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Insidernews\""&gt;/r/Insidernews&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/FakeNews\""&gt;/r/FakeNews&lt;/a&gt;&lt;/li&gt;\n&lt;/ul&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;&lt;a href=\""http://goo.gl/RV3yw6\""&gt;N. America&lt;/a&gt;&lt;/p&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;a href=\""/r/Politics\""&gt;/r/Politics&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/USA\""&gt;/r/USA&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/USANews\""&gt;/r/USANews&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Canada\""&gt;/r/Canada&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Cuba\""&gt;/r/Cuba&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Mexico\""&gt;/r/Mexico&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/PuertoRico\""&gt;/r/PuertoRico&lt;/a&gt;&lt;/li&gt;\n&lt;/ul&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;&lt;a href=\""http://goo.gl/5hxkxn\""&gt;S. America&lt;/a&gt;&lt;/p&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;a href=\""/r/Argentina/\""&gt;/r/Argentina/&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Brasil\""&gt;/r/Brasil&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Chile\""&gt;/r/Chile&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Colombia\""&gt;/r/Colombia&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Guyana\""&gt;/r/Guyana&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/vzla\""&gt;/r/Venezuela&lt;/a&gt;&lt;/li&gt;\n&lt;/ul&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;&lt;a href=\""http://goo.gl/yoALC8\""&gt;Europe&lt;/a&gt;&lt;/p&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;a href=\""/r/Armenia\""&gt;/r/Armenia&lt;/a&gt; &lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Azerbaijan\""&gt;/r/Azerbaijan&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Belgium\""&gt;/r/Belgium&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/BiH\""&gt;/r/Bosnia&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Bulgaria\""&gt;/r/Bulgaria&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Croatia\""&gt;/r/Croatia&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Denmark\""&gt;/r/Denmark&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Europe\""&gt;/r/Europe&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/France\""&gt;/r/France&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/sakartvelo\""&gt;/r/Georgia&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Germany\""&gt;/r/Germany&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Greece\""&gt;/r/Greece&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Hungary\""&gt;/r/Hungary&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Ireland\""&gt;/r/Ireland&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Italy\""&gt;/r/Italy&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/TheNetherlands\""&gt;/r/TheNetherlands&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Poland\""&gt;/r/Poland&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Portugal\""&gt;/r/Portugal&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Romania\""&gt;/r/Romania&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Russia\""&gt;/r/Russia&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Scotland\""&gt;/r/Scotland&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Serbia\""&gt;/r/Serbia&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Spain\""&gt;/r/Spain&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Sweden\""&gt;/r/Sweden&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Switzerland\""&gt;/r/Switzerland&lt;/a&gt; &lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Turkey\""&gt;/r/Turkey&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/UnitedKingdom\""&gt;/r/UnitedKingdom&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/UKPolitics\""&gt;/r/UKPolitics&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Ukraina\""&gt;/r/Ukraina&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Ukraine\""&gt;/r/Ukraine&lt;/a&gt; &lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/UkrainianConflict\""&gt;/r/UkrainianConflict&lt;/a&gt;&lt;/li&gt;\n&lt;/ul&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;&lt;a href=\""http://goo.gl/gsotVH\""&gt;Asia&lt;/a&gt;&lt;/p&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;a href=\""/r/Afghanistan\""&gt;/r/Afghanistan&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Bangladesh\""&gt;/r/Bangladesh&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/China\""&gt;/r/China&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/India\""&gt;/r/India&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/IndiaNews\""&gt;/r/IndiaNews&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Malaysia\""&gt;/r/Malaysia&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/NorthKoreaNews\""&gt;/r/NorthKoreaNews&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Pakistan\""&gt;/r/Pakistan&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Philippines\""&gt;/r/Philippines&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Singapore\""&gt;/r/Singapore&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Sino\""&gt;/r/Sino&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Thailand\""&gt;/r/Thailand&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Turkey\""&gt;/r/Turkey&lt;/a&gt;&lt;/li&gt;\n&lt;/ul&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;&lt;a href=\""http://goo.gl/i2FSG9\""&gt;Middle East&lt;/a&gt;&lt;/p&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;a href=\""/r/Assyria\""&gt;/r/Assyria&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Iran\""&gt;/r/Iran&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Iraq\""&gt;/r/Iraq&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Israel\""&gt;/r/Israel&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Kurdistan\""&gt;/r/Kurdistan&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/LevantineWar\""&gt;/r/LevantineWar&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/MiddleEastNews\""&gt;/r/MiddleEastNews&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/MideastPeace\""&gt;/r/MideastPeace&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Palestine\""&gt;/r/Palestine&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Syria\""&gt;/r/Syria&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Yemen\""&gt;/r/Yemen&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/YemeniCrisis\""&gt;/r/YemeniCrisis&lt;/a&gt;&lt;/li&gt;\n&lt;/ul&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;&lt;a href=\""http://goo.gl/DifApT\""&gt;Africa&lt;/a&gt;&lt;/p&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;a href=\""/r/Africa\""&gt;/r/Africa&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/SouthAfrica\""&gt;/r/SouthAfrica&lt;/a&gt;&lt;/li&gt;\n&lt;/ul&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;&lt;a href=\""http://goo.gl/k8cUqs\""&gt;Oceania&lt;/a&gt;&lt;/p&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;a href=\""/r/Australia\""&gt;/r/Australia&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/NewZealand\""&gt;/r/NewZealand&lt;/a&gt; &lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Westpapua\""&gt;/r/Westpapua&lt;/a&gt;&lt;br/&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""/r/Fijian\""&gt;/r/Fijian&lt;/a&gt;&lt;br/&gt;&lt;/li&gt;\n&lt;/ul&gt;&lt;/li&gt;\n&lt;/ul&gt;\n&lt;/blockquote&gt;\n&lt;/blockquote&gt;\n&lt;/blockquote&gt;\n\n&lt;h2&gt;&lt;strong&gt;Filter out dominant topics:&lt;/strong&gt;&lt;/h2&gt;\n\n&lt;p&gt;&lt;a href=\""http://www.reddit.com/r/worldnews#nt\""&gt;Display Trump submissions&lt;/a&gt;&lt;/p&gt;\n\n&lt;p&gt;&lt;a href=\""http://nt.reddit.com/r/worldnews#www\""&gt;&amp;nbsp; &lt;em&gt;NEW!&lt;/em&gt; &amp;nbsp;&amp;nbsp;&amp;nbsp; Filter Trump &amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;em&gt;NEW!&lt;/em&gt; &amp;nbsp;&lt;/a&gt;&lt;/p&gt;\n\n&lt;p&gt;&lt;a href=\""http://www.reddit.com/r/worldnews#du\""&gt;Display Philippines submissions&lt;/a&gt;&lt;/p&gt;\n\n&lt;p&gt;&lt;a href=\""http://du.reddit.com/r/worldnews#www\""&gt;Filter Philippines&lt;/a&gt;&lt;/p&gt;\n\n&lt;p&gt;&lt;a href=\""http://www.reddit.com/r/worldnews#ii\""&gt;Display Syria/Iraq submissions&lt;/a&gt;&lt;/p&gt;\n\n&lt;p&gt;&lt;a href=\""http://ii.reddit.com/r/worldnews#www\""&gt;Filter Syria / Iraq&lt;/a&gt;&lt;/p&gt;\n\n&lt;p&gt;&lt;a href=\""http://www.reddit.com/r/worldnews#ni\""&gt;Display Israel/Palestine submissions&lt;/a&gt;&lt;/p&gt;\n\n&lt;p&gt;&lt;a href=\""http://ni.reddit.com/r/worldnews#www\""&gt;Filter Israel / Palestine&lt;/a&gt;&lt;/p&gt;\n\n&lt;p&gt;&lt;a href=\""http://www.reddit.com/r/worldnews#iu\""&gt;Display all submissions&lt;/a&gt;&lt;/p&gt;\n\n&lt;p&gt;&lt;a href=\""http://iu.reddit.com/r/worldnews#www\""&gt;Filter all dominant topics&lt;/a&gt;&lt;/p&gt;\n\n&lt;h4&gt;&lt;a href=\""#h4-green\""&gt;&lt;/a&gt;&lt;/h4&gt;\n\n&lt;blockquote&gt;\n&lt;h1&gt;Welcome!&lt;/h1&gt;\n\n&lt;p&gt;&lt;a href=\""/r/worldnews\""&gt;/r/worldnews&lt;/a&gt; is for major news from around the world except US-internal news / US politics&lt;/p&gt;\n&lt;/blockquote&gt;\n\n&lt;h6&gt;&lt;a href=\""#h6-red\""&gt;&lt;/a&gt;&lt;/h6&gt;\n\n&lt;blockquote&gt;\n&lt;h1&gt;Worldnews Rules&lt;/h1&gt;\n\n&lt;h3&gt;&lt;strong&gt;Disallowed submissions&lt;/strong&gt;&lt;/h3&gt;\n\n&lt;ul&gt;\n&lt;li&gt;US internal news/US politics&lt;/li&gt;\n&lt;li&gt;Editorialized titles&lt;/li&gt;\n&lt;li&gt;Misleading titles&lt;/li&gt;\n&lt;li&gt;Editorials, opinion, analysis&lt;/li&gt;\n&lt;li&gt;Feature stories&lt;/li&gt;\n&lt;li&gt;Non-English articles&lt;/li&gt;\n&lt;li&gt;Images, videos or audio clips&lt;/li&gt;\n&lt;li&gt;Petitions, advocacy, surveys&lt;/li&gt;\n&lt;li&gt;All caps words in titles&lt;/li&gt;\n&lt;li&gt;Blogspam (if stolen content/direct copy)&lt;/li&gt;\n&lt;li&gt;Twitter, Facebook, Tumblr&lt;/li&gt;\n&lt;li&gt;Old news (≥1 week old) articles&lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;p&gt;&lt;a href=\""/r/worldnews/wiki/rules#wiki_disallowed_submissions\""&gt;See the wiki&lt;/a&gt; for details on each rule&lt;/p&gt;\n\n&lt;h3&gt;&lt;strong&gt;Disallowed comments&lt;/strong&gt;&lt;/h3&gt;\n\n&lt;ul&gt;\n&lt;li&gt;Bigotry / Other offensive content&lt;/li&gt;\n&lt;li&gt;Personal attacks on other users&lt;/li&gt;\n&lt;li&gt;Memes/GIFs&lt;/li&gt;\n&lt;li&gt;Unlabeled NSFW images/videos&lt;/li&gt;\n&lt;li&gt;URL shorteners&lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;p&gt;&lt;a href=\""/r/worldnews/wiki/rules#wiki_disallowed_comments\""&gt;See the wiki&lt;/a&gt; for details on each rule&lt;/p&gt;\n\n&lt;p&gt;&lt;strong&gt;Continued or outstandingly blatant violation&lt;/strong&gt; of the submission or commenting rules will result in you being temporarily &lt;strong&gt;banned&lt;/strong&gt; from the subreddit without a warning.&lt;/p&gt;\n\n&lt;hr/&gt;\n\n&lt;p&gt;&lt;strong&gt;Please don&amp;#39;t ever feed the trolls.&lt;/strong&gt;&lt;br/&gt;\nDownvote, report and move on.&lt;/p&gt;\n\n&lt;hr/&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;a href=\""/r/worldnews/wiki/whatmodsdo\""&gt;&lt;strong&gt;What moderators do and can&amp;#39;t do&lt;/strong&gt;&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""http://www.reddit.com/message/compose?to=%2Fr%2Fworldnews\""&gt;&lt;strong&gt;Message the moderators&lt;/strong&gt;&lt;/a&gt;&lt;/li&gt;\n&lt;/ul&gt;\n&lt;/blockquote&gt;\n\n&lt;h4&gt;&lt;a href=\""#h4-green\""&gt;&lt;/a&gt;&lt;/h4&gt;\n\n&lt;blockquote&gt;\n&lt;h1&gt;Sticky Posts&lt;/h1&gt;\n\n&lt;p&gt;• &lt;a href=\""/r/worldnews/search?q=author%3AWorldNewsMods&amp;amp;sort=new&amp;amp;restrict_sr=on&amp;amp;t=all\""&gt;A list of all recent stickied posts.&lt;/a&gt;&lt;/p&gt;\n&lt;/blockquote&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""title"": ""World News"",
				""collapse_deleted_comments"": true,
				""public_description"": ""A place for major news from around the world, excluding US-internal news.\n"",
				""over18"": false,
				""public_description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;p&gt;A place for major news from around the world, excluding US-internal news.&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""spoilers_enabled"": true,
				""icon_size"": null,
				""suggested_comment_sort"": null,
				""icon_img"": """",
				""header_title"": ""News from Planet Earth"",
				""display_name_prefixed"": ""r/worldnews"",
				""user_is_muted"": null,
				""submit_link_label"": null,
				""accounts_active"": 11161,
				""public_traffic"": true,
				""header_size"": null,
				""subscribers"": 15619995,
				""submit_text_label"": null,
				""key_color"": """",
				""lang"": ""en"",
				""whitelist_status"": ""all_ads"",
				""name"": ""t5_2qh13"",
				""created"": 1201259919.0,
				""url"": ""/r/worldnews/"",
				""quarantine"": false,
				""hide_ads"": false,
				""created_utc"": 1201231119.0,
				""banner_size"": null,
				""user_is_moderator"": null,
				""accounts_active_is_fuzzed"": false,
				""advertiser_category"": ""Lifestyles"",
				""user_sr_theme_enabled"": true,
				""allow_images"": false,
				""show_media_preview"": false,
				""comment_score_hide_mins"": 90,
				""subreddit_type"": ""public"",
				""submission_type"": ""link"",
				""user_is_subscriber"": null
			}
		}"
		);
	private JObject bestofJson = JObject.Parse (@"{
			""kind"": ""t5"",
			""data"": {
				""user_is_contributor"": null,
				""banner_img"": """",
				""submit_text_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;ul&gt;\n&lt;li&gt;Please search &lt;a href=\""/r/Bestof\""&gt;/r/Bestof&lt;/a&gt; before submitting.    Repeat submissions will be mercilessly removed.&lt;br/&gt;&lt;/li&gt;\n&lt;li&gt;See &lt;a href=\""http://www.reddit.com/r/bestof/about/sidebar\""&gt;our sidebar&lt;/a&gt; for our rules and guidelines.&lt;br/&gt;\n&lt;strong&gt;&lt;a href=\""http://www.reddit.com/r/bestof/comments/285lwh/rbestof_subscriber_feedback_thread_part_2/\""&gt;Please remember to delete www and add np to your link before you submit!&lt;/a&gt;&lt;/strong&gt;&lt;/li&gt;\n&lt;li&gt;&lt;a href=\""http://www.reddit.com/wiki/reddiquette\""&gt;Reddiquette&lt;/a&gt; is to be observed and followed at all times.&lt;br/&gt;&lt;/li&gt;\n&lt;/ul&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""user_is_banned"": null,
				""wiki_enabled"": false,
				""show_media"": false,
				""id"": ""2qh3v"",
				""description"": ""This subreddit features the very best hidden commentary that reddit has to offer!\n\n[New to reddit? click here!](/wiki/reddit_101)\n\n#Filters \n\n[Display Politics](http://www.reddit.com/r/vancouver#ni)\n[Filter Politics](https://ni.reddit.com/r/bestof/search?q=-flair%3Apolitics&amp;restrict_sr=on&amp;sort=new&amp;t=week#www)\n\n#Bestof's Rules\n\n**Please read these before submitting your post!**\n\nHover for details\n\n|||\n|:------|:-----|\n|1. This subreddit accepts links to singular comments, comment chains, and self posts from the reddit.com domain only. | A self post is any post where if the title of the post is clicked on, it will *not* take you elsewhere, e.g. another place on reddit, imgur, or redditmetrics.\n|2. **Your submission must have np instead of www in the URL field.** | Due to popular demand /r/bestof now requires No Participation links. Please respect other communities on reddit by not voting when you visit from /r/bestof |\n|3. Comments or self posts from certain subreddits may be automatically removed. | To have your subreddit added to the \""do not disturb list\"", [message the mods](http://www.reddit.com/message/compose?to=%23bestof) |\n|4. Links to entire subreddits or user pages will be removed.|\n|5. Do not link to your own comments or self posts please. |\n|6. Provide Context! | If the comment you're linking to requires some context, just add \""?context=3\"" to the URL. For a detailed explanation on *why* this is important, [please see this thread.](http://www.reddit.com/r/bestof/comments/ohqy0/rbestof_lets_talk_context/) |\n|7. Please don't include the subreddit name in your submission title. | Our moderation bot includes that information automatically, and doing so only makes the tag show up twice. |\n|8. Bad novelty accounts will be banned. | You're bad, and you should feel bad. |\n|9. This is a curated space.  | The moderators reserve the right to remove posts, users, and comments at their own discretion. |\n|10. Bot comments are not acceptable submission material for /r/bestof. | Bot comments as submissions will be removed on sight. |\n|11. Celebrities | Everyday interaction with celebrities may not be suitable for /r/bestof and may be removed based on lack of quality |\n\n#Some relevant subreddits\n\n|||\n|:------|:-----|\n|/r/bestofTLDR | The best TL;DRs of reddit. |\n|/r/DailyDot | For when you want to catch up on all the reddit that you missed while you were sleeping, eating, vacationing, or otherwise AFK. |\n|/r/DefaultGems | For the very best gems that the default reddits have to offer! |\n|/r/DepthHub | For the best in-depth conversations to be found on reddit. |\n/r/GoodLongPosts | A automated subreddit for finding the best longform posts of reddit. |\n|/r/Help | For help with reddit. |\n|/r/MetaHub | For the best meta discussion to be found on reddit. |\n|/r/ModHelp | For help with modding on reddit. |\n|/r/MuseumOfReddit | Historic Posts and Comments |\n|/r/NewReddits | For the best (and worst) new reddits! |\n|/r/RedditorOfTheDay | For all you need to know (and maybe more) about a featured redditor.  |\n|/r/ObscureSubreddits | All about interesting reddits which have fallen out of the limelight due to no fault of their own |\n|/r/SubredditOfTheDay | For a daily feature on a subreddit you've probably never heard of before.  |\n|/r/ThankYou | For when a certain redditor needs a good, hard thanking. |\n|/r/TheoryOfReddit | For inquiring into what makes the Reddit community work and what we in the community can do to help make it better. |\n|/r/VeryPunny | For the best (and worst) pun threads on reddit. |\n|/r/WorstOf | For the best (and worst) trolls to be found on reddit. |\n|/r/weeklyreddit | For new and interesting subreddit discovery. |\n|/r/YouGotTold | Collection of fine retorts |\n\n \n*Please tag all NSFW posts as such.*\n\n*If you're having trouble finding your submission, please feel free to [message the moderators.](http://www.reddit.com/message/compose?to=%23bestof) Make sure to provide us with a link to the post in question and as long it meets all of the criteria, it will be set free!*"",
				""submit_text"": ""* Please search /r/Bestof before submitting.    Repeat submissions will be mercilessly removed.   \n* See [our sidebar](http://www.reddit.com/r/bestof/about/sidebar) for our rules and guidelines.  \n**[Please remember to delete www and add np to your link before you submit!](http://www.reddit.com/r/bestof/comments/285lwh/rbestof_subscriber_feedback_thread_part_2/)**\n* [Reddiquette](http://www.reddit.com/wiki/reddiquette) is to be observed and followed at all times.  \n"",
				""display_name"": ""bestof"",
				""header_img"": ""http://c.thumbs.redditmedia.com/ObJvmSLNyEuazEYz.png"",
				""description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;p&gt;This subreddit features the very best hidden commentary that reddit has to offer!&lt;/p&gt;\n\n&lt;p&gt;&lt;a href=\""/wiki/reddit_101\""&gt;New to reddit? click here!&lt;/a&gt;&lt;/p&gt;\n\n&lt;h1&gt;Filters&lt;/h1&gt;\n\n&lt;p&gt;&lt;a href=\""http://www.reddit.com/r/vancouver#ni\""&gt;Display Politics&lt;/a&gt;\n&lt;a href=\""https://ni.reddit.com/r/bestof/search?q=-flair%3Apolitics&amp;amp;restrict_sr=on&amp;amp;sort=new&amp;amp;t=week#www\""&gt;Filter Politics&lt;/a&gt;&lt;/p&gt;\n\n&lt;h1&gt;Bestof&amp;#39;s Rules&lt;/h1&gt;\n\n&lt;p&gt;&lt;strong&gt;Please read these before submitting your post!&lt;/strong&gt;&lt;/p&gt;\n\n&lt;p&gt;Hover for details&lt;/p&gt;\n\n&lt;table&gt;&lt;thead&gt;\n&lt;tr&gt;\n&lt;th align=\""left\""&gt;&lt;/th&gt;\n&lt;th align=\""left\""&gt;&lt;/th&gt;\n&lt;/tr&gt;\n&lt;/thead&gt;&lt;tbody&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;1. This subreddit accepts links to singular comments, comment chains, and self posts from the reddit.com domain only.&lt;/td&gt;\n&lt;td align=\""left\""&gt;A self post is any post where if the title of the post is clicked on, it will &lt;em&gt;not&lt;/em&gt; take you elsewhere, e.g. another place on reddit, imgur, or redditmetrics.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;2. &lt;strong&gt;Your submission must have np instead of www in the URL field.&lt;/strong&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;Due to popular demand &lt;a href=\""/r/bestof\""&gt;/r/bestof&lt;/a&gt; now requires No Participation links. Please respect other communities on reddit by not voting when you visit from &lt;a href=\""/r/bestof\""&gt;/r/bestof&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;3. Comments or self posts from certain subreddits may be automatically removed.&lt;/td&gt;\n&lt;td align=\""left\""&gt;To have your subreddit added to the &amp;quot;do not disturb list&amp;quot;, &lt;a href=\""http://www.reddit.com/message/compose?to=%23bestof\""&gt;message the mods&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;4. Links to entire subreddits or user pages will be removed.&lt;/td&gt;\n&lt;td align=\""left\""&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;5. Do not link to your own comments or self posts please.&lt;/td&gt;\n&lt;td align=\""left\""&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;6. Provide Context!&lt;/td&gt;\n&lt;td align=\""left\""&gt;If the comment you&amp;#39;re linking to requires some context, just add &amp;quot;?context=3&amp;quot; to the URL. For a detailed explanation on &lt;em&gt;why&lt;/em&gt; this is important, &lt;a href=\""http://www.reddit.com/r/bestof/comments/ohqy0/rbestof_lets_talk_context/\""&gt;please see this thread.&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;7. Please don&amp;#39;t include the subreddit name in your submission title.&lt;/td&gt;\n&lt;td align=\""left\""&gt;Our moderation bot includes that information automatically, and doing so only makes the tag show up twice.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;8. Bad novelty accounts will be banned.&lt;/td&gt;\n&lt;td align=\""left\""&gt;You&amp;#39;re bad, and you should feel bad.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;9. This is a curated space.&lt;/td&gt;\n&lt;td align=\""left\""&gt;The moderators reserve the right to remove posts, users, and comments at their own discretion.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;10. Bot comments are not acceptable submission material for &lt;a href=\""/r/bestof\""&gt;/r/bestof&lt;/a&gt;.&lt;/td&gt;\n&lt;td align=\""left\""&gt;Bot comments as submissions will be removed on sight.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;11. Celebrities&lt;/td&gt;\n&lt;td align=\""left\""&gt;Everyday interaction with celebrities may not be suitable for &lt;a href=\""/r/bestof\""&gt;/r/bestof&lt;/a&gt; and may be removed based on lack of quality&lt;/td&gt;\n&lt;/tr&gt;\n&lt;/tbody&gt;&lt;/table&gt;\n\n&lt;h1&gt;Some relevant subreddits&lt;/h1&gt;\n\n&lt;table&gt;&lt;thead&gt;\n&lt;tr&gt;\n&lt;th align=\""left\""&gt;&lt;/th&gt;\n&lt;th align=\""left\""&gt;&lt;/th&gt;\n&lt;/tr&gt;\n&lt;/thead&gt;&lt;tbody&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/bestofTLDR\""&gt;/r/bestofTLDR&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;The best TL;DRs of reddit.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/DailyDot\""&gt;/r/DailyDot&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;For when you want to catch up on all the reddit that you missed while you were sleeping, eating, vacationing, or otherwise AFK.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/DefaultGems\""&gt;/r/DefaultGems&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;For the very best gems that the default reddits have to offer!&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/DepthHub\""&gt;/r/DepthHub&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;For the best in-depth conversations to be found on reddit.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/GoodLongPosts\""&gt;/r/GoodLongPosts&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;A automated subreddit for finding the best longform posts of reddit.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/Help\""&gt;/r/Help&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;For help with reddit.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/MetaHub\""&gt;/r/MetaHub&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;For the best meta discussion to be found on reddit.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/ModHelp\""&gt;/r/ModHelp&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;For help with modding on reddit.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/MuseumOfReddit\""&gt;/r/MuseumOfReddit&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;Historic Posts and Comments&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/NewReddits\""&gt;/r/NewReddits&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;For the best (and worst) new reddits!&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/RedditorOfTheDay\""&gt;/r/RedditorOfTheDay&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;For all you need to know (and maybe more) about a featured redditor.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/ObscureSubreddits\""&gt;/r/ObscureSubreddits&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;All about interesting reddits which have fallen out of the limelight due to no fault of their own&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/SubredditOfTheDay\""&gt;/r/SubredditOfTheDay&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;For a daily feature on a subreddit you&amp;#39;ve probably never heard of before.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/ThankYou\""&gt;/r/ThankYou&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;For when a certain redditor needs a good, hard thanking.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/TheoryOfReddit\""&gt;/r/TheoryOfReddit&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;For inquiring into what makes the Reddit community work and what we in the community can do to help make it better.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/VeryPunny\""&gt;/r/VeryPunny&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;For the best (and worst) pun threads on reddit.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/WorstOf\""&gt;/r/WorstOf&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;For the best (and worst) trolls to be found on reddit.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/weeklyreddit\""&gt;/r/weeklyreddit&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;For new and interesting subreddit discovery.&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/YouGotTold\""&gt;/r/YouGotTold&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;Collection of fine retorts&lt;/td&gt;\n&lt;/tr&gt;\n&lt;/tbody&gt;&lt;/table&gt;\n\n&lt;p&gt;&lt;em&gt;Please tag all NSFW posts as such.&lt;/em&gt;&lt;/p&gt;\n\n&lt;p&gt;&lt;em&gt;If you&amp;#39;re having trouble finding your submission, please feel free to &lt;a href=\""http://www.reddit.com/message/compose?to=%23bestof\""&gt;message the moderators.&lt;/a&gt; Make sure to provide us with a link to the post in question and as long it meets all of the criteria, it will be set free!&lt;/em&gt;&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""title"": ""best of reddit"",
				""collapse_deleted_comments"": false,
				""public_description"": ""The very best comments on reddit."",
				""over18"": false,
				""public_description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;p&gt;The very best comments on reddit.&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""spoilers_enabled"": true,
				""icon_size"": null,
				""suggested_comment_sort"": null,
				""icon_img"": """",
				""header_title"": null,
				""display_name_prefixed"": ""r/bestof"",
				""user_is_muted"": null,
				""submit_link_label"": null,
				""accounts_active"": 2063,
				""public_traffic"": false,
				""header_size"": [
					160,
					40
				],
				""subscribers"": 4692131,
				""submit_text_label"": null,
				""key_color"": """",
				""lang"": ""en"",
				""whitelist_status"": ""all_ads"",
				""name"": ""t5_2qh3v"",
				""created"": 1201272796.0,
				""url"": ""/r/bestof/"",
				""quarantine"": false,
				""hide_ads"": false,
				""created_utc"": 1201243996.0,
				""banner_size"": null,
				""user_is_moderator"": null,
				""accounts_active_is_fuzzed"": false,
				""advertiser_category"": ""Lifestyles"",
				""user_sr_theme_enabled"": true,
				""allow_images"": true,
				""show_media_preview"": true,
				""comment_score_hide_mins"": 0,
				""subreddit_type"": ""public"",
				""submission_type"": ""link"",
				""user_is_subscriber"": null
			}
		}"
		);
	private JObject explainitlikeimfiveJson = JObject.Parse (@"{
			""kind"": ""t5"",
			""data"": {
				""user_is_contributor"": null,
				""banner_img"": """",
				""submit_text_html"": null,
				""user_is_banned"": null,
				""wiki_enabled"": false,
				""show_media"": false,
				""id"": ""2sp76"",
				""description"": ""/r/explainlikeimfive\n\n[How do you redirect subreddits like this?](http://www.reddit.com/r/anonymous123421/comments/1il3jr/how_to_redirect/#howto)"",
				""submit_text"": """",
				""display_name"": ""explainitlikeimfive"",
				""header_img"": null,
				""description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;p&gt;&lt;a href=\""/r/explainlikeimfive\""&gt;/r/explainlikeimfive&lt;/a&gt;&lt;/p&gt;\n\n&lt;p&gt;&lt;a href=\""http://www.reddit.com/r/anonymous123421/comments/1il3jr/how_to_redirect/#howto\""&gt;How do you redirect subreddits like this?&lt;/a&gt;&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""title"": ""explainitlikeimfive"",
				""collapse_deleted_comments"": false,
				""public_description"": ""You meant to visit /r/explainlikeimfive"",
				""over18"": false,
				""public_description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;p&gt;You meant to visit &lt;a href=\""/r/explainlikeimfive\""&gt;/r/explainlikeimfive&lt;/a&gt;&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""spoilers_enabled"": true,
				""icon_size"": null,
				""suggested_comment_sort"": null,
				""icon_img"": """",
				""header_title"": null,
				""display_name_prefixed"": ""r/explainitlikeimfive"",
				""user_is_muted"": null,
				""submit_link_label"": null,
				""accounts_active"": 5,
				""public_traffic"": false,
				""header_size"": null,
				""subscribers"": 786,
				""submit_text_label"": null,
				""key_color"": """",
				""lang"": ""en"",
				""whitelist_status"": null,
				""name"": ""t5_2sp76"",
				""created"": 1312389014.0,
				""url"": ""/r/explainitlikeimfive/"",
				""quarantine"": false,
				""hide_ads"": false,
				""created_utc"": 1312360214.0,
				""banner_size"": null,
				""user_is_moderator"": null,
				""accounts_active_is_fuzzed"": true,
				""advertiser_category"": null,
				""user_sr_theme_enabled"": true,
				""allow_images"": true,
				""show_media_preview"": true,
				""comment_score_hide_mins"": 0,
				""subreddit_type"": ""restricted"",
				""submission_type"": ""any"",
				""user_is_subscriber"": null
			}
		}"
		);
	private JObject spaceJson = JObject.Parse (@"{
			""kind"": ""t5"",
			""data"": {
				""user_is_contributor"": null,
				""banner_img"": ""http://a.thumbs.redditmedia.com/Viwu-zOMDlD_GZOsO-OpNIKeFQa406KtExOdft5vqN8.png"",
				""submit_text_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;p&gt;&lt;strong&gt;Read our &lt;a href=\""/r/space/about/sidebar\""&gt;entire sidebar&lt;/a&gt; before submitting!&lt;/strong&gt;&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""user_is_banned"": null,
				""wiki_enabled"": true,
				""show_media"": true,
				""id"": ""2qh87"",
				""description"": ""### [Week of February 26, 2017 'All Space Questions' Thread](https://www.reddit.com/r/space/comments/5wb1rs/week_of_february_26_2017_all_space_questions/) | Subreddit shoutout: /r/SpaceX\r\n##About\r\n\r\n/r/space is dedicated to the insightful and thoughtful discussion of outer space\r\n\r\n## Schedule\r\n\r\nDate | Event \r\n:-:|:-\r\n1 Mar, 12:50pm|[Atlas 5 • NROL-79](http://www.ulalaunch.com/atlas-v-to-launch-nrol79.aspx)\n6 Mar, 8:49pm|[Vega • Sentinel 2B](https://earth.esa.int/web/guest/missions/esa-operational-eo-missions/sentinel-2)\n8 Mar, 6:48pm|[Delta 4 • WGS 9](http://www.ulalaunch.com/delta-iv-to-launch-wgs9.aspx)\n12 Mar, 12:27pm|[Falcon 9 • EchoStar 23](http://www.echostar.com/launch.aspx)\n15 Mar, 9pm|H-2A • IGS Radar 5\n\r\n\r\n[View Full Calendar](http://goo.gl/IXM141)  \r\n*All times are in Eastern Time (GMT-4)*\r\n\r\nIf you know of an event that should be here, [message the moderators](/message/compose?to=%2Fr%2Fspace) and let us know!\r\n\r\n##Allowed submissions\r\n\r\n* Top-level source news and articles about outer space\r\n\r\n* Directly linked, quality images with a strong connection to Space/Astronomy/Cosmology (must use the original source of image)\r\n\r\n* Informative and thought provoking self-posts\r\n\r\n* Discussions about outer space\r\n\r\n##Disallowed submissions\r\n\r\n* Submissions with no direct connection to Space/Astronomy/Cosmology. This includes \""circlejerky\"" submissions or space-related art, with the exception of art from space agencies or historically-significant art.\r\n\r\n* Amateur (non-space agency) astrophotography - /r/AstroPhotography, /r/Astronomy, and /r/Spaceporn are good alternatives for this.\r\n  * **You may post these on Fridays**\r\n\r\n* Memes, image macros, comics, and other low-quality/low-effort images\r\n\r\n* Editorialized, sensationalized, personalized, vague, misleading, generic, or pandering titles\r\n\r\n* Re-hosted content, with the exception of GIF/WebMs with a link to the original source in the comments. NASA content may also be rehosted with a link to the original in the comments.\r\n\r\n* DAE/TIL/ELI5/PSA/[SERIOUS]/CMV/[FIXED] styled titles\r\n\r\n* Websites that utilize a paywall\r\n\r\n* Petitions/surveys/crowdfunders\r\n\r\n* Mobile links/link shorteners/facebook links\r\n\r\n* [Any form of spam/blogspam](http://goo.gl/RQpjJi)\r\n\r\n* Enabling or linking to piracy\r\n\r\n* **Reposts**\r\n\r\n  * [Use KarmaDecay to check if your image has already been submitted.](http://goo.gl/onm9uC 'Before posting an image, use http://KarmaDecay.com/r/space to see if it has been posted already in this sub. After checking your image, KarmaDecay provides a link to submit the image /r/space. To check an image after it has been posted, you can click this link from the post page. (May not work for some users; uses HTTP referrer.) Or replace \""reddit\"" with \""karmadecay\"" in the URL. We also recommend the Karma Decay bookmarklet or browser extension. ')\r\n\r\n       ^^^Hover ^^^over ^^^the ^^^link ^^^for ^^^details\r\n\r\n  * [Use reddit's search function to check if your article/image has already been submitted](/r/space/search?q=&amp;sort=new&amp;restrict_sr=on)\r\n\r\n  * Google \""site:reddit.com/r/space ***search terms***\"" to check if your article/image has already been submitted\r\n\r\n##Disallowed comments\r\n\r\n* Low-effort comments or ones that do not contribute to discussion\r\n\r\n* Comments consisting solely of an image\r\n\r\n* Memes, image macros, jokes, circlejerking, or spamming\r\n\r\n* Trolling, insults, or excessive hostility\r\n\r\n* Link shorteners or Facebook links\r\n\r\n* Unmarked NSFW links\r\n\r\n* Enabling or linking to piracy\r\n\r\n^^We ^^reserve ^^the ^^right ^^to ^^moderate ^^at ^^our ^^own ^^discretion.\r\n\r\n##Subreddits\r\n\r\nOur Friends||\r\n:--|:--\r\n/r/New_Horizons | /r/telescopes | \n/r/Europa | /r/Cosmos | \n/r/spacequestions | /r/LiveFromSpace | \n/r/moon | /r/astrophotography | \n/r/YutuRover | /r/BlueOrigin | \n\r\n\r\nThis section is randomized every hour, from our [small space subreddits](/u/SpaceMods/m/otherspacesubreddits) multireddit\r\n\r\n##Traffic Stats\r\n\r\n* Want to see our traffic statistics? [Click here for further information.](http://goo.gl/LdA9kS)\r\n\r\n##IRC Channel\r\n\r\n* Visit our [irc channel](http://space.snoo.me) to chat with fellow users on irc.snoonet.org/6667 at [#space](irc://irc.snoonet.org/%23space).\r\n\r\n\r\n##Messaging The Moderators\r\n\r\nIf you believe that your submission was caught by the spam filter or you have any questions/concerns, [feel free to message us.](/message/compose?to=/r/space)\r\n\r\n[Nightmode](http://nm.reddit.com/r/space)\r\n[Normal](http://www.reddit.com/r/space)\r\n\r\n###### [](/r/space/wiki/edit/sidebar_template)\r\n\r\n[](#/RES_SR_Config/NightModeCompatible) [](https://redd.it/3o3yp1/) "",
				""submit_text"": ""**Read our [entire sidebar](/r/space/about/sidebar) before submitting!**"",
				""display_name"": ""space"",
				""header_img"": ""http://b.thumbs.redditmedia.com/CltPFEKKCn6cw7Qc0_uoQl7HY6jGdpCGQSL6PgadzyY.png"",
				""description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;h3&gt;&lt;a href=\""https://www.reddit.com/r/space/comments/5wb1rs/week_of_february_26_2017_all_space_questions/\""&gt;Week of February 26, 2017 &amp;#39;All Space Questions&amp;#39; Thread&lt;/a&gt; | Subreddit shoutout: &lt;a href=\""/r/SpaceX\""&gt;/r/SpaceX&lt;/a&gt;&lt;/h3&gt;\n\n&lt;h2&gt;About&lt;/h2&gt;\n\n&lt;p&gt;&lt;a href=\""/r/space\""&gt;/r/space&lt;/a&gt; is dedicated to the insightful and thoughtful discussion of outer space&lt;/p&gt;\n\n&lt;h2&gt;Schedule&lt;/h2&gt;\n\n&lt;table&gt;&lt;thead&gt;\n&lt;tr&gt;\n&lt;th align=\""center\""&gt;Date&lt;/th&gt;\n&lt;th align=\""left\""&gt;Event&lt;/th&gt;\n&lt;/tr&gt;\n&lt;/thead&gt;&lt;tbody&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;1 Mar, 12:50pm&lt;/td&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""http://www.ulalaunch.com/atlas-v-to-launch-nrol79.aspx\""&gt;Atlas 5 • NROL-79&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;6 Mar, 8:49pm&lt;/td&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""https://earth.esa.int/web/guest/missions/esa-operational-eo-missions/sentinel-2\""&gt;Vega • Sentinel 2B&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;8 Mar, 6:48pm&lt;/td&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""http://www.ulalaunch.com/delta-iv-to-launch-wgs9.aspx\""&gt;Delta 4 • WGS 9&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;12 Mar, 12:27pm&lt;/td&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""http://www.echostar.com/launch.aspx\""&gt;Falcon 9 • EchoStar 23&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""center\""&gt;15 Mar, 9pm&lt;/td&gt;\n&lt;td align=\""left\""&gt;H-2A • IGS Radar 5&lt;/td&gt;\n&lt;/tr&gt;\n&lt;/tbody&gt;&lt;/table&gt;\n\n&lt;p&gt;&lt;a href=\""http://goo.gl/IXM141\""&gt;View Full Calendar&lt;/a&gt;&lt;br/&gt;\n&lt;em&gt;All times are in Eastern Time (GMT-4)&lt;/em&gt;&lt;/p&gt;\n\n&lt;p&gt;If you know of an event that should be here, &lt;a href=\""/message/compose?to=%2Fr%2Fspace\""&gt;message the moderators&lt;/a&gt; and let us know!&lt;/p&gt;\n\n&lt;h2&gt;Allowed submissions&lt;/h2&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;p&gt;Top-level source news and articles about outer space&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Directly linked, quality images with a strong connection to Space/Astronomy/Cosmology (must use the original source of image)&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Informative and thought provoking self-posts&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Discussions about outer space&lt;/p&gt;&lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;h2&gt;Disallowed submissions&lt;/h2&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;p&gt;Submissions with no direct connection to Space/Astronomy/Cosmology. This includes &amp;quot;circlejerky&amp;quot; submissions or space-related art, with the exception of art from space agencies or historically-significant art.&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Amateur (non-space agency) astrophotography - &lt;a href=\""/r/AstroPhotography\""&gt;/r/AstroPhotography&lt;/a&gt;, &lt;a href=\""/r/Astronomy\""&gt;/r/Astronomy&lt;/a&gt;, and &lt;a href=\""/r/Spaceporn\""&gt;/r/Spaceporn&lt;/a&gt; are good alternatives for this.&lt;/p&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;strong&gt;You may post these on Fridays&lt;/strong&gt;&lt;/li&gt;\n&lt;/ul&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Memes, image macros, comics, and other low-quality/low-effort images&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Editorialized, sensationalized, personalized, vague, misleading, generic, or pandering titles&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Re-hosted content, with the exception of GIF/WebMs with a link to the original source in the comments. NASA content may also be rehosted with a link to the original in the comments.&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;DAE/TIL/ELI5/PSA/[SERIOUS]/CMV/[FIXED] styled titles&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Websites that utilize a paywall&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Petitions/surveys/crowdfunders&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Mobile links/link shorteners/facebook links&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;&lt;a href=\""http://goo.gl/RQpjJi\""&gt;Any form of spam/blogspam&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Enabling or linking to piracy&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;&lt;strong&gt;Reposts&lt;/strong&gt;&lt;/p&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;p&gt;&lt;a href=\""http://goo.gl/onm9uC\"" title=\""Before posting an image, use http://KarmaDecay.com/r/space to see if it has been posted already in this sub. After checking your image, KarmaDecay provides a link to submit the image /r/space. To check an image after it has been posted, you can click this link from the post page. (May not work for some users; uses HTTP referrer.) Or replace &amp;quot;reddit&amp;quot; with &amp;quot;karmadecay&amp;quot; in the URL. We also recommend the Karma Decay bookmarklet or browser extension. \""&gt;Use KarmaDecay to check if your image has already been submitted.&lt;/a&gt;&lt;/p&gt;\n\n&lt;p&gt;&lt;sup&gt;&lt;sup&gt;&lt;sup&gt;Hover&lt;/sup&gt;&lt;/sup&gt;&lt;/sup&gt; &lt;sup&gt;&lt;sup&gt;&lt;sup&gt;over&lt;/sup&gt;&lt;/sup&gt;&lt;/sup&gt; &lt;sup&gt;&lt;sup&gt;&lt;sup&gt;the&lt;/sup&gt;&lt;/sup&gt;&lt;/sup&gt; &lt;sup&gt;&lt;sup&gt;&lt;sup&gt;link&lt;/sup&gt;&lt;/sup&gt;&lt;/sup&gt; &lt;sup&gt;&lt;sup&gt;&lt;sup&gt;for&lt;/sup&gt;&lt;/sup&gt;&lt;/sup&gt; &lt;sup&gt;&lt;sup&gt;&lt;sup&gt;details&lt;/sup&gt;&lt;/sup&gt;&lt;/sup&gt;&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;&lt;a href=\""/r/space/search?q=&amp;amp;sort=new&amp;amp;restrict_sr=on\""&gt;Use reddit&amp;#39;s search function to check if your article/image has already been submitted&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Google &amp;quot;site:reddit.com&lt;a href=\""/r/space\""&gt;/r/space&lt;/a&gt; &lt;strong&gt;&lt;em&gt;search terms&lt;/em&gt;&lt;/strong&gt;&amp;quot; to check if your article/image has already been submitted&lt;/p&gt;&lt;/li&gt;\n&lt;/ul&gt;&lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;h2&gt;Disallowed comments&lt;/h2&gt;\n\n&lt;ul&gt;\n&lt;li&gt;&lt;p&gt;Low-effort comments or ones that do not contribute to discussion&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Comments consisting solely of an image&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Memes, image macros, jokes, circlejerking, or spamming&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Trolling, insults, or excessive hostility&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Link shorteners or Facebook links&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Unmarked NSFW links&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Enabling or linking to piracy&lt;/p&gt;&lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;p&gt;&lt;sup&gt;&lt;sup&gt;We&lt;/sup&gt;&lt;/sup&gt; &lt;sup&gt;&lt;sup&gt;reserve&lt;/sup&gt;&lt;/sup&gt; &lt;sup&gt;&lt;sup&gt;the&lt;/sup&gt;&lt;/sup&gt; &lt;sup&gt;&lt;sup&gt;right&lt;/sup&gt;&lt;/sup&gt; &lt;sup&gt;&lt;sup&gt;to&lt;/sup&gt;&lt;/sup&gt; &lt;sup&gt;&lt;sup&gt;moderate&lt;/sup&gt;&lt;/sup&gt; &lt;sup&gt;&lt;sup&gt;at&lt;/sup&gt;&lt;/sup&gt; &lt;sup&gt;&lt;sup&gt;our&lt;/sup&gt;&lt;/sup&gt; &lt;sup&gt;&lt;sup&gt;own&lt;/sup&gt;&lt;/sup&gt; &lt;sup&gt;&lt;sup&gt;discretion.&lt;/sup&gt;&lt;/sup&gt;&lt;/p&gt;\n\n&lt;h2&gt;Subreddits&lt;/h2&gt;\n\n&lt;table&gt;&lt;thead&gt;\n&lt;tr&gt;\n&lt;th align=\""left\""&gt;Our Friends&lt;/th&gt;\n&lt;th align=\""left\""&gt;&lt;/th&gt;\n&lt;/tr&gt;\n&lt;/thead&gt;&lt;tbody&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/New_Horizons\""&gt;/r/New_Horizons&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/telescopes\""&gt;/r/telescopes&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/Europa\""&gt;/r/Europa&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/Cosmos\""&gt;/r/Cosmos&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/spacequestions\""&gt;/r/spacequestions&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/LiveFromSpace\""&gt;/r/LiveFromSpace&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/moon\""&gt;/r/moon&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/astrophotography\""&gt;/r/astrophotography&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/YutuRover\""&gt;/r/YutuRover&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""/r/BlueOrigin\""&gt;/r/BlueOrigin&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;/tbody&gt;&lt;/table&gt;\n\n&lt;p&gt;This section is randomized every hour, from our &lt;a href=\""/u/SpaceMods/m/otherspacesubreddits\""&gt;small space subreddits&lt;/a&gt; multireddit&lt;/p&gt;\n\n&lt;h2&gt;Traffic Stats&lt;/h2&gt;\n\n&lt;ul&gt;\n&lt;li&gt;Want to see our traffic statistics? &lt;a href=\""http://goo.gl/LdA9kS\""&gt;Click here for further information.&lt;/a&gt;&lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;h2&gt;IRC Channel&lt;/h2&gt;\n\n&lt;ul&gt;\n&lt;li&gt;Visit our &lt;a href=\""http://space.snoo.me\""&gt;irc channel&lt;/a&gt; to chat with fellow users on irc.snoonet.org/6667 at &lt;a href=\""irc://irc.snoonet.org/%23space\""&gt;#space&lt;/a&gt;.&lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;h2&gt;Messaging The Moderators&lt;/h2&gt;\n\n&lt;p&gt;If you believe that your submission was caught by the spam filter or you have any questions/concerns, &lt;a href=\""/message/compose?to=/r/space\""&gt;feel free to message us.&lt;/a&gt;&lt;/p&gt;\n\n&lt;p&gt;&lt;a href=\""http://nm.reddit.com/r/space\""&gt;Nightmode&lt;/a&gt;\n&lt;a href=\""http://www.reddit.com/r/space\""&gt;Normal&lt;/a&gt;&lt;/p&gt;\n\n&lt;h6&gt;&lt;a href=\""/r/space/wiki/edit/sidebar_template\""&gt;&lt;/a&gt;&lt;/h6&gt;\n\n&lt;p&gt;&lt;a href=\""#/RES_SR_Config/NightModeCompatible\""&gt;&lt;/a&gt; &lt;a href=\""https://redd.it/3o3yp1/\""&gt;&lt;/a&gt; &lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""title"": ""/r/space: news, articles, images, and discussion"",
				""collapse_deleted_comments"": true,
				""public_description"": ""/r/space is dedicated to the insightful and thoughtful discussion of outer space."",
				""over18"": false,
				""public_description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;p&gt;&lt;a href=\""/r/space\""&gt;/r/space&lt;/a&gt; is dedicated to the insightful and thoughtful discussion of outer space.&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
				""spoilers_enabled"": true,
				""icon_size"": [
					240,
					240
				],
				""suggested_comment_sort"": null,
				""icon_img"": ""http://b.thumbs.redditmedia.com/Zf90LsQEOyfU9RKf5NgXRATeMlFHULaD-B6UlicR5Sc.png"",
				""header_title"": null,
				""display_name_prefixed"": ""r/space"",
				""user_is_muted"": null,
				""submit_link_label"": ""Submit a link"",
				""accounts_active"": 3671,
				""public_traffic"": true,
				""header_size"": [
					60,
					64
				],
				""subscribers"": 11009539,
				""submit_text_label"": ""Submit a new post"",
				""key_color"": ""#222222"",
				""lang"": ""en"",
				""whitelist_status"": ""all_ads"",
				""name"": ""t5_2qh87"",
				""created"": 1201356474.0,
				""url"": ""/r/space/"",
				""quarantine"": false,
				""hide_ads"": false,
				""created_utc"": 1201327674.0,
				""banner_size"": [
					1280,
					720
				],
				""user_is_moderator"": null,
				""accounts_active_is_fuzzed"": false,
				""advertiser_category"": ""Lifestyles"",
				""user_sr_theme_enabled"": true,
				""allow_images"": true,
				""show_media_preview"": true,
				""comment_score_hide_mins"": 240,
				""subreddit_type"": ""public"",
				""submission_type"": ""any"",
				""user_is_subscriber"": null
			}
		}"
		);




	private JObject AskRedditJson = JObject.Parse(@"{""kind"": ""t5"", ""data"": {
    ""user_is_contributor"": null,
    ""banner_img"": ""http://b.thumbs.redditmedia.com/PXt8GnqdYu-9lgzb3iesJBLN21bXExRV1A45zdw4sYE.png"",
    ""submit_text_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;p&gt;&lt;strong&gt;AskReddit is all about DISCUSSION. Your post needs to inspire discussion, ask an open-ended question that prompts redditors to share ideas or opinions.&lt;/strong&gt;&lt;/p&gt;\n\n&lt;p&gt;&lt;strong&gt;Questions need to be neutral and the question alone.&lt;/strong&gt; Any opinion or answer must go as a reply to your question, this includes examples or any kind of story about you. This is so that all responses will be to your question, and there&amp;#39;s nothing else to respond to. Opinionated posts are forbidden.&lt;/p&gt;\n\n&lt;ul&gt;\n&lt;li&gt;If your question has a factual answer, try &lt;a href=\""/r/answers\""&gt;/r/answers&lt;/a&gt;.&lt;/li&gt;\n&lt;li&gt;If you are trying to find out about something or get an explanation, try &lt;a href=\""/r/explainlikeimfive\""&gt;/r/explainlikeimfive&lt;/a&gt;&lt;/li&gt;\n&lt;li&gt;If your question has a limited number of responses, then it&amp;#39;s not suitable.&lt;/li&gt;\n&lt;li&gt;If you&amp;#39;re asking for any kind of advice, then it&amp;#39;s not suitable.&lt;/li&gt;\n&lt;li&gt;If you feel the need to add an example in order for your question to make sense then you need to re-word your question.&lt;/li&gt;\n&lt;li&gt;If you&amp;#39;re explaining why you&amp;#39;re asking the question, you need to stop.&lt;/li&gt;\n&lt;/ul&gt;\n\n&lt;p&gt;You can always ask where best to post in &lt;a href=\""/r/findareddit\""&gt;/r/findareddit&lt;/a&gt;.&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
    ""user_is_banned"": null,
    ""wiki_enabled"": true,
    ""show_media"": false,
    ""id"": ""2qh1i"",
    ""description"": ""###### [ [ SERIOUS ] ](http://www.reddit.com/r/askreddit/submit?selftext=true&amp;title=%5BSerious%5D)\n\n\n##### [Rules](https://www.reddit.com/r/AskReddit/wiki/index#wiki_rules):\n1. You must post a clear and direct question in the title. The title may contain two, short, necessary context sentences.\nNo text is allowed in the textbox. Your thoughts/responses to the question can go in the comments section. [more &gt;&gt;](http://goo.gl/tMUR4k)\n\n2. Any post asking for advice should be generic and not specific to your situation alone. [more &gt;&gt;](http://goo.gl/2L771B)\n\n3. Askreddit is for open-ended discussion questions. [more &gt;&gt;](http://goo.gl/DcPPLf)\n\n4. Posting, or seeking, any identifying personal information, real or fake, will result in a ban without a prior warning. [more &gt;&gt;](http://goo.gl/imCCMb)\n\n5. Askreddit is not your soapbox, personal army, or advertising platform. [more &gt;&gt;](http://goo.gl/DG4Q2M)\n\n6. Questions seeking professional advice are inappropriate for this subreddit and will be removed. [more &gt;&gt;](http://goo.gl/G6Zbap)\n\n7. Soliciting money, goods, services, or favours is not allowed. [more &gt;&gt;](http://goo.gl/Ce2LTY)\n\n8. Mods reserve the right to remove content or restrict users' posting privileges as necessary if it is deemed detrimental to the subreddit or to the experience of others. [more &gt;&gt;](http://goo.gl/a5GQTm)\n\n9. Comment replies consisting solely of images will be removed. [more &gt;&gt;](http://goo.gl/YVNgU0)\n\n##### If you think your post has disappeared, see spam or an inappropriate post, please do not hesitate to [contact the mods](http://goo.gl/xnppZr), we're happy to help.\n\n---\n\n#### Tags to use:\n\n&gt; ## [[Serious]](http://goo.gl/gMFZjB)\n\n### Use a **[Serious]** post tag to designate your post as a serious, on-topic-only thread.\n\n-\n\n#### Filter posts by subject:\n\n[Mod posts](http://ud.reddit.com/r/AskReddit/#ud)\n[Serious posts](http://dg.reddit.com/r/AskReddit/#dg)\n[Megathread](http://bu.reddit.com/r/AskReddit/#bu)\n[Breaking news](http://nr.reddit.com/r/AskReddit/#nr)\n[Unfilter](http://goo.gl/qJBQRm)\n\n-\n\n### Do you have ideas or feedback for Askreddit? Submit to [/r/Ideasforaskreddit](http://www.reddit.com/r/Ideasforaskreddit).\n\n-\n\n### Interested in the amount of traffic /r/AskReddit receives daily/monthly? Check out our [traffic stats here](http://goo.gl/su6Az1)!\n\n-\n\n### We have spoiler tags, please use them! /spoiler, #spoiler, /s, #s. Use it `[like this](/spoiler)`\n\n-\n\n#### Other subreddits you might like:\nsome|header\n:---|:---\n[Ask Others](https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_ask_others)|[Self &amp; Others](https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_self_.26amp.3B_others)\n[Find a subreddit](https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_find_a_subreddit)|[Learn something](https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_learn_something)\n[Meta Subs](https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_meta)|[What is this ___](https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_what_is_this______)\n[AskReddit Offshoots](https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_askreddit_offshoots)|[Offers &amp; Assistance](https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_offers_.26amp.3B_assistance)\n\n\n-\n\n### Ever read the reddiquette? [Take a peek!](http://goo.gl/pxsd8T)\n\n[](#/RES_SR_Config/NightModeCompatible)\n[](http://goo.gl/TQnRmU)\n[](#may4th)\n"",
    ""submit_text"": ""**AskReddit is all about DISCUSSION. Your post needs to inspire discussion, ask an open-ended question that prompts redditors to share ideas or opinions.**\n\n**Questions need to be neutral and the question alone.** Any opinion or answer must go as a reply to your question, this includes examples or any kind of story about you. This is so that all responses will be to your question, and there's nothing else to respond to. Opinionated posts are forbidden.\n\n* If your question has a factual answer, try /r/answers.\n* If you are trying to find out about something or get an explanation, try /r/explainlikeimfive\n* If your question has a limited number of responses, then it's not suitable.\n* If you're asking for any kind of advice, then it's not suitable.\n* If you feel the need to add an example in order for your question to make sense then you need to re-word your question.\n* If you're explaining why you're asking the question, you need to stop.\n\nYou can always ask where best to post in /r/findareddit."",
    ""display_name"": ""AskReddit"",
    ""header_img"": ""http://a.thumbs.redditmedia.com/IrfPJGuWzi_ewrDTBlnULeZsJYGz81hsSQoQJyw6LD8.png"",
    ""description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;h6&gt;&lt;a href=\""http://www.reddit.com/r/askreddit/submit?selftext=true&amp;amp;title=%5BSerious%5D\""&gt; [ SERIOUS ] &lt;/a&gt;&lt;/h6&gt;\n\n&lt;h5&gt;&lt;a href=\""https://www.reddit.com/r/AskReddit/wiki/index#wiki_rules\""&gt;Rules&lt;/a&gt;:&lt;/h5&gt;\n\n&lt;ol&gt;\n&lt;li&gt;&lt;p&gt;You must post a clear and direct question in the title. The title may contain two, short, necessary context sentences.\nNo text is allowed in the textbox. Your thoughts/responses to the question can go in the comments section. &lt;a href=\""http://goo.gl/tMUR4k\""&gt;more &amp;gt;&amp;gt;&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Any post asking for advice should be generic and not specific to your situation alone. &lt;a href=\""http://goo.gl/2L771B\""&gt;more &amp;gt;&amp;gt;&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Askreddit is for open-ended discussion questions. &lt;a href=\""http://goo.gl/DcPPLf\""&gt;more &amp;gt;&amp;gt;&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Posting, or seeking, any identifying personal information, real or fake, will result in a ban without a prior warning. &lt;a href=\""http://goo.gl/imCCMb\""&gt;more &amp;gt;&amp;gt;&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Askreddit is not your soapbox, personal army, or advertising platform. &lt;a href=\""http://goo.gl/DG4Q2M\""&gt;more &amp;gt;&amp;gt;&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Questions seeking professional advice are inappropriate for this subreddit and will be removed. &lt;a href=\""http://goo.gl/G6Zbap\""&gt;more &amp;gt;&amp;gt;&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Soliciting money, goods, services, or favours is not allowed. &lt;a href=\""http://goo.gl/Ce2LTY\""&gt;more &amp;gt;&amp;gt;&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Mods reserve the right to remove content or restrict users&amp;#39; posting privileges as necessary if it is deemed detrimental to the subreddit or to the experience of others. &lt;a href=\""http://goo.gl/a5GQTm\""&gt;more &amp;gt;&amp;gt;&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;\n&lt;li&gt;&lt;p&gt;Comment replies consisting solely of images will be removed. &lt;a href=\""http://goo.gl/YVNgU0\""&gt;more &amp;gt;&amp;gt;&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;\n&lt;/ol&gt;\n\n&lt;h5&gt;If you think your post has disappeared, see spam or an inappropriate post, please do not hesitate to &lt;a href=\""http://goo.gl/xnppZr\""&gt;contact the mods&lt;/a&gt;, we&amp;#39;re happy to help.&lt;/h5&gt;\n\n&lt;hr/&gt;\n\n&lt;h4&gt;Tags to use:&lt;/h4&gt;\n\n&lt;blockquote&gt;\n&lt;h2&gt;&lt;a href=\""http://goo.gl/gMFZjB\""&gt;[Serious]&lt;/a&gt;&lt;/h2&gt;\n&lt;/blockquote&gt;\n\n&lt;h3&gt;Use a &lt;strong&gt;[Serious]&lt;/strong&gt; post tag to designate your post as a serious, on-topic-only thread.&lt;/h3&gt;\n\n&lt;h2&gt;&lt;/h2&gt;\n\n&lt;h4&gt;Filter posts by subject:&lt;/h4&gt;\n\n&lt;p&gt;&lt;a href=\""http://ud.reddit.com/r/AskReddit/#ud\""&gt;Mod posts&lt;/a&gt;\n&lt;a href=\""http://dg.reddit.com/r/AskReddit/#dg\""&gt;Serious posts&lt;/a&gt;\n&lt;a href=\""http://bu.reddit.com/r/AskReddit/#bu\""&gt;Megathread&lt;/a&gt;\n&lt;a href=\""http://nr.reddit.com/r/AskReddit/#nr\""&gt;Breaking news&lt;/a&gt;\n&lt;a href=\""http://goo.gl/qJBQRm\""&gt;Unfilter&lt;/a&gt;&lt;/p&gt;\n\n&lt;h2&gt;&lt;/h2&gt;\n\n&lt;h3&gt;Do you have ideas or feedback for Askreddit? Submit to &lt;a href=\""http://www.reddit.com/r/Ideasforaskreddit\""&gt;/r/Ideasforaskreddit&lt;/a&gt;.&lt;/h3&gt;\n\n&lt;h2&gt;&lt;/h2&gt;\n\n&lt;h3&gt;Interested in the amount of traffic &lt;a href=\""/r/AskReddit\""&gt;/r/AskReddit&lt;/a&gt; receives daily/monthly? Check out our &lt;a href=\""http://goo.gl/su6Az1\""&gt;traffic stats here&lt;/a&gt;!&lt;/h3&gt;\n\n&lt;h2&gt;&lt;/h2&gt;\n\n&lt;h3&gt;We have spoiler tags, please use them! /spoiler, #spoiler, /s, #s. Use it &lt;code&gt;[like this](/spoiler)&lt;/code&gt;&lt;/h3&gt;\n\n&lt;h2&gt;&lt;/h2&gt;\n\n&lt;h4&gt;Other subreddits you might like:&lt;/h4&gt;\n\n&lt;table&gt;&lt;thead&gt;\n&lt;tr&gt;\n&lt;th align=\""left\""&gt;some&lt;/th&gt;\n&lt;th align=\""left\""&gt;header&lt;/th&gt;\n&lt;/tr&gt;\n&lt;/thead&gt;&lt;tbody&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_ask_others\""&gt;Ask Others&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_self_.26amp.3B_others\""&gt;Self &amp;amp; Others&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_find_a_subreddit\""&gt;Find a subreddit&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_learn_something\""&gt;Learn something&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_meta\""&gt;Meta Subs&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_what_is_this______\""&gt;What is this ___&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;tr&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_askreddit_offshoots\""&gt;AskReddit Offshoots&lt;/a&gt;&lt;/td&gt;\n&lt;td align=\""left\""&gt;&lt;a href=\""https://www.reddit.com/r/AskReddit/wiki/sidebarsubs#wiki_offers_.26amp.3B_assistance\""&gt;Offers &amp;amp; Assistance&lt;/a&gt;&lt;/td&gt;\n&lt;/tr&gt;\n&lt;/tbody&gt;&lt;/table&gt;\n\n&lt;h2&gt;&lt;/h2&gt;\n\n&lt;h3&gt;Ever read the reddiquette? &lt;a href=\""http://goo.gl/pxsd8T\""&gt;Take a peek!&lt;/a&gt;&lt;/h3&gt;\n\n&lt;p&gt;&lt;a href=\""#/RES_SR_Config/NightModeCompatible\""&gt;&lt;/a&gt;\n&lt;a href=\""http://goo.gl/TQnRmU\""&gt;&lt;/a&gt;\n&lt;a href=\""#may4th\""&gt;&lt;/a&gt;&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
    ""title"": ""Ask Reddit..."",
    ""collapse_deleted_comments"": true,
    ""public_description"": ""/r/AskReddit is the place to ask and answer thought-provoking questions."",
    ""over18"": false,
    ""public_description_html"": ""&lt;!-- SC_OFF --&gt;&lt;div class=\""md\""&gt;&lt;p&gt;&lt;a href=\""/r/AskReddit\""&gt;/r/AskReddit&lt;/a&gt; is the place to ask and answer thought-provoking questions.&lt;/p&gt;\n&lt;/div&gt;&lt;!-- SC_ON --&gt;"",
    ""spoilers_enabled"": true,
    ""icon_size"": [
      256,
      256
    ],
    ""suggested_comment_sort"": null,
    ""icon_img"": ""http://b.thumbs.redditmedia.com/EndDxMGB-FTZ2MGtjepQ06cQEkZw_YQAsOUudpb9nSQ.png"",
    ""header_title"": ""Ass Credit"",
    ""display_name_prefixed"": ""r/AskReddit"",
    ""user_is_muted"": null,
    ""submit_link_label"": null,
    ""accounts_active"": 46892,
    ""public_traffic"": true,
    ""header_size"": [
      125,
      73
    ],
    ""subscribers"": 15980424,
    ""submit_text_label"": ""Ask a question"",
    ""key_color"": ""#222222"",
    ""lang"": ""es"",
    ""whitelist_status"": ""all_ads"",
    ""name"": ""t5_2qh1i"",
    ""created"": 1201261935.0,
    ""url"": ""/r/AskReddit/"",
    ""quarantine"": false,
    ""hide_ads"": false,
    ""created_utc"": 1201233135.0,
    ""banner_size"": [
      1280,
      384
    ],
    ""user_is_moderator"": null,
    ""accounts_active_is_fuzzed"": false,
    ""advertiser_category"": ""Lifestyles"",
    ""user_sr_theme_enabled"": true,
    ""allow_images"": true,
    ""show_media_preview"": true,
    ""comment_score_hide_mins"": 60,
    ""subreddit_type"": ""public"",
    ""submission_type"": ""self"",
    ""user_is_subscriber"": null
  }
}");





}


