import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css', './light.min.css']
})
export class HomeComponent {
  code =
`CREATE TABLE [dbo].[Author] (
	[AuthorId] [int] IDENTITY(1,1) NOT NULL CONSTRAINT PK_Author PRIMARY KEY,
	[FirstName] [nvarchar](256) NULL,
	[LastName] [nvarchar](256) NOT NULL,
	[Birthday] [datetime] NULL )
	
CREATE TABLE [dbo].[Post] (
	[PostId] [INT] IDENTITY(1,1) NOT NULL CONSTRAINT PK_Post PRIMARY KEY,
	[Title] [NVARCHAR](256) NOT NULL,
	[Body] [NVARCHAR](MAX) NOT NULL,
	[AuthorId] [INT] NOT NULL)

ALTER TABLE [dbo].[Post] 
ADD [CreationDateTime] [DATETIME] NOT NULL DEFAULT(GETDATE())

 
INSERT INTO [Author]([FirstName], [LastName], [Birthday])
VALUES( 'Ivan', 'Ivanov', '1990-02-03' )

INSERT INTO [Author]([FirstName], [LastName], [Birthday])
VALUES( 'Adriel', 'Perez', '1990-02-03' ), ( 'Eric', 'Miller', '1998-11-13' )

INSERT INTO [Post]([Title], [Body], [AuthorId])
VALUES('SQL Introduction', 'Some text about SQL', 1)

INSERT INTO [Post]([Title], [Body], [AuthorId])
VALUES('SELECT Syntax', 'Some text about SELECT Syntax', 1)


UPDATE [Post] 
SET [CreationDateTime] = GETDATE()
WHERE Title = 'SQL Introduction'

DELETE FROM [Author]`;
  tags = ["Tom", "Bob", "Sam", "Bill", "Tom", "Bob", "Sam", "Bill", "Tom", "Bob", "Sam", "Bill", "Tom", "Bob", "Sam", "Bill", "Tom", "Bob", "Sam", "Bill", "Tom", "Bob"];
}
