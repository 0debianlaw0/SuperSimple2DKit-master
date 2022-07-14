using System.Collections;
using System;
using System.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.SqliteClient;

/*Allows buttons to fire various functions, like QuitGame and LoadScene*/

public class MenuHandler : MonoBehaviour {

	[SerializeField] private string whichScene;
    public string vida;
    public string monedas;
    public string posicionx;
    public string posiciony;
    public Text monedasNum;
    public Text posicionXNum;
    public Text posicionYNum;
    public Text vidaNum;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(whichScene);
    }

    private void Start()
    {
        monedasNum = GameObject.Find("MonedasNum").GetComponent<Text>();
        posicionXNum = GameObject.Find("XNum").GetComponent<Text>();
        posicionYNum = GameObject.Find("YNum").GetComponent<Text>();
        vidaNum = GameObject.Find("VidaNum").GetComponent<Text>();
        IDbConnection conexion = new SqliteConnection("URI=file:" + Application.dataPath + "/Plugins/Juego2DSQL.db");
        conexion.Open();

        IDbCommand cmd = conexion.CreateCommand();
        cmd.CommandText = "SELECT * FROM DATOS";
        IDataReader datos = cmd.ExecuteReader();

        while (datos.Read())
        {
            vida = datos.GetString(0);
            monedas = datos.GetString(1);
            posicionx = datos.GetString(2);
            posiciony = datos.GetString(3);
        }
        vidaNum.text = vida;
        monedasNum.text = monedas;
        posicionXNum.text = posicionx;
        posicionYNum.text = posiciony;
        conexion.Close();
    }
}
