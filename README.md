# Api Cazarecompensas

![](logo.png)

Api REST para la App cazaRecompensas

Para usar en el ambiente local con un device real apuntar con la ip de la pc. Ambos dispositivos deben estar en la misma red.

Para usar en el ambiente local con un device emulado, apuntar a la dirección 10.0.2.2

# ¿Cómo levantar la api localmente?

1. git clone git@github.com:julianbermolen/ApiCazarecompensa.git
2. cd ApiCazarecompensa
3. dotnet restore
4. cd /api
5. dotnet build
6. dotnet run

# UPDATE:
Ya se encuentra la api en producción: http://li1166-116.members.linode.com/
```sh
Ejemplo: http://li1166-116.members.linode.com/api/comentarios/obtener
```


# Endpoints


### Login
```sh
Registrar usuario: POST api/login/registrarUsuario (Se envia por post un usuario de tipo Usuario)
```

### Publicaciones
```sh
Obtener todos las publicaciones: GET  api/publicaciones/obtener
Obtener publicación por IdPublicacion: GET api/publicaciones/obtener/{id} donde id: int
Eliminar publicacion por IdPublicacion: DELETE api/publicaciones/eliminar/{id} donde id: int
Guardar publicación: POST api/publicaciones/guardar (se le pasa una publicación de tipo Publicacion)
```

### Usuario
```sh
Obtener todos los usuarios: GET api/usuarios/obtener
Obtener usuario por IdUsuario: GPeticionRecompensaET api/usuarios/obtener/{id}  donde id: int
Elminar usuario por IdUsuario: DELETE api/usuarios/eliminar/{id} donde id:int
Guardar usuario: POST api/usuarios/guardar (se le pasa un usuario de tipo Usuario)
```

### Tesoros
```sh
Obtener todos los tesoros: GET  api/tesoros/obtener
Obtener tesoro por IdTesoro: GET api/tesoros/obtener/{id} donde id: int
Eliminar tesoro por IdTesoro: DELETE api/tesoros/eliminar/{id} donde id: int
Obtener IdPublicacion por IdTesoro: GET /api/tesoros/ObtenerIdPublicacionPorIdTesoro/{id} donde id:int
```

### Comentarios
```sh
Obtener todos los Comentarios: GET  api/comentarios/obtener
Obtener comentario por IdComentario: GET api/comentarios/obtener/{id} donde id: int
Obtener comentarios por IdPublicacion: GET api/comentarios/obtener/publicacion/{id} donde id: int
Obtener Bandeja de entrada por idUsuario: GET api/comentarios/obtener/bandejaEntrada/{id} donde id:int
Cambiar estado de comentario a "Leido": POST api/comentarios/cambiarEstadoALeido (se le pasa un id:int con el id del comentario)
Guardar comentario: POST api/comentarios/guardar (se le pasa un comentario de tipo Comentario)
```

### Peticiones de recompensa
```sh
Obtener las peticiones donde mi tesoro existe : GET  api/obtenerPorIdUsuario/{id}
Guarda Peticion: POST api/guardar (se le pasa una PeticionRecompensa)
Actualizar estadp de petición: POST actualizarEstado (se le pasa idUsuario, idTesoro, estado)

```



