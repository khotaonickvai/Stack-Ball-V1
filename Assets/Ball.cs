using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private int maxFuzzy;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color fuzzyColor;
    private bool isFuzzying;
    private Slider fuzzySlider;
    private TextMeshProUGUI scoreText;
    private float currentEnergy;
    public const float speed = 10f;
    private GameObject Stacks;
    private Material mat;
    private const float energyPenaty = 10f;
    private int score;
    private ParticleSystem[] fireVFX;
    private bool isIngame;
    private Canvas GameoverCanvas;

    private Canvas GameplayCanvas;
    // Start is called before the first frame update
    private void Awake()
    {
        Stacks = GameObject.FindWithTag("Stacks");
        int yStart = Stacks.GetComponent<Stacks>().GetListCount() - 1;
        transform.position = new Vector3(transform.position.x, yStart, transform.position.z);
        fuzzySlider = GameObject.FindWithTag("FuzzySlider").GetComponent<Slider>();
        isFuzzying = false;
        currentEnergy = 0;
        fuzzySlider.minValue = 0;
        fuzzySlider.maxValue = maxFuzzy;
        mat = GetComponentInChildren<Renderer>().material;
        fireVFX = GetComponentsInChildren<ParticleSystem>();
        score = 0;
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        GameoverCanvas = GameObject.FindWithTag("GameoverCanvas").GetComponent<Canvas>();
        GameplayCanvas = GameObject.FindWithTag("GameplayCanvas").GetComponent<Canvas>();
    }

    
    void Start()
    {
        AddScore(0);
        GameoverCanvas.enabled = false;
        GameplayCanvas.enabled = true;
        isIngame = true;
        //fireVFX.Play(true);
        //Debug.Log(fireVFX.gameObject.name);
        //fireVFX.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        move();
        FuzzyHandle();
    }
    private void move()
    {
       
        if (Input.GetKey(KeyCode.A)&& isIngame)
        {
            transform.Translate(Vector3.down * speed * Time.smoothDeltaTime);
            if (isFuzzying)
            {
                currentEnergy -= energyPenaty * Time.deltaTime;
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.FloorToInt(transform.position.y),
                transform.position.z);
            if (currentEnergy >= Mathf.Epsilon)
            {
                currentEnergy -= energyPenaty * Time.deltaTime;
            }
           
        }
    }

    private void FuzzyHandle()
    {
        fuzzySlider.value = Mathf.Lerp(fuzzySlider.value, currentEnergy, 0.2f);
        if (fuzzySlider.value >= maxFuzzy)
        {
            isFuzzying = true;
        }

        if (isFuzzying && currentEnergy <= Mathf.Epsilon)
        {
            isFuzzying = false;
        }

        if (isFuzzying)
        {
            PlayFireFX();
           
            if (mat.color != fuzzyColor)
            {
                mat.color = fuzzyColor;
            }

           
           
        }
        else
        {
           StopFireFX();
          //  fireVFX.Stop(true);
            if (mat.color != normalColor)
            {
                mat.color = normalColor;
                
            }
            
        }
    
    }

    public void AddEnergy()
    {
        currentEnergy++;
    }

    

   

    public bool IsFuzzying()
    {
        return isFuzzying;
    }

    private void PlayFireFX()
    {
        foreach (var fire in fireVFX)
        {
            if (!fire.isPlaying)
            {
                fire.Play(true);
            }
            
        }
    }

    private void StopFireFX()
    {
        foreach (var fire in fireVFX)
        {
            if (fire.isPlaying)
            {
                fire.Stop(true);
            }
            
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "SCORE: " + score;
    }

    public void SetGameOver()
    {
        GameplayCanvas.enabled = false;
        GameoverCanvas.enabled = true;
        isIngame = false;
    }

    public bool IsInGame()
    {
        return isIngame;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
