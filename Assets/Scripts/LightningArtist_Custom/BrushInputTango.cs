using UnityEngine;
using System.Collections;

public class BrushInputTango : MonoBehaviour {

	public LightningArtist lightningArtist;
	public TangoButtons tangoButtons;

	public enum DrawMode { FREE, FIXED, ALL_ROTO }
	public DrawMode drawMode = DrawMode.FREE;
	public GameObject depthTarget;
	public bool rotoEnabled = true;
	public bool drawEnabled = true;

	[HideInInspector] public bool touchActive = false;
	[HideInInspector] public bool touchDown = false;
	[HideInInspector] public bool touchUp = false;

	private Vector3 origTargetPos = Vector3.zero;

	/*
	private bool blockStickHMax = false;
	private bool blockStickHMin = false;
	private bool blockStickVMax = false;
	private bool blockStickVMin = false;
	private float stickThreshold = 0.9f;
	*/

	void Awake() {
		if (lightningArtist == null) lightningArtist = GetComponent<LightningArtist>();
	}

	void Start() {
		if (drawMode == DrawMode.FIXED)	lightningArtist.target.transform.SetParent(Camera.main.transform, true);
		origTargetPos = lightningArtist.target.transform.position;
	}

	void Update() {
		touchDown = false;
		touchUp = false;

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && GUIUtility.hotControl == 0) { 
			touchActive = true;
			touchDown = true;
		} else if (Input.touchCount < 1 || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) {
			touchActive = false;
			touchUp = true;
		}

		if (touchActive && drawEnabled) {
			Vector3 p = lightningArtist.target.transform.position;

			if (drawMode == DrawMode.FREE) {
                // snap z to camera viewpoint, not real depth
                if (rotoEnabled) {
                    p = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, depthTarget.transform.position.z));
                } else {
                    p = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, origTargetPos.z));
                }
            } else if (drawMode == DrawMode.FIXED) {
				// z isn't changed
				//p = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, p.z));
			} else if (drawMode == DrawMode.ALL_ROTO) {
				// xyz snapped
				p = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, depthTarget.transform.position.z));
				p.z = depthTarget.transform.position.z;
			}
			lightningArtist.target.transform.position = p;
		}

		lightningArtist.clicked = touchActive;

		// ~ ~ ~ ~ ~ gamepad ~ ~ ~ ~ ~ ~
		/*
		lightningArtist.clicked = Input.GetButton("Fire2");

		if (Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.P)) {
			lightningArtist.inputPlay();
		}

		if (Input.GetButtonDown("Jump")) {
			lightningArtist.inputFrameBack();
		}

		if (Input.GetButtonDown("Fire1")) {
			lightningArtist.inputFrameForward();
		}

		if (!blockStickHMax && Input.GetAxis("Horizontal") >= stickThreshold) {
			blockStickHMax = true;
			if (lightningArtist.layerList[lightningArtist.currentLayer].frameList.Count > 0) lightningArtist.inputShowFrames();
		} else if (blockStickHMax && Input.GetAxis("Horizontal") < stickThreshold) {
			blockStickHMax = false;
		}

		if (!blockStickHMin && Input.GetAxis("Horizontal") <= -stickThreshold) {
			blockStickHMin = true;
			if (lightningArtist.layerList[lightningArtist.currentLayer].frameList.Count > 0) lightningArtist.inputHideFrames();
		} else if (blockStickHMin && Input.GetAxis("Horizontal") > -stickThreshold) {
			blockStickHMin = false;
		}
		*/

		// ~ ~ ~ ~ ~ ~ ~

	}
		
}
