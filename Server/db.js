const mongoose = require('mongoose');

mongoose.connect('mongodb+srv://ark:arkdbPassword@cluster0.lsoekhq.mongodb.net/test?retryWrites=true&w=majority');


const db = mongoose.connection;

db.on('error', console.error.bind(console, 'connection error:'));
db.once('open', () => {
  console.log('Connected to MongoDB');
});

module.exports = db;