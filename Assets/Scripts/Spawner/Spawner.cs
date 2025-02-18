using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Inspector
    [SerializeField] private GameObject prefab;
    [SerializeField] private float delay;
    [SerializeField] private float speed;
    [SerializeField] private Rect spawnArea;
    #endregion

    public static readonly float Width = 15f;

    public float Speed { get { return speed; } set { speed = value; } }

    private WaitForSeconds seconds;

    private readonly List<GameObject> objectPool = new();

    private void Awake()
    {
        seconds = new(delay);
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private void Update()
    {
        foreach (var item in objectPool)
        {
            if (item.activeSelf == false)
            {
                continue;
            }

            Transform child = item.transform;
            child.Translate(speed * Time.deltaTime * Vector3.left);
            if (child.position.x <= -Width)
            {
                item.SetActive(false);
            }
        }
    }

    public virtual GameObject Spawn(Rect spawnArea)
    {
        GameObject gameObject;

        var pool = objectPool.Where(x => x.activeSelf == false).ToArray();
        if (pool.Length > 0)
        {
            gameObject = pool[0];
            gameObject.SetActive(true);
        }
        else
        {
            gameObject = Instantiate(prefab, transform);
            objectPool.Add(gameObject);
        }

        float x = Random.Range(spawnArea.xMin, spawnArea.xMax);
        float y = transform.position.y + Random.Range(spawnArea.yMin, spawnArea.yMax);
        gameObject.transform.position = new(x, y);

        return gameObject;
    }

    private IEnumerator Spawning()
    {
        while (true)
        {
            yield return seconds;
            Spawn(spawnArea);
        }
    }
}