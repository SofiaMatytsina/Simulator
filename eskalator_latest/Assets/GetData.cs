using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.IO;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;

public class GetData : MonoBehaviour
{
    public Positions pos;

    Thread mThread;
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 25001;
    IPAddress localAdd;
    TcpListener listener;
    TcpClient client;
    int receivedPulse;
    int receivedOxy;

    bool running;
    int num_i = 0;

    public Text pulseText;
    public Text oxygenText;

    private string dbName = "URI=file:db_PulseOxy.db3";

    
    void FixedUpdate()
    {
        string pulse = receivedPulse.ToString();
        string oxygen = receivedOxy.ToString();
        pulseText.text = pulse;
        oxygenText.text = oxygen;
        
    }

    private void Start()
    {
        ThreadStart ts = new ThreadStart(GetInfo);
        mThread = new Thread(ts);
        mThread.Start();
        CreateDB();
    }

    void GetInfo()
    {
        try
        {
            localAdd = IPAddress.Parse(connectionIP);
            listener = new TcpListener(IPAddress.Any, connectionPort);
            listener.Start();
            byte[] buffer = new byte[client.ReceiveBufferSize];
            string dataReceivedP;
            string dataReceivedO;

            while (true)
            {
                Debug.Log("Waiting for a connection... ");

                client = listener.AcceptTcpClient();
                Debug.Log("Connected!");

                NetworkStream nwStream = client.GetStream();

                int bytesRead;
                int bytesRead1;
                //---receiving Data from the Host----
                bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize); //Getting data in Bytes from Python

                bytesRead1 = nwStream.Read(buffer, 0, client.ReceiveBufferSize); //Getting data in Bytes from Python

                while ((bytesRead != null) & (bytesRead1 != null))
                {
                    dataReceivedP = Encoding.UTF8.GetString(buffer, 0, bytesRead); //Converting byte data to string
                    dataReceivedO = Encoding.UTF8.GetString(buffer, 0, bytesRead1); //Converting byte data to string
                                                                                    //---Using received data---

                    print("received data, and wrote Pulse and Oxy!");

                    num_i++;

                    receivedPulse = Convert.ToInt32(dataReceivedP);
                    receivedOxy = Convert.ToInt32(dataReceivedO);

                    WriteData();
                    //---Sending Data to Host----
                    byte[] myWriteBuffer = Encoding.ASCII.GetBytes("Hey I got your message Python! Do You see this message?"); //Converting string to byte data
                    nwStream.Write(myWriteBuffer, 0, myWriteBuffer.Length); //Sending the data in Bytes to Python

                    bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize); //Getting data in Bytes from Python

                    bytesRead1 = nwStream.Read(buffer, 0, client.ReceiveBufferSize); //Getting data in Bytes from Python
                }

                // Shutdown and end connection
                client.Close();
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }



    }

    void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS Data1 (Number INT, Name VARCHAR(20), Position VARCHAR(20), HeartBeatLevel INT, OxygenLevel INT)";
                command.ExecuteNonQuery();
                Debug.Log("Создана бд");
            }
            connection.Close();
        }
    }

    void WriteData()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Data1 VALUES ('" + num_i + "', '" + New_Name.NewName + "', '" + pos.position + "', '" + receivedPulse + "', '" + receivedOxy + "')";
                command.ExecuteNonQuery();
                Debug.Log("Добавлены данные");
            }
            connection.Close();
        }
    }
}
