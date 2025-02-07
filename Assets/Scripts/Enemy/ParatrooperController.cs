using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParatrooperController : MonoBehaviour
{
    public GameObject trooperWithParachute;
    public GameObject trooperWithoutParachute;
    public GameObject trooperDeath;

    private Rigidbody2D rb;
    private bool hasLanded = false;
    private bool isShot = false;
    private bool isMoving = false;
    private bool isClimbing = false;

    public Collider2D triggerCollider;
    public Collider2D solidCollider;

    public LayerMask groundLayer;
    public LayerMask troopLayer;

    private static List<ParatrooperController> leftSideTroops = new List<ParatrooperController>();
    private static List<ParatrooperController> rightSideTroops = new List<ParatrooperController>();

    private static float fixedLandingY = -4.36f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -1f);
        solidCollider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasLanded)
        {
            if (other.CompareTag("Ground"))
            {
                Land();
            }
            if (other.CompareTag("Trooper"))
            {
                Land(other);
            }
        }

        if (other.CompareTag("Bullet"))
        {
            GetShot();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Gun") && isMoving)
        {
            StopAtGun();
        }



    }
    void Update()
    {
        if (!hasLanded)
        {
            CheckLanding();
        }
        else
        {

            if (leftSideTroops.Count >= 4)
            {
                MoveTroop(leftSideTroops);
            }
            if (rightSideTroops.Count >= 4)
            {
                MoveTroop(rightSideTroops);
            }
        }
    }

    void CheckLanding()
    {
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayer);
        RaycastHit2D hitTroop = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, troopLayer);

        if (hitGround.collider != null || hitTroop.collider != null)
        {
            Land(hitTroop.collider);
        }
    }

    void Land(Collider2D hitTroop = null)
    {
        if (hasLanded) return;

        hasLanded = true;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;

        trooperWithParachute.SetActive(false);
        trooperWithoutParachute.SetActive(true);

        solidCollider.enabled = true;

        transform.position = new Vector3(transform.position.x, fixedLandingY, 0);

        if (transform.position.x < 0)
        {
            leftSideTroops.Add(this);
            leftSideTroops.Sort(CompareTroopPriority);
        }
        else
        {
            rightSideTroops.Add(this);
            rightSideTroops.Sort(CompareTroopPriority);
        }
    }

    int CompareTroopPriority(ParatrooperController a, ParatrooperController b)
    {
        if (Mathf.Abs(a.transform.position.x) < Mathf.Abs(b.transform.position.x))
            return -1;
        else if (Mathf.Abs(a.transform.position.x) > Mathf.Abs(b.transform.position.x))
            return 1;


        return b.transform.position.y.CompareTo(a.transform.position.y);
    }

    void MoveTroop(List<ParatrooperController> troopList)
    {
        if (troopList.Count == 0) return;

        ParatrooperController movingTroop = troopList[0];

        if (!movingTroop.isMoving && !movingTroop.isClimbing)
        {
            movingTroop.StartMoving();
        }
    }

    void StartMoving()
    {
        if (isMoving) return;

        isMoving = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = new Vector2(transform.position.x < 0 ? 1f : -1f, 0);
    }

    void StopAtGun()
    {
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = new Vector3(1.06f, -4.36f, 0);
        isMoving = false;

        StartCoroutine(BuildPyramid());
    }

    IEnumerator BuildPyramid()
    {
        yield return new WaitForSeconds(1f);

        List<ParatrooperController> troopList = transform.position.x < 0 ? leftSideTroops : rightSideTroops;
        if (troopList.Count >= 2)
        {
            ParatrooperController secondTroop = troopList[1];
            secondTroop.ClimbOnTop(this);
        }

        if (troopList.Count >= 3)
        {
            ParatrooperController thirdTroop = troopList[2];
            thirdTroop.MoveBeside(this);
        }

        if (troopList.Count >= 4)
        {
            ParatrooperController fourthTroop = troopList[3];
            fourthTroop.ClimbAndDestroyGun();
        }
    }

    void ClimbOnTop(ParatrooperController lowerTroop)
    {
        if (isClimbing) return;

        isClimbing = true;
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = new Vector3(lowerTroop.transform.position.x, -4.11f, 0);
    }

    void MoveBeside(ParatrooperController firstTroop)
    {
        StartCoroutine(MoveToPosition(new Vector3(1.18f, -4.36f, 0)));
    }

    void ClimbAndDestroyGun()
    {
        StartCoroutine(MoveToPosition(new Vector3(transform.position.x, transform.position.y + 1f, 0)));
        StartCoroutine(DestroyGun());
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 2f * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }

    IEnumerator DestroyGun()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(GameObject.FindGameObjectWithTag("Gun"));
        UIManager uiManager = FindAnyObjectByType<UIManager>();
        if (uiManager != null)
        {
            uiManager.ShowGameOverScreen();
        }
    }

    public void GetShot()
    {
        if (!isShot)
        {
            isShot = true;
            rb.velocity = Vector2.zero;


            trooperWithParachute.SetActive(false);
            trooperWithoutParachute.SetActive(false);
            trooperDeath.SetActive(true);


            Destroy(gameObject, 2f);
        }
    }

}
