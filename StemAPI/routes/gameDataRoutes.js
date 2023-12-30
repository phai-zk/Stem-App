const mongoose = require("mongoose");
const Account = mongoose.model("UserData");

module.exports = (app) => {

    app.post("/data/SaveData/:username", async (req, res) => {
        
        let rUserName = req.params.username;
        const { rData } = req.body;

        if (!rData)
            return res.send("Error : Not enough info");

        let userAccount = await Account.findOne({ username: rUserName })
        
        if (!userAccount)
            return res.send(`Error : Can't find ${rUserName} account`);

        try {

            console.log(rData);
            res.send(rData)
            
            
        } catch (error) {
            res.send(`Error : ${error}`)
        }
    });

}