require("dotenv").config();
const express = require('express');


const app = express();
const bodyParser = require('body-parser');


app.use(bodyParser.urlencoded({ extended: false }))
app.use(express.json());

// Setup DB
const mongoose = require('mongoose');
mongoose.set('strictQuery', false);

mongoose.connect(process.env.mongoURI, { useNewUrlParser: true, useUnifiedTopology: true }, async (err, client) => {
    if (err) {
        console.error(err);
        return;
    }
});

//Setup database models
require('./model/Account');

//setup routes
require('./routes/authenticationRoutes')(app);
require('./routes/backlistRoutes')(app);

let port = process.env.port;
app.listen(port, () => {
    console.log("Listening on " + port)
})