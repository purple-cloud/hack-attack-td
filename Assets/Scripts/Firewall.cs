namespace Defenses {
	public class Firewall : Defense {
		private int maxReqPerSec;

		private void Start() {
			// Records total requests per second sent from/to the computer
			this.maxReqPerSec = 1000;
			active = true;
		}

		public bool Receive(int reqPerSec) {
			if (active) {
				if (reqPerSec > this.maxReqPerSec) {
					active = false;
				}
			}
			return active;
		}

		public override void Upgrade() {
			throw new System.NotImplementedException();
		}

		public override void Repair() {
			throw new System.NotImplementedException();
		}

		public override void Destroy() {
			throw new System.NotImplementedException();
		}
	}
}
