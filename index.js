require('dotenv').config();
const express = require('express');
const mongoose = require('mongoose');
const Usuario = require('./usuario.model');
const cors = require('cors'); // ✅ Importante para permitir peticiones desde Unity

const app = express();

// ✅ Middlewares
app.use(cors()); // Permitir CORS (Unity lo necesita)
app.use(express.json()); // Parsear JSON

// ✅ Conexión a MongoDB
mongoose.connect(process.env.MONGO_URI, {
  useNewUrlParser: true,
  useUnifiedTopology: true,
})
  .then(() => console.log('✅ Conectado a MongoDB'))
  .catch((error) => console.error('❌ Error de conexión a MongoDB:', error));

// ✅ Ruta para guardar usuario
app.post('/guardar-usuario', async (req, res) => {
  try {
    const { email, nombre, uID } = req.body;

    if (!email || !nombre || !uID) {
      return res.status(400).json({ error: 'Faltan campos requeridos' });
    }

    let usuario = await Usuario.findOne({ uID });

    if (!usuario) {
      usuario = new Usuario({ nombre, email, uID });
      await usuario.save();
      console.log('✅ Usuario nuevo guardado:', usuario);
    } else {
      console.log('ℹ️ Usuario ya existente:', usuario);
    }

    res.json({ success: true, usuario });
  } catch (error) {
    console.error('❌ Error al guardar usuario:', error);
    res.status(500).json({ error: 'Error interno del servidor' });
  }
});

// ✅ Iniciar servidor
const PORT = process.env.PORT || 3000;
app.listen(PORT, () => console.log(`🚀 Servidor corriendo en puerto ${PORT}`));
