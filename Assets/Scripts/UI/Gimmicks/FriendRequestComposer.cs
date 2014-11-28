using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Text

public class FriendRequestComposer : MonoBehaviour {

	public GameObject headerText;
	public GameObject infoText;
	public GameObject messageText;
	
	private struct FriendRequest {
		public string title;
		public string info;
		public string msg;

		public FriendRequest(string title, string info, string msg) {
			this.title = title;
			this.info = info;
			this.msg = msg;
		}
	}

	private static FriendRequest[] good = {
		new FriendRequest("Johnny Guitar has sent you a friend request!",
		                  "You have 13 friends in common",
		                  "Hey dude it's your old friend Johnny, just trying " +
		                  "to get in touch with everyone from the old band."),

		new FriendRequest("Jim Carrey has sent you a friend request!",
		                  "You have 23 friends in common",
		                  "")
	};

	private static FriendRequest[] bad = {
		new FriendRequest("xX_head-:-hunt_Xx has sent you a friend request!",
		                  "You have 0 friends in common",
		                  "lol noob i told u u would regret camping my spawn, " +
		                  "my friends in anonymuos helped me track down your " +
		                  "profile and now im gonna doxx you all over the chans"),
		
		new FriendRequest("spicykeychain has sent you a friend request!",
		                  "You have 1 friend in common",
		                  "Come check out the HOTTEST new site for singles and " +
		                  "car enthusiasts! Like cars? Like dating? Then " +
		                  "spicykeychain.com is the place to be!")
	};

	void Start () {
		FriendRequest fr;
		
		if(GetComponent<GimmickResponseHandler> ().isGood)
			fr = good [Random.Range (0, good.Length)];
		else
			fr = bad [Random.Range (0, bad.Length)];
		
		headerText.GetComponent<Text> ().text = fr.title;
		infoText.GetComponent<Text> ().text = fr.info;
		messageText.GetComponent<Text> ().text = fr.msg;
	}
}
