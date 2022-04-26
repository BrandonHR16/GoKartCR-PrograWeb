use [GoKart];

insert into TB_RolUsuario
values('Administrador','Es el admin del negocio'),
('Cliente','Cliente basico del sistema');

insert into 
TB_Usuario
(cedulaUsuario,primerNombre,primerApellido,correoElectronico,contrasennia,telefono,direccionResidencia,idRol)
values('116180111','Manuel','Quesada','mquesada80111@ufide.ac.cr',HASHBYTES('SHA2_256','WASD'),'87117042','CASA',1);