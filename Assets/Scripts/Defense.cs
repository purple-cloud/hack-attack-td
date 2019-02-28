using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defenses {
	public abstract class Defense : MonoBehaviour {
		// Status of the protection
		protected bool active = true;
		protected System.Random rand = new System.Random();
		protected Defense input;
		protected Defense output;

		// Different interraction
		public abstract void Upgrade();
		public abstract void Repair();
		public abstract void Destroy();
	}
}