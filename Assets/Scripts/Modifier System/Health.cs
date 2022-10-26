using UnityEngine;

public class Health : MonoBehaviour
{
	public Stat maxHealth;
	[Range(0.0f, 1.0f)] public float currentHealth = 1;

	public float healingTimer = 6;
	[Range(0.0f, 1.0f)] public float healPercentage = 0.2f;
	private float hitTime;
	[HideInInspector]public float damageTaken;

	public float decaySpeed = 0;

    private void Update()
    {
		if (Time.time > hitTime + healingTimer)
		{
			if (damageTaken >= 0)
			{
				damageTaken -= decaySpeed * Time.deltaTime;
			}
            else
            {
				damageTaken = 0;
            }
		}
	}

	[ContextMenu("OnDealDamage")]
	public void OnDealDamage() => OnDealDamage(new DamageArgs(null, 20.0f));
    public void OnDealDamage(DamageArgs damageArgs)
    {
		currentHealth += (damageTaken * healPercentage) / maxHealth.GetFor(this);
		damageTaken -= damageTaken * healPercentage;
    }

	public void Damage(DamageArgs damageArgs)
	{
		damageTaken = damageArgs.damage;
		currentHealth -= damageArgs.damage / maxHealth.GetFor(this);

		if (currentHealth >= 0)
		{
			hitTime = Time.time;
		}
		else
		{
			Die(damageArgs);
		}
	}

	public void Die(DamageArgs damageArgs)
	{
		gameObject.SetActive(false);
	}

	[ContextMenu("TakeDamage")]
	public void TakeDamageTest()
    {

		Damage(new DamageArgs(null, 20));
    }


}

public struct DamageArgs
{
	public GameObject damager;
	public float damage;
	public DamageArgs(GameObject damager, float damage)
	{
		this.damager = damager;
		this.damage = damage;
	}
}
