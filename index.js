require('dotenv').config();
const express = require('express');
const mongoose = require('mongoose');
const Usuario = require('./usuario.model');
const app = express();

app.use(express.json());

mongoose.connect(process.env.MONGO_URI)
  .then(() => console.log('Conectado a MongoDB'))
  .catch((error) => console.error('Error de conexión a MongoDB:', error));

app.post('/guardar-usuario', async (req, res) => {
  try {
    const { nombre, email, telefono, uid } = req.body;
    if (!nombre || !email || !telefono || !uid) {
      return res.status(400).json({ error: 'Faltan campos requeridos' });
    }

    let usuario = await Usuario.findOne({ uid });
    if (!usuario) {
      usuario = new Usuario({ nombre, email, telefono, uid });
      await usuario.save();
    }

    res.json(usuario);
  } catch (error) {
    res.status(500).json({ error: 'Error interno del servidor' });
  }
});

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => console.log(`Servidor corriendo en puerto ${PORT}`));