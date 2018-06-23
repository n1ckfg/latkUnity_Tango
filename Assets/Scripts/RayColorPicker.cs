using System;
using System.Collections;
using UnityEngine;

public class RayColorPicker : MonoBehaviour {

	public GameObject hitObj;
	public WebcamPhoto webcamPhoto;
    public bool clicked = false;
    public bool hitSuccess = false;
	public Vector3 dragLocation = Vector3.zero;
	public LineRenderer lineRenderer;

	private bool debugMode = false;
	private Renderer hitRen;

	void Awake() {
		hitRen = hitObj.GetComponent<Renderer>();
	}

	void Start() {
		if (lineRenderer != null) {
			debugMode = true;
			lineRenderer.enabled = false;
		} else {
			debugMode = false;
		}
	}

    void Update() {
		if (Input.GetMouseButtonUp(0)) {
			lineRenderer.enabled = false;
		}

        if (!Input.GetMouseButtonDown(0)) {
            clicked = false;
            return;
        } else {
            clicked = true;
			lineRenderer.enabled = true;
        }

        Camera mainCamera = FindCamera();

        // We need to actually hit an object
        RaycastHit hit = new RaycastHit();
		Vector3 origin = mainCamera.ScreenPointToRay(Input.mousePosition).origin;
		Vector3 dir = mainCamera.ScreenPointToRay(Input.mousePosition).direction;
		//Vector3[] points = { origin, origin + (dir * 20f) };
		//lineRenderer.SetPositions(points);

		if (Physics.Raycast(origin, dir, out hit, 100, Physics.DefaultRaycastLayers)) {
			hitObj.transform.position = hit.point;

			Vector3[] points = { origin, hit.point };
			lineRenderer.SetPositions(points);

			//Texture tex = hit.transform.GetComponent<Renderer>().material.mainTexture;
			Vector2 pixelUV = hit.textureCoord2;
			webcamPhoto.updatePhotoTex();
			pixelUV.x *= webcamPhoto.photo.width;
			pixelUV.y *= webcamPhoto.photo.height;
			Color c = webcamPhoto.photo.GetPixel((int)pixelUV.x, (int)pixelUV.y);
			Debug.Log(c);
			hitRen.material.SetColor("_Color", c);
			lineRenderer.material.SetColor("_Color", c);
		}

		/*
		if (!Physics.Raycast(origin, dir, out hit, 100, Physics.DefaultRaycastLayers)) {
            hitSuccess = false;
            return;
        }
        
        // We need to hit a rigidbody that is not kinematic
        if (!hit.rigidbody || hit.rigidbody.isKinematic) {
            hitSuccess = false;
            return;
        } else {
            hitSuccess = true;
        }

		StartCoroutine(DragObject(hit.distance, points));
		*/
    }

	private IEnumerator DragObject(float distance, Vector3[] points) {
        Camera mainCamera = FindCamera();
        while (Input.GetMouseButton(0)) {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            dragLocation = ray.GetPoint(distance);
            yield return null;
        }
    }

    private Camera FindCamera() {
        if (GetComponent<Camera>()) {
            return GetComponent<Camera>();
        }
        return Camera.main;
    }

}

