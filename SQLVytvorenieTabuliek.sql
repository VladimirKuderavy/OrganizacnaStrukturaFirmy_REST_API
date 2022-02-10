/*Vytvorenie tabulky zamestnanci*/

USE [db_organizacna_struktura_firmy]
GO

/****** Object:  Table [dbo].[zamestnanci]    Script Date: 10. 2. 2022 9:33:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[zamestnanci](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[titul] [varchar](10) NULL,
	[meno] [varchar](30) NOT NULL,
	[priezvisko] [varchar](30) NOT NULL,
	[telefon] [varchar](13) NULL,
	[email] [varchar](50) NULL,
 CONSTRAINT [PK_zamestnanci] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/*Vytvorenie tabulky organizacna_struktura*/

USE [db_organizacna_struktura_firmy]
GO

/****** Object:  Table [dbo].[organizacna_struktura]    Script Date: 10. 2. 2022 9:32:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[organizacna_struktura](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[kod_urovne] [int] NOT NULL,
	[nazov] [varchar](30) NOT NULL,
	[veduci] [int] NOT NULL,
	[vyssi_uzol] [int] NULL,
 CONSTRAINT [PK_organizacna_struktura] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[organizacna_struktura]  WITH CHECK ADD  CONSTRAINT [FK_organizacna_struktura_organizacna_struktura] FOREIGN KEY([vyssi_uzol])
REFERENCES [dbo].[organizacna_struktura] ([id])
GO

ALTER TABLE [dbo].[organizacna_struktura] CHECK CONSTRAINT [FK_organizacna_struktura_organizacna_struktura]
GO

ALTER TABLE [dbo].[organizacna_struktura]  WITH CHECK ADD  CONSTRAINT [FK_organizacna_struktura_zamestnanci] FOREIGN KEY([veduci])
REFERENCES [dbo].[zamestnanci] ([id])
GO

ALTER TABLE [dbo].[organizacna_struktura] CHECK CONSTRAINT [FK_organizacna_struktura_zamestnanci]
GO


/*Naplnenie tabulky zamestnanci*/

INSERT INTO dbo.zamestnanci VALUES('Bc.', 'Jaroslav', 'Valenta', null, null);
INSERT INTO dbo.zamestnanci VALUES('Ing.', 'Ladislav', 'Král', null, null);
INSERT INTO dbo.zamestnanci VALUES(null , 'Vlastimil', 'Reichel', null, null);
INSERT INTO dbo.zamestnanci VALUES('Bc.', 'Pavel', 'Talafous', null, null);
INSERT INTO dbo.zamestnanci VALUES('Bc.', 'Miroslav', 'Trpák', null, null);
INSERT INTO dbo.zamestnanci VALUES('Ing.', 'Zdenìk', 'Rára', null, null);
INSERT INTO dbo.zamestnanci VALUES('Ing.', 'Tomáš', 'Baiger', null, null);
INSERT INTO dbo.zamestnanci VALUES('Bc.', 'Jan', 'Švec', null, null);
INSERT INTO dbo.zamestnanci VALUES('Bc.', 'Jakub', 'Sobotka', null, null);
INSERT INTO dbo.zamestnanci VALUES('Ing.', 'Josef', 'Eisenhamer', null, null);
INSERT INTO dbo.zamestnanci VALUES(null , 'Adriana', 'Formánková', null, null);
INSERT INTO dbo.zamestnanci VALUES('Bc.', 'Hana', 'Zemanová', null, null);
INSERT INTO dbo.zamestnanci VALUES('Bc.', 'Marie', 'Fúsková', null, null);
INSERT INTO dbo.zamestnanci VALUES('Ing.', 'Jana', 'Drastiková', null, null);
INSERT INTO dbo.zamestnanci VALUES('Ing.', 'Marketa', 'Tinková', null, null);
INSERT INTO dbo.zamestnanci VALUES('Bc.', 'Iveta', 'Novotná', null, null);
GO


/*Naplnenie tabulky organizacna struktura*/

INSERT INTO dbo.organizacna_struktura VALUES(1, 'Firma123', 1, null);
INSERT INTO dbo.organizacna_struktura VALUES(2, 'Divizia1', 4, 1);
INSERT INTO dbo.organizacna_struktura VALUES(2, 'Divizia2', 3, 1);
INSERT INTO dbo.organizacna_struktura VALUES(3, 'Projekt1', 2, 2);
INSERT INTO dbo.organizacna_struktura VALUES(3, 'Projekt2', 5, 3);
INSERT INTO dbo.organizacna_struktura VALUES(3, 'Projekt3', 8, 3);
INSERT INTO dbo.organizacna_struktura VALUES(4, 'Oddelenie1', 6, 4);
INSERT INTO dbo.organizacna_struktura VALUES(4, 'Oddelenie2', 7, 4);
INSERT INTO dbo.organizacna_struktura VALUES(4, 'Oddelenie3', 12, 5);
INSERT INTO dbo.organizacna_struktura VALUES(4, 'Oddelenie4', 9, 5);
INSERT INTO dbo.organizacna_struktura VALUES(4, 'Oddelenie5', 14, 6);
INSERT INTO dbo.organizacna_struktura VALUES(4, 'Oddelenie6', 10, 6);
GO
