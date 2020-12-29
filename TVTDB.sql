use master
go
drop database TVTDB
go
create database TVTDB
go
use TVTDB

--TABLES--
-->-->-->-->--
--Show	(includes all movies and tvshows)
--Movie
--TVShow
--Season
--Episode
--Genre
--Images
--Comments
--loginDetails
--Users
--Watchlist
--Watchhistory
-->-->-->-->--

create table Genre(
	genreID int NOT NULL,
	genreName varchar(20),
	primary key(genreID),
)

create table Movie(
	movieID int NOT NULL,--Primary Key
	movieName varchar(50),
	releaseDate date,	
	movieRatings int,	--Score out of 10
	genreID int NOT NULL,--FOREIGN from GENRE
	primary key(movieID),
	foreign key(genreID) references Genre(genreID) on delete no action on update cascade, 
)

create table TVShow(
	TVShowID int NOT NULL,
	TVShowName varchar(50),
	releaseDate date,
	TVShowRatings int,
	genreID int NOT NULL,	--FOREIGN from GENRE
	primary key(TVShowID),
	foreign key(genreID) references Genre(genreID) on delete no action on update cascade,
)

create table Show(
	showID int NOT NULL,	--Primary Key
	showType varchar(10),
	movieID int,	--FOREIGN from MOVIE
	TVShowID int,	--FOREIGN from TVSHOW
	airDate date NOT NULL,
	primary key(showID, showType), --Composite primary key to check foreign key constraint issue
	foreign key(TVShowID) references TVShow(TVShowID) on delete no action on update no action,
	foreign key(movieID) references Movie(movieID) on delete no action on update cascade,
	
)

create table Season(
	seasonID int NOT NULL,
	TVShowID int NOT NULL,	--FOREIGN from TVSHOW
	seasonNumber int,
	primary key(seasonID, seasonNumber),
	foreign key(TVShowID) references TVShow(TVShowID) on delete cascade on update cascade,
)

create table Episode(
	episodeID int NOT NULL,
	seasonID int NOT NULL, --FOREIGN from Season
	seasonNumber int,
	episodeName varchar(50),
	episodeNumber int,
	releaseDate date,
	primary key(episodeID),
	foreign key(seasonID, seasonNumber) references Season(seasonID, seasonNumber) on delete cascade on update cascade,
)

create table Users(	
	userID int NOT NULL identity(1,1),
	userName varchar(50),
	password  varchar(20),
	gender varchar(10),
	bDate date,
	primary key(userID),
)

create table Watchlists(
	userID int NOT NULL,	--FOREIGN from USERS
	showID int NOT NULL,	--FOREIGN from Show
	showType varchar(10),	--FOREIGN from Show
	watchedOptions varchar(10),
	primary Key(userID, showID),	--Composite primary key
	foreign key(userID) references Users(userID) on delete cascade on update cascade,
	foreign key(showID, showType) references Show(showID, showType) on delete cascade on update cascade,
)

create table Watchhistory(
	userID int NOT NULL,	--FOREIGN from USERS
	showID int NOT NULL,	--FOREIGN from Show
	showType varchar(10),	--FOREIGN from Show
	watchedDate date,
	primary Key(userID, showID),	--Composite primary key
	foreign key(userID) references Users(userID) on delete cascade on update cascade,
	foreign key(showID, showType) references Show(showID, showType) on delete cascade on update cascade,
)

create table loginDetails(
	loginID int NOT NULL,
	userID int NOT NULL, 
	loginTime time NOT NULL,
	--SuspendTime can be calculated accordingly
	sessionTime int NOT NULL,
	primary key (loginID),
	foreign key(userID) references Users(userID) on delete cascade on update cascade,
)

create table Images(
	imageID int NOT NULL,
	imageLink varchar(100), --Local link to directory of image
	showID int NOT NULL,	--FOREIGN from Show
	showType varchar(10),	--FOREIGN from Show
	primary key (imageID),
	foreign key(showID, showType) references Show(showID, showType) on delete cascade on update cascade,
)

create table comments(
	commentID int NOT NULL,
	commentDesc varchar(240),
	showID int NOT NULL,	--FOREIGN from Show
	showType varchar(10),	--FOREIGN from Show
	primary key (commentID),
	foreign key(showID, showType) references Show(showID, showType) on delete cascade on update cascade,
)

--View all Tables
Select * from Show
Select * from Movie
Select * from TVShow
Select * from Season
Select * from Episode
Select * from Genre
Select * from Images
Select * from Comments
Select * from loginDetails
Select * from Users
Select * from Watchlists
Select * from Watchhistory

