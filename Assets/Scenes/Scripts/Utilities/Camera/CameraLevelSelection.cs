using UnityEngine;
using System.Collections.Generic;

public class CameraLevelSelection : CameraDrive {
	
	public Material [] materials;
	public List<GameObject>cubes;
	protected override void Start ()
	{
		base.Start ();
		
		for (int i = 0; i < cubes.Count;i++){
			if(i == 0){
				cubes[i].renderer.material=materials[0];
			}else if(i <= 2){
				cubes[i].renderer.material=materials[1];
			}else if (i == 3){
				cubes[i].renderer.material=materials[3];
			}else if(i <= 5){
				cubes[i].renderer.material=materials[2];
			}else if(i == 6){
				cubes[i].renderer.material=materials[5];
			}else if(i <= 8){
				cubes[i].renderer.material=materials[4];
			}else if(i==9){
				cubes[i].renderer.material=materials[7];
			}else if(i<=11){
				cubes[i].renderer.material=materials[6];
			}else{
				cubes[i].renderer.material=materials[16];
			}
		}
		cubep.Set(cubep.x,cubep.y-5,cubep.z);
		error=0;
		
	}
	
	protected override void Update ()
	{
		base.Update ();
		
		if(Input.GetMouseButtonUp(0)){
			Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(ray,out hit,100)){
				string name = hit.collider.gameObject.name;
				if(hit.collider.gameObject.renderer.material.color != materials[16].color){
					
				}
			}
		}
	}
	
	public void setLevels(List<string> levels){
		/*for(int i = 0; i < levels.Count;i++){
			if(int.Parse(levels[i]) > 7 ){
				
			}else if (cubes[int.Parse(levels[i])].renderer.material==materials[1]){
				cubes[int.Parse(levels[i])].renderer.material=materials[0];
			}else {
				for(int j = 3; j < materials.Length; j+=2){
					if(cubes[int.Parse(levels[i])].renderer.material==materials[j]){
						cubes[int.Parse(levels[i])].renderer.material=materials[j+1];
					}
				}
			}
			cubes[int.Parse(levels[i])].renderer.material=materials[2];
		}*/
		
	}
	
	public enum Dias{
		Lunes = 1, Martes, Miercoles, Jueves, Viernes
	}
	
}
