using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeScreenShot : MonoBehaviour {

    public bool done;
    private int resWidth = 200, resHeight = 200;

    public Camera _camera;
    private Texture2D _screenShot;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!done && Input.GetKeyDown(KeyCode.P))
        {
            

            done = true;
            GameObject.Find("SpriteObject").GetComponent<Image>().sprite = ScreenShotAsSprite();


            _camera.gameObject.SetActive(false);
        }
	}

    public Sprite ScreenShotAsSprite()
    {
        _camera.gameObject.SetActive(true);

        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        _camera.targetTexture = rt;
        _screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        _camera.Render();
        RenderTexture.active = rt;
        _screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);

        _screenShot.Apply(); //Add this?

        _camera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        string filename = "ScreenShot";

        //byte[] bytes = _screenShot.EncodeToPNG();
        //System.IO.File.WriteAllBytes(filename, bytes);

        Debug.Log(string.Format("Took screenshot to: {0}", filename));

        Sprite tempSprite = Sprite.Create(_screenShot, new Rect(0, 0, resWidth, resHeight), new Vector2(0, 0));
        return tempSprite;
    }
}
