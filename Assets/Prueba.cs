using UnityEngine;
using Mono.Data.SqliteClient;
using System.Data;
using System;

public class Prueba : MonoBehaviour
{

    public string vida;
    public string vidaT;
    public string monedas;
    public string monedasT;
    public string posicionX;
    public string posicionXT;
    public string posicionY;
    public string posicionYT;
    NewPlayer player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<NewPlayer>();

        IDbConnection conexion = new SqliteConnection("URI=file:" + Application.dataPath + "/Plugins/Juego2DSQL.db");
        conexion.Open();

        IDbCommand cmd = conexion.CreateCommand();
        cmd.CommandText = "SELECT * FROM DATOS";
        IDataReader datos = cmd.ExecuteReader();

        while (datos.Read())
        {
            string vida = datos.GetString(0);
            string monedas = datos.GetString(1);
            string posicionx = datos.GetString(2);
            string posiciony = datos.GetString(3);
            Debug.Log("Vida: " + vida + " Monedas: " + monedas + "PosicionX:" + posicionx + "PosicionY:" + posiciony);
        }
        conexion.Close();

        //También puedes utilizar esta otra nomenclatura si te resulta más cómodo... 
    }
    void Guardar()
    {
        SqliteConnection conexion = new SqliteConnection("URI=file:" + Application.dataPath + "/Plugins/Juego2DSQL.db");
        conexion.Open();
        Debug.Log("Conectado a la base de datos:" + conexion.ConnectionString);
        Debug.Log("Guardando");

        SqliteCommand cmdGuardado = new SqliteCommand("INSERT INTO DATOS (Vida, Monedas, PosicionX, PosicionY) VALUES (" + vida + ", " + monedas + ", " + posicionX + ", " + posicionY + ")", conexion);
        Debug.Log(cmdGuardado.CommandText);
        cmdGuardado.ExecuteNonQuery();
        
        conexion.Close();
    }

    //void TriggerAAAAA()
    //{
        //SqliteConnection conexion = new SqliteConnection("URI=file:" + Application.dataPath + "/Plugins/Juego2DSQL.db");
        //conexion.Open();
        //string time = DateTime.Now.ToString();
        //Debug.Log("Guardado");
        //SqliteCommand MAMAAAAAAAAAA = new SqliteCommand("CREATE TRIGGER save AFTER INSERT ON DATOS BEGIN INSERT INTO FECHA(Fecha) VALUES(" + time + "); END; ");
        //MAMAAAAAAAAAA.ExecuteNonQuery();
        //Debug.Log("SUUUUUUUUUU");
        //conexion.Close();
    //}
    private void Update()
    {
        vida = Convert.ToString(player.health);
        monedas = Convert.ToString(player.coins);
        string postx = Convert.ToString(player.transform.position.x);
        string posty = Convert.ToString(player.transform.position.y);
        posicionX = Convert.ToString(Math.Round(float.Parse(postx)));
        posicionY = Convert.ToString(Math.Round(float.Parse(posty)));

        if (Input.GetKeyDown(KeyCode.G))
        {
            Guardar();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restore();
        }
    }
    public void Restore()
    {
        IDbConnection conexion = new SqliteConnection("URI=file:" + Application.dataPath + "/Plugins/Juego2DSQL.db");
        conexion.Open();

        IDbCommand cmd = conexion.CreateCommand();
        cmd.CommandText = "SELECT * FROM DATOS";
        IDataReader datos = cmd.ExecuteReader();

        while (datos.Read())
        {
            vidaT = datos.GetString(0);
            monedasT = datos.GetString(1);
            posicionXT = datos.GetString(2);
            posicionYT = datos.GetString(3);
            Debug.Log("Vida: " + vidaT + " Monedas: " + monedasT + " PosicionX: " + posicionXT + " PosicionY: " + posicionYT);
        }

        player.health = Convert.ToInt32(vidaT);
        player.coins = Convert.ToInt32(monedasT);
        player.transform.position = new Vector3(float.Parse(posicionXT), float.Parse(posicionYT), 0f);
    }
}
