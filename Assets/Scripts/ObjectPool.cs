using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    [SerializeField] private Arrow arrowPrefab;
    [SerializeField] private int poolSize = 10; 

    private Queue<Arrow> _availableArrows = new Queue<Arrow>(); // Fletxes disponibles en el pool
    private List<Arrow> _allArrows = new List<Arrow>(); // Totes les fletxes

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Arrow arrow = Instantiate(arrowPrefab, transform);
            arrow.gameObject.SetActive(false);
            _availableArrows.Enqueue(arrow); // Afegir a la cua de disponibles
            _allArrows.Add(arrow);
        }
    }

    // Obtener fletxa del pool
    public Arrow GetArrow()
    {
        if (_availableArrows.Count == 0)
        {
            // Si el pool està buit, pot expandir-se
            Debug.LogWarning("ObjectPool: Pool buit, creant noves fletxes.", this);
            Arrow newArrow = Instantiate(arrowPrefab, transform);
            newArrow.gameObject.SetActive(false);
            _allArrows.Add(newArrow);
            return newArrow;
        }

        Arrow arrowToUse = _availableArrows.Dequeue(); // Treure de la cua
        return arrowToUse;
    }

    // Retornar una fletxa al pool
    public void ReturnArrow(Arrow arrow)
    {
        arrow.gameObject.SetActive(false); // Desactivar
        arrow.transform.position = transform.position;
        _availableArrows.Enqueue(arrow); // Afegir a la cua
    }

    // Controlar que les fletxes no surtin de l'espai de la càmera
    void Update()
    {
        // Límits de la càmera en coordenades del mon
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        float minX = Camera.main.transform.position.x - screenBounds.x - 1f;
        float maxX = Camera.main.transform.position.x + screenBounds.x + 1f;
        float minY = Camera.main.transform.position.y - screenBounds.y - 1f;
        float maxY = Camera.main.transform.position.y + screenBounds.y + 1f;


        foreach (Arrow arrow in _allArrows)
        {
            if (arrow.gameObject.activeInHierarchy) // Verificar fletxes actives
            {
                if (arrow.transform.position.x < minX ||
                    arrow.transform.position.x > maxX ||
                    arrow.transform.position.y < minY ||
                    arrow.transform.position.y > maxY)
                {
                    arrow.ReturnToPool(); // Retornar la fletxa al pool si surt dels límits
                }
            }
        }
    }
}
