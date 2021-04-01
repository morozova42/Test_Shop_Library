CREATE PROCEDURE [dbo].[Create_reserve]
	@user_id int,
	@product_id int,
	@reserve_qty int,
	@product_qty int OUTPUT
AS
SET NOCOUNT ON
BEGIN TRANSACTION
	INSERT INTO [reserves] (user_id, product_id, reserve_qty, reserve_succeed, reserve_date)
	VALUES (@user_id, @product_id, @reserve_qty, 0, SYSUTCDATETIME())

	DECLARE @reserve_id int, @success bit
		SET @reserve_id = SCOPE_IDENTITY()
		SET @success = 0

		SET @product_qty = (SELECT qty FROM [product_info] WHERE product_id = @product_id)
			IF @reserve_qty <= @product_qty
				BEGIN
					UPDATE [product_info] 
					SET qty = qty - @reserve_qty
					WHERE product_id = @product_id 
					SET @success = 1

					UPDATE [reserves] 
					SET reserve_succeed = @success
					WHERE reserve_id = @reserve_id
				END

	SELECT * FROM [reserves] WHERE reserve_id = @reserve_id
COMMIT