using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    // Liczba skok�w gracza
    private int maxJumps = 1; // Maksymalna liczba skok�w (1 skok na ziemi + 1 w powietrzu)
    private int currentJumps = 0; // Liczba wykonanych skok�w
    private float defaultSpeed;    // Domy�lna pr�dko�� ruchu
    private float defaultHeight;   // Domy�lna wysoko�� postaci

    public float speed = 6f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    public float sprintSpeed = 24f; // Pr�dko�� podczas sprintu
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float sneakSpeed = 3f; // Pr�dko�� podczas skradania si�
    public float crouchSpeed = 3f; // Pr�dko�� podczas kucania
    public float crouchHeight = 0.5f; // Wysoko�� postaci podczas kucania

    

    public int damageToInflict = 20;
    Vector3 velocity;

    bool isGrounded;
    bool isCrouching = false; // Flaga okre�laj�ca, czy posta� kuca

    void Start()
    {
        // Zapisanie domy�lnej pr�dko�ci na pocz�tku gry
        defaultSpeed = speed;

        // Zapisanie domy�lnej wysoko�ci postaci
        defaultHeight = controller.height;

        // Zapisanie domy�lnego rozmiaru postaci (normalna wysoko��)
        transform.localScale = new Vector3(1f, 1f, 1f); // Normalny rozmiar postaci

        // Pocz�tkowy stan kucania (posta� nie kuca na pocz�tku)
        isCrouching = false;

        // Ustawienie domy�lnej pr�dko�ci ruchu
        speed = defaultSpeed;

        // Ustawienie domy�lnej wysoko�ci postaci
        controller.height = defaultHeight;

        // Ustawienia zwi�zane z kucaniem
        crouchSpeed = 3f; // Pr�dko�� podczas kucania
        crouchHeight = 0.5f; // Wysoko�� postaci podczas kucania

        // Ustawienia zwi�zane z poruszaniem si�
        sprintSpeed = 24f; // Pr�dko�� podczas sprintu
        sneakSpeed = 3f; // Pr�dko�� podczas skradania si�
        jumpHeight = 3f; // Wysoko�� skoku
        gravity = -9.81f * 2; // Grawitacja
    }


    // Update wywo�ywane raz na klatk�
    void Update()
    {
        // Sprawdzamy, czy posta� znajduje si� na ziemi, aby zresetowa� pr�dko�� opadania
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            currentJumps = 0;
        }

        // Sprawdzamy, czy posta� kuca pod przyciskiem "C"
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching; // Zmieniamy stan kucania
            if (isCrouching)
            {
                // Zmniejszamy wysoko�� postaci podczas kucania
                controller.height = crouchHeight;
                transform.localScale = new Vector3(1f, 0.5f, 1f); // Zmiana rozmiaru postaci (wysoko��)
                speed = crouchSpeed; // Zmniejszamy pr�dko�� podczas kucania
            }
            else
            {
                // Przywracamy domy�ln� wysoko�� postaci
                controller.height = defaultHeight;
                transform.localScale = new Vector3(1f, 1f, 1f); // Przywracamy normalny rozmiar postaci
                speed = defaultSpeed; // Przywracamy domy�ln� pr�dko��
            }
        }

        // Je�eli nie kucamy, pozwalamy na zmian� pr�dko�ci
        if (!isCrouching)
        {
            // Sprawdzenie, czy klawisz sprintu (np. lewy Shift) jest wci�ni�ty
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = sprintSpeed; // Zwi�kszenie pr�dko�ci
            }
            else if (Input.GetKey(KeyCode.LeftControl)) // Skradanie si�
            {
                speed = sneakSpeed; // Zmniejszenie pr�dko�ci podczas skradania
            }
            else
            {
                speed = defaultSpeed; // Powr�t do domy�lnej pr�dko�ci
                controller.height = defaultHeight; // Powr�t do domy�lnej wysoko�ci
            }
        }
        // Sprawdzenie, czy klawisz sprintu (np. lewy Shift) jest wci�ni�ty
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed; // Zwi�kszenie pr�dko�ci
        }
        else if (Input.GetKey(KeyCode.LeftControl)) // Skradanie si�
        {
            speed = sneakSpeed; // Zmniejszenie pr�dko�ci podczas skradania
        }
        else
        {
            speed = defaultSpeed; // Powr�t do domy�lnej pr�dko�ci
            controller.height = defaultHeight; // Powr�t do domy�lnej wysoko�ci
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Ruch w prawo to o� czerwona, a w prz�d to o� niebieska
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Sprawdzamy, czy posta� stoi na ziemi
        if (isGrounded)
        {
            // Resetujemy liczb� skok�w, gdy gracz dotknie ziemi
            currentJumps = 0;
        }

        if (Input.GetButtonDown("Jump") && currentJumps < maxJumps)
        {
            // Zwi�kszamy licznik skok�w
            currentJumps++;

            // Obliczamy pr�dko�� w pionie dla skoku
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}