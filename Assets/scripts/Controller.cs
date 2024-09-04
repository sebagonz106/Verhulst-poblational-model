using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controller : MonoBehaviour
{
    [SerializeField] TMP_InputField initialValue;
    [SerializeField] TMP_InputField parameter;
    [SerializeField] TMP_Text error;
    [SerializeField] TMP_Text[] resultsDisplay;
    [SerializeField] GameObject main;
    [SerializeField] GameObject graph;
    [SerializeField] GameObject results;
    [SerializeField] GameObject dot;
    double[] values;
    GameObject[] dots;
    Vector2 coordinatesCenter = new Vector2(-155.2f, -45.4f);
    (float,float) enhance = (3.5f, 3.5f * 37.5f);

    public void Calculate(bool graph = true)
    {
        if (Validate())
        {
            values = Program.VerhulstModel(initialValue.text, parameter.text);
            main.SetActive(false);

            if (graph) ActivateGraph();
            else ActivateResults();
        }
    }

    public bool Validate()
    {
        if (!Program.Validate(initialValue.text, 0, 1))
        {
            initialValue.text = "";
            error.text = "El valor inicial debe ser un número entre 0 y 1";
        }
        else if (!Program.Validate(parameter.text, 0, 4))
        {
            parameter.text = "";
            error.text = "El parámetro debe ser un número entre 0 y 4";
        }
        else
        {
            error.text = "";
            return true;
        }

        return false;
    }

    void ActivateGraph()
    {
        graph.SetActive(true);
        dots = new GameObject[values.Length];

        for (int i = 0; i < values.Length; i++)
        {
            dots[i] = Instantiate(dot, 
                                  new Vector3(coordinatesCenter.x + (float)(i*enhance.Item1), coordinatesCenter.y +(float)(values[i] * enhance.Item2)), 
                                  new Quaternion(), 
                                  graph.transform);
        }
    }
    void ActivateResults()
    {
        results.SetActive(true);
        for (int i = 0; i < resultsDisplay.Length; i++)
        {
            resultsDisplay[i].text = "";
            for (int k = 1; k <= 20; k++)
            {
                resultsDisplay[i].text += $"{20*i+k}. {System.Math.Round(values[20 * i + k - 1], 12)}\n";
            }
        }
    }
    public void DeactivateGraph()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            Destroy(dots[i]);
        }
        dots = new GameObject[0];
        main.SetActive(true);
        graph.SetActive(false);
    }
    public void DeactivateResult()
    {
        for (int i = 0; i < resultsDisplay.Length; i++)
        {
            resultsDisplay[i].text = "";
        }
        main.SetActive(true);
        results.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
