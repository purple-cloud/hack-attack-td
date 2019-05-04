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

    #endregion

    #region VARIABLES

    private bool readyToMoveOn;

    private int state;

    #endregion

    private void Start() {
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
    }

    private void StateMachine() {
        switch (state) {
            case 1:
                FirstTask();
                break;

            case 2:
                SecondTask();
                break;

            case 3:
                ThirdTask();
                break;

            case 4:
                FourthTask();
                break;

            case 5:
                FifthTask();
                break;

            case 6:
                SixthTask();
                break;

            case 7:
                SeventhTask();
                break;

            case 8:
                EightTask();
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

    private void TutorialInit() {
        ShowTutorialPanel(true);
        this.tutorialTxt.text = "This is your system. \n\nAt the moment, it consists of a computer with a connection to the Internet, and a document. ";
    }

    private void FirstTask() {
        this.firewallSlot.SetActive(true);
        HighlightComponent(this.firewallSlot, true);
        this.tutorialTxt.text = "Your system is exposed to threats from the Internet. \n\nYou need defenses. \n\nThey can be added from the action bar. ";
    }

    private void SecondTask() {
        this.tutorialTxt.text = "Add a firewall. \n\n 1.LEFT CLICK the firewall in the action bar \n 2.RIGHT CLICK the computer \n 3.RIGHT CLICK the Internet (the earth)\n 4.LEFT CLICK to add the firewall anywhere you’d like\nRemember, you can cancel the addition anytime by pressing ESC!";
    }

    private void ThirdTask() {
        HighlightComponent(this.firewallSlot, false);
        this.modifyPathBtn.SetActive(true);
        HighlightComponent(this.modifyPathBtn, true);
        this.tutorialTxt.text = "Now there are two connections from the Internet to your computer. This is not optimal. \n\nTo remove the direct connection from the Internet to the computer, click the highlighted button in the top right corner, and delete the unwanted connection by pressing X. ";
        this.readyToMoveOn = true;
        ChangeBtnState(this.nextBtn);
    }
    //slett fourth task! 
    private void FourthTask() {
        HighlightComponent(this.modifyPathBtn, false);
        ChangeBtnState(this.nextBtn);
        this.readyToMoveOn = false;
        this.tutorialTxt.text = "Now click on the highlighted internet and find the connection that goes from the internet to the computer and delete it. This can be done by pressing the x symbol to the right of the connection (You can cancel modification anytime by pressing the ESC key). Press Next to continue.";
    }

    private void FifthTask() {
        this.tutorialTxt.text = "The system is now more secure. \n\nThe firewall can be configured trough the firewall panel, that appears when you select the firewall.Here you can choose to enable or disable traffic.\n\nYou can inspect the traffic from the panel. ";
    }

    private void SixthTask() {
        this.addPathBtn.SetActive(true);
        this.tutorialTxt.text = "You can create your own connections between existing components by clicking the highlighted button in the top right corner. ";
        HighlightComponent(this.addPathBtn, true);
        this.readyToMoveOn = true;
        ChangeBtnState(this.nextBtn);
    }

    private void SeventhTask() {
        HighlightComponent(this.addPathBtn, false);
        ChangeBtnState(this.nextBtn);
        this.readyToMoveOn = false;
        this.tutorialTxt.text = "Click the two components you want to connect. \n\nIf you already have deleted the connection from the internet to the computer you can establish a new connection there. After that you can delete it again\n\nYou can cancel path connecting anytime by pressing the ESC";
    }

    private void EightTask() {
        this.tutorialTxt.text = "It’s time to take a backup. \n\nYou can take a backup of each individual component in the system. \n\nBy taking backup of a component, you store the state and the functionality. ";
    }

    private void NinthTask() {
        this.backupSlot.SetActive(true);
        HighlightComponent(this.backupSlot, true);
        this.tutorialTxt.text = "To take a backup, first click backup on the action bar. \n\nThen select “Backup component” and click the component you would like to backup. \n\nThe backup can be cancelled by pressing the ESC key. ";
    }

    private void TenthTask() {
        HighlightComponent(this.backupSlot, false);
        this.tutorialTxt.text = "This is your backup management panel. \n\nYou can store up to 6 individual backups. If you add a 7th, the oldest backup will be deleted. \n\nChoose wisely. ";
    }

    private void EleventhTask() {
        this.tutorialTxt.text = "To restore a backup, click the backup you want and select where to place it. \n\nYou can only replace components of the same type as the backup itself.";
    }

    private void TwelfthTask() {
        this.tutorialTxt.text = "You are now ready to start defending yourself against real attacks! \n\nYou can access the settings menu in the top right corner. \n\nClick Next level when you are ready. \n\nWe recommend that you try and click around in the interface before jumping onwards.";
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
            }
        } else {
            if (objectToHighlight.transform.Find("HighlightBorder") != null) {
                Destroy(objectToHighlight.transform.Find("HighlightBorder").gameObject);
            }
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