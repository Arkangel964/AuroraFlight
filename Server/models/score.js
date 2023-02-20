const mongoose = require('mongoose');

const scoreSchema = new mongoose.Schema({
  name: { type: String, required: true },
  score: { type: String, required: true },
  date: { type: Date, default: Date.now }
});


const Score = mongoose.model('score', scoreSchema);

module.exports = Score;