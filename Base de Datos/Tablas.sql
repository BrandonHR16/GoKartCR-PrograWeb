use [GoKart];

create table TB_RolUsuario(

	idRol int not null identity,
	nombreRol varchar(32) not null,
	descripcionRol varchar(512) not null, 

	constraint PK_RolUsuario_IdInternoRol primary key (idRol),
	constraint RolUsuario_NombreRol_Unico unique (nombreRol)

);

create table TB_Usuario(

	idUsuario int not null identity,
	cedulaUsuario varchar(32) not null,
	primerNombre varchar(32) not null,
	primerApellido varchar(32) not null,
	segundoApellido varchar(32) null,
	correoElectronico varchar(64) not null,
	contrasennia binary(128) not null,
	telefono varchar(20) not null,
	direccionResidencia varchar(524) not null,
	tokenRecuperacion varchar(128) null,
	idRol int not null,

	constraint PK_Usuario_IdInternoPersona primary key (idUsuario),
	constraint FK_Usuario_IdInternoRol foreign key (idRol) references TB_RolUsuario(idRol),
	constraint Usuario_CedulaUsuario_Unico unique (cedulaUsuario),
	constraint Usuario_CorreoUsuario_Unico unique (correoElectronico)

);

create table TB_Pista(

	idPista int not null identity,
	nombrePista varchar(32) not null,
	distanciaMetros decimal(6,2) null,
	capacidadUsuarios int not null,
	imagen varbinary(max) null,

	constraint PK_Pista_IdPista primary key (idPista),
	constraint Pista_NombrePista_Unico unique (nombrePista)

);

create table TB_UsuarioPistaRecord(

	idUsuario int not null,
	idPista int not null,
	fecha datetime not null,
	tiempo time not null,

	constraint PK_UsuarioPistaRecord primary key (idUsuario,idPista,fecha),
	constraint FK_UsuarioPistaRecord_IdUsuario foreign key (idUsuario) references TB_Usuario(idUsuario),
	constraint FK_UsuarioPistaRecord_IdPista foreign key (idPista) references Tb_Pista(idPista)

);


create table TB_Kart(

	idGoKart int not null identity,
	nombreKart varchar(32) not null,
	imagen image null,

	constraint PK_Kart_IdGoKart primary key (idGoKart),
	constraint Kart_NombreKart_Unico unique (nombreKart),

);

create table TB_Paquete(

	idPaquete int not null identity,
	nombre varchar(64) not null,
	descripcion varchar(64) not null,
	costo decimal(19,4) not null,
	tiempoOfrecido time not null,
	cantidadUsuarios int not null,
	imagen varbinary(max) null,
	idGoKart int not null,
	idPista int not null,


	constraint PK_Paquete_IdPaquete primary key (idPaquete),
	constraint Paquete_Nombre_Unico unique (nombre),
	constraint FK_Paquete_IdGoKart foreign key (idGoKart) references TB_Kart(idGoKart)


);

create table TB_Reserva(

	idReserva int not null identity,
	fecha datetime not null,
	idUsuario int not null,
	idPaquete int not null,

	constraint PK_Reserva_IdReserva primary key (idReserva),
	constraint FK_Reserva_IdUsuario foreign key (idUsuario) references TB_Usuario(idUsuario),
	constraint FK_Reserva_IdPaquete foreign key (idPaquete) references TB_Paquete(idPaquete),

);

create table TB_Factura(

	idFactura int not null identity,
	fecha datetime not null,
	costoTotal decimal(19,2) not null,
	idReserva int not null,

	constraint PK_Factura_IdFactura primary key (idFactura),
	constraint FK_Factura_IdReserva foreign key (idReserva) references TB_Reserva(idReserva),

);

create table TB_Pregunta(

	idPregunta int not null identity,
	nombreUsuario varchar(64) not null,
	correo varchar(64) not null,
	mensaje  varchar(4024) not null,

	constraint PK_Pregunta_IdPregunta primary key (idPregunta),

);

create table TB_Evento(

	idEvento int not null identity,
	categoriaDelEvento varchar(32) not null,
	mensaje varchar(4024) not null,
	tiempoDelEvento datetime not null,

	constraint PK_Evento_IdEvento primary key (idEvento),

);