# ApiCazarecompensa
Api para la App cazaRecompensas

Para usar en el ambiente local con un device real apuntar con la ip de la pc. Ambos dispositivos deben estar en la misma red.

Para usar en el ambiente local con un device emulado, apuntar a la dirección 10.0.2.2

# Endpoints


### Login
```sh
Registrar usuario: POST api/login/registrarUsuario (Se envia por post un usuario de tipo Usuario)
```

### Publicaciones
```sh
Obtener todos las publicaciones: GET  api/publicaciones/obtener
Obtener publicación por IdPublicacion: GET api/publicaciones/obtenerporid/{id} donde id: int
Eliminar publicacion por IdPublicacion: DELETE api/publicaciones/eliminarporid/{id} donde id: int
Guardar publicación: POST api/publicaciones/guardar (se le pasa una publicación de tipo Publicacion)
```

### Usuario
```sh
Obtener todos los usuarios: GET api/usuarios/obtener
Obtener usuario por IdUsuario: GET api/usuarios/obtenerporid/{id}  donde id: int
Elminar usuario por IdUsuario: DELETE api/usuarios/eliminarporid/{id} donde id:int
Guardar usuario: POST api/usuarios/guardar (se le pasa un usuario de tipo Usuario)
```

### Tesoros
```sh
Obtener todos los tesoros: GET  api/tesoros/obtener
Obtener tesoro por IdTesoro: GET api/tesoros/obtenerporid/{id} donde id: int
Eliminar tesoro por IdTesoro: DELETE api/tesoros/eliminarporid/{id} donde id: int
```



