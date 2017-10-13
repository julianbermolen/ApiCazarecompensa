# ApiCazarecompensa
Api para la App cazaRecompensas

Para usar en el ambiente local con un device real apuntar con la ip de la pc. Ambos dispositivos deben estar en la misma red.

Para usar en el ambiente local con un device emulado, apuntar a la dirección 10.0.2.2

# Endpoints


### Login
Registrar usuario: POST api/login/registrarUsuario (Se envia por post un usuario de tipo Usuario)

### Usuario
Obtener todos los usuarios: GET api/usuarios/obtener
Obtener usuario por IdUsuario: GET api/usuarios/obtenerusuarioporid/{id}  donde id: int

### Tesoros
Obtener todos los tesoros: GET  api/tesoros/obtener
Obtener tesoro por IdTesoro: GET api/tesoros/obtenerporid/{id} donde id: int



