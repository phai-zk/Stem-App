const bcrypt = require('bcrypt');
const mongoose = require("mongoose");
const Account = mongoose.model("UserData");

module.exports = (app) => {

  // Sign in Account ----------------------------------------------------------
  app.post("/account/createAccount", async (req, res) => {
    const { email, username, password } = req.body;
    let notEnough_Info_Error = [];
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    try {
      console.log(emailRegex.test(email));
      if (email && !emailRegex.test(email)) {
        res.send("Please, Type the true email.")
        return;
      }

      if (username) {
        let checkUser = await Account.findOne({ username: username });
        if (checkUser) {
          res.send("Username is already taken.")
          return;
        }
      }

      if (!email || !username || !password) {
        if (!email) {
          notEnough_Info_Error.push("Email");
        }
        if (!username) {
          notEnough_Info_Error.push("Username");
        }
        if (!password) {
          notEnough_Info_Error.push("Password");
        }

        res.send(`Please, Type the ${notEnough_Info_Error.join(", ")}.`);
        return;
      }

      bcrypt.hash(password, 10, (err, hash) => {

        if (err) throw err;
        else {
          CreateAccount(hash)
        }

      })

      const CreateAccount = async (hashPassword) => {

        let defaultData = {
          email: email,
          username: username,
          password: hashPassword,
          data: {},
          lastAuthentication: Date.now()
        };


        let newAccount = await Account.create(defaultData)
        res.send({ data: newAccount.username, message: "create complete!" })

      }

    } catch (error) {
      res.send(error.error)
    }
  });

  // Log in Account ----------------------------------------------------------
  app.post("/account/Login", async (req, res) => {

    const { username, password } = req.body;
    let notEnough_Info_Error = [];

    try {

      if (!username || !password) {
        if (!username) {
          notEnough_Info_Error.push("Username");
        }
        if (!password) {
          notEnough_Info_Error.push("Password");
        }
        res.send(`Please, Type the ${notEnough_Info_Error.join(", ")}.`);
        return;
      }

      let userAccount = await Account.findOne({ username: username })
      if (!userAccount) {
        res.send("Sorry you don't have an account.")
        return;
      }
      else {
        bcrypt.compare(password, userAccount.password, (err, result) => {
          if (err) throw err;
          else {

            let data = { data: userAccount, message: "Login Complete!" };
            res.send((!result)? "Password is wrong.": data);

          }
        })
      }

    } catch (error) {
      res.send(error.error);
    }
  })

}