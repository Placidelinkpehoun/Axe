using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;
    private GameObject currentWeapon;

    void Start()
    {
        transform.localScale = weaponPrefab.transform.localScale;
        transform.rotation = weaponPrefab.transform.rotation;
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
