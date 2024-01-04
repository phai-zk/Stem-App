// Account
const mongoose = require('mongoose');
const { Schema } = mongoose;

const accountSchema = new Schema({
    email: String,
    username: String,
    password: String,
    treeData:[],
    lastAuthentication: { type: Date, default: Date.now }
});

mongoose.model('UserData', accountSchema);