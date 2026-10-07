using UnityEngine;

public class DummyManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    void Update()
    {
        if (transform.childCount == 0)
        {
            enemy.SetActive(true);
        }
    }
}
