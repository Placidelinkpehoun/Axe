using UnityEngine;

public class RotatingWeapon : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform orbitTarget; // L'objet autour duquel tourner
    [SerializeField] private bool findPlayerByTag = true; // Chercher automatiquement le joueur
    [SerializeField] private string playerTag = "Player"; // Tag du joueur

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float orbitRadius = 2f;
    [SerializeField] private float orbitHeight = 1f;
    [SerializeField] private LayerMask collisionLayers;

    [Header("Weapon Orientation")]
    [SerializeField] private bool faceOutward = true;
    [SerializeField] private bool faceMovementDirection = false;

    [Header("References")]
    [SerializeField] private Transform weaponModel;

    private float currentAngle = 0f;
    private int rotationDirection = 1;

    void Start()
    {
        // Si aucune cible n'est définie, chercher automatiquement
        if (orbitTarget == null)
        {
            if (findPlayerByTag)
            {
                // Chercher le joueur par tag
                GameObject player = GameObject.FindGameObjectWithTag(playerTag);
                if (player != null)
                {
                    orbitTarget = player.transform;
                    Debug.Log($"RotatingWeapon : Joueur trouvé automatiquement ({player.name})");
                }
                else
                {
                    Debug.LogError($"RotatingWeapon : Aucun objet avec le tag '{playerTag}' trouvé !");
                }
            }
            else
            {
                // Essayer de récupérer le parent
                orbitTarget = transform.parent;
            }

            if (orbitTarget == null)
            {
                Debug.LogError("RotatingWeapon : Aucune cible d'orbite définie !");
            }
        }
    }

    void Update()
    {
        if (orbitTarget == null)
        {
            Debug.LogWarning("RotatingWeapon : Pas de cible d'orbite !");
            return;
        }

        // Incrémenter l'angle
        currentAngle += rotationSpeed * rotationDirection * Time.deltaTime;

        // Normaliser l'angle
        if (currentAngle >= 360f) currentAngle -= 360f;
        if (currentAngle < 0f) currentAngle += 360f;

        UpdateOrbitPosition();
    }

    void UpdateOrbitPosition()
    {
        float angleInRadians = currentAngle * Mathf.Deg2Rad;

        // Calculer la position sur le cercle
        float x = Mathf.Cos(angleInRadians) * orbitRadius;
        float z = Mathf.Sin(angleInRadians) * orbitRadius;

        // Position finale = position de la cible + offset du cercle
        Vector3 orbitPosition = orbitTarget.position + new Vector3(x, orbitHeight, z);
        transform.position = orbitPosition;

        // Orientation de l'arme
        if (faceOutward)
        {
            Vector3 outwardDirection = (transform.position - orbitTarget.position).normalized;
            outwardDirection.y = 0;
            if (outwardDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(outwardDirection);
            }
        }
        else if (faceMovementDirection)
        {
            Vector3 tangentDirection = new Vector3(-Mathf.Sin(angleInRadians), 0, Mathf.Cos(angleInRadians));
            if (tangentDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(tangentDirection * rotationDirection);
            }
        }
    }

    public void ReverseRotation()
    {
        rotationDirection *= -1;
    }

    public void OnWeaponCollision(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & collisionLayers) != 0)
        {
            ReverseRotation();
            Debug.Log($"Collision détectée avec {collision.gameObject.name}, inversion de rotation");
        }
    }

    public void SetOrbitTarget(Transform newTarget)
    {
        orbitTarget = newTarget;
    }

    public void SetOrbitRadius(float newRadius)
    {
        orbitRadius = Mathf.Max(0f, newRadius);
    }

    public void SetRotationSpeed(float newSpeed)
    {
        rotationSpeed = newSpeed;
    }

    public void SetOrbitHeight(float newHeight)
    {
        orbitHeight = newHeight;
    }
}