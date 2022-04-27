use [GoKart];

insert into TB_RolUsuario
values('Administrador','Es el admin del negocio'),
('Cliente','Cliente basico del sistema');

insert into 
TB_Usuario
(cedulaUsuario,primerNombre,primerApellido,correoElectronico,contrasennia,telefono,direccionResidencia,idRol)
values('116180111','Manuel','Quesada','mquesada80111@ufide.ac.cr',HASHBYTES('SHA2_256','WASD'),'87117042','CASA',2);

insert into TB_Kart(nombreKart)
values('Mario Kart.');

insert into TB_Pista(nombrePista,capacidadUsuarios,distanciaMetros)
values('Pista Arcoiris',3,800);

insert into TB_Paquete(nombre,descripcion,costo,tiempoOfrecido,cantidadUsuarios,idGoKart,idPista)
values('Familiar','Para toda la familia',12000,'3:00',7,1,1);

insert into TB_Paquete(nombre,descripcion,costo,tiempoOfrecido,cantidadUsuarios,idGoKart,idPista)
values('Profesional','Amantes de los karts',15000,'6:00',12,1,1);

insert into TB_Reserva(idUsuario,idPaquete,fecha)
values(1,1,getDate());

insert into TB_Reserva(idUsuario,idPaquete,fecha)
values(1,1,getDate());



