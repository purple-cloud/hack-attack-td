using UnityEngine;
using UnityEngine.UI;
using static PathConnectionManager;

/// <summary>
/// Represents references to placements of individual components.
/// </summary>
public class ConnectionRowLayout : MonoBehaviour {
	private Component comp1;
	private Component comp2;

	[SerializeField]
	private Image comp1Image;

	[SerializeField]
	private Image comp2Image;

	public Image directionImage;
	public Button removeBtn;
	private DirectionType direction;

	//TODO Remove button needs a reference to connection path in order to remove the path
	public void Start() {
		removeBtn.onClick.AddListener(() => RemovePath());
	}

	private void SetDirection(DirectionType direction) {
		this.direction = direction;
		switch (direction) {
			case DirectionType.INPUT:
				directionImage.sprite = Resources.Load("arrow_input", typeof(Sprite)) as Sprite;
				break;
			case DirectionType.OUTPUT:
				directionImage.sprite = Resources.Load("arrow_output", typeof(Sprite)) as Sprite;
				break;
		}
	}

	private void RemovePath() {
		if (direction == DirectionType.INPUT) {
			PathConnectionManager.Instance.UnlinkPath(comp1, comp2);
		} else {
			PathConnectionManager.Instance.UnlinkPath(comp2, comp1);
		}
	}

	public void Assign(Component comp1, DirectionType direction, Component comp2) {
		this.comp1 = comp1;
		comp1Image.sprite = comp1.GetCanvasImage().sprite;

		SetDirection(direction);

		this.comp2 = comp2;
		comp2Image.sprite = comp2.GetCanvasImage().sprite;
	}
}
