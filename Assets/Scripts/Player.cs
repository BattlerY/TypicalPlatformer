using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _cameraLerp;

    public PlayerMover Mover { get; private set; }

    private void Awake()
    {
        Mover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        cameraPosition.x = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPosition, _cameraLerp*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin coin))
            collision.gameObject.SetActive(false);
    }
}
