using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    #region SERIALIZED FIELDS

    [Header("Tutorial Panel")]

    [SerializeField]
    private GameObject tutorialPanel;

    [SerializeField]
    private Text tutorialTxt;

    [SerializeField]
    private Button nextBtn;

    [SerializeField]
    private Button prevBtn;

    [Header("Level Information Panel")]

    [SerializeField]
    private Button tutorialInfoBtn;

    [Header("Restricted Access")]

    [SerializeField]
    private GameObject modifyPathBtn;

    [SerializeField]
    private GameObject addPathBtn;

    [SerializeField]
    private GameObject firewallSlot;

    [SerializeField]
    private GameObject backupSlot;

    [SerializeField]
    private GameObject predefinedLocationForFirstFirewall;

    [SerializeField]
    private GameObject predefinedLocationForSecondFirewall;

    [Header("Components")]

    [SerializeField]
    private GameObject earth;

    [SerializeField]
    private GameObject computer;

    [SerializeField]
    private GameObject document;

    #endregion

    #region VARIABLES

    private bool readyToMoveOn;

    private int state;

    private bool firewallSlotState = false;

    private bool backupSlotState = false;

    private bool computerState = false;

    private bool internetState = false;

    private bool inbetweenStructPlacement = false;

    #endregion

    private void Start() {
        UserBehaviourProfile.Instance.tutorialLvl = true;
        Defenses.CompController.Instance.canPlaceTutorialStruct = false;

        this.readyToMoveOn = false;
        this.state = 0;
        this.nextBtn.onClick.AddListener(NextStep);
        this.prevBtn.onClick.AddListener(PrevStep);
        this.tutorialInfoBtn.onClick.AddListener(TutorialInit);
        GameManager.Instance.SetCurrency(100000);

        // Disable all access to restricted buttons
        this.modifyPathBtn.SetActive(false);
        this.addPathBtn.SetActive(false);
        this.firewallSlot.SetActive(false);
        this.backupSlot.SetActive(false);

        // Init restricted access event trigger
        this.firewallSlot.GetComponent<Button>().onClick.AddListener(FireFirewallEventTrigger);
        this.backupSlot.GetComponent<Button>().onClick.AddListener(FireBackupEventTrigger);
        this.computer.GetComponent<Button>().onClick.AddListener(FireComputerEventTrigger);
        this.earth.GetComponent<Button>().onClick.AddListener(FireInternetEventTrigger);
        this.predefinedLocationForFirstFirewall.GetComponent<Button>().onClick.AddListener(PlaceFirewallComponentOnSpecifiedLocation);
        this.predefinedLocationForSecondFirewall.GetComponent<Button>().onClick.AddListener(PlaceFirewallComponentOnSpecifiedLocation);

        // Catches component placed event
        EventManager.onComponentPlaced += PlaceFirewallComponentOnSpecifiedLocation;

        // Disable component clickable
        ChangeComponentClickableState(this.computer, false);
        ChangeComponentClickableState(this.earth, false);
        ChangeComponentClickableState(this.document, false);
    }

    private void StateMachine() {
        switch (this.state) {
            case 1:
                FirstTask();
                break;

            case 2:
                NewSecondTask();
                break;

            case 3:
                NewThirdTask();
                break;

            case 4:
                NewFourthTask();
                break;

            case 5:
                NewFifthTask();
                break;

            case 6:
                NewSixthTask();
                break;

            case 7:
                NewSeventhTask();
                break;

            case 8:
                NewEightTask();
                break;

            case 9:
                NinthTask();
                break;

            case 10:
                TenthTask();
                break;

            case 11:
                EleventhTask();
                break;

            case 12:
                TwelfthTask();
                break;

            default:
                Debug.Log("Couldn't find next or previous stage");
                break;
        }
    }

    private void FireFirewallEventTrigger() {
        if (this.firewallSlotState) {
            NextStep();
        }
    }

    private void FireComputerEventTrigger() {
        if (this.computerState && this.inbetweenStructPlacement == false) {
            NextStep();
        } else if (this.computerState && this.inbetweenStructPlacement) {
            // Check what mouse btn is pressed
            if (Input.GetMouseButtonUp(1)) {
                NextStep();
            }
        }
    }

    private void FireInternetEventTrigger() {
        if (this.internetState && this.inbetweenStructPlacement == false) {
            NextStep();
        } else if (this.internetState && this.inbetweenStructPlacement) {
            // Check what mouse btn is pressed
            if (Input.GetMouseButtonDown(1)) {
                Debug.Log("Right Mouse Click..");
                NextStep();
            }
        }
    }

    private void FireBackupEventTrigger() {
        if (this.backupSlotState) {
            NextStep();
        }
    }

    public void PlaceFirewallComponentOnSpecifiedLocation() {
        Defenses.CompController.Instance.canPlaceTutorialStruct = true;
        NextStep();
    }

    private void TutorialInit() {
        ShowTutorialPanel(true);
        this.tutorialTxt.text = "For starters we can see we have a computer, a document and the internet. All these are connected together via inputs and outputs";
    }

    private void FirstTask() {
        ChangeBtnState(this.nextBtn);
        this.firewallSlotState = true;
        // TODO Add Stop here and wait for firewall panel to be clicked
        this.firewallSlot.SetActive(true);
        HighlightComponent(this.firewallSlot, true);
        this.tutorialTxt.text = "To extend your system with another component, (this is just an example) click the highlighted FirewallSlot in your actionbar";
    }

    private void NewSecondTask() {
        this.firewallSlotState = false;
        this.computerState = true;
        ChangeComponentClickableState(this.computer, true);
        HighlightComponent(this.firewallSlot, false);
        // Highlight computer component
        HighlightComponent(this.computer, true);
        this.tutorialTxt.text = "Now LEFT CLICK on the hightlighted computer";
    }

    private void NewThirdTask() {
        ChangeComponentClickableState(this.computer, false);
        this.computerState = false;
        HighlightComponent(this.computer, false);
        HighlightComponent(this.predefinedLocationForFirstFirewall, true);
        this.predefinedLocationForFirstFirewall.SetActive(true);
        this.tutorialTxt.text = "Now LEFT CLICK the highlighted area in the canvas";
    }

    private void NewFourthTask() {
        Defenses.CompController.Instance.canPlaceTutorialStruct = false;
        HighlightComponent(this.predefinedLocationForFirstFirewall, false);
        this.predefinedLocationForFirstFirewall.SetActive(false);
        this.tutorialTxt.text = "WELL DONE! Now you can try and place a firewall between to existing components. First LEFT CLICK on the firewall slot again";
        this.firewallSlotState = true;
        HighlightComponent(this.firewallSlot, true);
        this.inbetweenStructPlacement = true;
    }

    private void NewFifthTask() {
        this.firewallSlotState = false;
        HighlightComponent(this.firewallSlot, false);
        this.tutorialTxt.text = "Now comes the really important task. To place a defense between to existing components you have to use the RIGHT CLICK mouse button. First RIGHT CLICK on the internet.";
        this.internetState = true;
        ChangeComponentClickableState(this.earth, true);
        HighlightComponent(this.earth, true);

		// Waits for component being right clicked before proceeding
		// TODO Check what component has been pressed before doing next step
		EventManager.onRightClickComponent += NextStep;
	}

    private void NewSixthTask() {
        ChangeComponentClickableState(this.earth, false);
        HighlightComponent(this.earth, false);
        this.internetState = false;
        this.tutorialTxt.text = "Now RIGHT CLICK the computer";
        ChangeComponentClickableState(this.computer, true);
        HighlightComponent(this.computer, true);
    }

    private void NewSeventhTask() {
		// After the two previous tasks has been done, unsubscribe
		EventManager.onRightClickComponent -= NextStep;
		this.tutorialTxt.text = "Now LEFT CLICK the highlighted area in the canvas";
    }

    private void NewEightTask() {
        this.inbetweenStructPlacement = false;
    }



    private void SecondTask() {
        this.tutorialTxt.text = "You might want to place a firewall between your computer and the internet to protect yourself. To do this, choose firewall from actionbar. RIGHT CLICK computer and then RIGHT CLICK internet. After this you can then choose where you want to place the firewall in the map. Press Next to continue.";
    }

    private void ThirdTask() {
        HighlightComponent(this.firewallSlot, false);
        this.modifyPathBtn.SetActive(true);
        HighlightComponent(this.modifyPathBtn, true);
        this.tutorialTxt.text = "Now we have two connections from the internet to our computer. This isn't really optimal. To remove our direct connection from the internet to our computer first click the highlighted button in the top right corner...";
        this.readyToMoveOn = true;
        ChangeBtnState(this.nextBtn);
    }

    private void FourthTask() {
        HighlightComponent(this.modifyPathBtn, false);
        ChangeBtnState(this.nextBtn);
        this.readyToMoveOn = false;
        this.tutorialTxt.text = "Now click on the highlighted internet and find the connection that goes from the internet to the computer and delete it. This can be done by pressing the x symbol to the right of the connection (You can cancel modification anytime by pressing the ESC key). Press Next to continue.";
    }

    private void FifthTask() {
        this.tutorialTxt.text = "Our system is now a lot more secure. Our firewall can be configured by clicking it and choosing to enable traffic through our different ports or disable the traffic. Press Next to continue.";
    }

    private void SixthTask() {
        this.addPathBtn.SetActive(true);
        this.tutorialTxt.text = "Another thing which is nice to be aware of is that you can create your own connections between existing components. This can be done by clicking the highlighed button in the top right corner...";
        HighlightComponent(this.addPathBtn, true);
        this.readyToMoveOn = true;
        ChangeBtnState(this.nextBtn);
    }

    private void SeventhTask() {
        HighlightComponent(this.addPathBtn, false);
        ChangeBtnState(this.nextBtn);
        this.readyToMoveOn = false;
        this.tutorialTxt.text = "Now you can click on the two components you want to connect. (If you already have deleted the connection from the internet to the computer you can establish a new connection there. After that you can delete it again) (You can cancel path connecting anytime by pressing the ESC key). Press Next to continue.";
    }

    private void EightTask() {
        this.tutorialTxt.text = "Now you have come so far it's time to take backup. You can take backup of each individual component in the system. By taking backup you store all its states and functionality. Press Next to start.";
    }

    private void NinthTask() {
        this.backupSlot.SetActive(true);
        HighlightComponent(this.backupSlot, true);
        this.tutorialTxt.text = "To take backup, first click on the backup slot in the actionbar. Then select 'Backup Component', and click the component you would like to take backup of (You can cancel backup by pressing the ESC key). Press Next to continue.";
    }

    private void TenthTask() {
        HighlightComponent(this.backupSlot, false);
        this.tutorialTxt.text = "If you took a backup you will see it in a panel that pops up. This is your backup management panel. Here you can store up to 6 individual backups. If you choose to add one more after having 6, the oldest backup will be deleted. Choose wisely which components are important. Press Next to continue.";
    }

    private void EleventhTask() {
        this.tutorialTxt.text = "To restore a backup, simply click the backup you want and then select where to place it. You can only replace components of the same type as the backup itself. Press Next to continue.";
    }

    private void TwelfthTask() {
        this.tutorialTxt.text = "You are now ready to start defending yourself against real attacks. You can access the settings menu in the top right corner. Click Next level when you are ready. We recommend that you try and click around in the interface before jumping onwards.";
        ChangeBtnState(this.nextBtn);
        Settings.Instance.ChangeConcedeAndNextLevel();
    }

    private void NextStep() {
        if (this.state <= 12) {
            Debug.Log("Next step...");
            this.state++;
            StateMachine();
        }
    }

    public void NextStepOnAction() {
        if (this.readyToMoveOn) {
            Debug.Log("Next step...");
            this.state++;
            StateMachine();
        }
    }

    private void PrevStep() {
        if (this.state > 0) {
            Debug.Log("Previous step...");
            this.state--;
            StateMachine();
        }
    }

    private void HighlightComponent(GameObject objectToHighlight, bool state) {
        GameObject borderGO;
        if (state) {
            borderGO = Instantiate(Defenses.CompController.Instance.highlightBorder as GameObject);
            borderGO.name = "HighlightBorder";
            borderGO.GetComponent<Image>().color = Color.green;
            borderGO.transform.position = objectToHighlight.transform.position;
            borderGO.transform.SetParent(objectToHighlight.transform);
            if (objectToHighlight == this.firewallSlot || objectToHighlight == this.backupSlot) {
                borderGO.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            } else if (objectToHighlight == this.computer || objectToHighlight == this.earth || objectToHighlight == this.document) {
                borderGO.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        } else {
            if (objectToHighlight.transform.Find("HighlightBorder") != null) {
                Destroy(objectToHighlight.transform.Find("HighlightBorder").gameObject);
            }
        }
    }

    private void ChangeComponentClickableState(GameObject obj, bool clickableState) {
        if (clickableState) {
            obj.GetComponent<Button>().enabled = true;
        } else {
            obj.GetComponent<Button>().enabled = false;
        }
    }

    private void ChangeBtnState(Button btnToChange) {
        btnToChange.enabled = !btnToChange.enabled;
        if (btnToChange.enabled) {
            btnToChange.GetComponent<Image>().color = Color.green;
        } else {
            btnToChange.GetComponent<Image>().color = Color.grey;
        }
    }

    private void ShowTutorialPanel(bool state) {
        this.tutorialPanel.SetActive(state);
    }

}