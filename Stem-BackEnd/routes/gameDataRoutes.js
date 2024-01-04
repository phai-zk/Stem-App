const mongoose = require("mongoose");
const Account = mongoose.model("UserData");

module.exports = (app) => {

    app.post("/data/SaveData/:username", async (req, res) => {
        
        let rUserName = req.params.username;
        const { rData } = req.body;

        if (!rData)
            return res.status(500).send("Error : Not enough info");

        let data = JSON.parse(rData);

        try {

            let updateData = await Account.updateOne(
                { username: rUserName },
                {
                    $set: {
                        treeData: data.treeDatas,
                    }
                }
            );

            res.send(updateData.$set)
            
        } catch (error) {
            res.status(500).send(`Error : ${error}`)
        }
    });

    app.get("/data/getData/:username", async (req, res) => {
        
        let rUserName = req.params.username;

        if (!rUserName)
            return res.status(500).send("Error : Not enough info");

        let userAccount = await Account.findOne({ username: rUserName })
        
        if (!userAccount)
            return res.status(500).send(`Error : Can't find ${rUserName} account`);

        try {

            let data = {
                treeData: userAccount.treeData,
                treeCount: userAccount.treeData.length,
                message: "GetDataComplate"
            };
            console.log(data);
            res.send(JSON.stringify(data));
            
        } catch (error) {
            res.status(500).send(`Error : ${error}`)
        }
    });

}