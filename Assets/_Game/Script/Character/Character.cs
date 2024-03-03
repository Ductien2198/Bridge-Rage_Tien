using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ColorObject
{
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected LayerMask stepLayer;

    private List<PlayerBrick> playerBricks = new List<PlayerBrick>();
    [SerializeField] protected PlayerBrick playerBrickPrefab;
    [SerializeField] protected Transform brickHolder;

    [HideInInspector] public Stage stage;
    [SerializeField] protected Transform playerSkin;

    public Animator anim;
    private string currentAnim;

    public int BrickCount => playerBricks.Count;

    //protected virtual void Start()
    //{
    //    ChangeColor(colorType);
    //}

    public virtual void OnInit()
    {
        ClearBrick();
        playerSkin.rotation = Quaternion.identity;
    }

    public Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 1.1f;
        }

        return TF.position;

    }

    public bool CanMove(Vector3 nextPoint)
    {
        bool isCanmove = true;

        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, stepLayer))
        {
            Set_Step step = hit.collider.GetComponent<Set_Step>();
            if (step.colorType != colorType && playerBricks.Count > 0)
            {
                step.ChangeColor(colorType);
                RemoveBrick();
                stage.NewBrick(colorType);
            }
            if (step.colorType != colorType && playerBricks.Count == 0 && playerSkin.forward.z > 0)
            {
                isCanmove = false;
            }
        }

        return isCanmove;
    }

    public void AddBrick()
    {
        int index = playerBricks.Count;

        PlayerBrick playerBrick = Instantiate(playerBrickPrefab, brickHolder);
        playerBrick.ChangeColor(colorType);
        playerBrick.TF.localPosition = index * 0.25f * Vector3.up;
        playerBricks.Add(playerBrick);
    }

    public void RemoveBrick()
    {
        int index = playerBricks.Count - 1;

        if (index >= 0)
        {
            PlayerBrick playerBrick = playerBricks[index];
            playerBricks.Remove(playerBrick);
            Destroy(playerBrick.gameObject);
        }

    }

    public void ClearBrick()
    {
        for (int i = 0; i < playerBricks.Count; i++)
        {
            Destroy(playerBricks[i].gameObject);
        }

        playerBricks.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BRICK))
        {
            Brick brick = Cache.GetBrick(other);
            if (brick.colorType == colorType)
            {
                brick.OnDespawn();
                AddBrick();
                Destroy(brick.gameObject);
            }
        }
    }

    public void ChangeAnim(string animName)
    {
        if(currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
}
