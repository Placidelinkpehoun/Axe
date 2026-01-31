using UnityEngine;

public class WeaponCollisionDetector : MonoBehaviour
{
    private RotatingWeapon weaponController;

    void Start()
    {
        // Récupérer le script principal de l'arme
        weaponController = GetComponentInParent<RotatingWeapon>();

        if (weaponController == null)
        {
            Debug.LogError("WeaponCollisionDetector ne trouve pas le RotatingWeapon parent!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (weaponController != null)
        {
            weaponController.OnWeaponCollision(collision);
        }
    }
}