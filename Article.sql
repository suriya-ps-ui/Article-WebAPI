/*
Create Database
*/
Create Database ApplicationDev;
Go
Use ApplicationDev;
Go
/*Create Schema*/
Create Schema Articles;
Go
/*
Create Table Article
*/
Create table Articles.ArticleTable(
 articleId int,
 articleTitle varchar(250));
Go
Go
/*SP for GET*/
Create or Alter Procedure Articles.GetValues
	As
	Begin
		Select * From Articles.ArticleTable;
	End
Go
/*SP for POST*/
Create or Alter Procedure Articles.PostRecord @articleId int,@articleTitle varchar(250)
	As
	Begin
		Insert into Articles.ArticleTable Values(@articleId,@articleTitle);
	End
Go
Go
/*SP for DELETE*/
Create or Alter Procedure Articles.DeleteRecord @articleId int
	As
	Begin
		Delete From Articles.ArticleTable where articleId=@articleId;
	End
Go
/*SP for PUT*/
Go
Create or Alter Procedure Articles.UpdateRecord @articleId int,@articleTitle varchar(250)
	As
	Begin
		Update Articles.ArticleTable set articleTitle=@articleTitle where articleId=@articleId;
	End
Go
/*SP for POST and PUT*/
Create or Alter Procedure Articles.PostOrUpdateRecord @articleId int,@articleTitle varchar(250)
	As
	Begin
		if Exists(Select * From Articles.ArticleTable where articleId=@articleId)
		Begin
			Update Articles.ArticleTable Set articleTitle=@articleTitle where articleId=@articleId;
		End
		Else
		Begin
			Insert into Articles.ArticleTable Values(@articleId,@articleTitle);
		End
	End
Go
Exec Articles.PostOrUpdateRecord 4,'Article 4';
Select * From Articles.ArticleTable;
--User
Create Table Articles.Users(
	userId int,
	userName varchar(50),
	userPassword varchar(50));
Go
--Get user by password and username
Create or Alter Procedure Articles.GetUser @userName varchar(50),@userPassword varchar(50)
As
	Select * From Articles.Users where userName=@userName and userPassword=@userPassword;
Go
Insert into Articles.Users Values(1,'Suriya','pass@12345');
Select * From Articles.Users;
Exec Articles.GetUser 'Suriya','pass@12345';