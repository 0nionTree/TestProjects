using UnityEngine;
using System.Collections.Generic;

public class Sword : MonoBehaviour
{
    [Header("��� ĳ���� ����")]
    public GameObject user;
    private NormalState userState;
    private Character character;

    [Header("�� ���� ��Ȳ")]
    public bool isAttacking = false;
    public bool spUp = false;
    public float damage;

    public List<GameObject> HitMonsters = new List<GameObject>();

    private void Start()
    {
        userState = user.GetComponent<NormalState>();
        character = user.GetComponent<Character>();
    }

    private void Update()
    {
        if (isAttacking)
        {
            Vector2 direction = ((Vector2)transform.position - (Vector2)user.transform.position).normalized;
            float Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, Angle - 90f);
        }
        else
            transform.rotation = Quaternion.Euler(0f, 0f, character.Angle - 90f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking)
        {
            if (collision.CompareTag("Enemy"))
            {
                if (spUp)
                {   // ó������ �������� �� Sp ȹ��
                    spUp = false;
                    character.curSp = Mathf.Min(character.maxSp, character.curSp += 1);
                }
                if(!HitMonsters.Contains(collision.gameObject))
                {
                    HitMonsters.Add(collision.gameObject);
                    GameManager.instance.Damage(user, collision.gameObject, damage);
                }
            }
        }
    }
}
