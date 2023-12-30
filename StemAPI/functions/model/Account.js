// Account
const mongoose = require('mongoose');
const { Schema } = mongoose;

const accountSchema = new Schema({
    email: String,
    username: String,
    password: String,
    data: {
        treeDatas:[
        {
            treeName : String,
            treeModel : String,
            moistureData : String,
            lightData : String,
            tempData : String,
        }],
        setting:
        {
            noti : Boolean,
            bgm : Boolean,
            plant : Boolean,
        },
    },
    lastAuthentication: { type: Date, default: Date.now }
});

mongoose.model('UserData', accountSchema);