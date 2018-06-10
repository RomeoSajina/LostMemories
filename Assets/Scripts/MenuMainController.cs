using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMainController : MonoBehaviour {

	public Camera mainCamera;

	private Animator am;

    public GameObject[] levels;

    void Start() {
		am = mainCamera.GetComponent<Animator>();

        for(int i=0; i<levels.Length; i++) {
            
            if (GameManager.instance.ImageExsist(i + 1)) {

                var bytes = GameManager.instance.ReadImage(i + 1);
                var tex = new Texture2D(1, 1);
                tex.LoadImage(bytes);

                Material material = new Material(Shader.Find("Diffuse"));

                material.mainTexture = tex;

                levels[i].GetComponent<Renderer>().material = material;                
            }

            /*
            Texture2D texture = Resources.Load("/SavedScreen") as Texture2D;

            Material material = new Material(Shader.Find("Diffuse"));

            material.mainTexture = texture;
            
            levels[i].GetComponent<Renderer>().material = material;
            */
        }
  
    }

	public void Tutorial() {
		am.Play("MenuCameraToTutorial");
	}

	public void Levels() {
		am.Play("MenuCameraToLevels");
	}

	public void MainMenuT() {
		am.Play("MenuCameraFromTutorial");
	}

	public void MainMenuL() {
		am.Play("MenuCameraFromLevels");
	}

    public void StartLevel(int level) {

        Debug.Log("start level " + level + " current is: " + GameManager.instance.GetReachedLevel());

        //-1 jer sljedeci (ne proden) level mora bit otkjucan
        if (level - 1 > GameManager.instance.GetReachedLevel()) return;
       
        //Pretpostavlja da je prva scena Idle ili Main ili nesto drugo
        GameManager.instance.SetSelectedLevel(level);

        SceneManager.LoadScene(5);
    }
}
