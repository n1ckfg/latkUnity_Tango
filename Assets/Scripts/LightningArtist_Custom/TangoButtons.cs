using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangoButtons : MonoBehaviour {

	public LightningArtist lightningArtist;
	public ShowHideGeneric showHideGeneric;
	public BrushInputTango brushInputTango;

	float LABEL_START_X = 15.0f;
	float LABEL_START_Y = 15.0f;
	float LABEL_SIZE_X = 1920.0f;
	float LABEL_SIZE_Y = 35.0f;
	float LABEL_GAP_Y = 3.0f;
	float BUTTON_SIZE_X = 200f; //250.0f;
	float BUTTON_SIZE_Y = 90f; //130.0f;
	float BUTTON_GAP_X = 5.0f;
	//float CAMERA_BUTTON_OFFSET = BUTTON_SIZE_X + BUTTON_GAP_X;
	//float LABEL_OFFSET = LABEL_GAP_Y + LABEL_SIZE_Y;
	//float FPS_LABEL_START_Y = LABEL_START_Y + LABEL_OFFSET;
	//float EVENT_LABEL_START_Y = FPS_LABEL_START_Y + LABEL_OFFSET;
	//float POSE_LABEL_START_Y = EVENT_LABEL_START_Y + LABEL_OFFSET;
	//float DEPTH_LABLE_START_Y = POSE_LABEL_START_Y + LABEL_OFFSET;
	string FLOAT_FORMAT = "F3";
	string FONT_SIZE = "<size=25>";

	int menuCounter = 1;
	int menuCounterMax = 2;

	void Awake() {
		if (lightningArtist == null) lightningArtist = GetComponent<LightningArtist>();
		if (brushInputTango == null) brushInputTango = GetComponent<BrushInputTango>();
	}

	void OnGUI() {
		string isOn = "";

		if (menuCounter == 1) {
			// 1-1.
			Rect writeButton = new Rect(BUTTON_GAP_X, Screen.height - (1 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
			//isOn = lightningArtist.showOnionSkin ? "Off" : "On";
			if (GUI.Button(writeButton, FONT_SIZE + "Write" + "</size>")) {
				lightningArtist.armWriteFile = true;
			}

            // 1-2.
            Rect undoButton = new Rect(BUTTON_GAP_X, Screen.height - (2 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
			//isOn = lightningArtist.showOnionSkin ? "Off" : "On";
			if (GUI.Button(undoButton, FONT_SIZE + "Undo" + "</size>")) {
				lightningArtist.inputEraseLastStroke();
			}

            // 1-3.
            Rect colorButton = new Rect(BUTTON_GAP_X, Screen.height - (3 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
			isOn = showHideGeneric.target[0].activeSelf ? "Off" : "On";
			if (GUI.Button(colorButton, FONT_SIZE + "Palette " + isOn + "</size>")) {
				if (showHideGeneric.target[0].activeSelf) {
					showHideGeneric.hideColor();
				} else {
					showHideGeneric.showColor();
				}
			}

            // 1-4.
            Rect onionButton = new Rect(BUTTON_GAP_X, Screen.height - (4 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
			isOn = lightningArtist.showOnionSkin ? "Off" : "On";
			if (GUI.Button(onionButton, FONT_SIZE + "Onion Skin " + isOn + "</size>")) {
				lightningArtist.inputOnionSkin();
			}

            // 1-5.
            Rect rotoButton = new Rect(BUTTON_GAP_X, Screen.height - (5 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
			isOn = brushInputTango.rotoEnabled ? "ON" : "OFF";
			if (GUI.Button(rotoButton, FONT_SIZE + "Roto " + isOn + "</size>")) {
				brushInputTango.rotoEnabled = !brushInputTango.rotoEnabled;
			}

            // 1-6.
            Rect drawButton = new Rect(BUTTON_GAP_X, Screen.height - (6 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
			isOn = brushInputTango.drawEnabled ? "ON" : "OFF";
			if (GUI.Button(drawButton, FONT_SIZE + "Draw " + isOn + "</size>")) {
				brushInputTango.drawEnabled = !brushInputTango.drawEnabled;
			}

            // 1-7.
            Rect copyFrameButton = new Rect(BUTTON_GAP_X, Screen.height - (7 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
			//isOn = m_arCameraPostProcess.enabled ? "Off" : "On";
			if (GUI.Button(copyFrameButton, FONT_SIZE + "Copy Frame " + "</size>")) {
				lightningArtist.inputNewFrameAndCopy();
			}

            // 1-8.
            Rect newFrameButton = new Rect(BUTTON_GAP_X, Screen.height - (8 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
			//isOn = m_arCameraPostProcess.enabled ? "Off" : "On";
			if (GUI.Button(newFrameButton, FONT_SIZE + "New Frame " + "</size>")) {
				lightningArtist.inputNewFrame();
			}

            // 1-9.
            Rect playButton = new Rect(BUTTON_GAP_X, Screen.height - (9 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
			isOn = lightningArtist.isPlaying ? "Stop" : "Play";
			if (GUI.Button(playButton, FONT_SIZE + isOn + "</size>")) {
				lightningArtist.inputPlay();
			}

            // 1-10.
            Rect rewButton = new Rect(BUTTON_GAP_X, Screen.height - (10 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X/2f, BUTTON_SIZE_Y);
			//isOn = m_arCameraPostProcess.enabled ? "Off" : "On";
			if (GUI.Button(rewButton, FONT_SIZE + "<|" + "</size>")) {
				lightningArtist.inputFrameBack();
			}

			Rect ffButton = new Rect(BUTTON_GAP_X + (BUTTON_SIZE_X/2f), Screen.height - (10 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X/2f, BUTTON_SIZE_Y);
			//isOn = m_arCameraPostProcess.enabled ? "Off" : "On";
			if (GUI.Button(ffButton, FONT_SIZE + "|>" + "</size>")) {
				lightningArtist.inputFrameForward();
			}
		} else if (menuCounter == 2) {
            // 2-10.
            Rect layerChangeButton = new Rect(BUTTON_GAP_X, Screen.height - (10 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
            //isOn = lightningArtist.showOnionSkin ? "Off" : "On";
            if (GUI.Button(layerChangeButton, FONT_SIZE + "Next Layer" + "</size>")) {
                lightningArtist.inputNextLayer();
            }

            // 2-9.
            Rect newLayerButton = new Rect(BUTTON_GAP_X, Screen.height - (9 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
            //isOn = lightningArtist.showOnionSkin ? "Off" : "On";
            if (GUI.Button(newLayerButton, FONT_SIZE + "New Layer" + "</size>")) {
                lightningArtist.inputNewLayer();
            }

            // 2-1.
            Rect readButton = new Rect(BUTTON_GAP_X, Screen.height - (1 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
            //isOn = lightningArtist.showOnionSkin ? "Off" : "On";
            if (GUI.Button(readButton, FONT_SIZE + "Demo" + "</size>")) {
                lightningArtist.armReadFile = true;
            }
        }

        // 1-11.
        Rect menuButton = new Rect(BUTTON_GAP_X, Screen.height - (11 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
		//isOn = m_arCameraPostProcess.enabled ? "Off" : "On";
		if (GUI.Button(menuButton, FONT_SIZE + "MENU " + menuCounter + "</size>")) {
			menuCounter++;
			if (menuCounter > menuCounterMax) menuCounter = 1;
		}
	}

}
