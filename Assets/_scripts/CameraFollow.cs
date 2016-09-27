using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform player;
    Vector3 offset;
    public float rotationSpeed = 30f;
    Vector3 distance;

	// Use this for initialization
	void Start () {
	    offset = new Vector3(0f, 5f, -9f);
        gameObject.transform.position = player.position + offset; 
    }
	
	// Update is called once per frame
	void Update () {
        distance = player.position - gameObject.transform.position;
        print(distance.magnitude);
        Vector3 input = Vector3.zero;
        input.x = Input.GetAxis("rightHorizontal");
        input.y = Input.GetAxis("rightVertical");
        

        gameObject.transform.LookAt(player, Vector3.up);
        
        if (input.x >= 0.7)
        {
            gameObject.transform.RotateAround(player.position, Vector3.down, rotationSpeed * 2f * Time.deltaTime);
            //gameObject.transform.Translate(Vector3.right * Time.deltaTime*rotationSpeed);
        }
        else if(input.x <= -0.7)
        {
            //gameObject.transform.Translate(Vector3.left * Time.deltaTime*rotationSpeed);
            gameObject.transform.RotateAround(player.position, Vector3.up, rotationSpeed* 2f * Time.deltaTime);
        }
        if (input.y >= 0.7)
        {
            //gameObject.transform.RotateAround(player.position, Vector3.right, rotationSpeed * Time.deltaTime);
            //gameObject.transform.Translate(Vector3.up * Time.deltaTime * rotationSpeed / 2f);
        }
        else if (input.y <= -0.7)
        {
            //gameObject.transform.RotateAround(player.position, Vector3.left, rotationSpeed * Time.deltaTime);
            //gameObject.transform.Translate(Vector3.down * Time.deltaTime * rotationSpeed / 2f);
        }
        
    }
}
