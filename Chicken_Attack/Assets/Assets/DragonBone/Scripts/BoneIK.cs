﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DragonBone
{
	/// <summary>
	/// SimpleCCD
	/// </summary>
	[ExecuteInEditMode]
	public class BoneIK : MonoBehaviour {

		public int iterations = 5;

		[Range(0.01f, 1)]
		public float damping = 1;

		public Transform targetIK;
		public Transform endTransform;

		public bool bendPositive = true;

		private float m_angleMin,m_angleMax;

		void Start()
		{
			if(bendPositive){
				m_angleMin=  180f;
				m_angleMax = 360f;
			}else{
				m_angleMin= 0f;
				m_angleMax = 180f;
			}
		}

		void LateUpdate()
		{
			if (!Application.isPlaying)
				Start();

			if (targetIK == null || endTransform == null)
				return;

			int i = 0;
			while (i < iterations)
			{
				CalculateIK ();
				i++;
			}
		}

		public void CalculateIK()
		{	
			Transform node = endTransform.parent;
			while (true)
			{
				RotateTowardsTarget (node , node==transform?false:true );
				if (node == transform)
					break;
				node = node.parent;
			}
		}

		void RotateTowardsTarget(Transform transform , bool limit = true)
		{	
			Vector2 toTarget = targetIK.position - transform.position;
			Vector2 toEnd = endTransform.position - transform.position;
			// Calculate how much we should rotate to get to the target
			float angle = SignedAngle(toEnd, toTarget);
			// Flip sign if character is turned around
			angle *= Mathf.Sign(transform.root.localScale.x);
			// "Slows" down the IK solving
			angle *= damping; 
			// Wanted angle for rotation
			angle = -(angle - transform.eulerAngles.z);

			// Take care of angle limits 
			float parentRotation = transform.parent ? transform.parent.eulerAngles.z : 0;
			angle -= parentRotation;

			if(limit)
				angle = ClampAngle1(angle, m_angleMin,m_angleMax);
			else
				angle = ClampAngle(angle, 0f,360f);
			
			angle += parentRotation;

			Vector3 temp = transform.localEulerAngles;
			transform.rotation = Quaternion.Euler(temp.x,temp.y, angle);
		}

		public static float SignedAngle (Vector3 a, Vector3 b)
		{
			float angle = Vector3.Angle (a, b);
			float sign = Mathf.Sign (Vector3.Dot (Vector3.back, Vector3.Cross (a, b)));

			return angle * sign;
		}

		float ClampAngle (float angle, float min, float max)
		{
			angle = Mathf.Abs((angle % 360) + 360) % 360;
			return Mathf.Clamp(angle, min, max);
		}

		float ClampAngle1(float angle, float from, float to)
		{
			angle = Mathf.Abs((angle % 360) + 360) % 360;
			//Check limits
			if (from > to && (angle > from || angle < to))
				return angle;
			else if (to > from && (angle < to && angle > from))
				return angle;
			//Return nearest limit if not in bounds
			return (Mathf.Abs(angle - from) < Mathf.Abs(angle - to) && Mathf.Abs(angle - from) < Mathf.Abs((angle + 360) - to)) || (Mathf.Abs(angle - from - 360) < Mathf.Abs(angle - to) && Mathf.Abs(angle - from - 360) < Mathf.Abs((angle + 360) - to)) ? from : to;

		}

		#if UNITY_EDITOR
		void OnDrawGizmos() {
			if(endTransform)
			{
				Gizmos.color = Color.blue;
				Transform endTrans = endTransform;
				while(endTrans!=transform)
				{
					Transform parent = endTrans.parent;

					Vector3 v = Quaternion.AngleAxis(30, Vector3.forward) * ((endTrans.position - parent.position) / 20 );
					Gizmos.DrawLine(parent.position, parent.position + v);
					Gizmos.DrawLine(parent.position + v, endTrans.position);

					v = Quaternion.AngleAxis(-30, Vector3.forward) * ((endTrans.position - parent.position) / 20);
					Gizmos.DrawLine(parent.position, parent.position + v);
					Gizmos.DrawLine(parent.position + v, endTrans.position);

//					Gizmos.DrawLine(parent.position,endTrans.position);

					endTrans = parent;
				}
			}
		}
		#endif
	}

}