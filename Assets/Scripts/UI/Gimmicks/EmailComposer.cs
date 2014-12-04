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
		new Email("admin@bestbank.com",
		          "Thank you for applying for our online banking service!",

		          "Dear valued client,\n\n" +
		          "Thank you for applying for our secure online banking service! " +
		          "Please reply to this email to verify your email address and complete " +
		          "the registration process. Remember: Best Bank will never send emails " +
		          "asking you to divulge personally identifiable information or secure " +
		          "account details. All such matters will only be discussed in person " +
		          "at one of our many local branches. Have a pleasant day!"),

		new Email("humanresources@towertech.org",
		          "Still interested in a promotion? [URGENT]",

		          "Hello, this is your friendly HR department wondering if you are still " +
		          "interested in that promotion we discussed last week. A position just opened " +
		          "up today, but you'll have to let me know if you would be willing to fill it " +
		          "ASAP before someone else steps up."),

		new Email("accountservices@socialshopper.com",
		          "Account locked, action needed",

		          "Our automated fraud detection system recently observed suspicious activity " +
		          "regarding your account. The event is summarized below:\n\n" +
		          "02:05:03.1342 EST failed login attempt from Beijing, China\n" +
		          "02:05:04.5423 EST failed login attempt from Beijing, China\n" +
		          "02:05:06.6523 EST failed login attempt from Beijing, China\n" +
		          "02:05:07.0012 EST failed login attempt from Beijing, China\n\n" +
		          "Please respond to this message to unlock your account and reset your password."),

		new Email("swpixy@galmail.com",
		          "Yo buddy, still alive?",

		          "Haven't seen you in a few days, just checking in to make sure you haven't starved " +
		          "to death at your computer desk. This internet thing sure is great, huh? It's like " +
		          "a world with no boundaries. I'm getting so much more work done now that I don't have " +
		          "to go to the library every day. Just make sure you don't get too distracted and you'll " +
		          "be doing more than you ever thought possible. Anyway just let me know you're alright so " +
		          "I don't have to worry.\n\n" +
		          "Thanks friend."),

		new Email("Mom",
		          "i got an email account!",

		          "hello sweetie its your mother. i just wanted to let u know that i finally got an email " +
		          "account like u told me to. please respond so i can know if im using it right! "),
		 
		new Email("management@towertech.org",
		          "Congratulations, you’ve won!",
		          
		          "We here at management have been very impressed with your work over the last few months. " +
		          "As such, we have selected you as our employee of the month! Please respond to this email " + 
		          "to officially accept your award. Failing to reply will cause this month’s award to go to " +
		          "our second choice candidate. Keep up the great work!"),
		          
		new Email("captainmurphy@lotusmail.net",
		          "Your skydiving session has been canceled",
		 		   
		          "Captain Murphy here. I regret to inform you that your currently scheduled skydiving session " +
		          "has been canceled. Please reply to this email with your future availability so we can " +
		          "reschedule your drop. Thank you, and I apologize for any inconvenience this may have caused."
		 ),
		 
		new Email("reservations@europeanvegas.com",
		          "A new room has been made available!",
		          
		          "Dear sir or madame, we are pleased to inform you that due to a recent cancellation you are " +
		          "eligible for a free upgrade to a master suite for the duration of your upcoming trip to " +
		          "European Vegas hotel and casino. Please respond promptly to confirm that you would like " +
		          "to accept this offer. We look forward to your visit!")
	};

	private static Email[] bad = {
		new Email("congratulations@madmoneyprize.com",
		          "Congratulations, you've won!",

		          "Dear lucky winner,\n\n" +
		          "Congratulations! You've won our $10,000 grand prize! Reply to this email " +
		          "with your date of birth to claim your winnings!"),

		new Email("admin@bestbank.biz",
		          "Your account has been hacked! [URGENT]",

		          "Your Best Bank online account has been hacked! Please reply to this " +
		          "email with your old password to verify your identity and reclaim your account!"),

		new Email("Mom",
		          "Make Cash Fast",

		          "Hello friend,\n\n" +
		          "I would like to register you for this new website I found. With this website's " +
		          "help, I made over $2000 dollars in just five days! Please tell me your mother's " +
		          "maiden name and I will make you an account.\n\n" +
		          "Your friend,\n" +
		          "Mom"),

		new Email("helpdesk@towertech.net",
		          "Routine server maintenance",

		          "Hello, this is the Tower Tech information technology helpdesk. We are doing some " +
		          "routine server maintenance and need you to verify your employee account password. " +
		          "Please reply to this email with your current password to ensure our database is " +
		          "consistent. Have a nice day!"),

		new Email("johnsmith@intermail.net",
		          "I seek a reliable and honest benefactor",

		          "I seize this opportunity to extend my unalloyed compliments of the new season to " +
		          "you and your family hoping that this year will bring more joy, happiness, and " +
		          "prosperity into your household. I am the son of a wealthy family in my home " +
		          "country of Namibia, but I have recently had a bout of bad luck. I am being " +
		          "prevented from returning home to my family after a political visit abroad. I come " +
		          "to you seeking a small monetary favor to secure my passage home. Once I arrive in " +
		          "my home country, I will use my position and influence to ensure that your favor " +
		          "will be repaid tenfold. I plead you to respond with all due haste lest our " +
		          "communications be further impeded."),
		          
			new Email("ivdamke@greatdealsonline.ru",
			          "great software deal",
			          
			          "Find great software deal online! No one beats our deal! software deals that" +
			          "cant be beat!! 9 in 10 people cant find good software deal online! all great " +
			          "deals found here!"),
			          
			new Email("yourhealthstartshere@chreaksdugisk.ru",
			          "Your health starts here!",
			          
			          "With years to come, your life will only become better. Find all the prescription " +
			          "deals you need right here. No prescription needed, get the pills you need fast " +
			          "and cheap. Order now!"),
			          
			new Email("richardmoreau@nowdeals.co.uk",
			          "Children of the Cathedral",
			          
			          "One time offer! No time to wait! Reply now to gain access to the greatest things " +
			          "on earth! The energy is flowing! The numbers are increasing! Soon there will be " +
			          "everything, but only if you act now! The time has begun, the process cannot be " +
			          "stopped. Become a part of The Unity!")
	};
	
	void Start () {
		Email msg;
		GimmickResponseHandler grh = GetComponent<GimmickResponseHandler>();
		
		if(Random.Range(0, 2) == 1)
			grh.isGood = true;
		else
			grh.isGood = false;

		if(grh.isGood)
			msg = good [Random.Range (0, good.Length)];
		else
			msg = bad [Random.Range (0, bad.Length)];

		senderText.GetComponent<Text> ().text = "From: " + msg.sender;
		subjectText.GetComponent<Text> ().text = "Subject: " + msg.subject;
		bodyText.GetComponent<Text> ().text = msg.body;
	}
}
