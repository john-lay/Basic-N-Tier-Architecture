SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<John Lay>
-- Create date: <17 Jun 2016>
-- Description:	<Get a customer>
-- =============================================

/* TEST SCRIPT
    EXEC [dbo].[stp_getCustomer] @CustomerId = 1
*/

CREATE PROCEDURE stp_getCustomer
	(@CustomerId bigint)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT TOP 1 [Id]
      ,[FirstName]
      ,[LastName]
	FROM [HelloWorld].[dbo].[Customer]
	WHERE [Customer].[Id] = @CustomerId;
END
GO
