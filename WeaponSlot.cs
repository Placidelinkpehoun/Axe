using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;
    private GameObject currentWeapon;

    void Start()
    {
        EquipWeapon();
    }

    public void EquipWeapon()
    {
        if (weaponPrefab != null && currentWeapon == null)
        {
            currentWeapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity, transform);
        }
    }

    public void UnequipWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
            currentWeapon = null;
        }
    }
}
