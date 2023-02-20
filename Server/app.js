const Score = require('./models/score');
const express = require("express");

const app = express();

app.use(express.json());
app.use(express.urlencoded({ extended: false }));

function GetScoreSortOrder() {    
  return function(a, b) {    
    const aVal = parseInt(a["score"]);
    const bVal = parseInt(b["score"]);
      if (aVal > bVal) {    
        console.log(aVal + " > " + bVal);
          return -1;    
      } else if (aVal < bVal) {    
        console.log(aVal + " < " + bVal);
          return 1;    
      }    
      return 0;    
  }    
}   

app.get("/score", async (req, res) => {
  const scores = await Score.find();
  const sortedScores = scores.sort(GetScoreSortOrder());
  res.status(200).json(sortedScores);
});

app.post("/score", (req, res) => {
  var obj = JSON.parse(JSON.stringify(req.body));
  console.log(obj);
  Score.insertMany(obj, (err, scores) => {
    if (err) return res.status(400).send(err);
    res.status(201).json(scores[0]);
  });
  return res.status(201);
});

function logErrors(err, req, res, next) {
  console.error(err.stack)
  next(err)
}
app.use(logErrors)
module.exports = app;