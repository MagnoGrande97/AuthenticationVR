const mongoose = require('mongoose');

const usuarioSchema = new mongoose.Schema({
  nombre: { type: String, required: true },
  email: { type: String, required: true },
  uID: { type: String, required: true, unique: true }
});

module.exports = mongoose.model('Usuario', usuarioSchema);
