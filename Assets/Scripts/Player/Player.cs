using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour, IDamageable
{

    private Rigidbody2D _rigid;
    public int diamonds;
    [SerializeField]
    private float _jumpforce = 5.0f;
    [SerializeField]
    private float _speed = 5.0f;

    private bool resetJumpNeeded = false;
    [SerializeField]
    private LayerMask _groundLayer;

    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;
    public bool flameSword =false;

    private bool _grounded = false;


    public int Health { get; set; }



    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;



    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(flameSword);
        Movement();

        if (CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded() == true)
        {
            if(flameSword == true)
                _playerAnim.FlameAttack();
            else
            _playerAnim.Attack();

        }




    }


    void Movement()
    {



        if (transform.position.x < 9.0f && transform.position.y < -4.0f)
        {
            //Debug.Log("---------------------@@@eikhane mara khiso" + transform.position.x);
            StartCoroutine(PlayerGameOver());
        }
        else if(transform.position.x > 80.59f && transform.position.y < -15.0f)
        {
            StartCoroutine(PlayerGameOver());
        }
        else if(transform.position.x >= 105.0f)
        {
            SceneManager.LoadScene("Game_2");
        }
        float move = CrossPlatformInputManager.GetAxis("Horizontal"); //Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();


        Flip(move);


        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) && IsGrounded() == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpforce);
            StartCoroutine(ResetJumpNeededRoutine());
            _playerAnim.Jump(true);


        }

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        _playerAnim.Move(move);


    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, _groundLayer.value);

        if (hitInfo.collider != null)
        {
            if (resetJumpNeeded == false)
            {
                _playerAnim.Jump(false);
                return true;
            }


        }
        return false;

    }

    void Flip(float move)
    {
        if (move > 0)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (move < 0)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;

        }
    }



    IEnumerator ResetJumpNeededRoutine()
    {
        resetJumpNeeded = true;

        yield return new WaitForSeconds(0.1f);

        resetJumpNeeded = false;


    }
    IEnumerator PlayerGameOver()
    {

        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Main_Menu");





    }

    public void Damage()
    {
        if (Health < 1)
        {
            return;
        }

        Health--;
        UIManager.Instance.UpdateLives(Health);
        if (Health >=1)
         _playerAnim.Hit();

        else if (Health<1)
        {
            StartCoroutine(PlayerGameOver());
            _playerAnim.Death();

        }

    }

    public void AddGems(int ammount)
    {
        diamonds += ammount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }

}
 