using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour {

    //PLACEHOLDER FIELDS
    public int health;
    public int maxHealth;
    public int habilidadesDisponibles;


    public SlideBar lifeBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RenderHealth();

        RenderHabilidades();
	}

    public void RenderHealth()
    {
        //Cambia el tamaño del slider en base a la vida máxima actual

        //Cambia el valor del slider en base a la vida actual
    }

    public void RenderHabilidades()
    {
        //Desactiva todos los gameobjects

        //Activa un número igual al de habilidades disponibles

        //Por cada habilidad, hace un render sobre ese GameObject
    }
}
