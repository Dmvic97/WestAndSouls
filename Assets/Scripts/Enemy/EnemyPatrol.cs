using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    //Limites del movimiento de patrulla
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;
    [SerializeField] private float idleDuration; //Tiempo que pasar� quieto al llegar a un limite
    private float idleTimer; //Temporizador para medir el tiempo que se esta quieto

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim; //Tambi�n hay un animator en enemy.cs, estar�a bien juntarlos pero soy incapaz

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void Update()
    {
        if (movingLeft)
        {   //Moverse a la izq hasta el l�mite
            if (enemy.position.x >= leftEdge.position.x)
            MoveInDirection(-1);
            else
            {
                //Cambiar direccion al llegar al limite
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
            {
                //Cambiar direccion al llegar al limite
                DirectionChange();
            }
        }
        
    }

    private void DirectionChange()
    {
        anim.SetBool("move", false);
        

        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration) //Si el temporizador es mayor al tiempo de espera se para el enemigo
            movingLeft = !movingLeft; //Si movingLeft es verdadera se convertir� en falso y viceversa



    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0; //Cuando se mueve reiniciamos el temporizador

        anim.SetBool("move", true);

        //Que el enemigo mire en la direcci�n correcta
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        
        //Mover el enemigo
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction,    //En el eje x modificamos el su posicion por tiempo y direccion
            enemy.position.y, enemy.position.z); //los dem�s ejes los dejamos igual
    }

    private void OnDisable() //Se llama cada vez que un objeto sea desactivado o destruido
    {
        anim.SetBool("move", false); //Detenemos movimiento cuando se desactiva la patrulla
    }
}