--Insertions--
sp_help Show
Insert into Show
values (1, 'Movie', 1, NULL, '4-26-2019'),
(2, 'Movie', 2, NULL, '5-5-2000'),
(3, 'TVShow', NULL, 1, '9-22-1994'),
(4, 'TVShow', NULL, 2, '6-14-2019'),
(5, 'TVShow', NULL, 3, '12-1-2019'),
(6, 'TVShow', NULL, 4, '12-1-2015'),
(7, 'TVShow', NULL, 5, '3-15-2019'),
(8, 'Movie', 3, NULL, '4-27-2018'),
(9, 'Movie', 4, NULL, '3-8-2019'),
(10, 'Movie', 5, NULL, '5-5-2019'),
(11, 'Movie', 6, NULL, '10-4-2019'),
(12, 'Movie', 7, NULL, '4-12-2019'),
(13, 'Movie', 8, NULL, '12-21-2018'),
(14, 'Movie', 9, NULL, '4-12-2019'),
(15, 'Movie', 10, NULL, '4-12-2019')

sp_help Movie
Insert into Movie
values (1, 'Avengers Endgame', '4-26-2019', 89, 1),
(2, 'Gladiator', '5-5-2000', 95, 1),
(3, 'Avengers Infinity Wars', '4-27-2018', 85, 1),
(4, 'Captain Marvel', '3-8-2019', 71, 1),
(5, 'Shazam', '5-5-2019', 75, 2),
(6, 'Gemini Man', '10-4-2019',75, 1),
(7, 'HellBoy', '4-12-2019', 54, 1),
(8, 'Aquaman', '12-21-2018', 71, 1),
(9, 'High Life', '4-12-2019', 60, 1),
(10, 'The Perfect date', '4-12-2019', 59, 2)

sp_help TVShow
Insert into TVShow
values (1, 'Friends', '9-22-1994', 95, 2),
(2, 'Too Old to Die Young', '6-14-2019', 70, 3),
(3, 'The New Pope', '12-1-2019', 65, 5),
(4, 'Mr.Robot', '12-1-2015', 85, 3),
(5, 'Deadwood', '3-15-2019', 56, 1),
(6, 'The Big Band Theory', '7-20-2007', 92, 2),
(7, 'Brooklyn Nine Nine', '10-11-2013', 87, 2)

delete from Season
sp_help Season
Insert into Season
values (1,1,1),
(2,1,2),
(3,1,3),
(4,1,4),
(5,1,5),
(6,1,6),
(7,1,7),
(8,1,8),
(9,1,9),
(10,2,1),
(11,3,1),
(12,4,1),
(13,5,1),
(14, 6, 1),
(15, 6, 2),
(16, 6, 3),
(17, 6, 4),
(18, 6, 5),
(19, 6, 6),
(20, 6, 7),
(21, 6, 8),
(22, 6, 9),
(23, 6, 10),
(24, 6, 11),
(25, 6, 12),
(26, 7, 1),
(27, 7, 2),
(28, 7, 3),
(29, 7, 4),
(30, 7, 5),
(31, 7, 6)

Insert into Genre
values (1, 'Action'),
(2, 'Comedy'),
(3, 'Crime'),
(4, 'Adventure'),
(5, 'Drama'),
(6, 'Fantasy'),
(7, 'History')

sp_help Comments
Insert into Comments
values (1, 'This is awesome', 1, 'Movie')

sp_help Episode
Insert into Episode
values (1, 25, 12, 'The Maternal Conclusion', 22, '5-10-2019'),
(2, 25, 12, 'The Plagiarism Schism', 23, '5-17-2019'),
(3, 31, 6, 'Cinco de Mayo', 16, '5-12-2019'),
(4, 31, 6, 'Cinco de Mayo', 16, '5-19-2019')

---- PROCEDURES ----

Create Procedure Login
@username varchar(50),
@password varchar(20),
@return int OUTPUT
As
Begin
	Select @return=userID	
	from Users
	where [Users].userName = @username AND [Users].password=@password
End

drop procedure Login

Create Procedure Signup
@username varchar(50),
@password varchar(20),
@gender varchar(10),
@bDate date,
@return int OUTPUT
As
Begin	
	IF NOT EXISTS (Select *
				   from Users
				   where userName = @username)
	Begin
			Insert Into Users(userName, password, gender, bDate) 
			values (@username, @password, @gender, @bDate)

			Select @return=userID
			from Users
			where userName = @username
	End
	ELSE
	begin
		set @return=null
	end
End

drop procedure Signup

Create procedure UserHistory
@username varchar(50)
As
Begin 
	Select showID, showType
	from Users As U, Watchhistory As W
	where U.userID = W.userID AND U.userName = @username
	order by W.showID desc
