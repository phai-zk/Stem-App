const bcrypt = require('bcrypt');
const mongoose = require("mongoose");
const Account = mongoose.model("UserData");

module.exports = (app) => {

    app.post("/account/remove/:username/:password", async (req, res) => {

        let username = req.params.username;
        let password = req.params.password;

        try {

            const userAccount = await Account.findOne({ username: username });
            if (!userAccount) {
                res.send("Can't find this account.");
                return;
            }

            bcrypt.compare(password, userAccount.password, async (err, result) => {
                if (err) throw err;
                if (result) {

                    await Account.deleteOne({_id: userAccount._id});
                    res.send("remove complete!");

                }
            })

        } catch (error) {

        }

    })
} 