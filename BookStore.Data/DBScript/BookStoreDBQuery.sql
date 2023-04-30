BEGIN
	BEGIN TRY
		BEGIN TRANSACTION BOOKSTORE_CONFIG
		SET NOCOUNT ON;

			 PRINT 'Creating Racks table'
			 CREATE TABLE Racks (
				Id int NOT NULL identity(1,1) PRIMARY KEY,
				Code varchar(10) NOT NULL,
				Name nvarchar(100) NULL,
				IsDeleted bit NOT NULL DEFAULT 0,
				CreatedDate datetime NULL,
				UpdatedDate datetime NULL
			);

			PRINT 'Creating Shelves table'
			CREATE TABLE Shelves (
				Id int NOT NULL identity(1,1) PRIMARY KEY,
				Code varchar(10) NOT NULL,
				Name nvarchar(100) NULL,
				RackId int NOT NULL,
				IsDeleted bit NOT NULL DEFAULT 0,
				CreatedDate datetime NULL,
				UpdatedDate datetime NULL
				FOREIGN KEY (RackId) REFERENCES Racks(Id)
			);

			PRINT 'Creating Books table'
			CREATE TABLE Books (
				Id int NOT NULL identity(1,1) PRIMARY KEY,
				Code varchar(10) NOT NULL,
				Name nvarchar(100) NOT NULL,
				IsAvailable bit NOT NULL DEFAULT 1, 
				Price DECIMAL(10,2) NOT NULL DEFAULT 0, 
				ShelfId int NOT NULL,
				IsDeleted bit NOT NULL DEFAULT 0,
				CreatedDate datetime NULL,
				UpdatedDate datetime NULL
				FOREIGN KEY (ShelfId) REFERENCES Shelves(Id)
			);

			PRINT 'Creating SP_GetShelveData'
			EXEC('CREATE PROCEDURE SP_GetShelveData   
			AS 
			BEGIN
			SELECT [Id]
					  ,[Code]
					  ,[Name]
					  ,[RackId]
					  ,[IsDeleted]
					  ,[CreatedDate]
					  ,[UpdatedDate]
				  FROM [dbo].[Shelves] 
			END')

			PRINT 'Creating SP_AddUpdateBook'
			EXEC('CREATE PROCEDURE SP_AddUpdateBook
				 @id int = -1,
				 @code varchar(100) = NULL,
				 @name varchar(100) = NULL,
				 @isavailable bit = '''',
				 @price decimal = '''',
				 @shelfid int = 0
				AS 
				BEGIN
					IF @id = -1 OR @id IS NULL
						BEGIN
						INSERT INTO dbo.Books
						VALUES(@code, @name, @isavailable, @price, @shelfid, 0, GETDATE(),GETDATE())
						END
					ELSE
						BEGIN
						UPDATE dbo.Books SET
						Code = @code,
						Name = @name,
						IsAvailable = @isavailable,
						Price = @price,
						ShelfId = @shelfid,
						UpdatedDate = GETDATE()
						WHERE Id = @id
					END
					SELECT @id
			END')			

			PRINT 'Creating SP_GetBook'
			EXEC('
			CREATE PROCEDURE SP_GetBook
			 @id int = -1
			AS 
			BEGIN
			IF @id != -1
				BEGIN
						SELECT
					   [Id]
					  ,[Code]
					  ,[Name]
					  ,[IsAvailable]
					  ,[Price]
					  ,[ShelfId]
					  ,[IsDeleted]
					  ,[CreatedDate]
					  ,[UpdatedDate]
					  ,'''' as ShelfName
					FROM [dbo].[Books] WHERE Id = @id
				  END
			END')

			PRINT 'Creating SP_GetAllBookList'
			EXEC('CREATE PROCEDURE [dbo].[SP_GetAllBookList]
				AS   
					BEGIN
							SELECT
						   B.[Id]
						  ,B.[Code]
						  ,B.[Name]
						  ,B.[IsAvailable]
						  ,B.[Price]
						  ,B.[ShelfId]
						  ,B.[IsDeleted]
						  ,B.[CreatedDate]
						  ,B.[UpdatedDate]
						  ,S.Name as ShelfName
						FROM [dbo].[Books] B JOIN [dbo].[Shelves] S
						on B.ShelfId = S.Id
					  END')

			PRINT 'Creating SP_DeleteBook'

			EXEC('CREATE PROCEDURE SP_DeleteBook
			 @id int = -1
			AS   
			BEGIN
				IF @id != -1
				BEGIN
					UPDATE [dbo].[Books] SET IsDeleted = 1 WHERE Id = @id
				  END
			END')

			PRINT 'Creating SP_AddShelve'
			EXEC('CREATE PROCEDURE SP_AddShelve
			 @code varchar(100) = NULL,
			 @name varchar(100) = NULL,
			 @rackid int = 0
			AS   
				BEGIN
					INSERT INTO dbo.Shelves
					VALUES(@code, @name, @rackid, '''', GETDATE(),GETDATE())
				END')

		  PRINT 'Creating SP_DeleteShelve'
          EXEC('CREATE PROCEDURE SP_DeleteShelve
			 @id int = -1
			 AS   
				IF @id != -1
				BEGIN
					UPDATE [dbo].[Shelves] SET IsDeleted = 1 WHERE Id = @id
				END') 

			PRINT 'Creating SP_GetRackData'
			EXEC('CREATE PROCEDURE SP_GetRackData   
				AS   
					SELECT[Id]
					  ,[Code]
					  ,[Name]
					  ,[IsDeleted]
					  ,[CreatedDate]
					  ,[UpdatedDate]
				  FROM [dbo].[Racks]')

			INSERT INTO Racks VALUES('R123','Rack1','',GETDATE(),GETDATE())
			INSERT INTO Racks VALUES('R456','Rack2','',GETDATE(),GETDATE())
			INSERT INTO Racks VALUES('R890','Rack3','',GETDATE(),GETDATE())

			--INSERT INTO Shelves VALUES('S111','Shelf1',@RackId,'',GETDATE(),GETDATE())
			--INSERT INTO Shelves VALUES('S222','Shelf2',@RackId,'',GETDATE(),GETDATE())
			--INSERT INTO Shelves VALUES('S333','Shelf3',@RackId,'',GETDATE(),GETDATE())
			--INSERT INTO Shelves VALUES('S444','Shelf4',@RackId,'',GETDATE(),GETDATE())
			
			COMMIT TRANSACTION BOOKSTORE_CONFIG
	END TRY
	BEGIN CATCH
	PRINT 'Inserting book store configuration failed'
	ROLLBACK TRANSACTION BOOKSTORE_CONFIG
	END CATCH
END