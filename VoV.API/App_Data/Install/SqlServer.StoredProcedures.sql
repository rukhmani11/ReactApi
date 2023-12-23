
/****** Object:  StoredProcedure [dbo].[BackUpDatabases]    Script Date: 04-Aug-23 1:05:40 PM ******/
--DROP PROCEDURE BackUpDatabases
IF OBJECT_ID('dbo.BackUpDatabases') IS NOT NULL
    DROP PROCEDURE BackUpDatabases;
GO

GO
/****** Object:  StoredProcedure [dbo].[BackUpDatabases]    Script Date: 04-Aug-23 1:05:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--BACKUP DATABASE [MUIMMS] TO  DISK = N'D:\Sentient\MUIMMS22AUG17.BAK' WITH NOFORMAT, INIT,  NAME = N'MUIMMS-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
--GO

CREATE proc [dbo].[BackUpDatabases]
as
begin

DECLARE @name VARCHAR(50) -- database name  
DECLARE @path VARCHAR(256) -- path for backup files  
DECLARE @fileName VARCHAR(256) -- filename for backup  
DECLARE @fileDate VARCHAR(20) -- used for file name
DECLARE @nameTxt VARCHAR(100) -- database name  
 
-- specify database backup directory
SET @path = N'F:\Backup\Database\' 
 
-- specify filename format
SELECT @fileDate = CONVERT(VARCHAR(20),GETDATE(),112) + CONVERT(VARCHAR(20),GETDATE(),108) 
 
set @fileDate = replace(@fileDate,':','')

DBCC SHRINKDATABASE ('VoV')

DECLARE db_cursor CURSOR READ_ONLY FOR  
SELECT name 
FROM master.dbo.sysdatabases 
WHERE 
--name NOT IN ('master','model','msdb','tempdb')  -- exclude these databases
--And 
(Name in ( 'VoV' )  )

OPEN db_cursor   
FETCH NEXT FROM db_cursor INTO @name   
 
WHILE @@FETCH_STATUS = 0   
BEGIN   
   SET @fileName = @path + @name + '_' + @fileDate + '.BAK'  
   --BACKUP DATABASE @name TO DISK = @fileName  
   Set @nameTxt = @name+'-Full Database Backup'
   BACKUP DATABASE @name TO  DISK = @fileName WITH NOFORMAT, INIT,  NAME = @nameTxt, SKIP, NOREWIND, NOUNLOAD,  STATS = 10
 
   --BACKUP DATABASE @name TO  DISK = N'D:\Sentient\MUIMMS22AUG17.BAK' WITH NOFORMAT, INIT,  NAME = N'MUIMMS-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10

   FETCH NEXT FROM db_cursor INTO @name   
END   

 
CLOSE db_cursor   
DEALLOCATE db_cursor
end

GO
---------------------------------------------------


 
/****** Object:  DdlTrigger [trgDDLAuditQuery]    Script Date: 22/08/2023 04:06:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE Trigger [trgDDLAuditQuery] 
on  database 
For             
        DDL_DATABASE_LEVEL_EVENTS
--Create_function, alter_function, drop_function ,
--Create_procedure, alter_procedure, drop_procedure ,
--Create_view, alter_view, drop_view,
--Create_table, alter_table, 
--drop_table,
--Create_trigger, alter_trigger, drop_trigger 
--, alter_constraint
--,drop_constraint
as  
begin     
set nocount on;      

declare @data xml
set @data = EVENTDATA()

Insert into dbo.Audit_DDL_Changes(
ObjectId,
ObjectSchema, ObjectName, ObjectSQL, Object_Host_Name, EventType, 
ObjectType, username)
values(
(Select isnull(Max(ObjectId),0)+1 from dbo.Audit_DDL_Changes) ,

@data.value('(/EVENT_INSTANCE/SchemaName)[1]',  'NVARCHAR(max)'), 
@data.value('(/EVENT_INSTANCE/ObjectName)[1]', 'varchar(max)'), 
@data.value('(/EVENT_INSTANCE/TSQLCommand)[1]', 'varchar(max)'), 
HOST_NAME(),
@data.value('(/EVENT_INSTANCE/EventType)[1]', 'varchar(max)'), 

@data.value('(/EVENT_INSTANCE/ObjectType)[1]', 'varchar(max)'), 
@data.value('(/EVENT_INSTANCE/LoginName)[1]', 'varchar(max)')
)

end


GO

ENABLE TRIGGER [trgDDLAuditQuery] ON DATABASE
GO

-------------------------Company Triggers----------------------------------------------------

/****** Object:  Trigger [dbo].[CompaniesInsertUpd]    Script Date: 22/08/2023 04:07:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter TRIGGER [dbo].[CompaniesIns] ON [dbo].[Companies] 
FOR INSERT 
AS
/*
By : Jagdish Jade
*/

Insert into [CompanyObservations] (
           [Id]
      ,[Name]
      ,[Description]
      ,[Sequence]
      ,[BusinessSegmentId]
      ,[Active]
      ,[CompanyId]
      ,[CreatedById]
      ,[CreatedOn]       
  )

SELECT newid() [Id]
      ,[Name]
      ,[Description]
      ,[Sequence]
      ,[BusinessSegmentId]
      ,[Active]
          , (Select ID CompanyID From Inserted I)
      ,[CreatedById]
      ,getdate() [CreatedOn]      
  FROM [StandardObservations]
  Where [Active] = 1


Insert into CompanyOpportunities (
           [Id]
      ,[Name]
      ,[Description]
      ,[Sequence]
      ,[BusinessSegmentId]
      ,[Active]
      ,[CompanyId]
      ,[CreatedById]
      ,[CreatedOn]       
  )

SELECT newid() [Id]
      ,[Name]
      ,[Description]
      ,[Sequence]
      ,[BusinessSegmentId]
      ,[Active]
          , (Select ID CompanyID From Inserted I)
      ,[CreatedById]
      ,getdate() [CreatedOn]      
  FROM [StandardOpportunities]
  Where [Active] = 1

  
Insert into CompanyRisks (
           [Id]
      ,[Name]
      ,[Description]
      ,[Sequence]
      ,[BusinessSegmentId]
      ,[Active]
      ,[CompanyId]
      ,[CreatedById]
      ,[CreatedOn]       
  )

SELECT newid() [Id]
      ,[Name]
      ,[Description]
      ,[Sequence]
      ,[BusinessSegmentId]
      ,[Active]
          , (Select ID CompanyID From Inserted I)
      ,[CreatedById]
      ,getdate() [CreatedOn]      
  FROM [StandardRisks]
  Where [Active] = 1
  GO
--------------------------------------------------------------------
ALTER TABLE [dbo].[Audit_DDL_Changes] ADD  CONSTRAINT [DF_DDL_MOD_DT]  DEFAULT (getdate()) FOR [Mod_Dt]
GO

