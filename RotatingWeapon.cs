using UnityEngine;

public class RotatingWeapon : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private LayerMask collisionLayers;

    [Header("References")]
    [SerializeField] private Transform weaponModel;

    private int rotationDirection = 1; // 1 pour sens horaire, -1 pour anti-horaire
    private Transform playerTransform;

    void Start()
    {
        // Récupérer le transform du parent (le joueur)
        playerTransform = transform.parent;
    }

    void Update()
    {
        // Rotation continue autour de l'axe Y
        transform.Rotate(Vector3.up, rotationSpeed * rotationDirection * Time.deltaTime, Space.Self);
    }

    void LateUpdate()
    {
        // S'assurer que l'arme suit le joueur
        if (playerTransform != null)
        {
            transform.position = playerTransform.position;
        }
    }

    // Inverser la direction de rotation
    public void ReverseRotation()
    {
        rotationDirection *= -1;
    }

    // Méthode appelée par le WeaponCollisionDetector
    public void OnWeaponCollision(Collision collision)
    {
        // Vérifier si l'objet touché est dans les layers définis
        if (((1 << collision.gameObject.layer) & collisionLayers) != 0)
        {
            ReverseRotation();
            Debug.Log($"Collision détectée avec {collision.gameObject.name}, inversion de rotation");
        }
    }
}