using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    // Liczba skoków gracza
    private int maxJumps = 1; // Maksymalna liczba skoków (1 skok na ziemi + 1 w powietrzu)
    private int currentJumps = 0; // Liczba wykonanych skoków
    private float defaultSpeed;    // Domyœlna prêdkoœæ ruchu
    private float defaultHeight;   // Domyœlna wysokoœæ postaci

    public float speed = 6f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    public float sprintSpeed = 24f; // Prêdkoœæ podczas sprintu
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float sneakSpeed = 3f; // Prêdkoœæ podczas skradania siê
    public float crouchSpeed = 3f; // Prêdkoœæ podczas kucania
    public float crouchHeight = 0.5f; // Wysokoœæ postaci podczas kucania

    

    public int damageToInflict = 20;
    Vector3 velocity;

    bool isGrounded;
    bool isCrouching = false; // Flaga okreœlaj¹ca, czy postaæ kuca

    void Start()
    {
        // Zapisanie domyœlnej prêdkoœci na pocz¹tku gry
        defaultSpeed = speed;

        // Zapisanie domyœlnej wysokoœci postaci
        defaultHeight = controller.height;

        // Zapisanie domyœlnego rozmiaru postaci (normalna wysokoœæ)
        transform.localScale = new Vector3(1f, 1f, 1f); // Normalny rozmiar postaci

        // Pocz¹tkowy stan kucania (postaæ nie kuca na pocz¹tku)
        isCrouching = false;

        // Ustawienie domyœlnej prêdkoœci ruchu
        speed = defaultSpeed;

        // Ustawienie domyœlnej wysokoœci postaci
        controller.height = defaultHeight;

        // Ustawienia zwi¹zane z kucaniem
        crouchSpeed = 3f; // Prêdkoœæ podczas kucania
        crouchHeight = 0.5f; // Wysokoœæ postaci podczas kucania

        // Ustawienia zwi¹zane z poruszaniem siê
        sprintSpeed = 24f; // Prêdkoœæ podczas sprintu
        sneakSpeed = 3f; // Prêdkoœæ podczas skradania siê
        jumpHeight = 3f; // Wysokoœæ skoku
        gravity = -9.81f * 2; // Grawitacja
    }


    // Update wywo³ywane raz na klatkê
    void Update()
    {
        // Sprawdzamy, czy postaæ znajduje siê na ziemi, aby zresetowaæ prêdkoœæ opadania
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            currentJumps = 0;
        }

        // Sprawdzamy, czy postaæ kuca pod przyciskiem "C"
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching; // Zmieniamy stan kucania
            if (isCrouching)
            {
                // Zmniejszamy wysokoœæ postaci podczas kucania
                controller.height = crouchHeight;
                transform.localScale = new Vector3(1f, 0.5f, 1f); // Zmiana rozmiaru postaci (wysokoœæ)
                speed = crouchSpeed; // Zmniejszamy prêdkoœæ podczas kucania
            }
            else
            {
                // Przywracamy domyœln¹ wysokoœæ postaci
                controller.height = defaultHeight;
                transform.localScale = new Vector3(1f, 1f, 1f); // Przywracamy normalny rozmiar postaci
                speed = defaultSpeed; // Przywracamy domyœln¹ prêdkoœæ
            }
        }

        // Je¿eli nie kucamy, pozwalamy na zmianê prêdkoœci
        if (!isCrouching)
        {
            // Sprawdzenie, czy klawisz sprintu (np. lewy Shift) jest wciœniêty
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = sprintSpeed; // Zwiêkszenie prêdkoœci
            }
            else if (Input.GetKey(KeyCode.LeftControl)) // Skradanie siê
            {
                speed = sneakSpeed; // Zmniejszenie prêdkoœci podczas skradania
            }
            else
            {
                speed = defaultSpeed; // Powrót do domyœlnej prêdkoœci
                controller.height = defaultHeight; // Powrót do domyœlnej wysokoœci
            }
        }
        // Sprawdzenie, czy klawisz sprintu (np. lewy Shift) jest wciœniêty
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed; // Zwiêkszenie prêdkoœci
        }
        else if (Input.GetKey(KeyCode.LeftControl)) // Skradanie siê
        {
            speed = sneakSpeed; // Zmniejszenie prêdkoœci podczas skradania
        }
        else
        {
            speed = defaultSpeed; // Powrót do domyœlnej prêdkoœci
            controller.height = defaultHeight; // Powrót do domyœlnej wysokoœci
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Ruch w prawo to oœ czerwona, a w przód to oœ niebieska
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Sprawdzamy, czy postaæ stoi na ziemi
        if (isGrounded)
        {
            // Resetujemy liczbê skoków, gdy gracz dotknie ziemi
            currentJumps = 0;
        }

        if (Input.GetButtonDown("Jump") && currentJumps < maxJumps)
        {
            // Zwiêkszamy licznik skoków
            currentJumps++;

            // Obliczamy prêdkoœæ w pionie dla skoku
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}