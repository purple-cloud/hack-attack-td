using UnityEngine;

namespace Defenses {
	public class Controller : MonoBehaviour {
		private Firewall fw;
		private Antivirus av;
		private int reqPerSec;

		void Start() {
			// Stores the defense mechanisms
			fw = GameObject.Find("firewall").GetComponent<Firewall>();
			av = GameObject.Find("antivirus").GetComponent<Antivirus>();

			// Sets the requests per second for the computer and updates the firewall
			reqPerSec = 5;
			UpdateFirewall();
		}

		void Update() {
			// Creates a line between components
			LineRenderer lr = GameObject.Find("line").GetComponent<LineRenderer>();
			lr.startColor = new Color(255, 0, 0);
			lr.endColor = new Color(0, 0, 255);
			lr.startWidth = 1f;
			lr.endWidth = 1f;

			lr.SetPosition(0, GameObject.Find("firewall").transform.position);
			lr.SetPosition(1, GameObject.Find("antivirus").transform.position);

			switch (Input.inputString) {
				case "i":
					SendFileToAV("virus");
					break;
				case "o":
					SendFileToAV("picture");
					break;
				case ",":
					reqPerSec += 100;
					UpdateFirewall();
					break;
				case ".":
					reqPerSec -= 100;
					UpdateFirewall();
					break;
			}

		}

		// Filters in-/out-going connections though the firewall
		void UpdateFirewall() {
			Debug.Log("[" + ((fw.Receive(reqPerSec)) ? "Active" : "Deactivated") + "] Requests per sec: " + reqPerSec);
		}

		// Sends file to antivirus to scan it
		void SendFileToAV(string type) {
			bool detectedInfection = av.AnalyzeFile(type);
		}
	}
}
