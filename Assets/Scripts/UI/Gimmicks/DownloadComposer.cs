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
		new Download("FILE: RapidBrowserInstaller.exe",
		             "SOURCE: rapidbrowser.org/64bit/download"),

		new Download("FILE: MediaBoxClient.exe",
		             "SOURCE: mediabox.com/public/download")
	};

	private static Download[] bad = {
		new Download("FILE: KasinoKing.exe",
		             "SOURCE: freegame.freehost.net/free/games"),

		new Download("FILE: lolcats.png.exe",
		             "SOURCE: megadownload.com/FdsE56F3gdRGH4EPL436m"),

		new Download("FILE: towerdefense.jpg.exe",
		             "SOURCE: gogle.ru/fungames")
	};

	void Start () {
		Download dl;
		
		if(GetComponent<GimmickResponseHandler> ().isGood)
			dl = good [Random.Range (0, good.Length)];
		else
			dl = bad [Random.Range (0, bad.Length)];
		
		fileText.GetComponent<Text> ().text = dl.file;
		sourceText.GetComponent<Text> ().text = dl.source;
	}
}
