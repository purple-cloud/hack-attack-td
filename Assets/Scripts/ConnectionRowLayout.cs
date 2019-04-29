using UnityEngine;
using UnityEngine.UI;
using static PathConnection;

/// <summary>
/// Represents references to placements of individual components.
/// </summary>
public class ConnectionRowLayout : MonoBehaviour {
	public Image comp1Image;
	public Image comp2Image;
	public Image directionImage;

	public void SetDirection(DirectionType direction) {
		switch (direction) {
			case DirectionType.INPUT:
				directionImage.sprite = Resources.Load("arrow_input", typeof(Sprite)) as Sprite;
				break;
			case DirectionType.OUTPUT:
				directionImage.sprite = Resources.Load("arrow_output", typeof(Sprite)) as Sprite;
				break;
		}
	}
}
