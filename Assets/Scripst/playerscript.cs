using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityStandardAssets.CrossPlatformInput;

public class playerscript : MonoBehaviour
{
    public float JumpForce;
    float score;

    public Animator animator;

    [SerializeField]
    bool isGrounded = false;
    bool isAlive = true;

    Rigidbody2D RB;

    public Text ScoreTxt;
    public Button StartBtn;
    public Button EndBtn;

    private void OnEnable()
    {
        StartBtn.onClick.AddListener(StartGame);
        //EndBtn.onClick.AddListener(Awake);
    }

    private void OnDisable()
    {
        StartBtn.onClick.RemoveListener(StartGame);
        //EndBtn.onClick.RemoveListener(Awake);
    }

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        score = 0;
        Time.timeScale = 0f;
        EndBtn.gameObject.SetActive(false);
    }

    
    private void StartGame()
    {
        Time.timeScale = 1f;
        score = 0;
        // Hides the button
        StartBtn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
          if(Input.GetMouseButton(0))
          {
                if(isGrounded == true)
                {
                  RB.AddForce(Vector2.up * JumpForce);
                  isGrounded = false;
                  animator.SetBool("Jump", true);
                }

          }

        if (isAlive)
        {
            score += Time.deltaTime * 4;
            ScoreTxt.text = "SCORE : " + score.ToString("F");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      if(collision.gameObject.CompareTag("ground"))
      {
        if(isGrounded == false)
        {
          isGrounded = true;
          animator.SetBool("Jump", false);
        }
      }

      if(collision.gameObject.CompareTag("Spike"))
        {
            isAlive = false;
            Time.timeScale = 0;
            EndBtn.gameObject.SetActive(true);

        }
    }

    public void ResetScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
