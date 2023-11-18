const express = require('express');
const mongoose = require('mongoose');
const mqtt = require('mqtt');

const app = express();
const port = 3000;

// mongoose.connect('mongodb+srv://StemApp:1a2s3d4f5g6h@cluster0.xbm1ymk.mongodb.net/StemData', {
//   useNewUrlParser: true,
//   useUnifiedTopology: true,
// });

// MQTT ----------------------------------------------------------------
// MQTT broker details
const brokerUrl = 'mqtt://Stem:1a2s3d4f5g6h@broker.emqx.io:1883/mqttx_773d63b3';

// Create an MQTT client
const mqttClient = mqtt.connect(brokerUrl);

// API endpoint to publish a message
app.post('/publish/:topic/:message', express.json(), (req, res) => {
  const topic = req.params.topic;
  const message = req.params.message;
  if (!topic || !message) {
    return res.status(400).json({ success: false, message: 'Topic and message are required' });
  }

  // Publish the message to the MQTT broker
  mqttClient.subscribe(topic, (err) => {
    if (!err) {
      console.log(`Subscribed to topic: ${topic}`);
      mqttClient.publish(topic, message, { qos: 1 });
      res.json({ success: true, message: `${topic} : ${message}` });
    } else {
      console.error(`Failed to subscribe to topic: ${topic}`);
    }
  });

});

app.get('/receive', (req, res) => {
  res.json({ message: lastReceivedMessage });
});

let lastReceivedMessage = '';

// Event handler for received messages
mqttClient.on('message', (receivedTopic, message) => {
  console.log(`Received message from ${receivedTopic}: ${message.toString()}`);
  lastReceivedMessage = message.toString();
});

mqttClient.on('connect', () => {
  console.log("mqtt connect");
})

// Handle errors
mqttClient.on('error', (err) => {
  console.error('MQTT error:', err);
});

// Handle disconnects
mqttClient.on('close', () => {
  console.log('Disconnected from MQTT broker');
});

// Handle unsubscribes
mqttClient.on('unsubscribe', (topic) => {
  console.log(`Unsubscribed from topic: ${topic}`);
});

// Handle unsubscribes
mqttClient.on('end', () => {
  console.log('Connection to MQTT broker closed');
});
// MQTT ----------------------------------------------------------------

// Start the API server
app.listen(port, () => {
  console.log(`API server is running on port ${port}`);
});