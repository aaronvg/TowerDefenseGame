using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Text

public class FriendRequestComposer : MonoBehaviour {

	public GameObject headerText;
	public GameObject infoText;
	public GameObject messageText;
	
	private struct FriendRequest {
		public string name;
		public string info;
		public string msg;

		public FriendRequest(string name, string info, string msg) {
			this.name = name;
			this.info = info;
			this.msg = msg;
		}
	}

	private static FriendRequest[] good = {
		new FriendRequest("Johnny Guitar",
		                  "You and Johnny went to the same high school",
		                  "Hey it's your old friend Johnny, just trying " +
		                  "to get the band back together for that reunion " +
		                  "show you wanted to do."),

		new FriendRequest("Walter Sparrow",
		                  "You have 23 friends in common",
		                  ""),

		new FriendRequest("Frank Pritchard",
		                  "You and Frank both work at Tower Tech",
		                  "Ready to start our next project?"),

		new FriendRequest("TowerMgmt",
		                  "You and TowerMgmt both work at Tower Tech",
		                  "Yeah, this is Ned from management. If you could accept " +
		                  "this friend request so we can get all of our workers in " +
		                  "the same network that would be greaaaat."),

		new FriendRequest("Donnie Yen",
		                  "You and Donnie both like IM Wu Shu Club",
		                  "Great sparring session the other day!")
	};

	private static FriendRequest[] bad = {
		new FriendRequest("xX_head-:-hunt_Xx",
		                  "You have no friends in common",
		                  "lol noob i told u u would regret camping my spawn, " +
		                  "my friends in anonymuos helped me track down your " +
		                  "profile and now im gonna doxx you all over the chans"),
		
		new FriendRequest("spicykeychain",
		                  "You have 1 friend in common",
		                  "Come check out the HOTTEST new site for singles and " +
		                  "car enthusiasts! Like cars? Like dating? Then " +
		                  "spicykeychain.com is the place to be!"),

		new FriendRequest("Edward Sallow",
		                  "You and Edward went to the same high school",
		                  "I know what you're thinking: I barely even talked to " +
		                  "Edward in high school, what does he want from me now? " +
		                  "Well I'm here on behalf of Conway Multilevel Marketing " +
		                  "to offer you the chance of a lifetime. " +
		                  "All you need to do is invest a few hundred dollars in some " +
		                  "Conway product, be the great entrepeneur I knew in high school, " +
		                  "and you'll be making thousands in no time. Be your own boss, " +
		                  "set your own hours, and get rich doing it."),

		new FriendRequest("John Smith",
		                  "You have no friends in common",
		                  ""),

		new FriendRequest("Dank Memes",
		                  "You and Dank both like The Internet",
		                  "Check out Dank Memes for the latest internet memes and lol " +
		                  "for hours! Be one step ahead of your friends! Identify reposts " +
		                  "and stolen content instantly! Dank Memes - where the internet " +
		                  "begins.")
	};

	void Start () {
		FriendRequest fr;
		
		if(GetComponent<GimmickResponseHandler> ().isGood)
			fr = good [Random.Range (0, good.Length)];
		else
			fr = bad [Random.Range (0, bad.Length)];
		
		headerText.GetComponent<Text> ().text = fr.name + " has sent you a friend request!";
		infoText.GetComponent<Text> ().text = fr.info;
		messageText.GetComponent<Text> ().text = fr.msg;
	}
}
