using UnityEngine;
using System.Collections;
using UnityEditor;

/*
 * level manager, fungsinya hanya 
 * menggeser level ke bawah
 */ 
public class LevelManager : MonoBehaviour {

    private float level_move_speed;

	// Use this for initialization
    void Start()
    {
		if (EditorApplication.currentScene.Equals ("MainScene")) {
						level_move_speed = 10.0f;
				} else {
			level_move_speed = 30.0f;		
		}
    }
	
	// Update is called once per frame
	void Update () 
    {
        transform.position -= new Vector3(0, 0.01f,0);
	}
}
