// Account
const mongoose = require('mongoose');
const { Schema } = mongoose;

const accountSchema = new Schema({
    email: String,
    username: String,
    password: String,
    treeData:[
    {
        treeName : String,
        treeModel : String,
        moistureData : String,
        lightData : String,
        tempData : String,
    }],
    lastAuthentication: { type: Date, default: Date.now }
});

mongoose.model('UserData', accountSchema);