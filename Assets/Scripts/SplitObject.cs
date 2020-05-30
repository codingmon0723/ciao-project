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
	Vector3[] globalExpPoints;

	public bool split;

	void Awake()
	{
		rigid_wrecks = new Rigidbody2D[wrecks.Length];
		obj_wrecks = new GameObject[wrecks.Length];
		globalExpPoints = new Vector3[wrecks.Length];

		for (int i = 0; i < wrecks.Length; i++)
		{
			obj_wrecks[i] = Instantiate(wrecks[i].wreck);
			obj_wrecks[i].SetActive(false);
			rigid_wrecks[i] = obj_wrecks[i].GetComponent<Rigidbody2D>();
			globalExpPoints[i] = wrecks[i].expPoint + transform.position;
		}
	}

	void Update()
	{
		if (split)
		{
			for (int i = 0; i < wrecks.Length; i++)
			{
				obj_wrecks[i].SetActive(true);
				obj_wrecks[i].transform.position = globalExpPoints[i];

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
		float size = 0.05f;

		Gizmos.color = Color.red;
		for (int i = 0; i < wrecks.Length; i++)	
		{
			Vector3 globalExpPoint = (Application.isPlaying) ? globalExpPoints[i] : transform.position + wrecks[i].expPoint;
			Gizmos.DrawLine(globalExpPoint + Vector3.left * size, globalExpPoint + Vector3.right * size);
			Gizmos.DrawLine(globalExpPoint + Vector3.up * size, globalExpPoint + Vector3.down * size);
		}
	}
}
