using System;
using UnityEngine;

namespace Defenses {
	public class Antivirus : Defense {
		private double detectionRate;
		private double falsePositiveRate;

		private void Start() {
			detectionRate = 0.8;
			falsePositiveRate = 0.01;
			active = true; // From parent
		}

		public bool AnalyzeFile(string file) {
			bool detectedInfection = false;
			double p = rand.NextDouble(); // From parent

			if (file == "virus") {
				if ((detectionRate - falsePositiveRate) >= p) {
					Debug.Log("Virus detected.");
					detectedInfection = true;
				} else {
					Debug.Log("Computer infected, virus undetected.");
				}
			} else {
				if (falsePositiveRate > p) {
					Debug.Log("False positive detected.");
					detectedInfection = true;
				} else {
					Debug.Log("File is OK.");
				}
			}

			return detectedInfection;
		}

		public override void Upgrade() {
			throw new NotImplementedException();
		}

		public override void Repair() {
			throw new NotImplementedException();
		}

		public override void Destroy() {
			throw new NotImplementedException();
		}
	}
}