End

Create Procedure DisplayInfo
@username varchar(50),
@gender varchar(20) OUTPUT, 
@bdate date OUTPUT
As 
Begin 
	Select @gender = gender, @bdate = bDate
	from Users
	where userName = @username
End 

--Discover--
----------------------------------------------
Create Procedure ShowCount
@maxId int OUTPUT
As
Begin
	Select @maxId = MAX(showID)
	from Show
End

Create Procedure ShowExistByID
@idInput int,
@return int OUTPUT
As
Begin
	select @return = showID
	from Show
	where showID = @idInput		
End

Create Procedure GetShowByID
@idInput int,
@showType varchar(10) OUTPUT,
@movieId int OUTPUT,
@TVShowID int OUTPUT,
@airDate date OUTPUT
As
Begin
	Select @showType = showType, @movieId = movieID, @TVShowID = TVShowID, @airDate = airDate
	from Show
	where showID = @idInput
End
----------------------------------------------
Create Procedure ShowCountMovies
@maxId int OUTPUT
As
Begin
	Select @maxId = MAX(movieID)
	from Movie
End

Create Procedure MovieExistByID
@idInput int,
@return int OUTPUT
As
Begin
	select @return = movieID
	from Movie
	where movieID = @idInput		
End

Create Procedure GetMovieByID
@idInput int,
@MovieName varchar(50) OUTPUT,
@releaseDate date OUTPUT,
@movieRatings int OUTPUT,
@genreID int OUTPUT

As
Begin
	Select @MovieName = movieName, @releaseDate = releaseDate, @movieRatings = movieRatings, @genreID = genreID
	from Movie
	where movieID = @idInput
End
----------------------------------------------
Create Procedure ShowCountTVShows
@maxId int OUTPUT
As
Begin
	Select @maxId = MAX(TVShowID)
	from TVShow
End

Create Procedure TVShowExistByID
@idInput int,
@return int OUTPUT
As
Begin
	select @return = TVShowID
	from TVShow
	where TVShowID = @idInput		
End

Create Procedure GetTVShowByID
@idInput int,
@TVShowName varchar(50) OUTPUT,
@releaseDate date OUTPUT,
@TVShowRatings int OUTPUT,
@genreID int OUTPUT

As
Begin
	Select @TVShowName = TVShowName, @releaseDate = releaseDate, @TVShowRatings = TVShowRatings, @genreID = genreID
	from TVShow
	where TVShowID = @idInput
End
----------------------------------------
----Trending have 85%+ rating and released in last 5 months
Create Procedure CheckTrendingMovies
@idInput int,
@return int OUTPUT
As
Begin
	Select @return=movieID
	from Movie
	where movieID = @idInput 
	AND movieRatings >= 85 
	AND year(GETDATE()) = year(releaseDate)
	AND (month(GETDATE()) - month(releaseDate)) <= 5
End

Create Procedure CheckTrendingTVShows
@idInput int,
@return int OUTPUT
As
Begin
	Select @return=TVShowID
	from TVShow
	where TVShowID = @idInput 
	AND TVShowRatings >= 85
End

----Calender----
Create View EpisodesInCurrentMonth
As
Select *
from Episode
where month(GETDATE())=month(releaseDate)
AND year(GETDATE())=year(releaseDate)

Create Procedure MaxIDEpisodeInCurr
@maxId int OUTPUT
As
Begin
	Select @maxId = MAX(episodeId)
	from EpisodesInCurrentMonth
End

Create Procedure EpisodeExistByID
@idInput int,
@return int OUTPUT
As
Begin
	Select @return = episodeID
	from EpisodesInCurrentMonth
	where @idInput = episodeId
End

Create Procedure EpisodeCurrByID
@idInput int,
@returnDay int OUTPUT,
@episodeName varchar(50) OUTPUT,
@episodeNumber int OUTPUT,
@seasonNumber int OUTPUT,
@seasonID int OUTPUT
As
Begin
	Select @returnDay = day(releaseDate), @episodeName = episodeName, @seasonNumber = seasonNumber, @seasonID = seasonID, @episodeNumber = episodeNumber
	from EpisodesInCurrentMonth
	where EpisodeID = @idInput
End

Create Procedure TVShowFromSeason
@seasonId int,
@TVShowName varchar(50) OUTPUT
As
Begin
	Select @TVShowName = T.TVShowName
	from TVShow As T, Season As S
	where T.TVShowID = S.TVShowID AND @seasonId = seasonID
End

Create Procedure GetGenreNameByID
@idInput int,
@genreName varchar(20) OUTPUT
As
Begin 
	Select @genreName = genreName
	from Genre
	where @idInput = genreID
End
