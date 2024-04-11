using System.ComponentModel;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class AlienVuela : MonoBehaviour
{
    private Animator animator;
    public bool isAnimated = false;

    public bool isRotating = false;
    public bool isFloating = false;
    public bool isScaling = false;

    public Vector3 rotationAngle;
    public float rotationSpeed;

    public float floatSpeed;
    private bool goingUp = true;
    public float floatRate;
    private float floatTimer;
    
    public Vector3 startScale;
    public Vector3 endScale;

    private bool scalingUp = true;
    public float scaleSpeed;
    public float scaleRate;
    private float scaleTimer;

    public Camera camara;
    bool animacionActiva = false;
    public AudioClip mareo;
    public AudioClip cohete;


    void Start()
    {
        // Obtener el componente Animator del objeto
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CambiarAnimacion("rig|SpaceGuyPose_ZeroGravity_1");
            animacionActiva = true;
            transform.position = new Vector3(-7.103f, 6.060875f, -30.312f);
            camara.transform.position = new Vector3(-7.343821f, 6.620454f, -27.85598f);
            GetComponent<AudioSource>().clip = mareo; 
            GetComponent<AudioSource>().Play();
        }

        if (animacionActiva)
        {
            // Realiza las acciones de animación
            if(isAnimated)
            {
                if(isRotating)
                {
                    transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);
                }

                if(isFloating)
                {
                    floatTimer += Time.deltaTime;
                    Vector3 moveDir = new Vector3(0.0f, floatSpeed, 0.0f);
                    transform.Translate(moveDir);

                    if (goingUp && floatTimer >= floatRate)
                    {
                        goingUp = false;
                        floatTimer = 0;
                        floatSpeed = -floatSpeed;
                    }
                    else if (!goingUp && floatTimer >= floatRate)
                    {
                        goingUp = true;
                        floatTimer = 0;
                        floatSpeed = +floatSpeed;
                    }
                }

                if(isScaling)
                {
                    scaleTimer += Time.deltaTime;

                    if (scalingUp)
                    {
                        transform.localScale = Vector3.Lerp(transform.localScale, endScale, scaleSpeed * Time.deltaTime);
                    }
                    else if (!scalingUp)
                    {
                        transform.localScale = Vector3.Lerp(transform.localScale, startScale, scaleSpeed * Time.deltaTime);
                    }

                    if(scaleTimer >= scaleRate)
                    {
                        if (scalingUp) { scalingUp = false; }
                        else if (!scalingUp) { scalingUp = true; }
                        scaleTimer = 0;
                    }
                }
            }
        }
        // Cambiar a la segunda animación cuando se presiona la tecla "2"
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animacionActiva = false;
            CambiarAnimacion("rig|SpaceGuyPose_Standing");
            transform.position = new Vector3(-9.543f, 7.37f, -33.427f);
            transform.rotation = new Quaternion(0f,0f,0f,0f);
            GetComponent<AudioSource>().clip = cohete; 
            GetComponent<AudioSource>().Play();
            camara.transform.position = new Vector3(-9.465719f, 8.143775f, -28.78481f);
            camara.transform.rotation = new Quaternion(18.3588505f,180.281967f,-18.3588505f,0f);
        }
    }

    void CambiarAnimacion(string nombreAnimacion)
    {
        // Si el componente Animator no está asignado, salir del método
        if (animator == null)
        {
            Debug.LogWarning("El componente Animator no está asignado.");
            return;
        }

        // Cambiar a la animación especificada por nombre
        animator.Play(nombreAnimacion);
    }
}