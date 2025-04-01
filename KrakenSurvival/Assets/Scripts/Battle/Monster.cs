using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monName;

    public float attackDamage = 5;

    private float attackCooldown = 1f;
    private float lastAttackTime = -999f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;
                GameManager.instance.Damage(this.gameObject, collision.gameObject, attackDamage);
            }
        }
    }


}
