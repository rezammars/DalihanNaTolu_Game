using UnityEngine;

public class NPCPatrolAI : MonoBehaviour
{
    [Header("Patrol")]
    public bool enablePatrol = true;
    public Transform[] titikPatrol;

    public float speed = 2f;
    public float waitTime = 2f;

    [Header("Player Detect")]
    public float jarakStop = 2f;

    Transform player;

    int currentPoint = 0;
    bool waiting = false;
    Vector3 originalScale;
    Animator anim;

    void Start()
    {
        originalScale = transform.localScale;
        anim = GetComponent<Animator>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");

        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        if (!enablePatrol)
            return;

        if (player != null)
        {
            float jarak = Vector2.Distance(transform.position, player.position);

            if (jarak <= jarakStop)
            {
                if (anim != null)
                    anim.SetBool("Walk", false);

                return;
            }
        }

        if (!waiting)
            Patrol();
    }

    void Patrol()
    {
        if (titikPatrol == null || titikPatrol.Length == 0)
        return;

        Transform target = titikPatrol[currentPoint];

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime);

        if (anim != null)
            anim.SetBool("Walk", true);

        Vector3 scale = originalScale;

        if (target.position.x > transform.position.x)
            scale.x = Mathf.Abs(originalScale.x);
        else
            scale.x = -Mathf.Abs(originalScale.x);

        transform.localScale = scale;

        if (Vector2.Distance(transform.position,target.position) < 0.05f)
            StartCoroutine(WaitRoutine());
    }

    System.Collections.IEnumerator WaitRoutine()
    {
        waiting = true;

        if (anim != null)
            anim.SetBool("Walk", false);

        yield return new WaitForSeconds(waitTime);

        currentPoint++;

        if (currentPoint >= titikPatrol.Length)
            currentPoint = 0;

        waiting = false;
    }
}