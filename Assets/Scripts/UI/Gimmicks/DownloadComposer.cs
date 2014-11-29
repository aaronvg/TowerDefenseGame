using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DownloadComposer : MonoBehaviour {

	public GameObject fileText;
	public GameObject sourceText;

	private struct Download {
		public string file;
		public string source;

		public Download(string file, string source) {
			this.file = file;
			this.source = source;
		}
	}

	private static Download[] good = {
		new Download("RapidBrowserInstaller.exe",
		             "rapidbrowser.org/64bit/download"),

		new Download("MediaBoxClient.exe",
		             "mediabox.com/user/download"),

		new Download("HifiPlayerInstall_64bit.exe",
		             "hifiplayer.org/install"),

		new Download("BabylonFirewall.exe",
		             "babylon.com/download/secure"),

		new Download("Setup.exe",
		             "securevpn.org/about/download")
	};

	private static Download[] bad = {
		new Download("KasinoKing.exe",
		             "freegame.freehost.net/clickbait"),

		new Download("lolcats.png.exe",
		             "megadownload.com/FdsE56F3gdRGH4EPL436m"),

		new Download("towerdefense.jpg.exe",
		             "gogle.ru/fungamescreens"),

		new Download("23jumpstreet_bluray1080p.exe",
		             "buccaneercove.se/torrents/video"),

		new Download("deathgrips_themoneystore.exe",
		             "freemp3bay.net/d/deathgrips/discog")
	};

	void Start () {
		Download dl;
		
		if(GetComponent<GimmickResponseHandler> ().isGood)
			dl = good [Random.Range (0, good.Length)];
		else
			dl = bad [Random.Range (0, bad.Length)];
		
		fileText.GetComponent<Text> ().text = "FILE: " + dl.file;
		sourceText.GetComponent<Text> ().text = "SOURCE: " + dl.source;
	}
}
