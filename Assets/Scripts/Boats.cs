using UnityEngine;
using System.Collections;

public class Boats : MonoBehaviour {

	float speed;
	Vector3 direction;
	float min;
	float max;
	float units = 0.5f;
	
	void Start()
	{
		max = transform.position.y;
		min = transform.position.y - units;

		direction = Vector3.down;
	}
	
	void Update()
	{
		if(direction == Vector3.down)
		{
			speed = Random.Range(0.01f, 0.1f);
		}
		
		else if(direction == Vector3.up)
		{
			speed = Random.Range(0.01f, 0.1f);
		}

		transform.Translate(direction * speed* 2 * Time.deltaTime);

		if(transform.position.y >= max)
		{
			direction = Vector3.down;
		}
		
		if(transform.position.y <= min)
		{
			direction = Vector3.up;    
		}
	}
}