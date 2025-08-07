const mongoose = require('mongoose');

const usuarioSchema = new mongoose.Schema({
  nombre: { type: String, required: true },
  email: { type: String, required: true },
  uID: { type: String, required: true, unique: true },
  telefono: { type: String, required: false },  // asegúrate que no sea required
  rol: { type: String, required: false }        // lo mismo aquí
});

module.exports = mongoose.model('Usuario', usuarioSchema);
