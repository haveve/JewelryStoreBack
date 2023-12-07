INSERT INTO SelectedProductsStatus(id,name) values(1,'beloved'),(2,'in_bucket'),(3,'ordered')


CREATE TYPE ProductStatusIds AS TABLE
    (
        id uniqueidentifier NOT NULL
	)

GO


CREATE PROCEDURE USP_COMMIT_BOUGHT
(@ProductIds ProductStatusIds READONLY,
@OrderId uniqueidentifier ,
@OrderedStatusId int,
@Address nvarchar(200))
AS
SET XACT_ABORT ON
SET NOCOUNT ON

DECLARE @Now DATETIME2(0) = GETUTCDATE()

BEGIN TRANSACTION;
BEGIN TRY

INSERT INTO HISTORY(id,date,total_cost,address) VALUES(@OrderId,@Now,0,@Address)

END TRY
BEGIN CATCH  
	SELECT 'Order with that ID already exist'
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;  
	RETURN;
END CATCH; 

IF @@TRANCOUNT > 0  
    COMMIT TRANSACTION;

BEGIN TRANSACTION;  
BEGIN TRY
	declare @Id uniqueidentifier 
	declare @SizeId int

	declare idsCursor CURSOR for SELECT id from @ProductIds
	
	OPEN idsCursor

	FETCH NEXT FROM idsCursor INTO
	@Id

DECLARE @ProductCost decimal(10,2)
DECLARE @BoughtCount int 
DECLARE @ProductId int 
DECLARE @PrevCreator int
DECLARE @Creator int
WHILE @@FETCH_STATUS = 0
	BEGIN

		SET @PrevCreator = @Creator

		SELECT @Creator = p.creator_id ,@ProductCost = sp.count * si.cost*COALESCE(d.[percent],100)/100,@BoughtCount = sp.count,@ProductId = sp.product_id,@SizeId = sp.size_id FROM  SelectedProducts as sp
		left JOIN Discount as d
		ON d.product_id = sp.product_id AND @Now between d.start and d.[end]
		JOIN SizeInfo as si
		ON si.product_id = sp.product_id AND si.size_id = sp.size_id
		JOIN Products as p 
		ON si.product_id = p.id
		WHERE sp.id = @Id 

		IF (@PrevCreator is not null AND @PrevCreator <> @Creator)
			BEGIN
				SELECT 'Products are not from one creator'			
				IF @@TRANCOUNT > 0 
					ROLLBACK TRANSACTION;
				DELETE FROM History WHERE id = @OrderId		
				RETURN;
			END

			UPDATE HISTORY set total_cost += @ProductCost WHERE id = @OrderId
			UPDATE SelectedProducts SET id = @OrderId,status_id = @OrderedStatusId WHERE id = @Id
		BEGIN TRY
			UPDATE SizeInfo SET count -= @BoughtCount WHERE size_id = @SizeId AND product_id = @ProductId
		END TRY
		BEGIN CATCH
			SELECT 'There is not enough items with it size'			
			IF @@TRANCOUNT > 0 
				ROLLBACK TRANSACTION;  
			DELETE FROM History WHERE id = @OrderId
			return;
		END CATCH

		FETCH NEXT FROM idsCursor INTO
		@Id
	END
	SELECT CONCAT('ORDER #', @OrderId,' was accepted')
END TRY
BEGIN CATCH  
    IF @@TRANCOUNT > 0  
        ROLLBACK TRANSACTION;  
	SELECT 'Unknown error'
	DELETE FROM History WHERE id = @OrderId;
END CATCH; 
  
CLOSE idsCursor
DEALLOCATE idsCursor

IF @@TRANCOUNT > 0  
    COMMIT TRANSACTION;
GO


CREATE PROCEDURE USP_RALLBACK_BOUGHT
@OrderId uniqueidentifier,
@UserRollback bit,
@UserId int
AS
SET NOCOUNT ON

DECLARE @RollBackTimeInMinutes int = 60

BEGIN TRANSACTION
BEGIN TRY

	DECLARE @orderData DATETIME2(0) 
	SELECT @orderData = date from History

	IF @UserRollback <> 0 AND (SELECT COUNT(id) FROM SelectedProducts WHERE id = @OrderId AND user_id = @UserId) = 0
	BEGIN
		SELECT 'There is no order with that ID'
		ROLLBACK TRANSACTION;
		return;
	END

	IF @UserRollback <> 0 AND datediff(minute, @orderData , GETUTCDATE()) > @RollBackTimeInMinutes 
	BEGIN
		SELECT CONCAT('You cannot decline order when was expired more than ',str(@RollBackTimeInMinutes),' minutes from order time')
		ROLLBACK TRANSACTION;
		return;
	END

	DELETE FROM History WHERE id = @OrderId

	IF @@ROWCOUNT = 0
	BEGIN
		select 'Bought was already rollbacked'
		ROLLBACK TRANSACTION;
		RETURN;
	END

	declare @count int
	declare @product_id int
	declare @size_id int

	declare idsCursor CURSOR for SELECT count,product_id,size_id from SelectedProducts
	WHERE id = @OrderId
	OPEN idsCursor

	FETCH NEXT FROM idsCursor INTO
	@count,@product_id,@size_id
		

	WHILE @@FETCH_STATUS = 0
	BEGIN
			UPDATE SizeInfo SET count += @count WHERE product_id = @product_id AND size_id = @size_id
			FETCH NEXT FROM idsCursor INTO
			@count,@product_id,@size_id
	END

	DELETE FROM SelectedProducts
	WHERE id = @OrderId
	
	SELECT convert(nvarchar(36), @OrderId)
END TRY
BEGIN CATCH  
    IF @@TRANCOUNT > 0  
        ROLLBACK TRANSACTION;  
	SELECT 'Unknown Error'
END CATCH; 

CLOSE idsCursor
DEALLOCATE idsCursor

IF @@TRANCOUNT > 0 
    COMMIT TRANSACTION;
GO

INSERT INTO SelectedProducts 
(id,count,last_status_changed,status_id,user_id,product_id,size_id)
VALUES('479a4281-48ce-4489-95c7-053b6b98a2ab',2,GETUTCDATE(),2,4,7,1)

DECLARE @Ids as ProductStatusIds

INSERT INTO @Ids(id) VALUES('479a4281-48ce-4489-95c7-053b6b98a2ab')

EXEC USP_COMMIT_BOUGHT @ProductIds = @Ids, @OrderId = '379fa126-92d5-41b6-bccd-4bf93fc97e6a',@OrderedStatusId = 3, @Address = 'dsfsd'

select * from SelectedProducts
select * from SizeInfo
SELECT * FROM History

exec USP_RALLBACK_BOUGHT @OrderId = '379fa126-92d5-41b6-bccd-4bf93fc97e6a',@UserRollBack = 1,@UserId = 4