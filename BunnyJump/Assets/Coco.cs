using UnityEngine;
using System.Collections;

/*
 * Coco si kelinci bulan
 */ 
public class Coco : MonoBehaviour {
    
    LANE lane;      
    LANE prev_lane; 
	
    enum STATE
	{
		IDLE,
		JUMPING
	}
	
	STATE cocoState;

    Rigidbody2D rigidBody2D;

    // variabel posisi untuk interpolasi animasi melompat
    float targetPositionX;
    float prevPositionX;

    float interpolateTimer;

	// Use this for initialization
	void Start () 
    {

        lane = LANE.TWO;
        prev_lane = LANE.TWO;

		cocoState = STATE.IDLE;
		
        rigidBody2D = GetComponent<Rigidbody2D>();

        targetPositionX = 0;
        prevPositionX = 0;

        interpolateTimer = 0;

	}
	
	// Update is called once per frame
	void Update () {

		switch(cocoState)
		{
            // pada saat idle cek tombol yang ditekan
			case STATE.IDLE :

    			if (Input.GetKeyUp(KeyCode.A))
				{
					lane = LANE.ONE;
                    rigidBody2D.AddForce(new Vector2(0, 350));
				}

				if (Input.GetKeyUp(KeyCode.S))
				{
					lane = LANE.TWO;
					rigidBody2D.AddForce(new Vector2(0, 350));
				}

				if (Input.GetKeyUp(KeyCode.D))
				{
					lane = LANE.THREE;
                    rigidBody2D.AddForce(new Vector2(0, 350));
				}
				
				if (Input.GetKeyUp(KeyCode.F))
				{
					lane = LANE.FOUR;
                    rigidBody2D.AddForce(new Vector2(0, 350));
				}
				
                if(prev_lane != lane)
				{
					prevPositionX = transform.position.x;

                    if (lane == LANE.ONE)
                        targetPositionX = -3.0f;

					if (lane == LANE.TWO)
						targetPositionX = -1.0f;

					if (lane == LANE.THREE)
						targetPositionX = 1.0f;

                    if (lane == LANE.FOUR)
                        targetPositionX = 3.0f;

					interpolateTimer = 0;
					prev_lane = lane;
					
					cocoState = STATE.JUMPING;
					
				}
			
				break;
			
			case STATE.JUMPING :

                // pada saat melompat, interpolasi posisi 
                if (interpolateTimer < 1.0f)
                {
                    interpolateTimer += Time.deltaTime;
                    float positionX = Mathf.Lerp(prevPositionX, targetPositionX, interpolateTimer);
                    transform.position = new Vector2(positionX, transform.position.y);
                }
                else
                {
                    cocoState = STATE.IDLE;
                    interpolateTimer = 0;
                }
					
				break;
		}        
        
	}
}
