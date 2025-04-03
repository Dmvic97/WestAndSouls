using UnityEngine;


public class ParallaxSun : MonoBehaviour
{
    public Transform camara; 
    public float speedParallax; 

    private Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position; //Guadamos la posicion inicial de la camara
    }

    void Update()
    {
        Vector3 newPos = initialPos + camara.position * speedParallax; //Creamos unanueva posicion teniendo en cuenta la posicion inicial del objeto
                                                                       //y la modificamos segun la velocidad indicada y la posicion de la camara
        transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z); //le asignamos la nueva posicion
    }
}
