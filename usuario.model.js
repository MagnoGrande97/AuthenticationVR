// usuario.model.js
const mongoose = require('mongoose');

const usuarioSchema = new mongoose.Schema({
  nombre: { type: String, required: true },
  email: { type: String, required: true },
  uid: { type: String, required: true, unique: true }, // 👈 debe llamarse `uid`
  telefono: { type: String, required: false },
  rol: { type: String, required: false }
});

module.exports = mongoose.model('Usuario', usuarioSchema);
