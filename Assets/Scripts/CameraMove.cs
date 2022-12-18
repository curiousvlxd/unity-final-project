using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private GameObject Sphere;

    private Vector3 cam_sphere;               // вектор від камери до сфери
    private float zoom;                       // наближення/віддалення камери
    private const float MIN_ZOOM  = 0.2f;     // мінімальний коеф. наближення
    private const float MAX_ZOOM  =   2f;     // максимальний (віддалення)
    private const float ZOOM_SENS =  10f;     // чутливість (скролів на 1)

    private float camAngleVertical;           // кут повороту камери по вертикалі
    private const float MAX_VERTICAL  = 75;   // макс. кут повороту камери 
    private const float MIN_VERTICAL  = -50;   // мін. кут повороту камери 
    private const float VERTICAL_SENS = 2;    // чутливість керування поворотом

    private float camAngleHorizontal;         // кут повороту камери по горизонталі
    private const float HORIZONTAL_SENS = 3;  // чутливість керування поворотом


    void Start()
    {
        cam_sphere =                          // Початкове відносне положення
            this.transform.position           // між камерою та сферою
            - Sphere.transform.position;      // 
        zoom = 1;                            
        camAngleVertical =                    // початковий поворот камери
            this.transform.eulerAngles.x;     // беремо зі сцени
        camAngleHorizontal =
            this.transform.eulerAngles.y;
    }

    void Update()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            zoom -= Input.mouseScrollDelta.y / ZOOM_SENS;
            if (zoom < MIN_ZOOM) zoom = MIN_ZOOM;
            if (zoom > MAX_ZOOM) zoom = MAX_ZOOM;
        }
        float mouseY = Input.GetAxis("Mouse Y");     // зрушення, швидкість (не координата курсора)
        camAngleVertical -= mouseY * VERTICAL_SENS;
        if(camAngleVertical < MIN_VERTICAL) camAngleVertical = MIN_VERTICAL;
        if(camAngleVertical > MAX_VERTICAL) camAngleVertical = MAX_VERTICAL;

        camAngleHorizontal += Input.GetAxis("Mouse X") * HORIZONTAL_SENS;
    }

    private void LateUpdate()
    {

        this.transform.position =            // Відстежуємо зміщення Sphere
            Sphere.transform.position        // та корегуємо власне положення
            + Quaternion.Euler(              //  до-обертання вектора для
                0, camAngleHorizontal, 0     //  компенсації повороту камери
            ) * cam_sphere                   // на відносну відстань
              * zoom;                        // враховуємо наближення
                                            
        this.transform.eulerAngles =         // transform.rotation - послідовне обертання
            new Vector3(                     //  .eulerAngles - пряме встановлення кутів
                camAngleVertical,            // кут навколо Х це ефект зрушення по Y
                camAngleHorizontal,          // обертання навколо Y - горизонтальний огляд
                0);                          // 
    }
}
