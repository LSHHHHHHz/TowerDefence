using UnityEngine;
using UnityEngine.UI;

public class TestTower2 : MonoBehaviour
{
    public DamageDataManager2 damageDataManager;

    public void TakeDamage(int amount)
    {
        Vector3 damagePosition = transform.position; 
        damageDataManager.OnTowerDamaged(amount, damagePosition);
    }
}
public class DamageDataManager2 : MonoBehaviour
{
    public GameObject damagePopupPrefab;

    public void OnTowerDamaged(int damageAmount, Vector3 damagePosition)
    {
        ActiveDamagePopup(damageAmount, damagePosition);
    }

    private void ActiveDamagePopup(int damageAmount, Vector3 damagePosition)
    {
        if (damagePopupPrefab != null)
        {
            GameObject popup = Instantiate(damagePopupPrefab, damagePosition, Quaternion.identity);

            DamagePopup damagePopup = popup.GetComponent<DamagePopup>();
            if (damagePopup != null)
            {
                damagePopup.SetDamageUI(damageAmount);
            }
        }
    }
}
public class DamagePopup : MonoBehaviour
{
    public Text damageText;

    public void SetDamageUI(int amount)
    {
        damageText.text = amount.ToString();
    }
}