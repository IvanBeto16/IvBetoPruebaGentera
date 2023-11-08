CREATE DATABASE IvBetoGentera

USE IvBetoGentera

CREATE TABLE Receta(
IdReceta INT PRIMARY KEY IDENTITY(1,1),
Fecha DATE NOT NULL,
Diagnostico NVARCHAR(MAX) NOT NULL,
Tratamiento NVARCHAR(MAX) NOT NULL
)

INSERT INTO Receta VALUES('2020-12-02','Escarlatina','Paracetamol y Ibuprofeno')
INSERT INTO RecetaAlumno VALUES('2','1')


ALTER PROCEDURE RecetasAdd
@Matricula INT, 
@Fecha DATE,
@Diagnostico NVARCHAR(MAX),
@Tratamiento NVARCHAR(MAX)
AS
	INSERT INTO Receta VALUES(@Fecha,@Diagnostico,@Tratamiento)
	INSERT INTO RecetaAlumno VALUES(@@IDENTITY,@Matricula)


CREATE PROCEDURE RecetaGetById '4'
@IdReceta INT
AS
	SELECT Receta.IdReceta,
		Receta.Fecha,
		Receta.Diagnostico,
		Receta.Tratamiento,
		Alumno.Matricula,
		Alumno.Nombre,
		Alumno.ApellidoPaterno,
		Alumno.ApellidoMaterno,
		Alumno.FechaNacimiento
	FROM Receta
	INNER JOIN RecetaAlumno
	ON Receta.IdReceta = RecetaAlumno.IdReceta
	INNER JOIN Alumno
	ON RecetaAlumno.Matricula = Alumno.Matricula
	WHERE Receta.IdReceta = @IdReceta


CREATE TABLE Alumno(
Matricula INT PRIMARY KEY,
Nombre NVARCHAR(70) NOT NULL,
ApellidoPaterno NVARCHAR(70) NOT NULL,
ApellidoMaterno NVARCHAR(70),
FechaNacimiento DATE NOT NULL
)

INSERT INTO Alumno VALUES(1,'Ivan','Beto','Perez','1999-08-16')
INSERT INTO Alumno VALUES(2,'Scarlet','Garcia','Sanchez','1998-11-16')
INSERT INTO Alumno VALUES(3,'Roberto','Mosqueda','Angulo','1997-09-21')
INSERT INTO Alumno VALUES(4,'Christian','Aguillon','Castellanos','1999-05-28')
INSERT INTO Alumno VALUES(5,'Josué','Gómez','González','1999-01-16')

CREATE TABLE RecetaAlumno(
Id INT PRIMARY KEY IDENTITY(1,1),
IdReceta INT FOREIGN KEY REFERENCES Receta(IdReceta),
Matricula INT FOREIGN KEY REFERENCES Alumno(Matricula)
)


SELECT Receta.IdReceta,
		Receta.Fecha,
		Receta.Diagnostico,
		Receta.Tratamiento,
		Alumno.Matricula,
		Alumno.Nombre,
		Alumno.ApellidoPaterno,
		Alumno.ApellidoMaterno,
		Alumno.FechaNacimiento
FROM Receta
INNER JOIN RecetaAlumno
ON Receta.IdReceta = RecetaAlumno.IdReceta
INNER JOIN Alumno
ON RecetaAlumno.Matricula = Alumno.Matricula



SELECT * FROM Alumno
SELECT * FROM RecetaAlumno
SELECT * FROM Receta