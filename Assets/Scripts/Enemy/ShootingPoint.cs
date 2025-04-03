using UnityEngine;

public class ShootingPoint : MonoBehaviour
{
    [SerializeField] private Transform enemy; //Referenciamos enemy para poder utilizar su scale
    void Update()
    {
        transform.localScale = enemy.localScale; //Hacemos que el scale de la bala sea igual que la de enemy,
                                                 //de forma que cuando cuando gire el enemigo tambien lo hara la bala
    }
}
