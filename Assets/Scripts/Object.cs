using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField] private float objectSpeed = 1f;
	[SerializeField] private float resetPosition = 120f;
	[SerializeField] private float startPosition = 0f;

	void Start()
	{

	}

	// Update is called once per frame
	protected virtual void Update()
	{

		if (!GameManager.instance.GameOver)
		{
			transform.Translate(Vector3.right * (objectSpeed * Time.deltaTime));

			if (transform.localPosition.x >= resetPosition)
			{
				Vector3 newPos = new Vector3(startPosition, transform.position.y, transform.position.z);
				transform.position = newPos;
			}
		}
	}
}
