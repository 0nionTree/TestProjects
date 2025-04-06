using UnityEngine;

public enum State { Normal, Nuckback }

public class NormalState : MonoBehaviour
{
    public bool isLive = true;
    public State state = State.Normal;

    public float maxHp = 100;
    public float curHp = 100;

    public bool canMove = true;
    public float moveSpeed = 0.5f;

    public bool isInvincible = false;
    public float invincibleTime = 0;
}
