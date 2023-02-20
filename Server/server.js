const app = require("./app");

require('./db');

// Start the server
app.listen(3000, () => {
  console.log("Server started on port 3000");
});
