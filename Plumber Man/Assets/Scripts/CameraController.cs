using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float cameraDist;
    public float smoothingTime;
    public float shakeMagnitude;
    public float shakeDuration; 

    private Transform target;
    Vector2 mousePos;
    Vector3 shakeOffset; 
    Vector3 targetPos, refVel;
    private float duration;
    private float zStart;
    Vector3 shakeVector;
    bool shaking;


    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        zStart = transform.position.z; 
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mousePos = CaptureMousePos();
        shakeOffset = UpdateShake(); 
        targetPos = UpdateTargetPos();
        UpdateCameraPosition(); 
	}

    Vector2 CaptureMousePos()
    {
        Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        ret *= 2;
        ret -= Vector2.one;
        float max = 0.9f; 
        if(Mathf.Abs(ret.x)> max || Mathf.Abs(ret.y) > max)
        {
            ret = ret.normalized; 
        }
        return ret; 
    }

    Vector3 UpdateTargetPos()
    {
        Vector3 mouseOffset = mousePos * cameraDist;
        Vector3 ret = target.position + mouseOffset;
        ret += shakeOffset; 
        ret.z = zStart;
        return ret; 
    }

    void UpdateCameraPosition()
    {
        Vector3 tempPos;
        tempPos = Vector3.SmoothDamp(transform.position, targetPos, ref refVel, smoothingTime);
        transform.position = tempPos; 
    }

    public void Shake(Vector3 direction)
    {
        shaking = true;
        shakeVector = direction;
        duration = Time.time + shakeDuration; 
    }

    Vector3 UpdateShake()
    {
        if(!shaking || Time.time> duration)
        {
            shaking = false;  
            return Vector3.zero; 
        }
        Vector3 tempOffset = shakeVector;
        tempOffset *= shakeMagnitude;
        return tempOffset; 
    }
}
