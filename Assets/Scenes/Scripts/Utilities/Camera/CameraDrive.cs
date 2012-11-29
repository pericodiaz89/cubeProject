using UnityEngine;
using System.Collections;

public class CameraDrive : MonoBehaviour {
	
	public GameObject cube;
	protected Vector3 cubep;
	public float error;
	public float speedRot;
	public float speedMov;
	
	protected virtual void Start () {
		cubep=cube.transform.position;
		cubep.Set(cubep.x,cubep.y+5,cubep.z);
		error=-5;
	}
	
	
	protected virtual void Update () {
		camera.transform.LookAt(cubep);
		if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)){
			camera.transform.RotateAround(cube.transform.position,new Vector3(0,-1,0),speedRot*Time.deltaTime);
		} else if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)){
			camera.transform.RotateAround(cube.transform.position,new Vector3(0,1,0),speedRot*Time.deltaTime);
		}
		
		
		if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)){
			cubep.Set(cubep.x,cubep.y+(speedMov)*Time.deltaTime,cubep.z);
			camera.transform.Translate(0,speedMov*Time.deltaTime,0,cube.transform);
		} else if(camera.transform.position.y>=cube.transform.position.y&&(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))){
			if(cubep.y+error>=cube.transform.position.y){
				cubep.Set(cubep.x,cubep.y+(-speedMov)*Time.deltaTime,cubep.z);
				camera.transform.Translate(0,-speedMov*Time.deltaTime,0,cube.transform);
			}
			
		}
		
		
		if(camera.transform.position.y-2>=cubep.y&&Input.GetAxis("Mouse ScrollWheel") > 0){
			Vector3 mov = (cubep-camera.transform.position)*Time.deltaTime;
			mov=mov.normalized;
			camera.transform.position+=mov;
	    }
	    if(Input.GetAxis("Mouse ScrollWheel") < 0){
			Vector3 mov = (cube.transform.position-camera.transform.position)*-Time.deltaTime;
			mov=mov.normalized;
			camera.transform.position+=mov;
	    } 
			
	}
}
