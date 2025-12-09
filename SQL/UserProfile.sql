
CREATE TABLE [dbo].[UserProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](500) NOT NULL,
	[UserCode] [varchar] (500) NOT NULL,
	[Password] VARBINARY(16) NOT NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[UserCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


insert into UserProfile(Email, UserCode, Password)
values('neerajdh.krw@gmeremit.com','123654',HASHBYTES('MD5', 'GME@12345'))

select * from UserProfile



