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
    console.log("Cuerpo recibido:", req.body);

    const { email, nombre, uID, telefono = null, rol = null } = req.body;

    if (!email || !nombre || !uID) {
      return res.status(400).json({ error: 'Faltan campos requeridos' });
    }

    // Buscar por uid (minúscula)
    let usuario = await Usuario.findOne({ uid: uID });

    if (!usuario) {
      // Crear el usuario correctamente
      usuario = new Usuario({
        nombre,
        email,
        uid: uID,       // 👈 CORREGIDO aquí
        telefono,
        rol
      });

      await usuario.save();
    }

    res.json(usuario);
  } catch (error) {
    console.error("Error en /guardar-usuario:", error);
    res.status(500).json({ error: error.message }); // para debug más claro
  }
});

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => console.log(`Servidor corriendo en puerto ${PORT}`));
