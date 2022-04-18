use [GoKart];

/*Registro Eventos.*/
go
create or alter procedure registrarEvento(@categoriaDelEvento varchar(32), @mensaje varchar(4024))
as
begin

	insert into TB_Evento(categoriaDelEvento,mensaje,tiempoDelEvento)
	values(Upper(@categoriaDelEvento),@mensaje,SYSDATETIME());

end;
/*Roles de usyarios.*/
go 
create or alter procedure selectRolesUsuario
as
begin 

	select idRol, nombreRol, descripcionRol
	from TB_RolUsuario;

end;
/*Usuarios.*/
go
create or alter procedure selectUsuarios
as
begin

	select idUsuario,
	cedulaUsuario,
	primerNombre,
	primerApellido,
	segundoApellido,
	correoElectronico,
	telefono,
	direccionResidencia,
	idRol
	from TB_Usuario;

end;

go
create or alter procedure selectUsuarioPorCorreo(@correoUsuario varchar(64))
as
begin

	select idUsuario,
	cedulaUsuario,
	primerNombre,
	primerApellido,
	segundoApellido,
	correoElectronico,
	telefono,
	direccionResidencia,
	idRol
	from TB_Usuario
	where upper(correoElectronico) like(upper(@correoUsuario));

end;

go
create or alter procedure selectUsuarioPorCedula(@cedulaUsuario varchar(32))
as
begin

	select idUsuario,
	cedulaUsuario,
	primerNombre,
	primerApellido,
	segundoApellido,
	correoElectronico,
	telefono,
	direccionResidencia,
	idRol
	from TB_Usuario
	where upper(cedulaUsuario) like(upper(@cedulaUsuario));

end;

go
create or alter procedure iniciarSesion(@correoUsuario varchar(64), @contrasennia varchar(128))
as
begin

	select idUsuario,
	cedulaUsuario,
	primerNombre,
	primerApellido,
	segundoApellido,
	correoElectronico,
	telefono,
	direccionResidencia,
	idRol
	from TB_Usuario
	where upper(correoElectronico) like(upper(@correoUsuario)) and
	contrasennia like(HASHBYTES('SHA2_256',@contrasennia));

end;

go
create or alter procedure registrarUsuario
(

	@cedulaUsuario varchar(32),
	@primerNombre varchar(32),
	@primerApellido varchar(32),
	@segundoApellido varchar(32),
	@correoElectronico varchar(64),
	@contrasennia varchar(128),
	@telefono varchar(20),
	@direccionResidencia varchar(524),
	@idRol int

)
as
begin

	insert into TB_Usuario(cedulaUsuario,primerNombre,primerApellido,segundoApellido,
	correoElectronico,contrasennia,telefono,direccionResidencia,idRol)
	values(@cedulaUsuario,@primerNombre,@primerApellido,@segundoApellido,
	lower(@correoElectronico), 

	HASHBYTES('SHA2_256',@contrasennia)
	
	,@telefono,@direccionResidencia,@idRol);

end;

go
create or alter procedure actualizarUsuario
(
	
	@cedulaUsuario varchar(32),
	@correoElectronico varchar(64),
	@contrasennia varchar(128),
	@telefono varchar(20),
	@direccionResidencia varchar(524),
	@idRol int

)
as
begin

	update TB_Usuario
	set correoElectronico = @correoElectronico, contrasennia = HASHBYTES('SHA2_256',@contrasennia),
	telefono = @telefono, direccionResidencia = @direccionResidencia, idRol=@idRol
	where cedulaUsuario = @cedulaUsuario;

end;

/*Pistas*/

USE [GoKart]
GO
/****** Object:  StoredProcedure [dbo].[selectPistas]    Script Date: 16/04/2022 11:37:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   procedure [dbo].[selectPistas]
as
begin

	select idPista, nombrePista, distanciaMetros, capacidadUsuarios, imagen
	from TB_Pista;

end;
go
create or alter procedure selectPistaPorID(@idPista int)
as
begin

	select idPista, nombrePista, distanciaMetros, capacidadUsuarios, imagen
	from TB_Pista
	where idPista = @idPista;

end;

go
create or alter procedure selectPistaPorNombre(@nombrePista varchar(32))
as
begin

	select idPista, nombrePista, distanciaMetros, capacidadUsuarios, imagen
	from TB_Pista
	where upper(nombrePista) like (upper('%'+@nombrePista+'%'));

end;

go
create or alter procedure registrarPista
(

	@nombrePista varchar(32),
	@distanciaMetros decimal(6,2),
	@capacidadUsuarios int

)
as
begin

	insert into TB_Pista(nombrePista,distanciaMetros,capacidadUsuarios,imagen)
	values(@nombrePista,@distanciaMetros,@capacidadUsuarios,null);

end;

go
create or alter procedure actualizarPista
(

	@idPista int,
	@nombrePista varchar(32),
	@distanciaMetros decimal(6,2),
	@capacidadUsuarios int

)
as
begin

	update TB_Pista
	set nombrePista = @nombrePista, distanciaMetros = @distanciaMetros, capacidadUsuarios = @capacidadUsuarios
	
	where idPista = @idPista;

end;

/*UsuarioPistaRecord*/


/*Kart*/
 
 go
create or alter procedure selectKarts
as
begin

	select idGoKart, nombreKart
	from TB_Kart;

end;

go
create or alter procedure selectKartPorID(@idGoKart int)
as
begin

	select idGoKart, nombreKart
	from TB_Kart
	where idGoKart = @idGoKart;

end;

go
create or alter procedure selectKartPorNombre(@nombreKart varchar(32))
as
begin

	select idGoKart, nombreKart
	from TB_Kart
	where upper(nombreKart) like (upper('%'+@nombreKart+'%'));

end;

go
create or alter procedure registrarKart
(

	@nombreKart varchar(32)

)
as
begin

	insert into TB_Kart(nombreKart)
	values(@nombreKart);

end;

go
create or alter procedure actualizarKart
(

	@idGoKart int,
	@nombreKart varchar(32)

)
as
begin

	update TB_Kart
	set nombreKart = @nombreKart
	where idGoKart = @idGoKart;

end;

/*Paquete*/
go
Create or ALTER   procedure [dbo].[selectPaquetes]
as
begin

	select idPista, nombre, descripcion, costo, tiempoOfrecido, cantidadUsuarios, imagen
	from TB_Paquete;

end;

/*Preguntas Frecuentes*/
go
create or alter procedure registrarPregunta
(

	@nombreUsuario varchar(64),
	@correo varchar(64),
	@mensaje varchar(4024)

)
as
begin

	insert into TB_Pregunta(nombreUsuario,correo,mensaje)
	values(@nombreUsuario,@correo,@mensaje);

end;

go
create or alter procedure selectPreguntas
as
begin

	select idPregunta, nombreUsuario, correo, mensaje
	from TB_Pregunta;

end;


go
create or alter procedure eliminarPregunta
(

	@idPregunta int

)
as
begin

	delete from TB_Pregunta
	where idPregunta = @idPregunta;

end;
