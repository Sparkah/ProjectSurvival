using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _initHealth = 100; //Temp definition before SO
    [SerializeField] private DamagableObject _damagableObject;

    private void Start() //Temp initialization before pool initing
    {
        _damagableObject.OnDefeat += Defeat;
        _damagableObject.SetupHealth(_initHealth);
    }

    private void OnDestroy()
    {
        _damagableObject.OnDefeat -= Defeat;
    }

    private void Defeat()
    {
        Destroy(gameObject); //Temp code before pooling
    }
}
