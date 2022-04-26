use [GoKart];

/*Registro Eventos.*/
go
create or alter procedure registrarEvento(@categoriaDelEvento varchar(32), @mensaje varchar(4024))
as
begin

	insert into TB_Evento(categoriaDelEvento,mensaje,tiempoDelEvento)
	values(Upper(@categoriaDelEvento),@mensaje,SYSDATETIME());

end;
/*Roles de usuarios.*/
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

go
create or alter procedure generarTokenRecuperacionContrasennia(@correoUsuario varchar(64))
as
begin

	update TB_Usuario
	set tokenRecuperacion = ROUND(RAND() * 10000, 0)
	where correoElectronico = @correoUsuario;

	select tokenRecuperacion
	from TB_Usuario
	where correoElectronico = @correoUsuario;

end;

go
create or alter procedure restablecerContrasennia(@correoUsuario varchar(64), @tokenRecuperacion varchar(128),@nuevaContrasennia varchar(128))
as

	declare @@tokenRecuperacionUsuario varchar(128);

begin

	select @@tokenRecuperacionUsuario = tokenRecuperacion
	from TB_Usuario
	where correoElectronico = @correoUsuario;

	if(@@tokenRecuperacionUsuario = @tokenRecuperacion)
	begin

		update TB_Usuario
		set contrasennia = HASHBYTES('SHA2_256',@nuevaContrasennia), tokenRecuperacion = null
		where correoElectronico = @correoUsuario;

	end;

end;

/*Pistas*/

GO
create or ALTER procedure selectPistas
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
Create or ALTER   procedure selectPaquetes
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

/*Reservas*/

/*Selecciona toda la tabala Reserva*/
GO
CREATE or alter PROCEDURE selectReserva
AS
BEGIN
SELECT [idReserva]
      ,[fecha]
      ,[idUsuario]
      ,[idPaquete]
  FROM [dbo].[TB_Reserva]
END
GO 
/*Selecciona Reserva por id*/
GO
CREATE or alter PROCEDURE selectReservaPorID(@idReserva int)
AS
BEGIN
SELECT [idReserva]
	  ,[fecha]
	  ,[idUsuario]
	  ,[idPaquete]
  FROM [dbo].[TB_Reserva]
  WHERE idReserva = @idReserva
END
/*Agregar una reserva*/
GO
CREATE or alter PROCEDURE agregarReserva(@idUsuario int, @idPaquete int, @fecha date)
AS
BEGIN
INSERT INTO [dbo].[TB_Reserva]
		   ([idUsuario]
		   ,[idPaquete]
		   ,[fecha])
	 VALUES
		   (@idUsuario
		   ,@idPaquete
		   ,@fecha)
END
GO
/*Selecciona Reserva la fecha coincida ignorando la hora*/
GO
CREATE or alter PROCEDURE selectReservaPorFecha(@fecha date)
AS
BEGIN
SELECT [idReserva]
	  ,[fecha]
	  ,[idUsuario]
	  ,[idPaquete]
  FROM [dbo].[TB_Reserva]
  WHERE DATEPART(dd, fecha) = DATEPART(dd, @fecha)
		AND DATEPART(mm, fecha) = DATEPART(mm, @fecha)
		AND DATEPART(yy, fecha) = DATEPART(yy, @fecha)
END

/*Get reservas a pagar.*/

go 
create or alter procedure getReservasaPagar(@idUsuario int)
as
begin

	select R.idReserva, R.fecha, P.nombre as "nombrePaquete",
	P.tiempoOfrecido as "tiempoOfrecidoPaquete", P.descripcion as "descripcionPaquete", 
	P.cantidadUsuarios as "cantidadUsuariosPaquete", P.costo as "costoPaquete"
	from TB_Reserva as "R"
	inner join TB_Paquete as "P"
	on P.idPaquete = R.idPaquete
	left join TB_Factura as "F"
	on F.idReserva = R.idReserva
	where R.fecha >= GetDate() and
	R.idUsuario = @idUsuario and F.idReserva is null;

end;

/*Carrito de compras*/

go
create or alter procedure realizarCompra(@JSONIDReservas varchar(MAX))
as
begin

	insert into TB_Factura(idReserva,costoTotal,fecha)	
	select idReserva, costoTotal, getDate()
	from OPENJSON(@JSONIDReservas) 
	with(

		    idReserva int '$.idReserva',  
			costoTotal decimal(19,2) '$.precioFinal'

	);
		
end;
