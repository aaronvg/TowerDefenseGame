using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EmailComposer : MonoBehaviour {

	public GameObject senderText;
	public GameObject subjectText;
	public GameObject bodyText;

	private struct Email {
		public string sender;
		public string subject;
		public string body;

		public Email(string sender, string subject, string body) {
			this.sender = sender;
			this.subject = subject;
			this.body = body;
		}
	}

	private static Email[] good = {
		new Email("From: admin@bestbank.com",
		          "Subject: Thank you for applying for our online banking service!",

		          "Dear valued client,\n\n" +
		          "Thank you for applying for our secure online banking service! " +
		          "Please reply to this email to verify your email address and complete " +
		          "the registration process. Remember: Best Bank will never send emails " +
		          "asking you to divulge personally identifiable information or secure " +
		          "account details. All such matters will only be discussed in person " +
		          "at one of our many local branches. Have a pleasant day!"),

		new Email("From: humanresources@towertech.org",
		          "Subject: Still interested in a promotion? [URGENT]",

		          "Hello, this is your friendly HR department wondering if you are still " +
		          "interested in that promotion we discussed last week. A position just opened " +
		          "up today, but you'll have to let me know if you would be willing to fill it " +
		          "ASAP before someone else steps up."),

		new Email("From: accountservices@socialshopper.com",
		          "Subject: Account locked, action needed",

		          "Our automated fraud detection system recently observed suspicious activity " +
		          "regarding your account. The event is summarized below:\n\n" +
		          "02:05:03.1342 EST failed login attempt from Beijing, China\n" +
		          "02:05:04.5423 EST failed login attempt from Beijing, China\n" +
		          "02:05:06.6523 EST failed login attempt from Beijing, China\n" +
		          "02:05:07.0012 EST failed login attempt from Beijing, China\n\n" +
		          "Please respond to this message to unlock your account and reset your password."),

		new Email("From: swpixy@galmail.com",
		          "Subject: Yo buddy, still alive?",

		          "Haven't seen you in a few days, just checking in to make sure you haven't starved " +
		          "to death at your computer desk. This internet thing sure is great, huh? It's like " +
		          "a world with no boundaries. I'm getting so much more work done now that I don't have " +
		          "to go to the library every day. Just make sure you don't get too distracted and you'll " +
		          "be doing more than you ever thought possible. Anyway just let me know you're alright so " +
		          "I don't have to worry.\n\n" +
		          "Thanks friend."),

		new Email("From: Mom",
		          "Subject: i got an email account!",

		          "hello sweetie its your mother. i just wanted to let u know that i finally got an email " +
		          "account like u told me to. please respond so i can know if im using it right! ")
	};

	private static Email[] bad = {
		new Email("From: congratulations@madmoneyprize.com",
		          "Subject: Congratulations, you've won!",

		          "Dear lucky winner,\n\n" +
		          "Congratulations! You've won our $10,000 grand prize! Reply to this email " +
		          "with your date of birth to claim your winnings!"),

		new Email("From: admin@bestbank.biz",
		          "Subject: Your account has been hacked! [URGENT]",

		          "Your Best Bank online account has been hacked! Please reply to this " +
		          "email with your old password to verify your identity and reclaim your account!"),

		new Email("From: Mom",
		          "Subject: Make Cash Fast",

		          "Hello friend,\n\n" +
		          "I would like to register you for this new website I found. With this website's " +
		          "help, I made over $2000 dollars in just five days! Please tell me your mother's " +
		          "maiden name and I will make you an account.\n\n" +
		          "Your friend,\n" +
		          "Mom"),

		new Email("From: helpdesk@towertech.net",
		          "Subject: Routine server maintenance",

		          "Hello, this is the Tower Tech information technology helpdesk. We are doing some " +
		          "routine server maintenance and need you to verify your employee account password. " +
		          "Please reply to this email with your current password to ensure our database is " +
		          "consistent. Have a nice day!"),

		new Email("From: johnsmith@intermail.net",
		          "Subject: I seek a reliable and honest benefactor",

		          "I seize this opportunity to extend my unalloyed compliments of the new season to " +
		          "you and your family hoping that this year will bring more joy, happiness, and " +
		          "prosperity into your household. I am the son of a wealthy family in my home " +
		          "country of Namibia, but I have recently had a bout of bad luck. I am being " +
		          "prevented from returning home to my family after a political visit abroad. I come " +
		          "to you seeking a small monetary favor to secure my passage home. Once I arrive in " +
		          "my home country, I will use my position and influence to ensure that your favor " +
		          "will be repaid tenfold. I plead you to respond with all due haste lest our " +
		          "communications be further impeded.")
	};
	
	void Start () {
		Email msg;

		if(GetComponent<GimmickResponseHandler> ().isGood)
			msg = good [Random.Range (0, good.Length)];
		else
			msg = bad [Random.Range (0, bad.Length)];

		senderText.GetComponent<Text> ().text = msg.sender;
		subjectText.GetComponent<Text> ().text = msg.subject;
		bodyText.GetComponent<Text> ().text = msg.body;
	}
}
