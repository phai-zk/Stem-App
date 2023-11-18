using System;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using UnityEngine;

public class MqttExample : MonoBehaviour
{
    private IMqttClient mqttClient;

    void Start()
    {
        ConnectToMqttBroker();
    }

    async void ConnectToMqttBroker()
    {
        var factory = new MqttFactory();
        mqttClient = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer("broker.example.com", 1883) // Replace with your broker details
            .Build();

        mqttClient.UseConnectedHandler(e =>
        {
            Debug.Log("Connected to MQTT broker");
            SubscribeToTopic("your/topic");
        });

        mqttClient.UseDisconnectedHandler(e =>
        {
            Debug.Log("Disconnected from MQTT broker");
        });

        await mqttClient.ConnectAsync(options);
    }

    async void SubscribeToTopic(string topic)
    {
        await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());

        mqttClient.UseApplicationMessageReceivedHandler(e =>
        {
            Debug.Log($"Received message from topic {e.ApplicationMessage.Topic}: {e.ApplicationMessage.ConvertPayloadToString()}");
        });
    }

    void OnDestroy()
    {
        if (mqttClient != null && mqttClient.IsConnected)
        {
            mqttClient.DisconnectAsync().Wait();
        }
    }
}
