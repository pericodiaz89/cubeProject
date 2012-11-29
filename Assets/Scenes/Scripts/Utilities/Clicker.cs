using UnityEngine;
using System.Collections;

public class Clicker : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            Physics.Raycast(ray,out hitInfo, Mathf.Infinity);
            if(hitInfo.transform != null){
                MonoBehaviour[] scripts = hitInfo.transform.GetComponents<MonoBehaviour>();
                foreach(MonoBehaviour m in scripts){
                    if(m is IClickable){
                        ((IClickable)m).NotifyClick();
                    }
                }
            }
        }
	}
}
