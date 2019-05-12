using UnityEngine;
using UnityEngine.UI;
using static ConnectionManager;

/// <summary>
/// Represents references to placements of individual components and allows them to be set easily.
/// This class is can only be used for the 'ConnectionLinkRow' 
/// </summary>
public class ConnectionRowLayout : MonoBehaviour {
	// Components on the connection
	private Component comp1;
	private Component comp2;

	[SerializeField]
	private Image comp1Image;		// Left image of the prefab

	[SerializeField]
	private Image comp2Image;		// Right image of the prefab

	public Image directionImage;	// Image in the center of the prefab

	/// <summary>
	/// Button that will trigger event to unlink this connection.
	/// </summary>
	public Button removeBtn;		// Button at the outer right in the prefab

	/// <summary>
	/// The direction of the connection. 
	/// Type input:		comp1 --> comp2
	/// Type output:	comp2 --> comp1
	/// </summary>
	private DirectionType direction;

	public void Start() {
		// Sets the event for remove button
		removeBtn.onClick.AddListener(() => RemoveConnection());
	}

	/// <summary>
	/// Selects an arrow image based on direction of the connection and shows it.
	/// </summary>
	/// <param name="direction">Direction of the connection</param>
	private void SetDirection(DirectionType direction) {
		this.direction = direction;

		switch (direction) {
			case DirectionType.INPUT:
				// Arrow points from left to right
				directionImage.sprite = Resources.Load("arrow_input", typeof(Sprite)) as Sprite;
				break;
			case DirectionType.OUTPUT:
				// Arrow points from right to left
				directionImage.sprite = Resources.Load("arrow_output", typeof(Sprite)) as Sprite;
				break;
		}
	}

	/// <summary>
	/// Tells the ConnectionManager to remove the connection link in respect of the direction.
	/// </summary>
	/// <seealso cref="ConnectionManager.UnlinkConnection(Component, Component)"/>
	private void RemoveConnection() {
		if (direction == DirectionType.INPUT) {
			// Connection direction: comp1 ---> comp2
			ConnectionManager.Instance.UnlinkConnection(comp1, comp2);
		} else {
			// Connection direction: comp2 ---> comp1
			ConnectionManager.Instance.UnlinkConnection(comp2, comp1);
		}
	}

	/// <summary>
	/// Stores the components and uses their images to show on this gameobject.
	/// This is used by ConnectionManager mainly.
	/// </summary>
	/// <param name="comp1">First component</param>
	/// <param name="direction">Direction of the connection from the first component to second</param>
	/// <param name="comp2">Second component</param>
	/// <seealso cref="ConnectionManager.AddRowToPanel(Component, DirectionType, Component)"/>
	public void Assign(Component comp1, DirectionType direction, Component comp2) {
		this.comp1 = comp1;
		comp1Image.sprite = comp1.GetCanvasImage().sprite;

		// Set direction of the connection
		SetDirection(direction);

		this.comp2 = comp2;
		comp2Image.sprite = comp2.GetCanvasImage().sprite;
	}
}
