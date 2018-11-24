using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    public static readonly int MAXIMUM_HEALTH_YOU_CAN_HAVE = 600;

    //PLACEHOLDER FIELDS
    public int health;
    public int maxHealth;
    public int habilidadesDisponibles;
    public int mouse1Selected, mouse2Selected;


    public Slider lifeBar;
    public Transform papiHabilidades;

    private Transform[] botonesHabilidad;

	// Use this for initialization
	void Start () {
        botonesHabilidad = papiHabilidades.Cast<Transform>().ToArray();
    }
	
	// Update is called once per frame
	void Update () {
        RenderHealth();

        RenderHabilidades();
        
	}

    public void RenderHealth()
    {
        //Cambia el tamaño del slider en base a la vida máxima actual
        lifeBar.transform.localScale = new Vector3((float)maxHealth / MAXIMUM_HEALTH_YOU_CAN_HAVE, 1, 1);
        //Cambia el valor del slider en base a la vida actual
        lifeBar.value = (float)health / maxHealth;
    }

    public void RenderHabilidades()
    {
        //Desactiva todos los gameobjects
        foreach (Transform boton in botonesHabilidad)
            boton.gameObject.SetActive(false);
        //Activa un número igual al de habilidades disponibles
        //Por cada habilidad, hace un render sobre ese GameObject
        for (int i = 0; i < habilidadesDisponibles; i++)
        {
            botonesHabilidad[i].gameObject.SetActive(true);

            
            Image select1 = botonesHabilidad[i].GetComponentsInChildren<Image>(true)[1];
            Image select2 = botonesHabilidad[i].GetComponentsInChildren<Image>(true)[0];

            select1.gameObject.SetActive(false);
            select2.gameObject.SetActive(false);

            if (i == mouse1Selected)
                select1.gameObject.SetActive(true);
            if (i == mouse2Selected)
                select2.gameObject.SetActive(true);
        }
    }
}
