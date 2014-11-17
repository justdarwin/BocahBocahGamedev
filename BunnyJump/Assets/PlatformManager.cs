using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Kelas yang mengatur munculnya platform / pijakan  
 */
public class PlatformManager : MonoBehaviour {

    float platformGenerateTimer;        // timer munculnya platform

    List<GameObject> platformPool;      // daftar platform yang ada di dalam level

    Transform cocoTransform;            // transform untuk mengetahui 
                                        // posisi player(coco)

	// Use this for initialization
	void Start () 
    {
        platformGenerateTimer = 0;
        platformPool = new List<GameObject>();

        cocoTransform = GameObject.Find("Coco").transform;
	}
	
	// Update is called once per frame
	void Update () 
    {

        foreach (GameObject go in platformPool)
        {
            /*
             * Aktifkan collider hanya jika coco (player) berada 
             * lebih tinggi dari platform / pijakan 
             */ 
            if (go.transform.position.y < cocoTransform.position.y)
                go.GetComponent<BoxCollider2D>().enabled = true;
            else
                go.GetComponent<BoxCollider2D>().enabled = false;

            /*
             * Hapus pijakan dari scene jika sudah berada di bawah layar
             */ 
            if (go.transform.position.y < -Camera.main.orthographicSize)
            {
                platformPool.Remove(go);
                GameObject.Destroy(go);
                break;
            }
            
        }

        /*
         * Generate platform setiap dua detik
         */ 
        platformGenerateTimer += Time.deltaTime;
        if (platformGenerateTimer > 2.0f)
        {
            platformGenerateTimer = 0;
            AddPlatform((LANE)Random.Range(0, 4));
        }

	}

    /*
     * Fungsi untuk memasukkan platform 
     * pada lane tertentu ke dalam level
     */ 
    public void AddPlatform(LANE lane)
    {
        GameObject platform = new GameObject();

        // atur posisi sesuai lane
        float x_pos = -1.0f;
        if (lane == LANE.ONE) x_pos = -3.0f;
        else if (lane == LANE.TWO) x_pos = -1.0f;
        else if (lane == LANE.THREE) x_pos = 1.0f;
        else if (lane == LANE.FOUR) x_pos = 3.0f;

        platform.transform.position = new Vector3(x_pos, 4.5f, 0);
        
        // buat parent platform menjadi GameObject level
        platform.transform.parent = transform.root;

        SpriteRenderer sr = platform.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("level3_cloud");

        // ttambahkan komponen BoxCollider2D agar coco bisa berpijak
        BoxCollider2D collider = platform.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(2.0f, 0.2f);
        collider.center = new Vector2(0, -0.2f);
        collider.enabled = false;// disable, agar coco dapat melompat ke atasnya

		// masukkan dalam list
		platformPool.Add(platform);
    }
}
