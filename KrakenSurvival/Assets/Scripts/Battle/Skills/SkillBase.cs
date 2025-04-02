using UnityEngine;

public abstract class SkillBase : ScriptableObject
{
    public string skillName;

    public float power;
    public float fastRecovery;
    public float slowRecovery;
    public float baseSkillCooldown;
    public float currSkillCooldown = 0f;

    public abstract void Activate(GameObject user, GameObject target);
}
