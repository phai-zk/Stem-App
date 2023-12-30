const firebase = require("firebase-functions");
const express = require('express');
const app = express();

// Setup DB
const mongoose = require('mongoose');
mongoose.set('strictQuery', false);

const mongoURI = "mongodb+srv://StemM6:1a2s3d@cluster0.pkc5nnu.mongodb.net/UserAccount";

mongoose.connect(mongoURI, { useNewUrlParser: true, useUnifiedTopology: true })
    .then(() => {
        console.log("MongoDB connected successfully");
    })
    .catch((err) => {
        console.log("Connect mongoDB Error");
        console.error(err);
    });

// Setup database models
require('./model/Account.js');

// Setup routes
require('./routes/authenticationRoutes')(app);
require('./routes/backlistRoutes')(app);
require('./routes/gameDataRoutes')(app);

app.get("/", (req, res) => {
    res.send("Hello World");
});

exports.app = firebase.https.onRequest(app);
