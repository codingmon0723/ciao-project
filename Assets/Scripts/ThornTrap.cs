using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornTrap : RaycastController {

	public Transform thorn;
	public float waitTime = 0.5f;
	public float delayTime = 0.25f;
	public float smoothTime = 0.25f;
	public Vector3 targetPos;
	Vector3 globalTargetPos;
	Vector3 startPos;

	public bool work = false;
	
	public override void Start () {
		base.Start();
		globalTargetPos = targetPos + thorn.position;
		startPos = thorn.position;
	}
	
	void Update () {
		UpdateRaycastOrigins();

		float rayLength = skinWidth;

		for (int i = 0; i < verticalRayCount; i++)
		{
			Vector2 rayOrigin = raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * skinWidth, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.up * skinWidth, Color.red);

			if (hit)
			{
				if (!work)
				{
					StartCoroutine(DelayTrapWork());
				}
			}
		}

		thorn.position = Vector3.Lerp(thorn.position, work ? globalTargetPos : startPos, smoothTime * Time.deltaTime);
	}

	IEnumerator DelayTrapWork()
	{
		yield return new WaitForSeconds(waitTime);
		work = true;

		yield return new WaitForSeconds(delayTime);

		work = false;

	}

	void OnDrawGizmos()
	{
		float size = 0.1f;

		Gizmos.color = Color.red;
		Gizmos.DrawLine(thorn.position + Vector3.left * size, thorn.position + Vector3.right * size);
		Gizmos.DrawLine(thorn.position + Vector3.up * size, thorn.position + Vector3.down * size);

		Vector3 targetPos = (Application.isPlaying) ? globalTargetPos : thorn.position + this.targetPos;
		Gizmos.DrawLine(targetPos + Vector3.left * size, targetPos + Vector3.right * size);
		Gizmos.DrawLine(targetPos + Vector3.up * size, targetPos + Vector3.down * size);

		Gizmos.color = Color.blue;
		Gizmos.DrawLine(thorn.position, targetPos);
	}
}
