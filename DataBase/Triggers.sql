go
CREATE TRIGGER USP_SelectedProducts_After_Update 
ON SelectedProducts
AFTER UPDATE
as
IF @@ROWCOUNT = 0
	RETURN;
	
DECLARE @Id uniqueidentifier

DECLARE toLastStatusChange CURSOR  FOR
SELECT d.id FROM deleted as d
JOIN inserted as i
ON d.id = i.id AND i.last_status_changed <> d.last_status_changed

FETCH NEXT FROM toLastStatusChange INTO @Id

WHILE @@FETCH_STATUS = 0
BEGIN
	UPDATE SelectedProducts set last_status_changed = GETUTCDATE() where id = @id
	FETCH NEXT FROM toLastStatusChange INTO @Id
END


go