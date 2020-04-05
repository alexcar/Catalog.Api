
create user catalog_srv;
go
exec sp_addrolemember 'db_owner', 'catalog_srv';
go

-- Stored Procedures

create procedure [catalog].[GetAllItems]
as
begin
	select 
		[Id]
		,[Name]
		,[Description]
		,[LabelName]
		,[Price]
		,[PictureUri]
		,[ReleaseDate]
		,[Format]
		,[AvailableStock]
		,[GenreId]
		,[ArtistId]
	from 
		[catalog].[Items]
end

--------------------------------------

create procedure [catalog].[GetItemById]
	@Id uniqueidentifier
as
begin
	select 
		[Id]
		,[Name]
		,[Description]
		,[LabelName]
		,[Price]
		,[PictureUri]
		,[ReleaseDate]
		,[Format]
		,[AvailableStock]
		,[GenreId]
		,[ArtistId]
	from 
		[catalog].[Items]
	where
		Id = @Id
end

-------------------------------------------
create procedure [catalog].[InsertItem] (
	@Id uniqueidentifier,
	@Name nvarchar(max),
	@Description nvarchar(1000),
	@LabelName nvarchar(max) NULL,
	@Price nvarchar(max) NULL,
	@PictureUri nvarchar(max) NULL,
	@ReleaseDate datetimeoffset(7),
	@Format nvarchar(max) ,
	@AvailableStock int,
	@GenreId uniqueidentifier,
	@ArtistId uniqueidentifier
)
as

begin

	insert into [catalog].[Items] 
		(Id, Name, Description,LabelName,Price,PictureUri, ReleaseDate, Format,AvailableStock, GenreId,ArtistId)

	output 
		inserted.*
	values 
		(@Id, @Name, @Description, @LabelName, @Price, @PictureUri, @ReleaseDate, @Format, @AvailableStock, @GenreId, @ArtistId)
end

--------------------------------------------------
create procedure [catalog].[UpdateItem] (
	@Id uniqueidentifier,
	@Name nvarchar(max),
	@Description nvarchar(1000),
	@LabelName nvarchar(max) NULL,
	@Price nvarchar(max),
	@PictureUri nvarchar(max) NULL,
	@ReleaseDate datetimeoffset(7) NULL,
	@Format nvarchar(max) ,
	@AvailableStock int,
	@GenreId uniqueidentifier,
	@ArtistId uniqueidentifier
)
as

begin
	update [catalog].[Items]
	set 
		Name = @Name,
		Description = @Description,
		LabelName = @LabelName,
		Price = @Price,
		PictureUri = @PictureUri,
		ReleaseDate = @ReleaseDate,
		Format = @Format,
		AvailableStock = @AvailableStock,
		GenreId = @GenreId,
		ArtistId = @ArtistId
	output 
		inserted.*
	where 
		Id = @Id
end
