using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitObject : MonoBehaviour {
	[System.Serializable]
	public class WreckObject
	{
		public GameObject wreck;
		public Vector3 expPoint;
	}

	public float wreckDestroyTime = 2f;
	public WreckObject[] wrecks;
	Rigidbody2D[] rigid_wrecks;
	GameObject[] obj_wrecks;

	public bool split;

	void Awake()
	{
		rigid_wrecks = new Rigidbody2D[wrecks.Length];
		obj_wrecks = new GameObject[wrecks.Length];
	}

	void Start()
	{
		for (int i = 0; i < wrecks.Length; i++)
		{
			obj_wrecks[i] = Instantiate(wrecks[i].wreck);
			obj_wrecks[i].SetActive(false);
			rigid_wrecks[i] = obj_wrecks[i].GetComponent<Rigidbody2D>();
		}
	}

	void Update()
	{
		if (split)
		{
			for (int i = 0; i < wrecks.Length; i++)
			{
				obj_wrecks[i].SetActive(true);
				obj_wrecks[i].transform.position = transform.position + wrecks[i].expPoint;

				float randomX = Random.Range(-2f, 2f);
				float randomY = Random.Range(1f, 2f);
				Vector2 direction = new Vector2(randomX, randomY);
				rigid_wrecks[i].velocity = direction;

				Destroy(obj_wrecks[i], wreckDestroyTime);
			}
			Destroy(gameObject);
		}
	}

	void OnDrawGizmos()
	{
		if (wrecks != null)
		{
			float size = 0.05f;

			Gizmos.color = Color.red;
			for (int i = 0; i < wrecks.Length; i++)
			{
				Vector3 localExpPoint = transform.position + wrecks[i].expPoint;
				Gizmos.DrawLine(localExpPoint + Vector3.left * size, localExpPoint + Vector3.right * size);
				Gizmos.DrawLine(localExpPoint + Vector3.up * size, localExpPoint + Vector3.down * size);
			}
		}
	}
}
