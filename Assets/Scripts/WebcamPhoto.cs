using UnityEngine;
using System.Collections;
using System.IO;

public class WebcamPhoto : MonoBehaviour {

	public Renderer ren;
	public bool saveToDisk = false;
	public string filePath = "";

	[HideInInspector] public WebCamTexture webCamTexture;
	[HideInInspector] public Texture2D photo;
	[HideInInspector] public bool photoTaken = false;

	private int counter = 1;

	void Awake() {
		if (ren == null) {
			ren = GetComponent<Renderer>();
		}
	}

	void Start() {
		webCamTexture = new WebCamTexture();

		if (ren != null) {
			ren.material.mainTexture = webCamTexture;
			webCamTexture.Play();
		}
	}

	void Update() {
		photoTaken = false;

		//if (Input.GetKeyDown(KeyCode.Space) || Cardboard.SDK.Triggered) {
		if (Input.GetKeyDown(KeyCode.Space)) {
			takePhoto();
		}
	}

	public void updatePhotoTex() {
		photo = new Texture2D(webCamTexture.width, webCamTexture.height);
		photo.SetPixels(webCamTexture.GetPixels());
		photo.Apply();
	}

	public void takePhoto() {
		photoTaken = true;

		updatePhotoTex();

		if (saveToDisk) {
			// Encode to a PNG
			byte[] bytes = photo.EncodeToPNG();

			// Write out the PNG.
			File.WriteAllBytes(filePath + "photo_" + counter + ".png", bytes);
			counter++;
		}
	}

}