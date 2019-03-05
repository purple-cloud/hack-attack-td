using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defenses {
	/// <summary>
	/// This are the defensive structures in a system such as firewall.
	/// </summary>
	public abstract class Defense : BaseStructure {
		protected bool active = true;							// Status of the protection
		protected System.Random rand = new System.Random();		// Random number generator for probability

		public abstract void Upgrade();
		public abstract void Repair();
		public abstract void Destroy();
	}
}